using AlarmAndPlan.Model;
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlarmAndPlan.Services
{
    [Route("Alarm/[Controller]")]
    public class AlarmProcessedController : Controller
    {
        private ILogger<AlarmProcessedController> _logger;

        public AlarmProcessedController(ILogger<AlarmProcessedController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody]AlarmProcessed processInfo)
        {
            if (processInfo == null)
            {
                return BadRequest();
            }
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    _logger.LogInformation("开始添加报警处理意见...");
                    db.AlarmProcessed.Add(processInfo);
                    db.SaveChanges();

                    //推送处理结果到上级服务
                    //获取上级服务器的依据：按组织机构长地址排序，取第一个组织机构的应用中心信息
                    var topApplicationCenter = db.Organization.Include(t => t.Center).
                        OrderBy(t => t.OrganizationFullName).
                        Select(t => t.Center).
                        FirstOrDefault();
                    if (topApplicationCenter != null && topApplicationCenter.EndPoints != null && topApplicationCenter.EndPoints.Count > 0)
                    {
                        _logger.LogInformation("发送报警处理到上级平台...");
                        EndPointInfo endPoint = topApplicationCenter.EndPoints.First();
                        string url = string.Format("http://{0}:{1}/Alarm/AlarmProcessed/Publish", endPoint.IPAddress, endPoint.Port);
                        var result = HttpClientHelper.Post<AlarmProcessed>(processInfo, url);
                        _logger.LogInformation("发送报警处理到上级平台...result:{0}", result);
                    }
                    else
                    {
                        Console.WriteLine("未配置上级应用服务，未能完成发送报警处理到上级系统");
                    }

                    //本地广播报警处理消息
                    _logger.LogInformation("广播报警处理消息");
                    MQPulish.PublishMessage("AlarmProcess", processInfo);

                    _logger.LogInformation("完成添加报警处理意见...");
                    return CreatedAtRoute("GetById", processInfo);
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加报警处理意见异常，Message:{0}\r\n,StackTrace{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        //[HttpPut]
        //public IActionResult Update([FromBody]AlarmProcessed alarmProcessed)
        //{
        //    if (alarmProcessed == null)
        //    {
        //        return BadRequest();
        //    }

        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        try
        //        {
        //            db.AlarmProcessed.Update(alarmProcessed);
        //            db.SaveChanges();
        //            return NoContent();
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError("更新报警设置异常，Message:{0}\r\n,StackTrace{1}", ex.Message, ex.StackTrace);
        //            return BadRequest(ex);
        //        }
        //    }

        //}

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    AlarmProcessed delObj = db.AlarmProcessed.FirstOrDefault(s => s.AlarmProcessedId.Equals(id));
                    if (delObj == null || delObj.AlarmProcessedId.Equals(id))
                    {
                        return NotFound();
                    }
                    db.AlarmProcessed.Remove(delObj);
                    db.SaveChanges();
                    return NotFound();
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除报警处理异常：Message:{0}\r\nStackTrace{1}", ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        //[HttpGet]
        //public IEnumerable<AlarmProcessed> GetAll()
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        return db.AlarmProcessed;
        //    }
        //}

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    AlarmProcessed process = db.AlarmProcessed.FirstOrDefault(t => t.AlarmProcessedId.Equals(id));
                    return new OkObjectResult(process);
                } catch (Exception ex)
                {
                    _logger.LogError("获取报警处理{0}异常，Message:{1}\r\nStackTrace:{2}", id, ex.Message, ex.StackTrace);
                    return BadRequest(ex);
                }
            }
        }

        [HttpGet]
        [Route("~/Alarm/AlarmProcessed/alarmLogId={alarmLogId}")]
        public IEnumerable<AlarmProcessed> GetByAlarmLogId(Guid alarmLogId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.AlarmProcessed.Where(p => p.AlarmLogId.Equals(alarmLogId)).ToList();
            }
        }

        /// <summary>
        /// 广播报警处理消息，适用于下级应用服务器调用
        /// </summary>
        /// <param name="alarmLog"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Alarm/AlarmProcessed/Publish")]
        public IActionResult publish([FromBody]AlarmLog alarmLog)
        {
            MQPulish.PublishMessage("ForwardAlarmProcessed", alarmLog);
            return Ok();
        }
    }
}
