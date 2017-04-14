/**
 * 2016-12-16 zhrx 报警确认后，不再确认执行预案动作
 */
using AlarmAndPlan.Model;
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmAndPlan.Services
{
    [Route("Alarm/[controller]")]
    public class AlarmLogController : Controller
    {
        private ILogger<AlarmLogController> _logger;

        public AlarmLogController(ILogger<AlarmLogController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]AlarmLog alarmLog)
        {
            if (alarmLog == null)
            {
                return BadRequest("alarm log is null");
            }
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    _logger.LogInformation("收到报警日志....\r\n{0}", JsonConvert.SerializeObject(alarmLog));
                    if (alarmLog.AlarmLogId.Equals(Guid.Empty))
                        alarmLog.AlarmLogId = Guid.NewGuid();

                    alarmLog.AlarmLevelId = Guid.Parse("A0002016-E009-B019-E001-ABCD12900001");
                    alarmLog.ApplicationId = Guid.Parse("8DB3D774-5F99-4AA5-BA30-73E401137837");
                    alarmLog.AlarmStatusId = Guid.Parse("A0002016-E009-B019-E001-ABCD13100001");
                    alarmLog.AlarmStatus = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(alarmLog.AlarmStatusId));
                    alarmLog.TimeCreated = DateTime.Now;
                    db.AlarmLog.Add(alarmLog);
                    db.SaveChanges();
                    _logger.LogInformation("完成保存报警日志......");

                    //报警类型 //报警设备
                    alarmLog.AlarmType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(alarmLog.AlarmTypeId));
                    alarmLog.AlarmSource = db.IPDeviceInfo.Include(t=>t.Organization).FirstOrDefault(t => t.IPDeviceInfoId.Equals(alarmLog.AlarmSourceId));

                    //将报警日志加入队列
                    AlarmProcessExecutor.Instance.AddAlarmLogAction(alarmLog);
                    _logger.LogInformation("完成报警日志处理....");
                    //启动应急预案
                    return CreatedAtAction("", alarmLog);
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("添加报警日志异常，Message:{0}\r\nStackTrace:{1}\r\n{2}", ex.Message, ex.StackTrace,ex.InnerException);
                    return BadRequest(new ApplicationException() { ErrorCode= "DbUpdate", ErrorMessage=ex.Message});
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加报警日志异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 报警更新，确认
        /// </summary>
        /// <param name="alarmLog"></param>
        /// <param name="confirmTypeId">报警处理状态：确定，关闭（取消当前也标记为已关闭）</param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/Alarm/AlarmLog/Confirm/alarmLogId={alarmLogId}")]
        public IActionResult AlarmConfirm(Guid alarmLogId,Guid confirmTypeId,bool toTopServer= true)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    var alarmLog = db.AlarmLog.Include(t => t.AlarmSource).ThenInclude(t=>t.Organization)
                        .Include(t=>t.AlarmType).Include(t=>t.AlarmStatus).
                        FirstOrDefault(t => t.AlarmLogId.Equals(alarmLogId));
                    if (alarmLog != null)
                    {
                        var alarmStatus = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(confirmTypeId));
                        _logger.LogInformation("{0}的{1}确认Begin，状态改为{2}", alarmLog.AlarmSource.IPDeviceName,
                            alarmLog.AlarmType.SystemOptionName, alarmStatus.SystemOptionName);

                        //确认报警是否已确认
                        if (alarmLog.AlarmStatus != null && alarmLog.AlarmStatus.SystemOptionId.Equals(alarmStatus.SystemOptionId))
                        {
                            _logger.LogInformation("报警记录状态已确认/关闭,不再执行报警处理");
                            return NoContent();
                        }

                        //报警确认，加入到报警处理队列
                        alarmLog.AlarmStatus = alarmStatus;
                        db.AlarmLog.Update(alarmLog);
                        db.SaveChanges();

                        AlarmProcessExecutor.Instance.AddAlarmLogAction(alarmLog);

                        // 报警确认，推送到上级服务
                        if (toTopServer && confirmTypeId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD13100002")))
                            ForwardAlarmToTopOrganization(alarmLog);  
                    }
                    _logger.LogInformation("报警确认完成...");
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新报警日志异常，Message:{0}\r\nStackTrace{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 转发报警到上级
        /// </summary>
        /// <param name="alarmLog"></param>
        private void ForwardAlarmToTopOrganization(AlarmLog alarmLog)
        {
            Task.Run(() =>
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    string error = "";
                    var topApplicationCenter = db.Organization.Include(t => t.Center).
                            OrderBy(t => t.OrganizationFullName).
                            Select(t => t.Center).
                            FirstOrDefault();

                    if (topApplicationCenter != null &&
                        topApplicationCenter.EndPoints != null &&
                        topApplicationCenter.EndPoints.Count > 0)
                    {
                        _logger.LogInformation("开始推送{1}发生的{2}报警消息到上级系统...", alarmLog.AlarmSource.IPDeviceName, alarmLog.AlarmType.SystemOptionName);
                        EndPointInfo endPoint = topApplicationCenter.EndPoints.First();
                        string url = string.Format("http://{0}:{1}/Alarm/AlarmLog/Publish", endPoint.IPAddress, endPoint.Port);
                        _logger.LogInformation("推送URL:{0}...", url);
                        var result = HttpClientHelper.Post<AlarmLog>(alarmLog, url,false);
                        if (result.Success)
                        {
                            alarmLog.UploadStatus = 1;
                            db.SaveChanges();
                            _logger.LogInformation("报警消息推送到上级系统成功");
                        }
                        else
                        {
                            _logger.LogInformation("报警消息推送到上级系统失败，原因：{0}，将推送添加到报警转发队列", result);
                            ForwardAlarmLogTask.Instance.ForwardAlarmLog(alarmLog);
                        }
                    }
                    else
                        error = "未配置上级应用服务";

                    if (!string.IsNullOrEmpty(error))
                    {
                        error = string.Format("由于{0},推送{1}发生的{2}报警消息到上级系统失败!", error,
                            alarmLog.AlarmSource.IPDeviceName, alarmLog.AlarmType.SystemOptionName);
                        ForwardAlarmLogError forwardErr = new ForwardAlarmLogError()
                        {
                            ErrorDesc = error,
                            CreateTime = DateTime.Now
                        };
                        MQPulish.PublishMessage("ForwardAlarmLogError", forwardErr);  //改为提示.....
                    }
                }
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    AlarmLog delObj = db.AlarmLog.FirstOrDefault(s => s.AlarmLogId.Equals(id));
                    if (delObj == null || !delObj.AlarmLogId.Equals(id))
                    {
                        return NotFound();
                    }
                    db.AlarmLog.Remove(delObj);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除报警日志{0}异常，Message:{1}\r\n,StackTrace{2}", id, ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpGet]
        public IEnumerable<AlarmLog> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.AlarmLog.ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    AlarmLog alarmLog = db.AlarmLog.FirstOrDefault(t => t.AlarmLogId.Equals(id));
                    if (alarmLog == null || !alarmLog.AlarmLogId.Equals(id))
                    {
                        return NotFound();
                    }
                    return new OkObjectResult(alarmLog);
                }
                catch (Exception ex)
                {
                    _logger.LogError("获取报警日志记录{0}异常，Message:{1}\r\n,StackTrace:{2}.", id, ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 根据查询条件获取报警日志信息
        /// </summary>
        /// <param name="startTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="alarmTypeIds">报警类型</param>
        /// <param name="alarmSourceIds">报警设备id</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Alarm/AlarmLog/AlarmQuery")]
        //public IEnumerable<AlarmLog> GetByAlarmQuery([FromBody]AlarmQuery query)
        public IActionResult SearchAlarmLog(DateTime startTime, DateTime endTime, Guid[] alarmTypeIds, Guid[] alarmSourceIds,  int pageNo=1, int pageSize=10)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                if (pageNo <= 0)
                    pageNo = 1;
                if (pageSize <= 10)
                    pageSize = 10;
                var res = db.AlarmLog.Include(t => t.AlarmLevel).Include(t => t.AlarmSource).Include(t => t.AlarmType).Include(t => t.AlarmStatus).Include(t => t.Organization).Where(t => ((alarmTypeIds == null || alarmTypeIds.Length == 0) || alarmTypeIds.Contains(t.AlarmTypeId)) &&
                   ((alarmSourceIds == null || alarmSourceIds.Length == 0) || alarmSourceIds.Contains(t.AlarmSourceId)) &&
                    (t.TimeCreated >= startTime && t.TimeCreated <= endTime)).OrderByDescending(t => t.TimeCreated);
                var log= res.Skip((pageNo-1) * pageSize).Take(pageSize);
                QueryPagingRecord queryPagingRecord = new QueryPagingRecord
                {
                    SumRecordCount = res.Count(),
                    Record = log.ToList()
                };
                return new ObjectResult(queryPagingRecord);

            }
        }

        /// <summary>
        /// 根据查询条件获取报警日志总数
        /// </summary>
        /// <param name="startTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="alarmTypeIds">报警类型</param>
        /// <param name="alarmSourceIds">报警设备id</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Alarm/AlarmLog/AlarmQuery/Count")]
        //public IEnumerable<AlarmLog> GetByAlarmQuery([FromBody]AlarmQuery query)
        public int CountAlarmLog(DateTime startTime, DateTime endTime, Guid[] alarmTypeIds, Guid[] alarmSourceIds, int pageNo = 1, int pageSize = 10)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var res = db.AlarmLog.Include(t => t.AlarmLevel).Include(t => t.AlarmSource).Include(t => t.AlarmType).Include(t => t.AlarmStatus).Include(t => t.Organization).Where(t => ((alarmTypeIds == null || alarmTypeIds.Length == 0) || alarmTypeIds.Contains(t.AlarmTypeId)) &&
                   ((alarmSourceIds == null || alarmSourceIds.Length == 0) || alarmSourceIds.Contains(t.AlarmSourceId)) &&
                    (t.TimeCreated >= startTime && t.TimeCreated <= endTime));

                return res.Count();
            }
        }

        /// <summary>
        /// 广播报警日志消息，适用于下级应用服务器调用
        /// </summary>
        /// <param name="alarmLog"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Alarm/AlarmLog/Publish")]
        public IActionResult Publish([FromBody]AlarmLog alarmLog)
        {
            alarmLog.IsForwardAlarm = true;
            alarmLog.TimeCreated = DateTime.Now;
            MQPulish.PublishMessage("AlarmLog", alarmLog);
            return Ok();
        }
    }

}
