/* 2016-12-14 zhrx 增加两警联动查询
 * 2016-12-15 zhrx 增加哨位自动编号
 */
using HttpClientEx;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Route("Resources/[controller]")]
    public class SentinelController : Controller
    {
        ILogger<SentinelController> _logger;

        public SentinelController(ILogger<SentinelController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Sentinel sentinel)
        {
            if (sentinel == null)
                return BadRequest("Sentinel object can not be null!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    //if (sentinel.DefenseDevices != null)
                    //{
                    //    sentinel.DefenseDevices.ForEach(t => {
                    //        var defenceType = db.SystemOption.FirstOrDefault(f => f.SystemOptionId.Equals(t.DeviceInfo.DeviceTypeId));
                    //        if (defenceType.SystemOptionName.Contains("左"))
                    //            t.DefenseDirectionId = Guid.Parse("a0002016-e009-b019-e001-abcd13900001");//横向
                    //        else
                    //            t.DefenseDirectionId = Guid.Parse("a0002016-e009-b019-e001-abcd13900002");//纵向
                    //    });
                    //}
                    //自定义分配哨位编号...1 - 32  2016-12-23 已取消，哨位编号从所属哨位节点获取
                    // int sentinelNum = NewSentinelNum(db, sentinel.DeviceInfo.OrganizationId);
                    //if (sentinelNum == -1)
                    //{
                    //    return BadRequest(new ApplicationException() { ErrorMessage = "哨位编号已分配完成", ErrorCode = "Unknow" });
                    //}
                    //_logger.LogInformation("哨位设备编号：{0}", sentinelNum);
                    //Sentinel.DeviceInfo.IPDeviceCode = sentinelNum;

                    //判断哨位节点下是否已添加哨位台
                    var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                    var existSentinel = db.Sentinel.Include(t => t.DeviceInfo).FirstOrDefault(t => !deleteStatusId.Equals(t.DeviceInfo.StatusId) &&
                          t.DeviceInfo.OrganizationId.Equals(sentinel.DeviceInfo.OrganizationId));
                    if (existSentinel != null)
                    {
                        return BadRequest(new ApplicationException() {
                            ErrorCode = "限制错误",
                            ErrorMessage = "已添加哨位终端设备"
                        });
                    }

                    //防区设备编号从1000开始
                    var node = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(sentinel.DeviceInfo.OrganizationId));
                    if (sentinel.DefenseDevices != null)
                    {
                        var defenceNum = NewDefenceNum(db, sentinel.DeviceInfo.OrganizationId);
                        _logger.LogInformation("防区设备开始编号：{0}", defenceNum);
                        ++defenceNum;
                        sentinel.DefenseDevices.ForEach(t =>
                        {
                            t.DeviceInfo.IPDeviceCode = defenceNum;
                            t.DeviceInfo.Modified = DateTime.Now;
                            var deviceType = db.SystemOption.FirstOrDefault(f => f.SystemOptionId.Equals(t.DeviceInfo.DeviceTypeId));
                            var direction = db.SystemOption.FirstOrDefault(f => f.SystemOptionId.Equals(t.DefenseDirectionId));
                            t.DeviceInfo.IPDeviceName = string.Format("{0}-{1}-{2}",
                                node.OrganizationShortName, deviceType.SystemOptionName, direction.SystemOptionName);
                            ++defenceNum;
                        });
                    }
                    sentinel.DeviceInfo.Modified = DateTime.Now;
                    sentinel.DeviceInfo.IPDeviceName = node.OrganizationShortName;

                    if (sentinel.SentinelVideos != null)
                    {
                        int order = 0;
                        sentinel.SentinelVideos.ForEach(t => t.OrderNo = order++);
                    }
                    db.Sentinel.Add(sentinel);
                    db.SaveChanges();
                    //  return CreatedAtAction("GetById", new { id = sentinel.SentinelId }, sentinel);
                    SendDatachangeNotify(sentinel.DeviceInfo, 2);
                    return CreatedAtAction("", sentinel);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加哨位台异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加哨位台异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Sentinel sentinel)
        {
            if (sentinel == null)
                return BadRequest("Sentinel object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        Sentinel obj = GetQuery(db).FirstOrDefault(t => t.SentinelId.Equals(sentinel.SentinelId));
                        if (obj.BulletboxCamera != null)
                            db.Set<SentinelVideo>().Remove(obj.BulletboxCamera);
                        if (obj.FrontCamera != null)
                            db.Set<SentinelVideo>().Remove(obj.FrontCamera);
                        if (obj.SentinelVideos != null)
                            db.Set<SentinelVideo>().RemoveRange(obj.SentinelVideos);
                        //if (obj.DeviceInfo != null)
                        //    db.IPDeviceInfo.Remove(obj.DeviceInfo);
                        if (obj.SentinelSetting != null)
                            db.Set<SentinelSetting>().Remove(obj.SentinelSetting);
                        //db.Sentinel.Remove(obj);
                        //if (obj.DefenseDevices != null)
                        //    db.DefenseDevice.RemoveRange(obj.DefenseDevices);
                        if (obj.AlarmOutputChannels != null)
                            db.Set<DeviceChannelSetting>().RemoveRange(obj.AlarmOutputChannels);
                        db.SaveChanges();

                        //if (sentinel.DefenseDevices != null)
                        //{
                        //    sentinel.DefenseDevices.ForEach(t => {
                        //        var defenceType = db.SystemOption.FirstOrDefault(f => f.SystemOptionId.Equals(t.DeviceInfo.DeviceTypeId));
                        //        if (defenceType.SystemOptionName.Contains("左"))
                        //            t.DefenseDirectionId = Guid.Parse("a0002016-e009-b019-e001-abcd13900001");//横向
                        //        else
                        //            t.DefenseDirectionId = Guid.Parse("a0002016-e009-b019-e001-abcd13900002");//纵向
                        //    });
                        //}

                        //已删除的防区设备
                        var deleteStatus = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("13800005"));
                        if (obj.DefenseDevices != null)
                        {
                            if (sentinel.DefenseDevices == null)
                                obj.DefenseDevices.ForEach(t => t.DeviceInfo.StatusId = deleteStatus.SystemOptionId);
                            else
                            {
                                var uiDefenceDeviceIds = sentinel.DefenseDevices.Select(t => t.DefenseDeviceId).ToList();
                                obj.DefenseDevices.ForEach(t => { if (uiDefenceDeviceIds.Contains(t.DefenseDeviceId))
                                    {
                                        //更新
                                        t = sentinel.DefenseDevices.FirstOrDefault(f => f.DefenseDeviceId.Equals(t.DefenseDeviceId));
                                        var device = db.IPDeviceInfo.FirstOrDefault(f => f.IPDeviceInfoId.Equals(t.DeviceInfoId));
                                        device.DeviceTypeId = t.DeviceInfo.DeviceTypeId; //设备类型修改
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        t.DeviceInfo.StatusId = deleteStatus.SystemOptionId; //已删除
                                    }
                                });
                            }
                        }

                        //防区设备更新
                        if (sentinel.DefenseDevices != null)
                        {
                            if (obj.DefenseDevices == null)
                                obj.DefenseDevices = sentinel.DefenseDevices;
                            else
                            {
                                var dbDefenceDevices = obj.DefenseDevices.Select(t => t.DefenseDeviceId).ToList();
                                sentinel.DefenseDevices.ForEach(t => {
                                    if (!dbDefenceDevices.Contains(t.DefenseDeviceId))
                                    {
                                        obj.DefenseDevices.Add(t);
                                    }
                                    else //已添加，更新
                                    {
                                        var dbDefenceDevice = obj.DefenseDevices.FirstOrDefault(f => f.DefenseDeviceId.Equals(t.DefenseDeviceId));
                                        dbDefenceDevice.DeviceInfo.DeviceTypeId = t.DeviceInfo.DeviceTypeId;
                                        //dbDefenceDevice.DeviceInfo.IPDeviceName = t.DeviceInfo.IPDeviceName;
                                        dbDefenceDevice.DefenseDirectionId = t.DefenseDirectionId;
                                        dbDefenceDevice.AlarmIn = t.AlarmIn;
                                        dbDefenceDevice.AlarmOut = t.AlarmOut;
                                        dbDefenceDevice.AlarmInNormalOpen = t.AlarmInNormalOpen;
                                    }
                                });
                            }
                        }
                        //暂时编号从0开始
                        if (sentinel.SentinelVideos != null)
                        {
                            int order = 0;
                            sentinel.SentinelVideos.ForEach(t => t.OrderNo = order++);
                        }

                        //更新设备信息
                        obj.DeviceInfo.DeviceTypeId = sentinel.DeviceInfo.DeviceTypeId;
                        obj.DeviceInfo.EndPointsJson = sentinel.DeviceInfo.EndPointsJson;
                        //obj.DeviceInfo.IPDeviceCode = sentinel.DeviceInfo.IPDeviceCode;
                        //obj.DeviceInfo.IPDeviceName = sentinel.DeviceInfo.IPDeviceName;
                        obj.DeviceInfo.OrganizationId = sentinel.DeviceInfo.OrganizationId;
                        obj.DeviceInfo.Modified = DateTime.Now;

                        obj.SentinelSetting = sentinel.SentinelSetting;
                        obj.BulletboxCamera = sentinel.BulletboxCamera;
                        //obj.DeviceInfoId = sentinel.DeviceInfoId;
                        obj.FrontCamera = sentinel.FrontCamera;
                        obj.IsActive = sentinel.IsActive;
                        obj.Phone = sentinel.Phone;
                        obj.SentinelId = sentinel.SentinelId;
                        obj.SentinelSetting = sentinel.SentinelSetting;
                        obj.SentinelVideos = sentinel.SentinelVideos;
                        //obj.DefenseDevices = sentinel.DefenseDevices;
                        obj.AlarmOutputChannels = sentinel.AlarmOutputChannels;

                        db.Sentinel.Update(obj);
                        db.SaveChanges();
                        tran.Commit();
                        SendDatachangeNotify(sentinel.DeviceInfo, 1);
                        return NoContent();
                    }
                    catch (DbUpdateException dbEx)
                    {
                        tran.Rollback();
                        _logger.LogError("更新哨位台异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                        return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        _logger.LogError("更新哨位台异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        Sentinel deleteObj = GetQuery(db).FirstOrDefault(t => t.SentinelId.Equals(id));
                        if (deleteObj == null)
                            return NotFound();
                        //if (deleteObj.BulletboxCamera != null)
                        //    db.Set<SentinelVideo>().Remove(deleteObj.BulletboxCamera);
                        //if (deleteObj.FrontCamera != null)
                        //    db.Set<SentinelVideo>().Remove(deleteObj.FrontCamera);
                        //if (deleteObj.SentinelVideos != null)
                        //    db.Set<SentinelVideo>().RemoveRange(deleteObj.SentinelVideos);
                        //if (deleteObj.DeviceInfo != null)
                        //    db.IPDeviceInfo.Remove(deleteObj.DeviceInfo);
                        //if (deleteObj.SentinelSetting != null)
                        //    db.Set<SentinelSetting>().Remove(deleteObj.SentinelSetting);
                        //if (deleteObj.DefenseDevices != null)
                        //    db.DefenseDevice.RemoveRange(deleteObj.DefenseDevices);
                        //if (deleteObj.AlarmOutputChannels != null)
                        //    db.Set<DeviceChannelSetting>().RemoveRange(deleteObj.AlarmOutputChannels);
                        //移除哨位台人员指纹
                        DeleteSentinelFingerInfo(db, deleteObj);
                        //db.Sentinel.Remove(deleteObj);
                        //将设备标记为移除状态
                        var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                        deleteObj.DeviceInfo.StatusId = deleteStatusId;

                        //报警配置设备id
                        List<Guid> alarmSourceIds = new List<Guid>();

                        if (deleteObj.DefenseDevices != null)
                            deleteObj.DefenseDevices.ForEach(t =>
                            {
                                t.DeviceInfo.StatusId = deleteStatusId;
                                alarmSourceIds.Add(t.DeviceInfoId);
                            });
                        db.IPDeviceInfo.Update(deleteObj.DeviceInfo);
                        db.IPDeviceInfo.UpdateRange(deleteObj.DefenseDevices.Select(t => t.DeviceInfo));
                        db.SaveChanges();

                        alarmSourceIds.Add(deleteObj.DeviceInfoId);
                        //删除报警配置
                        AlarmSettingUtility.RemoveAlarmSetting(db, alarmSourceIds);

                        tran.Commit();
                        SendDatachangeNotify(deleteObj.DeviceInfo, 0);
                    }
                    catch (DbUpdateException dbEx)
                    {
                        tran.Rollback();
                        _logger.LogError("删除哨位台异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                        return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        _logger.LogError("删除哨位台异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                    }
                }
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Sentinel obj = GetQuery(db).FirstOrDefault(t => t.SentinelId.Equals(id));
                if (obj == null)
                    return NotFound();
                return new ObjectResult(obj);
            }
        }

        /// <summary>
        /// 获取哨位设备
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="includeLower"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/Sentinel/organizationId={organizationId}")]
        public IEnumerable<Sentinel> GetByOrganization(Guid organizationId, bool includeLower = false)
        {
            List<Sentinel> sentinels = new List<Sentinel>();
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                var deviceTypeId = Guid.Parse("a0002016-e009-b019-e001-abcd11000012");
                if (includeLower)
                {
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                    if (org != null)
                    {
                        sentinels = GetQuery(db).Include(t => t.SentinelSetting).Where(t => t.DeviceInfo.Organization.OrganizationFullName.
                            Contains(org.OrganizationFullName) //(t.DeviceInfo.StatusId == null || !t.DeviceInfo.StatusId.Equals(deleteStatusId)) &&
                            && !t.DeviceInfo.DeviceTypeId.Equals(deviceTypeId)).
                            OrderBy(t => t.DeviceInfo.IPDeviceName).ToList();
                    }
                }
                else
                {
                    sentinels = GetQuery(db).Include(t => t.SentinelSetting).Where(t => t.DeviceInfo.OrganizationId.Equals(organizationId)
                         //&& (t.DeviceInfo.StatusId == null || !t.DeviceInfo.StatusId.Equals(deleteStatusId)) &&
                         && !t.DeviceInfo.DeviceTypeId.Equals(deviceTypeId)).OrderBy(t => t.DeviceInfo.IPDeviceName).ToList();
                }
            }
            ReplenishDefenceDeviceInfo(sentinels);
            return sentinels;
        }

        /// <summary>
        /// 获取两警联动设备
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="includeLower"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/DoubleLinkage/organizationId={organizationId}")]
        public IEnumerable<Sentinel> GetDoubleLinkageDevice(Guid organizationId, bool includeLower = false)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                //var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                var deviceTypeId = Guid.Parse("a0002016-e009-b019-e001-abcd11000012");
                List<Sentinel> sentinels = new List<Sentinel>();
                if (includeLower)
                {
                    var org = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(organizationId));
                    if (org != null)
                    {
                        sentinels =  GetQuery(db).Include(t => t.SentinelSetting).Where(t => t.DeviceInfo.Organization.OrganizationFullName.
                            Contains(org.OrganizationFullName) && 
                            //(t.DeviceInfo.StatusId == null || !t.DeviceInfo.StatusId.Equals(deleteStatusId)) &&
                            t.DeviceInfo.DeviceTypeId.Equals(deviceTypeId)).
                            OrderBy(t => t.DeviceInfo.IPDeviceName).ToList();
                    }
                }
                else
                {
                    sentinels = GetQuery(db).Include(t => t.SentinelSetting).Where(t => t.DeviceInfo.OrganizationId.Equals(organizationId) &&
                         //&& (t.DeviceInfo.StatusId == null || !t.DeviceInfo.StatusId.Equals(deleteStatusId)) &&
                         t.DeviceInfo.DeviceTypeId.Equals(deviceTypeId)).OrderBy(t => t.DeviceInfo.IPDeviceName).ToList();
                }
                sentinels.ForEach(t => {
                    int code = 0;
                    if (Int32.TryParse(t.DeviceInfo.Organization.OrganizationCode, out code))
                        t.DeviceInfo.IPDeviceCode = code;

                });
                return sentinels;
            }
        }

        [HttpGet]
        public IEnumerable<Sentinel> GetAll()
        {
            List<Sentinel> sentinels = new List<Sentinel>();
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                sentinels = GetQuery(db)./*Where(t => t.DeviceInfo.StatusId == null || !t.DeviceInfo.StatusId.Equals(deleteStatusId)).*/ToList();
            }
            ReplenishDefenceDeviceInfo(sentinels);
            return sentinels;
        }

        /// <summary>
        /// 补充哨位防区信息，移除已删除的防区设备
        /// </summary>
        private void ReplenishDefenceDeviceInfo(List<Sentinel> sentinels)
        {
            if (sentinels != null)
                sentinels.ForEach(t =>
                {
                    string sentinelNodeName = t.DeviceInfo.Organization.OrganizationShortName;
                    t.DeviceInfo.IPDeviceName = sentinelNodeName;
                    int code = 0;
                    Int32.TryParse(t.DeviceInfo.Organization.OrganizationCode, out code);
                    t.DeviceInfo.IPDeviceCode = code;
                    if (t.DefenseDevices != null)
                    {
                        var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
                        t.DefenseDevices.ForEach(f => f.DeviceInfo.IPDeviceName = string.Format("{0}-{1}-{2}",
                            sentinelNodeName, f.DeviceInfo.DeviceType.SystemOptionName,f.DefenseDirection.SystemOptionName));

                        //移除已经删除的防区设备....
                        int i = t.DefenseDevices.Count - 1;
                        for (int j = i; j >= 0; j--)
                        {
                            if (deleteStatusId.Equals(t.DefenseDevices[j].DeviceInfo.StatusId))
                            {
                                t.DefenseDevices.RemoveAt(j);
                            }
                        }
                    }
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/Sentinel/ids={ids}")]
        public IEnumerable<Sentinel> GetBySentinelIds(string ids)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    var sentinelIds = JsonConvert.DeserializeObject<Guid[]>(ids);
                    var sentinels = from sen in db.Sentinel.Include(t => t.DeviceInfo).ToList()
                                    where sentinelIds.Contains(sen.SentinelId)
                                    select sen;
                    return sentinels.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("根据设备id列表获取获取哨位台信息异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                    return null;
                }
            }
        }

        private IQueryable<Sentinel> GetQuery(AllInOneContext.AllInOneContext dbContext)
        {
            var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
            return dbContext.Sentinel
                .Include(t => t.DeviceInfo).ThenInclude(t => t.DeviceType).Include(t => t.DeviceInfo.Organization)
                .Include(t => t.BulletboxCamera).ThenInclude(t => t.Camera)//ThenInclude(t => t.Encoder).ThenInclude(t => t.EncoderType).
                .Include(t => t.FrontCamera).ThenInclude(t => t.Camera)
                .Include(t => t.SentinelVideos).ThenInclude(t => t.Camera)
                .Include(t => t.SentinelSetting)
                .Include(t => t.DefenseDevices).ThenInclude(t => t.DeviceInfo).ThenInclude(t => t.DeviceType) //防区设备
                .Include(t => t.DefenseDevices).ThenInclude(t => t.DefenseDirection) //防区设备
                .Include(t => t.AlarmOutputChannels).ThenInclude(t => t.ChannelType) //报警输出通道
                .Where(t =>!deleteStatusId.Equals(t.DeviceInfo.StatusId));
                //.Where(t => t.DeviceInfo.StatusId == null || !t.DeviceInfo.StatusId.Equals(deleteStatusId));
            //.Include(t => t.AudioFile);
        }

        #region 哨位地图模板接口定义
        [HttpPost]
        [Route("~/Resources/Sentinel/Layout")]
        public IActionResult AddSentinelLayout([FromBody] SentinelLayout layout)
        {
            if (layout == null)
                return BadRequest("Sentinel object can not be null!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.SentinelLayout.Add(layout);
                    db.SaveChanges();
                    //  return CreatedAtAction("GetById", new { id = sentinel.SentinelId }, sentinel);
                    return CreatedAtAction("", layout);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加哨位台布局异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加哨位台布局异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        [Route("~/Resources/Sentinel/Layout")]
        public IActionResult UpdateSentinelLayout([FromBody] SentinelLayout layout)
        {
            if (layout == null)
                return BadRequest("Sentinel object can not be null!");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.SentinelLayout.Update(layout);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("更新哨位台异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新哨位台布局异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpDelete]
        [Route("~/Resources/Sentinel/Layout/id={id}")]
        public IActionResult DeleteSentinelLayout(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                SentinelLayout deleteObj = db.SentinelLayout.FirstOrDefault(t => t.SentinelLayoutId.Equals(id));
                if (deleteObj == null)
                    return NotFound();
                db.SentinelLayout.Remove(deleteObj);
                db.SaveChanges();
            }
            return NoContent();
        }

        /// <summary>
        /// 获取所有哨位地图模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/Sentinel/Layout")]
        public IEnumerable<SentinelLayout> GetAllSentinelLayout()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.SentinelLayout.ToList();
            }
        }

        /// <summary>
        /// 设备同步通知
        /// </summary>
        /// <param name="device"></param>
        /// <param name="state">0:删除，1：更新： 2：新增</param>
        private void SendDatachangeNotify(IPDeviceInfo device, int state)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var deviceOrg = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(device.OrganizationId));
                var service = db.ServiceInfo.Include(t => t.ServerInfo).FirstOrDefault(t => t.ServiceTypeId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD11300206"))
                        && (t.ServerInfo.OrganizationId.Equals(deviceOrg.ParentOrganizationId) || t.ServerInfo.OrganizationId.Equals(device.OrganizationId)));
                if (service != null)
                {
                    var deviceCode = Int32.Parse(deviceOrg.OrganizationCode);
                    PAPS.Data.DeviceInfoChange changeInfo = new PAPS.Data.DeviceInfoChange() {
                        DeviceCode = deviceCode, //device.IPDeviceCode,  //2016-12-23改为用哨位节点编号
                        DeviceId = device.IPDeviceInfoId,
                        DevicetypeId =  Guid.Parse("a0002016-e009-b019-e001-abcd11000004"),//device.DeviceTypeId, //不发哨位子类型
                        Operater = state
                    };
                    try
                    {
                        new ASCSApi(service).NotifyDeviceInfoChange(changeInfo);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("设备变更通知异常{0}\r\rStackTrace:{1}", ex.Message, ex.StackTrace);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 删除哨位终端数据
        /// </summary>
        /// <param name="fp"></param>
        /// <param name="db"></param>
        private void DeleteSentinelFingerInfo(AllInOneContext.AllInOneContext db, Sentinel sen)
        {
            //移除已下发到位台的指纹
            var sentinelFingers = db.Set<SentinelFingerPrintMapping>().Where(t => t.SentinelId.Equals(sen.SentinelId)).ToList();
            if (sentinelFingers != null && sentinelFingers.Count > 0)
            {
                Guid ascsServerType = Guid.Parse("A0002016-E009-B019-E001-ABCD11300206");
                var service = db.ServiceInfo.Include(t => t.ServerInfo).
                    FirstOrDefault(t => t.ServiceTypeId.Equals(ascsServerType) && t.ServerInfo.OrganizationId.Equals(sen.DeviceInfo.OrganizationId));

                if (service != null)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            _logger.LogInformation("清除哨位指纹 Begin...");
                            ASCSApi ascs = new ASCSApi(service);
                            var sentinelCode = Int32.Parse(sen.DeviceInfo.Organization.OrganizationCode);
                            var r = ascs.CleanFinger(sentinelCode, 0);
                            db.Set<SentinelFingerPrintMapping>().RemoveRange(sentinelFingers);
                            _logger.LogInformation("清除哨位指纹End,结果：{0}...", r.Success);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("清除哨位指纹结果异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
                        }
                    });
                }
            }
        }

        ///// <summary>
        ///// 获取可用的哨位编号 
        ///// </summary>
        ///// <param name="db"></param>
        ///// <param name="sentinelNodeId">哨位节点id</param>
        ///// <returns></returns>
        //private int NewSentinelNum(AllInOneContext.AllInOneContext db, Guid sentinelNodeId)
        //{
        //    //自定义分配哨位编号...1-32
        //    var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
        //    var organizationId = db.Organization.FirstOrDefault(f => f.OrganizationId.Equals(sentinelNodeId)).ParentOrganizationId;
        //    var sentinelNums = db.IPDeviceInfo.Include(t=>t.Organization).Where(t=>!deleteStatusId.Equals(t.StatusId)
        //        && t.Organization.ParentOrganizationId.Equals(organizationId)).
        //        OrderBy(t => t.IPDeviceCode).Select(t => t.IPDeviceCode).ToList();
        //    for (int i = 1; i <= 32; i++)
        //    {
        //        if (sentinelNums.Contains(i))
        //            continue;
        //        else
        //            return i;
        //    }
        //    return -1;
        //}

        /// <summary>
        /// 获取可用的防区设备编号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sentinelNodeId">哨位节点id</param>
        /// <returns></returns>
        private int NewDefenceNum(AllInOneContext.AllInOneContext db, Guid sentinelNodeId)
        {
            //自定义分配哨位编号..1000
            var deleteStatusId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005");
            var organizationid = db.Organization.FirstOrDefault(f => f.OrganizationId.Equals(sentinelNodeId)).ParentOrganizationId;
            var defenceDevceiNum = db.IPDeviceInfo.Include(t => t.Organization).Where(t => (t.StatusId == null || t.StatusId.Equals(deleteStatusId))
                  && t.Organization.ParentOrganizationId.Equals(organizationid)).Select(t => t.IPDeviceCode).Max();

            if (defenceDevceiNum < 1000)
                defenceDevceiNum = 1000;
            return defenceDevceiNum;
        }
    }
}
