/****
 * 2016-12-16 zhrx 增加精简数据的指纹下发和指纹清除
 * 2016-12-26 zhrx  子弹箱开/关接口调整，供弹申请处理需保存起来
 */

using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PAPS.Data;
using PAPS.Model;
using Resources.Data;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAPS.Services
{
    [Route("Paps/[controller]")]
    public class SentinelController:Controller
    {
        private readonly ILogger<SentinelController> _logger;
        public SentinelController(ILogger<SentinelController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 子弹箱状态记录，用于与哨位中心服务交互
        /// </summary>
        /// <param name="bulletbox"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/BulletBox")]
        public IActionResult AddBulletbox([FromBody]BulletboxLog bulletbox)
        {
            if (bulletbox == null)
                return BadRequest("BulletboxLog object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    _logger.LogInformation("收到子弹箱状态事件...");
                    if (bulletbox.BulletboxLogId.Equals(Guid.Empty))
                        bulletbox.BulletboxLogId = Guid.NewGuid();
                    bulletbox.Modified = DateTime.Now;
                    //取哨位台视图数据
                    SentinelView sv = GetSentinelViewByDeviceId(bulletbox.SentinelDeviceId);
                    //Attachment frontSnapshot = null;
                    //Attachment cartridgeBoxSnapshot = null;
                    ////调用dcp接口截图
                    //SentinelSnapshot(sv, ref frontSnapshot, ref cartridgeBoxSnapshot);

                    bulletbox.LockStatus = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(bulletbox.LockStatusId));
                    //bulletbox.FrontSnapshot = frontSnapshot;
                    //bulletbox.BulletboxSnapshot = cartridgeBoxSnapshot;
                    db.BulletboxLog.Add(bulletbox);
                    db.SaveChanges();
                    _logger.LogInformation("完成保存子弹箱状态事件...");

                    bulletbox.SentinelViewInfo = sv;
                    BulletboxSnaphot(bulletbox);
                    MQPulish.PublishMessage("BulletboxLog", bulletbox);
                    return CreatedAtAction("", bulletbox);
                }
                catch (Exception ex)
                {
                    _logger.LogError("保存子弹箱开关记录异常 ：message:{0}\r\n,stacktrace:{1},inner exception:{2}", ex.Message, 
                        ex.StackTrace, ex.InnerException);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 新增哨位告警信息
        /// </summary>
        /// <param name="warningInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/Warning")]
        public IActionResult AddWarningInfo([FromBody]SentinelWarning warningInfo)
        {
            if (warningInfo == null)
                return BadRequest("SentinelWarning object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    MQPulish.PublishMessage("SentinelWarning", warningInfo);
                    return CreatedAtAction("", warningInfo);
                }
                catch (Exception ex)
                {
                    _logger.LogError("保存哨位告警记录异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        [Route("~/Paps/Sentinel/Bulletbox")]
        public IActionResult UpdateBulletbox([FromBody]BulletboxLog bulletbox)
        {
            if (bulletbox == null)
                return BadRequest("Service object can not be null!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    bulletbox.Modified = DateTime.Now;
                    db.BulletboxLog.Update(bulletbox);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新子弹箱开关记录异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
            return NoContent();
        }

        //[HttpGet("{id}",Name ="GetById")]
        [HttpGet]
        [Route("~/Paps/Sentinel/Bulletbox/bulletboxId={bulletboxId}")]
        public IActionResult GetCartridgeBoxLockById(Guid bulletboxId)
        {
            _logger.LogInformation("Get Service info by id {0}", bulletboxId);
            using (var db = new AllInOneContext.AllInOneContext())
            {
                BulletboxLog si = db.BulletboxLog.FirstOrDefault(t => t.BulletboxLogId.Equals(bulletboxId));
                if (si == null)
                {
                    return NotFound();
                }
                return new ObjectResult(si);
            }
        }

        /// <summary>
        /// 子弹箱事件查询
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="deviceId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSise"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/Sentinel/Bulletbox/organizationId={organizationId}")]
        //http://127.0.0.1:5001/Paps/Bulletbox/organizationId=b31f22c1-bcd8-4b5a-ad5b-70760a1a9d74?sentinelId=a0002016-e009-b019-e001-abcd11300204
        //&beginTime=2016-10-11 10:10:021&endTime=2016-10-11 10:10:021&pageNo=1&pageSize=100
        public IEnumerable<BulletboxLog> SearchBulletbox(Guid organizationId, Guid deviceId,DateTime beginTime,
            DateTime endTime, int pageNo, int pageSise)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var q = db.BulletboxLog.Include(t => t.SentinelDevice).Where(t => (deviceId.Equals(Guid.Empty) || t.SentinelDeviceId.Equals(deviceId)) &&
                     t.Modified > beginTime && t.Modified < endTime).ToList();
                if (pageNo > 10 && pageSise > 0)
                    return q.Skip(pageSise * pageNo).Take(pageSise).ToList();
                else
                    return q;
            }
        }

        /// <summary>
        /// 新增打卡记录,
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/PunchLog")]
        public IActionResult FirstPunchLog([FromBody]PunchLog log)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    if (log == null)
                        return BadRequest();
                    //获取哨位视频
                    SentinelView sv = GetSentinelViewByDeviceId(log.PunchDeviceId);
                    //Attachment frontSnapshot = null;
                    //Attachment cartridgeBoxSnapshot = null;
                    ////调用dcp接口截图
                    //SentinelSnapshot(sv, ref frontSnapshot, ref cartridgeBoxSnapshot);

                    //获取人员信息
                    log.Staff = db.Staff.Include(t => t.Fingerprints).Include(t => t.Organization)
                        .Include(t => t.Photo).Include(t => t.PositionType).Include(t => t.Sex)
                        .ToList().FirstOrDefault(t => t.Fingerprints.Exists(f => f.FingerprintNo == log.FigureNo));
                    log.LogTime = DateTime.Now;

                    //log.FrontSnapshot = frontSnapshot;
                    //log.BulletboxSnapshot = cartridgeBoxSnapshot;

                    //保存记录
                    db.PunchLog.Add(log);
                    db.SaveChanges();

                    ////评价
                    //if (log.AppraiseTypeId != null)
                    //    log.AppraiseType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(log.AppraiseTypeId.Value));

                    ////结果
                    //if (log.LogResultId != null)
                    //    log.LogResult = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(log.LogResultId.Value));

                    log.SentinelViewInfo = sv;

                    //截图
                    PunchlogSnaphot(log);

                    log.PunchType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(log.PunchTypeId.Value));
                    //打卡类型
                    bool validPunchlog = log.Staff != null; //指纹是否通过验证
                    if (Guid.Parse("a0002016-e009-b019-e001-abcd14300002").Equals(log.PunchTypeId))//换岗
                    {
                        //值班/交班人员
                        DutyDetail r = GetDutydetail(log.PunchDeviceId);
                        if (r != null)
                        {
                            log.OnDutyStaff = r.OnDutyStaff;
                            log.OffDutyStaff = r.OffDutyStaff;
                        }
                    }
                    if (!validPunchlog) //指纹未通过验证
                    {
                        _logger.LogInformation("指纹未通过验证，广播消息！");
                        MQPulish.PublishMessage("PunchLog", log);
                    }
                    //if (log.PunchTypeId.Equals(Guid.Parse("a0002016-e009-b019-e001-abcd14300001")))//查哨
                    //{
                    //    ForwardPunchLogTopOrganization(log);
                    //}

                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError("FirstPunchLog 处理异常：message:{0}\r\n,stacktrace:{1},inner exception:{2}", ex.Message, ex.StackTrace, ex.InnerException);
                    return BadRequest(new ApplicationException() { ErrorCode = "Unkonw", ErrorMessage = ex.Message });
                }
            }
        }

        ///// <summary>
        ///// 哨位视频截图
        ///// </summary>
        //private void SentinelSnapshot(SentinelView sv,ref Attachment frontSnapshot,ref Attachment bulletSnapshot)
        //{
        //    //调用dcp接口截图
        //    if (sv.FrontCamera != null)
        //    {
        //        string fileName = string.Format("{0:yyyyMMddHHmmssfff}.png", DateTime.Now);
        //        frontSnapshot = new Attachment()
        //        {
        //            AttachmentId = Guid.NewGuid(),
        //            //ModifiedById = userId,
        //            Modified = DateTime.Now,
        //            AttachmentPath = "/attach/",
        //            AttachmentName = fileName
        //        };
        //        Snapshot(frontSnapshot.AttachmentId, sv.FrontCamera.IPDeviceId, fileName);
        //    }
        //    if (sv.BulletboxCamera != null)
        //    {
        //        string fileName = string.Format("{0:yyyyMMddHHmmssfff}.png", DateTime.Now);
        //        bulletSnapshot = new Attachment()
        //        {
        //            AttachmentId = Guid.NewGuid(),
        //            //ModifiedById = userId,
        //            Modified = DateTime.Now,
        //            AttachmentPath = "/attach/",
        //            AttachmentName = fileName
        //        };
        //        Snapshot(bulletSnapshot.AttachmentId, sv.FrontCamera.IPDeviceId, fileName);
        //    }
        //}


        /// <summary>
        /// dcp 截图
        /// </summary>
        private Task<bool> Snapshot(Guid attachmentId, Guid cameraDeviceId, string fileName, string source = "")
        {
            string url = string.Format("{0}/dcp/snapshot", GlobalSetting.DcpBaseUrl);
            return Task.Factory.StartNew(() =>
            {
                HttpRequestResult result = null;
                try
                {
                    _logger.LogInformation(source);
                    Surveillance.ViewModel.SnapshotParam param = new Surveillance.ViewModel.SnapshotParam()
                    {
                        AttachmentId = attachmentId,
                        IPDeviceInfoId = cameraDeviceId,
                        FileName = fileName
                    };
                    _logger.LogInformation("下发请求截图操{0}\r\n,协议：{1}", url, JsonConvert.SerializeObject(param));
                    result = HttpClientHelper.Post<Surveillance.ViewModel.SnapshotParam>(param, url, Encoding.UTF8, false);
                    _logger.LogInformation("截图操作结果{0}_{1}", result.Success, result.ResultText);
                }
                catch (Exception ex)
                {
                    _logger.LogError("截图异常，Message:{0}\r\nStackTrace:{1}\r\nInnerException", ex.Message, ex.StackTrace, ex.InnerException);
                }
                return result != null ? result.Success : false;
            });
        }

        [HttpGet]
        [Route("~/Paps/Sentinel/PunchLog/{id}")]
        public PunchLog GetPunchLogById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.PunchLog.FirstOrDefault(t => t.PunchLogId.Equals(id));
            }
        }

        /// <summary>
        /// 更新打卡记录，用户更新
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/Paps/Sentinel/PunchLog")]
        public IActionResult UpdatePunchLog([FromBody]PunchLog log)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //更新记录
                //获取之前已保存的记录
                var copyLog = db.PunchLog.FirstOrDefault(t => t.PunchLogId.Equals(log.PunchLogId));
                if (copyLog != null)
                { 
                    //获取哨位视频
                    SentinelView sv = GetSentinelViewByDeviceId(copyLog.PunchDeviceId);
                    copyLog.SentinelViewInfo = sv;

                    ////值班/交班人员
                    //ILogger<DutyCheckController> logger = Logger.CreateLogger<DutyCheckController>();
                    //DutyDetail r = new DutyCheckController(logger).GetDutyDetailBySentinelId(copyLog.PunchDeviceId);
                    //if (r != null)
                    //{
                    //    copyLog.OnDutyStaff = r.OnDutyStaff;
                    //    copyLog.OffDutyStaff = r.OffDutyStaff;
                    //}

                    //打卡类型
                    if (log.PunchTypeId != null)
                        copyLog.PunchType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(log.PunchTypeId.Value));

                    //评价
                    if (log.AppraiseTypeId != null)
                        log.AppraiseType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(log.AppraiseTypeId.Value));

                    //结果
                    if (log.LogResultId != null)
                        copyLog.LogResult = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(log.LogResultId.Value));

                    db.PunchLog.Update(copyLog);
                    db.SaveChanges();
                   // MQPulish.PublishMessage("PunchLog", log);
                }
                return Ok();
            }
        }

        /// <summary>
        /// 哨位台是二次打卡确认,更新打卡记录，用于与哨位中心对接（哨位中心用Put的方式未能把实体信息传递，故定义下面接口）
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/PunchLog/Second")]
        public IActionResult SecondPunchLog([FromBody]PunchLog log)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //更新记录
                //获取之前已保存的记录
                _logger.LogInformation("更新查哨换岗记录信息 Begin...");
                try
                {
                    var copyLog = db.PunchLog.Include(t => t.FrontSnapshot).
                        Include(t=>t.BulletboxSnapshot).
                        Include(t=>t.PunchDevice).ThenInclude(t=>t.Organization).
                        Include(t=>t.Staff).FirstOrDefault(t => t.PunchLogId.Equals(log.PunchLogId));
                    if (copyLog != null)
                    {
                        //获取哨位视频
                        SentinelView sv = GetSentinelViewByDeviceId(copyLog.PunchDeviceId);
                        copyLog.SentinelViewInfo = sv;

                        //打卡类型
                        if (log.PunchTypeId != null)
                            copyLog.PunchType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(log.PunchTypeId.Value));

                        //评价
                        if (log.AppraiseTypeId != null)
                            copyLog.AppraiseType = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(log.AppraiseTypeId.Value));

                        //结果
                        if (log.LogResultId != null)
                            copyLog.LogResult = db.SystemOption.FirstOrDefault(t => t.SystemOptionId.Equals(log.LogResultId.Value));

                        db.PunchLog.Update(copyLog);
                        db.SaveChanges();
                        _logger.LogInformation("更新查哨换岗记录信息 End...");

                        //获取人员信息
                        copyLog.Staff = db.Staff.Include(t => t.Organization)
                            .Include(t => t.Photo).Include(t => t.PositionType).Include(t => t.Sex)
                            .ToList().FirstOrDefault(t => t.StaffId.Equals(copyLog.StaffId));
                        //获取值班排班
                        if (log.PunchTypeId != null)
                        {
                            if (log.PunchTypeId.Equals(Guid.Parse("a0002016-e009-b019-e001-abcd14300001")))//查哨
                            {
                                ILogger<DutyCheckController> logger = Logger.CreateLogger<DutyCheckController>();
                                DutyDetail ddr = new DutyCheckController(logger).GetDutyDetailByFieldCheck(copyLog.PunchDeviceId, copyLog.LogTime, copyLog.StaffId);
                                if (ddr != null)
                                {
                                    copyLog.OnDutyStaff = ddr.OnDutyStaff;
                                }
                                ForwardPunchLogTopOrganization(copyLog);
                            }
                            else
                            {
                                DutyDetail r = GetDutydetail(log.PunchDeviceId);
                                if (r != null)
                                {
                                    copyLog.OnDutyStaff = r.OnDutyStaff;
                                    copyLog.OffDutyStaff = r.OffDutyStaff;
                                }
                            }
                        }
                        _logger.LogInformation("二次指纹验证成功，广播PunchLog消息");
                        MQPulish.PublishMessage("PunchLog", copyLog);

                        DutyCheckPackageHelper.Synchronization(copyLog);
                    }
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新查哨换岗记录信息异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 获取哨位排班信息
        /// </summary>
        /// <param name="sentinelDeviceId"></param>
        /// <returns></returns>
        private DutyDetail GetDutydetail(Guid sentinelDeviceId)
        {
            //值班/交班人员
            DutyDetail r = null;
            ILogger<DutyCheckController> logger = Logger.CreateLogger<DutyCheckController>();
            try
            {
                r = new DutyCheckController(logger).GetDutyDetailBySentinelId(sentinelDeviceId);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("获取查勤明细异常,Mesage:{0}", ex.Message);
            }
            return r;
        }

        /// <summary>
        /// 哨位设备状态自检
        /// </summary>
        /// <param name="checkingInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/SelfChecking")]
        public IActionResult UpdateSentinelSelfChecking([FromBody]SentinelSelfChecking checkingInfo)
        {
            try
            {
                //根据哨位编号获取
                if (checkingInfo != null && checkingInfo.Cmd == 215)
                {
                    StringBuilder sb = new StringBuilder();
                    if (checkingInfo.MainCOM > 0)
                    {
                        sb.Append(GetSentinelModelStatus(checkingInfo.MainCOM)).Append(",");
                    }
                    if (checkingInfo.BulletBox > 0)
                    {
                        sb.Append(GetSentinelModelStatus(checkingInfo.BulletBox)).Append(",");
                    }
                    if (checkingInfo.FingerCOM > 0)
                    {
                        sb.Append(GetSentinelModelStatus(checkingInfo.FingerCOM)).Append(",");
                    }
                    if (checkingInfo.NetModel > 0)
                    {
                        sb.Append(GetSentinelModelStatus(checkingInfo.NetModel)).Append(",");
                    }
                    if (checkingInfo.VoIPModel > 0)
                    {
                        sb.Append(GetSentinelModelStatus(checkingInfo.VoIPModel)).Append(",");
                    }
                    if (sb.Length > 0)
                    {
                        sb.Length = sb.Length - 1;
                    }
                    else
                    {
                        sb.Append("正常");
                    }
                    //广播自检消息
                }
            }
            catch (Exception)
            {

            }
            return Ok();
        }

        /// <summary>
        /// 哨位模块状态描述
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        private String GetSentinelModelStatus(int errorCode)
        {
            String err = "";
            switch (errorCode)
            {
                case 10000:
                    err = "底板通信异常";
                    break;
                case 10001:
                    err = "指纹仪通信异常";
                    break;
                case 10002:
                    err = "子弹箱光电异常";
                    break;
                case 10003:
                    err = "子弹箱霍尔异常";
                    break;
                case 10004:
                    err = "对讲模块异常";
                    break;
                case 10005:
                    err = "网络通信模块异常";
                    break;
                default: break;
            }
            return err;
        }

        #region  调用哨位中心Api,执行哨位报警确认，子弹箱开/关，喊话，警情控制等操作
            /// <summary>
            /// 确认/取消哨位报警
            /// </summary>
            /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/alarm")]
        public IActionResult SendAlarmStatus2ASCS([FromBody]DeviceAlarmStatus status)
        {
            try
            {
                if (status == null)
                {
                    return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = "数据错误" });
                }
             
                ServiceInfo serviceInfo = this.GetASCSServiceInfo(status.DeviceId);
                var r = new ASCSApi(serviceInfo).SendDeviceAlarmStatus(status);
                if (r.Success)
                    return Ok();
                else
                    return BadRequest(r.ResultText);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 报警复位
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("~/Paps/Sentinel/ResetAlarm")]
        public IActionResult ResetAlarm(Guid organizationId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    ServiceInfo ascs = GetASCSServiceInfoByOrganizationid(db, organizationId);
                    var r = new ASCSApi(ascs).ResetAlarm();
                    if (r.Success)
                    {
                        //将所有报警复位
                        Guid unprocessedStatusId = Guid.Parse("A0002016-E009-B019-E001-ABCD13100001");
                        Guid cancelStatusId = Guid.Parse("A0002016-E009-B019-E001-ABCD13100003");
                        _logger.LogInformation("报警复位，将未处理的报警记录状态设置为已取消");
                        db.AlarmLog.Where(t => unprocessedStatusId.Equals(t.AlarmStatusId))
                            .ToList().ForEach(t => t.AlarmStatusId = cancelStatusId);
                        db.SaveChanges();

                        //停止预案动作
                        Task.Run(() => ResetAlarmPlan());
                        return Ok();
                    }
                    else
                        return BadRequest(r.ResultText);
                }
                catch (Exception ex)
                {
                    _logger.LogError("发送报警复位异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
                }
            }
        }

        ///// <summary>
        ///// 子弹箱控制
        ///// </summary>
        ///// <param name="cartidges"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("~/Paps/Sentinel/cartidge/control")]
        //public IActionResult ControlCatridgeBox([FromBody]List<CartidgeBoxStatus> cartidges)
        //{
        //    try
        //    {
        //        if (cartidges == null || cartidges.Count == 0)
        //        {
        //            return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = "数据错误" });
        //        }
        //        ServiceInfo serviceInfo = this.GetASCSServiceInfo(cartidges[0].DeviceId);
        //        var r = new ASCSApi(serviceInfo).CatridgeBoxStatus(cartidges);
        //        if (r.Success)
        //            return Ok();
        //        else
        //            return BadRequest(r.ResultText);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
        //    }
        //}

        /// <summary>
        /// 子弹箱控制 //2016-12-26 接口调整，供弹申请处理需保存起来
        /// </summary>
        /// <param name="cbl"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/BulletBox/Control")]
        public IActionResult ControlBulletBox([FromBody]BulletboxLog cbl)
        {
            if (cbl == null)
            {
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = "数据错误" });
            }
            //判断，供弹记录是否已存在，更新供弹申请处理消息
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    var dbCartridgeBoxlock = db.BulletboxLog.FirstOrDefault(t => t.BulletboxLogId.Equals(cbl.BulletboxLogId));
                    if (dbCartridgeBoxlock != null)
                    {
                        dbCartridgeBoxlock.ComfirmInfo = cbl.ComfirmInfo;
                        dbCartridgeBoxlock.ModifiedById = cbl.ModifiedById;
                        dbCartridgeBoxlock.Modified = DateTime.Now;
                        _logger.LogInformation("找到供弹申请记录，更新处理消息！");
                    }
                    else
                    {
                        db.BulletboxLog.Add(cbl);
                        _logger.LogInformation("未找到供弹申请记录，添加！");
                    }
                    db.SaveChanges();
                    ServiceInfo serviceInfo = this.GetASCSServiceInfo(db,cbl.SentinelDeviceId);
                    List<CartidgeBoxStatus> cartridges = new List<CartidgeBoxStatus>();
                    cartridges.Add(new CartidgeBoxStatus()
                    {
                        DeviceId = cbl.SentinelDeviceId,
                        BulletBoxStatus = cbl.IsOpen,
                        SentinelCode = cbl.SentinelViewInfo.DeviceInfo.IPDeviceCode
                    });
                    var r = new ASCSApi(serviceInfo).CatridgeBoxStatus(cartridges);
                    if (r.Success)
                        return Ok();
                    else
                        return BadRequest(r.ResultText);
                }
                catch (Exception ex)
                {
                    _logger.LogError("子弹箱控制异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 子弹箱灯控制
        /// </summary>
        /// <param name="cartidges"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/cartidgelight/control")]
        public IActionResult ControlCartidgeLight([FromBody]List<CatridgeLightStatus> cartidges)
        {
            try
            {
                if (cartidges == null || cartidges.Count == 0)
                {
                    return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = "数据错误" });
                }
                ServiceInfo serviceInfo = this.GetASCSServiceInfo(cartidges[0].DeviceId);
                var r = new ASCSApi(serviceInfo).CartidgeLightStatus(cartidges);
                if (r.Success)
                    return Ok();
                else
                    return BadRequest(r.ResultText);
            }
            catch (Exception ex)
            {
                _logger.LogError("子弹箱灯控制异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 消息发布
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/pushmessage")]
        public IActionResult PushMessage([FromBody]PushMessage message)
        {
            try
            {
                if (message == null)
                {
                    return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = "数据错误" });
                }
                ServiceInfo serviceInfo = this.GetASCSServiceInfo(message.DeviceId);
                var r = new ASCSApi(serviceInfo).PushMessage(message);
                if (r.Success)
                {
                    return Ok();
                }
                else
                    return BadRequest(r.ResultText);
            }
            catch (Exception ex)
            {
                _logger.LogError("发送LED文字消息异常：Message{0}\r\nStackTrace{1}..", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 更新哨位台输出端口状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("~/Paps/Sentinel/outputChannel")]
        public IActionResult SendSentinelOutputStatus([FromBody]List<SentinelOutputStatus> status)
        {
            try
            {
                if (status == null || status.Count == 0)
                {
                    return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = "数据错误" });
                }
                ServiceInfo serviceInfo = this.GetASCSServiceInfo(status[0].DeviceId);
                var r = new ASCSApi(serviceInfo).SendSentinelOutputStatus(status);
                if (r.Success)
                    return Ok();
                else
                    return BadRequest(r.ResultText);
            }
            catch (Exception ex)
            {
                _logger.LogError("哨位端口输出控制异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        ///// <summary>
        /////  声光报警器LED灯闪烁状态
        ///// </summary>
        ///// <param name="status"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("~/Paps/Sentinel/ledlight/control")]
        //public IActionResult SendLedLightStatus([FromBody]List<LedLightStatus> status)
        //{
        //    try
        //    {
        //        if (status == null || status.Count == 0)
        //        {
        //            return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = "数据错误" });
        //        }
        //        ServiceInfo serviceInfo = this.GetASCSServiceInfo(status[0].DeviceId);
        //        new ASCSApi(serviceInfo).SendLedLightStatus(status);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
        //    }
        //}

        /// <summary>
        /// 查询哨位版本信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/Sentinel/version/deviceId={deviceId}")]
        public IActionResult GetDeviceVersion(Guid deviceId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ServiceInfo serviceInfo = this.GetASCSServiceInfo(db,deviceId);
                    var device = db.IPDeviceInfo.Include(t=>t.Organization).FirstOrDefault(t => t.IPDeviceInfoId.Equals(deviceId));
                    var sentinelCode = Int32.Parse(device.Organization.OrganizationCode);
                    DeviceVersion version = new ASCSApi(serviceInfo).GetDeviceVersion(sentinelCode);
                    return Ok(version);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 查询设备诊断
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/Sentinel/diagnosis/deviceId={deviceId}")]
        public IActionResult GetDeviceDiagnosis(Guid deviceId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ServiceInfo serviceInfo = this.GetASCSServiceInfo(db,deviceId);
                    var device = db.IPDeviceInfo.Include(t => t.Organization).FirstOrDefault(t => t.IPDeviceInfoId.Equals(deviceId));
                    var sentinelCode = Int32.Parse(device.Organization.OrganizationCode);
                    DeviceDiagnosis diagnosis = new ASCSApi(null).GetDeviceDiagnosis(sentinelCode);
                    return Ok(diagnosis);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        ///// <summary>
        ///// 分发指纹
        ///// </summary>
        ///// <param name="info"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("~/Paps/Sentinel/dispatchfigure")]
        //public IActionResult DispatchFinger([FromBody]DispatchFigureInfo info)
        //{
        //    try
        //    {
        //        ServiceInfo serviceInfo = this.GetASCSServiceInfo(info.DeviceId);
        //        var r = new ASCSApi(serviceInfo).DispatchFinger(info);
        //        //保存记录
        //        if (r.Success)
        //            return Ok();
        //        else
        //            return BadRequest(r.ResultText);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
        //    }
        //}

        ///// <summary>
        ///// 清除指纹
        ///// </summary>
        ///// <param name="deviceId">哨位设备id</param>
        ///// <param name="figureCode">指纹编码 备注（0：清除本哨位台上所有人员指纹）</param>
        ///// <returns></returns>
        //[HttpDelete]
        //[Route("~/Paps/Sentinel/dispatchfigure/sentinelCode={sentinelCode}")]
        //public IActionResult CleanFinger(Guid deviceId, int figureCode)
        //{
        //    try
        //    {
        //        using (var db = new AllInOneContext.AllInOneContext())
        //        {
        //            ServiceInfo serviceInfo = this.GetASCSServiceInfo(deviceId);
        //            var device = db.IPDeviceInfo.FirstOrDefault(t => t.IPDeviceInfoId.Equals(deviceId));
        //            var r = new ASCSApi(serviceInfo).CleanFinger(device.IPDeviceCode, figureCode);
        //            //删除数据库保存的记录
        //            if (r.Success)
        //                return Ok();
        //            else
        //                return BadRequest(r.ResultText);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
        //    }
        //}

        #region 批量下发和删除
        /// <summary>
        /// 批量分发指纹
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/DispatchFinger")]
        public IActionResult BatchDispatchFinger([FromBody]FingerAction param)
        {
            try
            {
                _logger.LogInformation("指纹下发 Begin...");
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    foreach (var sen in param.Sentinels)
                    {
                        ServiceInfo serviceInfo = this.GetASCSServiceInfo(db,sen.DeviceInfoId);
                        var api = new ASCSApi(serviceInfo);
                        //保存记录
                        foreach (var staff in param.Staffs)
                        {
                            //
                            if (staff.Fingerprints != null)
                            {
                                foreach (var fp in staff.Fingerprints)
                                {
                                    //指纹第一次下发到哨位台,只下发未下过的指纹
                                    if (db.SentinelFingerPrintMapping.FirstOrDefault(t => t.FingerprintId.Equals(fp.FingerprintId) && t.SentinelId.Equals(sen.SentinelId)) == null)
                                    {
                                        int sentinelCode = Int32.Parse(sen.DeviceInfo.Organization.OrganizationCode);
                                        DispatchFigureInfo dfi = new DispatchFigureInfo()
                                        {
                                            ByteFinger = fp.FingerprintBuffer,
                                            FingerLength = fp.FingerprintBuffer.Length,
                                            FingerId = fp.FingerprintNo,
                                            //SentinelCode = sen.DeviceInfo.IPDeviceCode,
                                            SentinelCode = sentinelCode,
                                            DutyType = staff.DutyCheckTypeId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD14300001")) ? 0 : 1
                                        };
                                        //string fingerHex = "";
                                        //foreach (byte b in dfi.ByteFinger)
                                        //    fingerHex += string.Format("{0:X2}", b);
                                        //Console.WriteLine(fingerHex);

                                        var result = api.DispatchFinger(dfi);
                                        if (result.Success)
                                        {
                                            db.SentinelFingerPrintMapping.Add(new SentinelFingerPrintMapping()
                                            {
                                                SentinelFingerPrintMappingId = Guid.NewGuid(),
                                                FingerprintId = fp.FingerprintId,
                                                SentinelId = sen.SentinelId
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    db.SaveChanges();
                    _logger.LogInformation("指纹下发 End...");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("指纹下发异常：Message{0}\r\nStackTrace{1}..", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 批量清除指纹
        /// </summary>
        ///<param name="param">清除指纹数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/CleanFinger")]
        public IActionResult BatchCleanFinger([FromBody]FingerAction param)
        {
            try
            {
                _logger.LogInformation("批量清除哨位终端指纹 Begin..");
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    foreach (var sen in param.Sentinels)
                    {
                        ServiceInfo serviceInfo = this.GetASCSServiceInfo(db, sen.DeviceInfoId);
                        var api = new ASCSApi(serviceInfo);
                        var sentinelCode = Int32.Parse(sen.DeviceInfo.Organization.OrganizationCode);
                        //保存记录
                        if (param.Staffs != null && param.Staffs.Count > 0)
                        {
                            foreach (var staff in param.Staffs)
                            {
                                if (staff.Fingerprints != null)
                                {
                                    foreach (var fp in staff.Fingerprints)
                                    {
                                        var result = api.CleanFinger(sentinelCode, fp.FingerprintNo);
                                        if (result.Success)
                                        {
                                            var sfm = db.SentinelFingerPrintMapping.FirstOrDefault(t => t.SentinelId.Equals(sen.SentinelId) && t.FingerprintId.Equals(fp.FingerprintId));
                                            if (sfm != null)
                                                db.SentinelFingerPrintMapping.Remove(sfm);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var result = api.CleanFinger(sentinelCode, 0);
                            if (result.Success)
                            {
                                //清除哨位指纹
                                var sfm = db.SentinelFingerPrintMapping.Where(t => t.SentinelId.Equals(sen.SentinelId)).ToList();
                                if (sfm != null && sfm.Count > 0)
                                    db.SentinelFingerPrintMapping.RemoveRange(sfm);
                            }
                        }
                    }
                    db.SaveChanges();
                    _logger.LogInformation("批量清除哨位终端指纹 End..");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("批量清除哨位终端指纹异常：Message{0}\r\nStackTrace{1}..", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }
        #endregion

        /// <summary>
        /// 哨位时间同步
        /// </summary>
        /// <param name="deviceIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/syntimer")]
        public IActionResult SyncTimer([FromBody]Guid[] deviceIds)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ServiceInfo serviceInfo = this.GetASCSServiceInfo(db,deviceIds[0]);
                    var deviceCodes = db.IPDeviceInfo.Include(t=>t.Organization)
                        .Where(t => deviceIds.Contains(t.IPDeviceInfoId)).ToList().Select(t =>Int32.Parse( t.Organization.OrganizationCode))
                        .ToArray();
                    var r = new ASCSApi(serviceInfo).SynTimer(deviceCodes);
                    if (r.Success)
                        return Ok();
                    else
                        return BadRequest(r.ResultText);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("时间同步异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 发起对讲
        /// </summary>
        /// <param name="talk"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/talk")]
        public IActionResult StartVoipTalk([FromBody]VoipTalk talk)
        {
            try
            {
                ServiceInfo serviceInfo = this.GetASCSServiceInfo(talk.DeviceId);
                var r = new ASCSApi(serviceInfo).StartVoipTalk(talk);
                if (r.Success)
                    return Ok();
                else
                    return BadRequest(r.ResultText);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 终止对讲
        /// </summary>
        /// <param name="caller"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("~/Paps/Sentinel/talk/caller={caller}")]
        public IActionResult StopVoipTalk(int caller)
        {
            try
            {
                var r = new ASCSApi(null).StopVoipTalk(caller);
                if (r.Success)
                    return Ok();
                else
                    return BadRequest(r.ResultText);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Voip 群呼
        /// </summary>
        /// <param name="caller">群呼号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/conference")]
        public IActionResult Conference([FromBody]int[] caller)
        {
            try
            {
                new ASCSApi(null).Conference(caller);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        ///// <summary>
        ///// 设备信息变更通知
        ///// </summary>
        ///// <param name="caller">群呼号</param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("~/Paps/Sentinel/devicechange")]
        //public IActionResult NotifyDeviceInfoChange([FromBody]DeviceInfoChange notify)
        //{
        //    try
        //    {
        //        ServiceInfo serviceInfo = this.GetASCSServiceInfo(notify.DeviceId);
        //        new ASCSApi(serviceInfo).NotifyDeviceInfoChange(notify);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
        //    }
        //}

        /// <summary>
        /// 地图数据同步
        /// </summary>
        /// <param name="mapInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/mapinfo")]
        public IActionResult SyncSentinelMap([FromBody]SentinelMapInfo mapInfo)
        {
            try
            {
                _logger.LogInformation("地图同步 Begin...");

                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var device = db.IPDeviceInfo.Include(t => t.Organization).FirstOrDefault(t => t.IPDeviceInfoId.Equals(mapInfo.DeviceId));
                    int sentinelCode = 0;
                    if (Int32.TryParse(device.Organization.OrganizationCode, out sentinelCode))
                        mapInfo.SentinelCode = (uint)sentinelCode;
                    ServiceInfo serviceInfo = this.GetASCSServiceInfo(db,mapInfo.DeviceId);
                    var r = new ASCSApi(serviceInfo).SyncSentinelMap(mapInfo);
                    if (r.Success)
                    {
                        _logger.LogInformation("地图同步 End...");
                        return Ok();
                    }
                    else
                    {
                        _logger.LogInformation("地图同步失败，" + r.ResultText);
                        return BadRequest(r.ResultText);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("地图同步异常：Message{0}\r\nStackTrace{1}..", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 声光报警控制
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/ledaction")]
        public IActionResult SendLedActionControl(SoundLightControl action)
        {
            try
            {
                ServiceInfo serviceInfo = this.GetASCSServiceInfo(action.DeviceId);
                var r = new ASCSApi(serviceInfo).SendSoundLightAction(action);
                if (r.Success)
                    return Ok();
                else
                    return BadRequest(r.ResultText);
            }
            catch (Exception ex)
            {
                _logger.LogError("声光报警器控制异常，Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException() { ErrorCode = "未知错误", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据设备id,获取设备连接的哨位中心服务
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        private ServiceInfo GetASCSServiceInfo(Guid deviceId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var device = db.IPDeviceInfo.FirstOrDefault(t => t.IPDeviceInfoId.Equals(deviceId));
                return GetASCSServiceInfoByOrganizationid(db,device.OrganizationId);
            }
        }

        /// <summary>
        /// 根据设备id,获取设备连接的哨位中心服务
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        private ServiceInfo GetASCSServiceInfo(AllInOneContext.AllInOneContext db, Guid deviceId)
        {
            var device = db.IPDeviceInfo.FirstOrDefault(t => t.IPDeviceInfoId.Equals(deviceId));
            return GetASCSServiceInfoByOrganizationid(db,device.OrganizationId);
        }

        /// <summary>
        /// 根据组织机构id,获取哨位中心服务
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private ServiceInfo GetASCSServiceInfoByOrganizationid(AllInOneContext.AllInOneContext db, Guid organizationId)
        {
            //哨位台挂在哨位节点上面
            //var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
            //var service = db.ServiceInfo.Include(t => t.ServerInfo).FirstOrDefault(t => t.ServiceTypeId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD11300206"))
            //        && (t.ServerInfo.OrganizationId.Equals(org.ParentOrganizationId) || t.ServerInfo.OrganizationId.Equals(organizationId)));

            var service = db.ServiceInfo.Include(t => t.ServerInfo).FirstOrDefault(t =>
                t.ServiceTypeId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD11300206")));

            return service;
        }
        #endregion

        private IQueryable<Sentinel> GetQuery(AllInOneContext.AllInOneContext dbContext)
        {
            var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
            return dbContext.Sentinel.Include(t => t.DeviceInfo).ThenInclude(t => t.Organization).
                Include(t => t.BulletboxCamera).ThenInclude(t => t.Camera).//ThenInclude(t => t.Encoder).ThenInclude(t => t.EncoderType).
                Include(t => t.FrontCamera).ThenInclude(t => t.Camera).
                Include(t => t.SentinelVideos).ThenInclude(t => t.Camera).
                Include(t => t.SentinelSetting).
                Include(t => t.DefenseDevices).ThenInclude(t => t.DeviceInfo).ThenInclude(t => t.DeviceType). //防区设备
                Include(t => t.DefenseDevices).ThenInclude(t => t.DefenseDirection). //防区设备
            Where(t => t.DeviceInfo.StatusId == null || !t.DeviceInfo.StatusId.Equals(deleteStatusId));
        }



        /// <summary>
        /// 获取哨位视图数据，主要提供子弹箱视频轮巡用
        /// </summary> 
        /// <returns></returns>
        [HttpGet]
        [Route("~/resources/Sentinel/View")]
        public IEnumerable<SentinelView> GetSentinelView()
        {
            List<SentinelView> sentinelViews = new List<SentinelView>();
            using (var db = new AllInOneContext.AllInOneContext())
            {              
                //var sentinels = GetQuery(db).Where(t => t.DeviceInfo.OrganizationId.Equals(organizationId)).ToList();
                var sentinels = GetQuery(db).ToList();
                var cameras = GetAllCameraView(db);
                //var defences = db.DefenseDevice.Include(t => t.DefenseDirection).Include(t => t.DeviceInfo).ToList();

                var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                sentinels.ForEach(t =>
                {
                    SentinelView sv = new SentinelView()
                    {
                        SentinelId = t.SentinelId,
                        SentinelSetting = t.SentinelSetting,
                        SentinelName = t.DeviceInfo.Organization.OrganizationShortName,
                        //DisasterSwitch = t.DisasterSwitch,
                        //BreakoutSwitch = t.BreakoutSwitch,
                        IsActive = t.IsActive,
                        Phone = t.Phone,
                        // LeftArea = t.LeftArea,
                        //RaidSwitch = t.RaidSwitch,
                        //RebellionSwitch = t.RebellionSwitch,
                        // RightArea = t.RightArea,
                        FrontCamera = cameras.FirstOrDefault(f => t.FrontCamera != null && t.FrontCamera.SentinelVideoId.Equals(f.SentinelVideoId)),
                        BulletboxCamera = cameras.FirstOrDefault(f => t.BulletboxCamera != null && t.BulletboxCamera.SentinelVideoId.Equals(f.SentinelVideoId)),
                        SentinelVideos = cameras.Where(f => t.SentinelVideos.Select(a => a.SentinelVideoId).Contains(f.SentinelVideoId)).ToList(),
                        DeviceInfo = t.DeviceInfo
                    };
                    if (sv.SentinelVideos != null)  //临时做法
                    {
                        int order = 0;
                        sv.SentinelVideos.ForEach(j => j.OrderNo = order++);
                    }
                    int sentinelCode = 0;
                    if (Int32.TryParse(t.DeviceInfo.Organization.OrganizationCode, out sentinelCode))
                        sv.DeviceInfo.IPDeviceCode = sentinelCode;
                    sv.DefenseDevices = t.DefenseDevices.Where(f => deleteStatusId.Equals(f.DeviceInfo.StatusId)).Select(d => new DefenseDeviceView()
                    {
                        AlarmIn = d.AlarmIn,
                        AlarmInNormalOpen = d.AlarmInNormalOpen,
                        AlarmOut = d.AlarmOut,
                        DefenseNo = d.DefenseNo,
                        DeviceInfo = d.DeviceInfo,
                        DefenseDirectionCode = string.IsNullOrEmpty(d.DefenseDirection.MappingCode) ? 0 : Int32.Parse(d.DefenseDirection.MappingCode),
                    }).ToList();
                    //sv.DefenseDevices = defences.Where(d => t.SentinelId.Equals(d.SentinelId)).Select(d => new DefenseDeviceView()
                    //{
                    //    AlarmIn = d.AlarmIn,
                    //    AlarmInNormalOpen = d.AlarmInNormalOpen,
                    //    AlarmOut = d.AlarmOut,
                    //    DefenseNo = d.DefenseNo,
                    //    DeviceInfo = d.DeviceInfo,
                    //    DefenseDirectionCode = string.IsNullOrEmpty(d.DefenseDirection.MappingCode) ? 0 : Int32.Parse(d.DefenseDirection.MappingCode),
                    //}).ToList();
                    sentinelViews.Add(sv);
                });
            }
            return sentinelViews;
        }


        /// <summary>
        /// 获取哨位视图数据，主要提供哨位中心服务调用
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/Sentinel/View/organizationId={organizationId}")]
        public IEnumerable<SentinelView> GetSentinelViewByOrganization(Guid organizationId)
        {
            List<SentinelView> sentinelViews = new List<SentinelView>();
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var organization = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                //var sentinels = GetQuery(db).Where(t => t.DeviceInfo.OrganizationId.Equals(organizationId)).ToList();
                var sentinels = GetQuery(db).Where(t => t.DeviceInfo.Organization.OrganizationFullName.Contains(organization.OrganizationFullName)).ToList();
                var cameras = GetAllCameraView(db);
                //var defences = db.DefenseDevice.Include(t => t.DefenseDirection).Include(t => t.DeviceInfo).ToList();

                var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                sentinels.ForEach(t =>
                {
                    SentinelView sv = new SentinelView()
                    {
                        SentinelId = t.SentinelId,
                        SentinelSetting = t.SentinelSetting,
                        SentinelName = t.DeviceInfo.Organization.OrganizationShortName,
                        //DisasterSwitch = t.DisasterSwitch,
                        //BreakoutSwitch = t.BreakoutSwitch,
                        IsActive = t.IsActive,
                        Phone = t.Phone,
                        // LeftArea = t.LeftArea,
                        //RaidSwitch = t.RaidSwitch,
                        //RebellionSwitch = t.RebellionSwitch,
                        // RightArea = t.RightArea,
                        FrontCamera = cameras.FirstOrDefault(f => t.FrontCamera != null && t.FrontCamera.SentinelVideoId.Equals(f.SentinelVideoId)),
                        BulletboxCamera = cameras.FirstOrDefault(f => t.BulletboxCamera != null && t.BulletboxCamera.SentinelVideoId.Equals(f.SentinelVideoId)),
                        SentinelVideos = cameras.Where(f => t.SentinelVideos.Select(a => a.SentinelVideoId).Contains(f.SentinelVideoId)).ToList(),
                        DeviceInfo = t.DeviceInfo
                    };
                    if (sv.SentinelVideos != null)  //临时做法
                    {
                        int order = 0;
                        sv.SentinelVideos.ForEach(j =>j.OrderNo = order++);
                    }
                    int sentinelCode = 0;
                    if (Int32.TryParse(t.DeviceInfo.Organization.OrganizationCode, out sentinelCode))
                        sv.DeviceInfo.IPDeviceCode = sentinelCode;
                    sv.DefenseDevices = t.DefenseDevices.Where(f => deleteStatusId.Equals(f.DeviceInfo.StatusId)).Select(d => new DefenseDeviceView()
                    {
                        AlarmIn = d.AlarmIn,
                        AlarmInNormalOpen = d.AlarmInNormalOpen,
                        AlarmOut = d.AlarmOut,
                        DefenseNo = d.DefenseNo,
                        DeviceInfo = d.DeviceInfo,
                        DefenseDirectionCode = string.IsNullOrEmpty(d.DefenseDirection.MappingCode) ? 0 : Int32.Parse(d.DefenseDirection.MappingCode),
                    }).ToList();
                    //sv.DefenseDevices = defences.Where(d => t.SentinelId.Equals(d.SentinelId)).Select(d => new DefenseDeviceView()
                    //{
                    //    AlarmIn = d.AlarmIn,
                    //    AlarmInNormalOpen = d.AlarmInNormalOpen,
                    //    AlarmOut = d.AlarmOut,
                    //    DefenseNo = d.DefenseNo,
                    //    DeviceInfo = d.DeviceInfo,
                    //    DefenseDirectionCode = string.IsNullOrEmpty(d.DefenseDirection.MappingCode) ? 0 : Int32.Parse(d.DefenseDirection.MappingCode),
                    //}).ToList();
                    sentinelViews.Add(sv);
                });
            }
            return sentinelViews;
        }

        /// <summary>
        /// 根据设备id获取哨位视图
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/Sentinel/View/deviceId={deviceId}")]
        public SentinelView GetSentinelViewByDeviceId(Guid deviceId)
        {
            List<SentinelView> sentinelViews = new List<SentinelView>();
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                var Sentinel = GetQuery(db).FirstOrDefault(t => t.DeviceInfoId.Equals(deviceId));
                if (Sentinel != null)
                {
                    var cameras = GetAllCameraView(db);
                    //var defences = db.DefenseDevice.Include(t => t.DefenseDirection).Include
                    //        (t => t.DeviceInfo).Where(t => t.SentinelId.Equals(Sentinel.SentinelId));

                    SentinelView sv = new SentinelView()
                    {
                        SentinelId = Sentinel.SentinelId,
                        SentinelSetting = Sentinel.SentinelSetting,
                        SentinelName = Sentinel.DeviceInfo.Organization.OrganizationShortName,
                        //DisasterSwitch = Sentinel.DisasterSwitch,
                        //BreakoutSwitch = Sentinel.BreakoutSwitch,
                        IsActive = Sentinel.IsActive,
                        Phone = Sentinel.Phone,
                        //LeftArea = Sentinel.LeftArea,
                        //RaidSwitch = Sentinel.RaidSwitch,
                        //RebellionSwitch = Sentinel.RebellionSwitch,
                        //RightArea = Sentinel.RightArea,
                        FrontCamera = cameras.FirstOrDefault(f => Sentinel.FrontCamera != null && Sentinel.FrontCamera.SentinelVideoId.Equals(f.SentinelVideoId)),
                        BulletboxCamera = cameras.FirstOrDefault(f => Sentinel.BulletboxCamera != null && Sentinel.BulletboxCamera.SentinelVideoId.Equals(f.SentinelVideoId)),
                        SentinelVideos = cameras.Where(f => Sentinel.SentinelVideos.Select(a => a.SentinelVideoId).Contains(f.SentinelVideoId)).ToList(),
                        DeviceInfo = Sentinel.DeviceInfo
                    };
                    var sentinelCode = Int32.Parse(Sentinel.DeviceInfo.Organization.OrganizationCode);
                    sv.DeviceInfo.IPDeviceCode = sentinelCode;
                    if (Sentinel.DefenseDevices != null)
                    {
                        sv.DefenseDevices = Sentinel.DefenseDevices.Where(f => deleteStatusId.Equals(f.DeviceInfo.StatusId)).Select(d => new DefenseDeviceView()
                        {
                            AlarmIn = d.AlarmIn,
                            AlarmInNormalOpen = d.AlarmInNormalOpen,
                            AlarmOut = d.AlarmOut,
                            DefenseNo = d.DefenseNo,
                            DeviceInfo = d.DeviceInfo,
                            DefenseDirectionCode = string.IsNullOrEmpty(d.DefenseDirection.MappingCode) ? 0 : Int32.Parse(d.DefenseDirection.MappingCode),
                        }).ToList();

                        //sv.DefenseDevices = defences.Where(d => Sentinel.SentinelId.Equals(d.SentinelId)).Select(d => new DefenseDeviceView()
                        //{
                        //    AlarmIn = d.AlarmIn,
                        //    AlarmInNormalOpen = d.AlarmInNormalOpen,
                        //    AlarmOut = d.AlarmOut,
                        //    DefenseNo = d.DefenseNo,
                        //    DeviceInfo = d.DeviceInfo,
                        //    DefenseDirectionCode = string.IsNullOrEmpty(d.DefenseDirection.MappingCode) ? 0 : Int32.Parse(d.DefenseDirection.MappingCode),
                        //}).ToList();
                    }
                    return sv;
                }
            }
            return null;
        }

        private List<SentinelCameraView> GetAllCameraView(AllInOneContext.AllInOneContext dbContext)
        {
            try
            {
                var camInfo = (from senVideo in dbContext.Set<SentinelVideo>().ToList()
                               join cam in dbContext.Set<Camera>().Include(t => t.VideoForward).Include(t => t.IPDevice).ThenInclude(t=>t.DeviceType).ToList() on senVideo.CameraId equals cam.CameraId
                               join encoder in dbContext.Encoder.Include(t => t.DeviceInfo).Include(t => t.EncoderType).ToList() on cam.EncoderId equals encoder.EncoderId
                               select new SentinelCameraView()
                               {
                                   SentinelVideoId = senVideo.SentinelVideoId,
                                   PlayByDevice = senVideo.PlayByDevice,
                                   OrderNo = senVideo.OrderNo,
                                   CameraId = cam.CameraId,
                                   IPDeviceId = cam.IPDeviceId,
                                   CameraName = cam.IPDevice.IPDeviceName,
                                   EncoderChannel = cam.EncoderChannel,
                                   CameraNo = cam.CameraNo,
                                   DeviceType = cam.IPDevice.DeviceType.SystemOptionName,
                                   EncoderInfo = new Surveillance.ViewModel.EncoderInfo()
                                   {
                                       EncoderType = cam.Encoder.EncoderType.EncoderCode,
                                       EndPoints = cam.Encoder.DeviceInfo.EndPoints,
                                       Password = cam.Encoder.DeviceInfo.Password,
                                       User = cam.Encoder.DeviceInfo.UserName
                                   },
                                   VideoForwardInfo = new Surveillance.ViewModel.ServiceInfo()
                                   {
                                       EndPoints = cam.VideoForward.EndPoints,
                                       User = cam.VideoForward.Username,
                                       Password = cam.VideoForward.Password
                                   }

                               }).ToList();// t => t.DeviceInfoId.Equals(deviceId));
                return camInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError("根据获取获取摄像机信息异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                return null;
            }
        }

        #region 指纹下发精简接口
        /// <summary>
        /// 批量清除指纹，若人员id为null，则清除哨位所有指纹
        /// </summary>
        ///<param name="param">清除指纹数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/Simple/CleanFinger")]
        public IActionResult BatchCleanFinger([FromBody]FingerActionEx param)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var staffs = db.Staff.Include(t => t.Fingerprints).Where(t => param.StaffIds != null && param.StaffIds.Contains(t.StaffId)).ToList();
                var sentinels = db.Sentinel.Include(t => t.DeviceInfo).ThenInclude(t=>t.Organization)
                    .Where(t => param.SentinelIds.Contains(t.SentinelId)).ToList();
                var fingerAction = new FingerAction()
                {
                    Staffs = staffs,
                    Sentinels = sentinels
                };
                return BatchCleanFinger(fingerAction);
            }
        }

        /// <summary>
        /// 批量下发 [精简，减少客户端下发数据]
        /// </summary>
        ///<param name="param">清除指纹数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Paps/Sentinel/Simple/DispatchFinger")]
        public IActionResult BatchDispatchFinger([FromBody]FingerActionEx param)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var staffs = db.Staff.Include(t => t.Fingerprints).Where(t => param.StaffIds.Contains(t.StaffId)).ToList();
                var sentinels = db.Sentinel.Include(t => t.DeviceInfo).ThenInclude(t=>t.Organization).Where(t => param.SentinelIds.Contains(t.SentinelId)).ToList();
                var fingerAction = new FingerAction()
                {
                    Staffs = staffs,
                    Sentinels = sentinels
                };
                return BatchDispatchFinger(fingerAction);
            }
        }
        #endregion

        /// <summary>
        /// 报警动作复位
        /// </summary>
        private void ResetAlarmPlan()
        {
            try
            {
                string url = string.Format("{0}/Task/PlanAction/Reset", GlobalSetting.TaskServerBaseUrl);
                string error = "";
                bool b = HttpClientHelper.Delete(url, ref error);
                _logger.LogInformation("调用{0} 复位报警预案，Error:{1}", url, error);
            }
            catch (Exception ex)
            {
                _logger.LogError("复位报警预案异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("~/Paps/Sentinel/PunchLog/Forward")]
        /// <summary>
        /// 转发指纹记录
        /// </summary>
        /// <param name="punchLog"></param>
        /// <returns></returns>
        public IActionResult ForwardPunchLog([FromBody]PunchLog punchLog)
        {
            try
            {
                punchLog.IsForward = true;
                MQPulish.PublishMessage("PunchLog", punchLog);
                _logger.LogError("收到{0}转发的指纹记录", punchLog.PunchDevice.Organization.OrganizationFullName);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("处理下级转发的指纹记录异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 转发查勤到上级
        /// </summary>
        /// <param name="alarmLog"></param>
        private void ForwardPunchLogTopOrganization(PunchLog log)
        {
            Task.Run(() =>
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    try
                    {
                        _logger.LogInformation("启动查哨消息推送到上级系统任务！！");
                        //string error = "";
                        var rootOrganization = db.Organization.Include(t => t.Center).
                                OrderBy(t => t.OrganizationFullName).
                                FirstOrDefault();

                        if (rootOrganization != null &&
                            rootOrganization.Center != null &&
                            rootOrganization.Center.EndPoints != null &&
                            rootOrganization.Center.EndPoints.Count > 0)
                        {
                            if (log.SentinelViewInfo != null)
                                log.SentinelViewInfo.SentinelName = rootOrganization.OrganizationShortName + "." + log.SentinelViewInfo.SentinelName;
                            _logger.LogInformation("开始推送{1}查哨消息到上级系统...", log.SentinelViewInfo.SentinelName);
                            EndPointInfo endPoint = rootOrganization.Center.EndPoints.First();
                            string url = string.Format("http://{0}:{1}/Paps/Sentinel/PunchLog/Forward", endPoint.IPAddress, endPoint.Port);
                            var result = HttpClientHelper.Post<PunchLog>(log, url, false);
                            _logger.LogInformation("查勤消息推送到上级系统结果{0}", result);
                        }
                        else
                            _logger.LogInformation("未配置上级应用服务，取消查勤消息推送到上级系统");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("查哨消息推送到上级系统异常：Message:{0}\r\nInnerException:{1}", ex.Message, ex.InnerException);
                    }
                }
            });
        }

       /// <summary>
       /// 换岗记录查询
       /// </summary>
       /// <param name="deviceId"></param>
       /// <param name="beginTime"></param>
       /// <param name="endTime"></param>
       /// <param name="pageNo"></param>
       /// <param name="pageSise"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/Sentinel/GuardLog")]
        //http://127.0.0.1:5001/Paps/Sentinel/GuardLog?deviceId=a0002016-e009-b019-e001-abcd11300204&beginTime=2016-10-11 10:10:02&endTime=2016-10-11 10:10:02&pageNo=1&pageSize=100
        public IActionResult SearchGuardlog(Guid deviceId, DateTime beginTime,
            DateTime endTime, int pageNo, int pageSize,Guid checkTypeId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var dutyCheckTypeId = Guid.Parse("a0002016-e009-b019-e001-abcd14300001");
                var query = db.PunchLog.Include(t => t.PunchDevice).ThenInclude(t => t.Organization).Include(t => t.Staff).Include(t => t.PunchType).Include(t => t.LogResult).Include(t =>t.AppraiseType).Include(t=>t.FrontSnapshot)
                    .Where(t => (deviceId.Equals(Guid.Empty) || t.PunchDeviceId.Equals(deviceId)) &&
                    (checkTypeId.Equals(dutyCheckTypeId) ? t.PunchTypeId.Equals(dutyCheckTypeId) : !t.PunchTypeId.Equals(dutyCheckTypeId)) &&
                    t.LogTime > beginTime && t.LogTime < endTime).ToList().OrderByDescending(t => t.LogTime);


                if (pageNo >= 0 && pageSize > 0)
                {
                    var data = query.Skip(pageSize * (pageNo-1)).Take(pageSize).ToList();
                    data.ForEach(t =>
                    {
                        ILogger<DutyCheckController> logger = Logger.CreateLogger<DutyCheckController>();
                        if (checkTypeId.Equals(new Guid("a0002016-e009-b019-e001-abcd14300001")))
                        {
                            DutyDetail r = new DutyCheckController(logger).GetDutyDetailByFieldCheck(t.PunchDeviceId, t.LogTime,t.StaffId);
                            if (r != null)
                            {
                                t.OnDutyStaff = r.OnDutyStaff;
                                t.OffDutyStaff = r.OffDutyStaff;
                            }
                        }
                        else
                        {
                            DutyDetail r = new DutyCheckController(logger).GetDutyDetailBySentinelId(t.PunchDeviceId, t.LogTime);
                            if (r != null)
                            {
                                t.OnDutyStaff = r.OnDutyStaff;
                                t.OffDutyStaff = r.OffDutyStaff;
                            }
                        }
                    });
                    QueryPagingRecord queryPagingRecord = new QueryPagingRecord
                    {
                        SumRecordCount = query.Count(),
                        Record = data
                    };
                    return new ObjectResult(queryPagingRecord);
                }
                else
                {
                    query.ToList().ForEach(t =>
                    {
                        ILogger<DutyCheckController> logger = Logger.CreateLogger<DutyCheckController>();
                        if (checkTypeId.Equals(new Guid("a0002016-e009-b019-e001-abcd14300001")))
                        {
                            DutyDetail r = new DutyCheckController(logger).GetDutyDetailByFieldCheck(t.PunchDeviceId, t.LogTime, t.StaffId);
                            if (r != null)
                            {
                                t.OnDutyStaff = r.OnDutyStaff;
                                t.OffDutyStaff = r.OffDutyStaff;
                            }
                        }
                        else
                        {
                            DutyDetail r = new DutyCheckController(logger).GetDutyDetailBySentinelId(t.PunchDeviceId, t.LogTime);
                            if (r != null)
                            {
                                t.OnDutyStaff = r.OnDutyStaff;
                                t.OffDutyStaff = r.OffDutyStaff;
                            }
                        }
                    });
                    QueryPagingRecord queryPagingRecord = new QueryPagingRecord
                    {
                        SumRecordCount = query.Count(),
                        Record = query
                    };
                    return new ObjectResult(queryPagingRecord);
                }
            }
        }

        /// <summary>
        /// 符合条件的数目
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSise"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Paps/Sentinel/GuardLog/Count")]
        //http://127.0.0.1:5001/Paps/Sentinel/GuardLog/Count?deviceId=a0002016-e009-b019-e001-abcd11300204&beginTime=2016-10-11 10:10:02&endTime=2016-10-11 10:10:02
        public int CountGuardlog(Guid deviceId, DateTime beginTime,
            DateTime endTime)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var checkTypeId = Guid.Parse("a0002016-e009-b019-e001-abcd14300001"); //查哨类型
                var q = db.PunchLog
                    .Where(t => (deviceId.Equals(Guid.Empty) || t.PunchDeviceId.Equals(deviceId)) &&
                    !checkTypeId.Equals(t.PunchTypeId) &&
                     t.LogTime > beginTime && t.LogTime < endTime).ToList().Count();
                return q;
            }
        }

        #region 查哨换岗消息事件截图

        /// <summary>
        /// 查哨换岗视频截图
        /// </summary>
        private void PunchlogSnaphot(PunchLog punchlog)
        {
            PunchlogFrontVideoSnapshot(punchlog);
            PunchlogBulletboxSnapshot(punchlog);
        }

        /// <summary>
        /// 查哨/换岗前端视频截图
        /// </summary>
        /// <param name="punchlog"></param>
        private void PunchlogFrontVideoSnapshot(PunchLog punchlog)
        {
            if (punchlog.SentinelViewInfo.FrontCamera != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    string source = string.Format("查哨换岗事件{0}的前端视频截图", punchlog.PunchLogId);
                    string fileName = string.Format("{0:yyyyMMddHHmmssfff}.png", DateTime.Now);
                    var frontSnapshot = new Attachment()
                    {
                        AttachmentId = Guid.NewGuid(),
                        Modified = DateTime.Now,
                        AttachmentPath = "/attach/",
                        AttachmentName = fileName
                    };
                    bool result = await Snapshot(frontSnapshot.AttachmentId, punchlog.SentinelViewInfo.FrontCamera.IPDeviceId, fileName, source);
                    if (result)
                    {
                        try
                        {
                            using (var db = new AllInOneContext.AllInOneContext())
                            {
                                var dbPunchLog = db.PunchLog.FirstOrDefault(t => t.PunchLogId.Equals(punchlog.PunchLogId));
                                var dbAttach = db.Attachment.FirstOrDefault(t => t.AttachmentId.Equals(frontSnapshot.AttachmentId));
                                if (dbAttach == null)
                                    dbPunchLog.FrontSnapshot = frontSnapshot;
                                else
                                    dbPunchLog.FrontSnapshotId = frontSnapshot.AttachmentId;
                                db.SaveChanges();
                                _logger.LogInformation("截取查哨换岗事件{0}的前端视频截图成功，保存记录", punchlog.PunchLogId);
                            }

                            _logger.LogInformation("查哨换岗事件前端视频截图成功，广播截图消息");
                            SentinelSnapshotEvent ev = new SentinelSnapshotEvent()
                            {
                                Snapshot = frontSnapshot,
                                EventSourceId = punchlog.PunchLogId,
                                EventType = SentinelSnapshotEventType.PunchLog,
                                SanpshotVideo = SentinelSnapshotVideo.FrontCamera
                            };
                            MQPulish.PublishMessage("SentinelSnapshot", ev);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("哨换岗事件前端视频截图异常：{0}", ex.InnerException);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 查哨/换岗子弹箱视频截图
        /// </summary>
        /// <param name="punchlog"></param>
        private void PunchlogBulletboxSnapshot(PunchLog punchlog)
        {
            if (punchlog.SentinelViewInfo.BulletboxCamera != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    string source = string.Format("查哨换岗事件{0}的子弹箱视频", punchlog.PunchLogId);
                    string fileName = string.Format("{0:yyyyMMddHHmmssfff}.png", DateTime.Now);
                    var bulletSnapshot = new Attachment()
                    {
                        AttachmentId = Guid.NewGuid(),
                        Modified = DateTime.Now,
                        AttachmentPath = "/attach/",
                        AttachmentName = string.Format("{0:yyyyMMddHHmmssfff}.png", DateTime.Now)
                    };
                    var result = await Snapshot(bulletSnapshot.AttachmentId, punchlog.SentinelViewInfo.BulletboxCamera.IPDeviceId, fileName, source);
                    if (result)
                    {
                        try
                        {
                            using (var db = new AllInOneContext.AllInOneContext())
                            {
                                var dbPunchLog = db.PunchLog.FirstOrDefault(t => t.PunchLogId.Equals(punchlog.PunchLogId));
                                var dbAttach = db.Attachment.FirstOrDefault(t => t.AttachmentId.Equals(bulletSnapshot.AttachmentId));
                                if (dbAttach == null)
                                    dbPunchLog.BulletboxSnapshot = bulletSnapshot;
                                else
                                    dbPunchLog.BulletboxSnapshotId = bulletSnapshot.AttachmentId;
                                db.SaveChanges();
                            }

                            _logger.LogInformation("查哨换岗事件子弹箱视频截图成功，广播截图消息");
                            SentinelSnapshotEvent ev = new SentinelSnapshotEvent()
                            {
                                Snapshot = bulletSnapshot,
                                EventSourceId = punchlog.PunchLogId,
                                EventType = SentinelSnapshotEventType.PunchLog,
                                SanpshotVideo = SentinelSnapshotVideo.BulletboxCamera
                            };
                            MQPulish.PublishMessage("SentinelSnapshot", ev);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("哨换岗事件子弹箱视频截图异常：{0}", ex.InnerException);
                        }
                    }
                });
            }
        }
        #endregion

        #region 子弹箱消息事件截图
        /// <summary>
        /// 供弹申请截图
        /// </summary>
        private void BulletboxSnaphot(BulletboxLog bulletboxLog)
        {
            BulletboxFrontVideoSnaphost(bulletboxLog);
            BulletboxBulletboxVideoSnapshot(bulletboxLog);
        }

        /// <summary>
        /// 子弹箱事件前端视频截图
        /// </summary>
        /// <param name="bulletboxLog"></param>
        private void BulletboxFrontVideoSnaphost(BulletboxLog bulletboxLog)
        {
            if (bulletboxLog.SentinelViewInfo.FrontCamera != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    string source = string.Format("子弹箱消息事件{0}的前端视频", bulletboxLog.BulletboxLogId);
                    string fileName = string.Format("{0:yyyyMMddHHmmssfff}.png", DateTime.Now);
                    var frontSnapshot = new Attachment()
                    {
                        AttachmentId = Guid.NewGuid(),
                        Modified = DateTime.Now,
                        AttachmentPath = "/attach/",
                        AttachmentName = fileName
                    };
                    bool result = await Snapshot(frontSnapshot.AttachmentId, bulletboxLog.SentinelViewInfo.FrontCamera.IPDeviceId, fileName, source);
                    if (result)
                    {
                        try
                        {
                            using (var db = new AllInOneContext.AllInOneContext())
                            {
                                var dbBulletLog = db.BulletboxLog.FirstOrDefault(t => t.BulletboxLogId.Equals(bulletboxLog.BulletboxLogId));
                                var dbAttach = db.Attachment.FirstOrDefault(t => t.AttachmentId.Equals(frontSnapshot.AttachmentId));
                                if (dbAttach == null)
                                    dbBulletLog.FrontSnapshot = frontSnapshot;
                                else
                                    dbBulletLog.FrontSnapshotId = frontSnapshot.AttachmentId;
                                db.SaveChanges();
                            }

                            _logger.LogInformation("子弹箱消息事件，前端视频截图成功，广播截图消息");
                            SentinelSnapshotEvent ev = new SentinelSnapshotEvent()
                            {
                                Snapshot = frontSnapshot,
                                EventSourceId = bulletboxLog.BulletboxLogId,
                                EventType = SentinelSnapshotEventType.BulletboxLog,
                                SanpshotVideo = SentinelSnapshotVideo.FrontCamera
                            };
                            MQPulish.PublishMessage("SentinelSnapshot", ev);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("子弹箱消息事件，前端视频截图异常：:{0}", ex.InnerException);
                        }
                    }
                });
            }
        }

        private void BulletboxBulletboxVideoSnapshot(BulletboxLog bulletboxLog)
        {
            if (bulletboxLog.SentinelViewInfo.BulletboxCamera != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    string source = string.Format("供弹申请事件{0}的子弹箱视频", bulletboxLog.BulletboxLogId);
                    string fileName = string.Format("{0:yyyyMMddHHmmssfff}.png", DateTime.Now);
                    var bulletSnapshot = new Attachment()
                    {
                        AttachmentId = Guid.NewGuid(),
                        Modified = DateTime.Now,
                        AttachmentPath = "/attach/",
                        AttachmentName = string.Format("{0:yyyyMMddHHmmssfff}.png", DateTime.Now)
                    };
                    var result = await Snapshot(bulletSnapshot.AttachmentId, bulletboxLog.SentinelViewInfo.BulletboxCamera.IPDeviceId, fileName, source);
                    if (result)
                    {
                        try
                        {
                            using (var db = new AllInOneContext.AllInOneContext())
                            {
                                var dbBulletboxlog = db.BulletboxLog.FirstOrDefault(t => t.BulletboxLogId.Equals(bulletboxLog.BulletboxLogId));
                                var dbAttach = db.Attachment.FirstOrDefault(t => t.AttachmentId.Equals(bulletSnapshot.AttachmentId));
                                if (dbAttach == null)
                                    dbBulletboxlog.BulletboxSnapshot = bulletSnapshot;
                                else
                                    dbBulletboxlog.BulletboxSnapshotId = bulletSnapshot.AttachmentId;
                                db.SaveChanges();
                            }
                            _logger.LogInformation("子弹箱消息事件，子弹箱视频截图成功，广播截图消息");
                            SentinelSnapshotEvent ev = new SentinelSnapshotEvent()
                            {
                                Snapshot = bulletSnapshot,
                                EventSourceId = bulletboxLog.BulletboxLogId,
                                EventType = SentinelSnapshotEventType.BulletboxLog,
                                SanpshotVideo = SentinelSnapshotVideo.BulletboxCamera
                            };
                            MQPulish.PublishMessage("SentinelSnapshot", ev);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("子弹箱消息事件，子弹箱截图异常：:{0}", ex.InnerException);
                        }
                    }
                });
            }
        }
        #endregion

    }
}
