using AllInOneContext;
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure.Services
{
    [Route("Infrastructure/[controller]")]
    /// <summary>
    /// 人员控制类
    /// </summary>
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 获取所有人员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Staff> GetAll()
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.Staff
                               .Include(t => t.Application)
                               .Include(t => t.DegreeOfEducation)
                               .Include(t => t.DutyCheckType)
                               .Include(t => t.Fingerprints)
                               .Include(t => t.MaritalStatus)
                               .Include(t => t.Nation)
                               .Include(t => t.Organization)
                               .Include(t => t.Photo)
                               .Include(t => t.PhysiclalStatus)
                               .Include(t => t.PoliticalLandscape)
                               .Include(t => t.PositionType)
                               .Include(t => t.RankType)
                               .Include(t => t.ReignStatus)
                               .Include(t => t.Sex)
                               .Include(t => t.WorkingProperty);
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// 根据组织机构ID获取所有人员
        /// </summary>
        /// <param name="id">组织机构ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Staff/organizationId={organizationId}")]
        public IActionResult GetStaffByOrganizationID(Guid organizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.Staff
                               .Include(t => t.Application)
                               .Include(t => t.DegreeOfEducation)
                               .Include(t => t.DutyCheckType)
                               .Include(t => t.Fingerprints)
                               .Include(t => t.MaritalStatus)
                               .Include(t => t.Nation)
                               .Include(t => t.Organization)
                               .Include(t => t.Photo)
                               .Include(t => t.PhysiclalStatus)
                               .Include(t => t.PoliticalLandscape)
                               .Include(t => t.PositionType)
                               .Include(t => t.RankType)
                               .Include(t => t.ReignStatus)
                               .Include(t => t.Sex)
                               .Include(t => t.WorkingProperty)
                               .Where(p => p.Organization.OrganizationId.Equals(organizationId));
                    if (data == null)
                    {
                        return NoContent();
                    }
                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetStaffByOrganizationID：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetStaffByOrganizationID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据应用ID获取所有人员
        /// </summary>
        /// <param name="id">应用ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Staff/applicationId={applicationId}")]
        public IActionResult GetStaffByApplicationID(Guid applicationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.Staff
                               .Include(t => t.Application)
                               .Include(t => t.DegreeOfEducation)
                               .Include(t => t.DutyCheckType)
                               .Include(t => t.Fingerprints)
                               .Include(t => t.MaritalStatus)
                               .Include(t => t.Nation)
                               .Include(t => t.Organization)
                               .Include(t => t.Photo)
                               .Include(t => t.PhysiclalStatus)
                               .Include(t => t.PoliticalLandscape)
                               .Include(t => t.PositionType)
                               .Include(t => t.RankType)
                               .Include(t => t.ReignStatus)
                               .Include(t => t.Sex)
                               .Include(t => t.WorkingProperty)
                               .Where(p => p.Application.ApplicationId.Equals(applicationId));
                    if (data == null)
                    {
                        return NoContent();
                    }

                    return new ObjectResult(data.ToList());
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("GetStaffByApplicationID：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("GetStaffByApplicationID：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据人员ID获取信息
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Staff data = db.Staff.Include(t => t.Fingerprints).FirstOrDefault(p => p.StaffId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }

                return new ObjectResult(data);
            }
        }



        /// <summary>
        /// 新增人员
        /// </summary>
        /// <param name="model">人员实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]Staff model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    //自动分配人员编号
                    var staffs = db.Staff
                                 .Where(p=>p.OrganizationId.Equals(model.OrganizationId))
                                 .OrderByDescending(p => p.StaffCode);      ;
                    if (staffs != null && staffs.ToList().Count > 0)
                    {
                        Staff Staff = staffs.ToList()[0];
                        model.StaffCode = Staff.StaffCode + 1;
                    }
                    else
                    {
                        model.StaffCode = 1;
                    }
                    //
                    if (model.Photo != null) //Entity Framework的默认值BUG暂时有BUG，临时这样处理
                    {
                        model.Photo.Modified = DateTime.Now;
                    }
                    db.Staff.Add(model);
                    db.SaveChanges();
                    return Created("", "OK");
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("Add：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Add：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 修改人员
        /// </summary>
        /// <param name="model">人员实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]Staff model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    ////同一个组织机构下人员编号不能重复
                    //var Staff = db.Staff
                    //          .FirstOrDefault(p => p.OrganizationId.Equals(model.OrganizationId)
                    //          && p.StaffCode.Equals(model.StaffCode)
                    //          && !p.StaffId.Equals(model.StaffId));
                    //if (Staff != null)
                    //{
                    //    return BadRequest(new ApplicationException { ErrorCode = "Staff-001", ErrorMessage = "同一个组织机构下人员编号不能重复" });
                    //}

                    using (var tran = db.Database.BeginTransaction())
                    {
                        var copyStaff = db.Staff.Include(t => t.Photo).FirstOrDefault(t => t.StaffId.Equals(model.StaffId));
                        try
                        {
                            if (model.Photo != null)//Entity Framework的默认值BUG暂时有BUG，临时这样处理
                            {
                                model.Photo.Modified = DateTime.Now;
                            }
                            //避免指令数据丢失，将更新的数据拷贝
                            copyStaff.OrganizationId = model.OrganizationId;
                            copyStaff.PositionTypeId = model.PositionTypeId;
                            copyStaff.RankTypeId = model.RankTypeId;
                            copyStaff.SexId = model.SexId;
                            copyStaff.StaffName = model.StaffName;
                            copyStaff.StaffCode = model.StaffCode;
                            copyStaff.ClassRow = model.ClassRow;
                            copyStaff.DateOfBirth = model.DateOfBirth;
                            copyStaff.DegreeOfEducationId = model.DegreeOfEducationId;
                            copyStaff.Description = model.Description;
                            copyStaff.DutyCheckTypeId = model.DutyCheckTypeId;
                            copyStaff.EnrolAddress = model.EnrolAddress;
                            copyStaff.EnrolTime = model.EnrolTime;
                            copyStaff.FamilyPhone = model.FamilyPhone;
                            copyStaff.MaritalStatusId = model.MaritalStatusId;
                            copyStaff.NationId = model.NationId;
                            copyStaff.NativePlace = model.NativePlace;
                            copyStaff.PartyTime = model.PartyTime;
                            copyStaff.Phone = model.Phone;
                            copyStaff.Stature = model.Stature;

                            if (model.Photo != null)
                            {
                                if (copyStaff.Photo != null)
                                {
                                    db.Set<UserPhoto>().Remove(copyStaff.Photo);
                                    db.SaveChanges();
                                }
                                copyStaff.PhotoId = model.PhotoId;
                                copyStaff.Photo = model.Photo;
                            }
                            copyStaff.PhysiclalStatusId = model.PhysiclalStatusId;
                            copyStaff.PoliticalLandscapeId = model.PoliticalLandscapeId;
                            copyStaff.PositionTypeId = model.PositionTypeId;
                            copyStaff.PostalZipCode = model.PostalZipCode;
                            copyStaff.RankTypeId = model.RankTypeId;
                            copyStaff.ReignStatusId = model.ReignStatusId;
                            copyStaff.ReligiousBelief = model.ReligiousBelief;
                            copyStaff.WorkingPropertyId = model.WorkingPropertyId;

                            db.Staff.Update(copyStaff);
                            db.SaveChanges();
                            tran.Commit();
                            return new NoContentResult();
                        }
                        catch (DbUpdateException dbEx)
                        {
                            tran.Rollback();
                            return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                        }
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("Update：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);

                return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Update：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        ///  根据人员ID删除人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        Staff data = db.Staff.Include(t => t.Fingerprints).FirstOrDefault(p => p.StaffId == id);
                        if (data == null)
                        {
                            return NoContent();
                        }
                        if (data.Fingerprints != null)
                        {
                            //删除人员已下发的指纹
                            foreach (var fp in data.Fingerprints)
                            {
                                DeleteFingerDispatch(fp, db);
                                db.Set<Fingerprint>().Remove(fp);
                            }
                        }
                        db.Staff.Remove(data);
                        db.SaveChanges();
                        tran.Commit();
                        return new NoContentResult();
                    }
                    catch (DbUpdateException dbEx)
                    {
                        tran.Rollback();
                        return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                    }
                    catch (System.Exception ex)
                    {
                        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                    }
                }
            }
        }

        /// <summary>
        /// 模糊搜索
        /// </summary>
        /// <param name="staffName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Staff/staffName={staffName}")]
        public IActionResult FuzzySearchByStaffName(string staffName)
        {
            if (staffName == "" || staffName.Length == 0)
                return BadRequest();

            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data=db.Staff
                               .Include(t => t.Application)
                               .Include(t => t.DegreeOfEducation)
                               .Include(t => t.DutyCheckType)
                               .Include(t => t.Fingerprints)
                               .Include(t => t.MaritalStatus)
                               .Include(t => t.Nation)
                               .Include(t => t.Organization)
                               .Include(t => t.Photo)
                               .Include(t => t.PhysiclalStatus)
                               .Include(t => t.PoliticalLandscape)
                               .Include(t => t.PositionType)
                               .Include(t => t.RankType)
                               .Include(t => t.ReignStatus)
                               .Include(t => t.Sex)
                               .Include(t => t.WorkingProperty)
                               .Where(p => p.StaffName.Contains(staffName));
                if (data ==null || data.Count()==0)
                {
                    return NoContent();
                }
                return new ObjectResult(data.ToList());
            }
        }

        #region 指纹增删
        /// <summary>
        /// 新增指纹
        /// </summary>
        /// <param name="fingerPrints"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Infrastructure/Staff/fingerprint/staffId={staffId}")]
        public IActionResult SaveFingerprint(Guid staffId, [FromBody]List<Fingerprint> fingerPrints)
        {
            if (fingerPrints == null || fingerPrints.Count == 0)
                return BadRequest("人员指纹数据不能为空！");

            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        //从db获取人员已配置的指纹
                        var Staff = db.Staff.Include(t => t.Fingerprints).FirstOrDefault(t => t.StaffId.Equals(staffId));   //db.Fingerprint.Where(t => t.StaffId.Equals(fingerPrints.First().StaffId)).ToList();
                        if (Staff != null)
                        {
                            var dbFingerPrintSet = Staff.Fingerprints;
                            if (dbFingerPrintSet != null)
                            {
                                var dataFingerPrintIds = fingerPrints.Select(t => t.FingerprintId).ToList();//客户端发送过来的指纹

                                //获取删除的指纹，并删除
                                var willDeleteFingerPrints = dbFingerPrintSet.Where(t => !dataFingerPrintIds.Contains(t.FingerprintId)).ToList();
                                foreach (var fp in willDeleteFingerPrints)
                                {
                                    DeleteFingerDispatch(fp, db);
                                }

                                if (dbFingerPrintSet.Count > 0)
                                    db.Fingerprint.RemoveRange(dbFingerPrintSet);
                            }
                            db.SaveChanges();

                            //指纹编号由后台分配
                            int fingerprintNo = db.Fingerprint.Max(t => t.FingerprintNo);
                            fingerPrints.ForEach(t => {
                                if (t.FingerprintNo == 0)
                                    t.FingerprintNo = ++fingerprintNo;
                                t.StaffId = staffId;
                            });
                            db.Fingerprint.AddRange(fingerPrints);
                            db.SaveChanges();
                            tran.Commit();
                            return Created("", "OK");
                        }
                        else
                        {
                            return BadRequest(new ApplicationException { ErrorCode = "数据错误", ErrorMessage = "人员id不能为空" });
                        }
                    }
                    catch (DbUpdateException dbEx)
                    {
                        tran.Rollback();
                        return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                    }
                }
            }
        }

        ///// <summary>
        ///// 更新指纹
        ///// </summary>
        ///// <param name="figure"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("~/Infrastructure/Staff/fingerprint")]
        //public IActionResult UpdateFingerprint([FromBody]Fingerprint figure)
        //{
        //    try
        //    {
        //        using (var db = new AllInOneContext.AllInOneContext())
        //        {
        //            db.Set<Fingerprint>().Update(figure);
        //            db.SaveChanges();
        //            return new NoContentResult();
        //        }
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
        //    }
        //}

        ///// <summary>
        ///// 删除指纹
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete]
        //[Route("~/Infrastructure/Staff/fingerprint/id={id}")]
        //public IActionResult DeleteFingerprint(Guid id)
        //{
        //    try
        //    {
        //        using (var db = new AllInOneContext.AllInOneContext())
        //        {
        //            var fp = db.Set<Fingerprint>().FirstOrDefault(t => t.FingerprintId.Equals(id));
        //            if (fp != null)
        //            {
        //                //查找关联的哨位，删除下发记录
        //                DeleteFingerDispatch(fp, db);

        //                //删除数据库中记录
        //                db.Set<Fingerprint>().Remove(fp);
        //                db.SaveChanges();
        //                return new NoContentResult();
        //            }
        //            else
        //                return NotFound();
        //        }
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
        //    }
        //}

        /// <summary>
        /// 删除哨位终端数据
        /// </summary>
        /// <param name="fp"></param>
        /// <param name="db"></param>
        private void DeleteFingerDispatch(Fingerprint fp, AllInOneContext.AllInOneContext db)
        {
            //var sentinels = db.Sentinel.Include(t => t.DeviceInfo).Include(t => t.FingerDispatch).ToList().Where(t => t.FingerDispatch != null
            //           && t.FingerDispatch.Exists(f => f.FingerprintId.Equals(fp.FingerprintId))).ToList();
            //foreach (var sen in sentinels)
            //{
            //    //调用哨位所在
            //    Guid ascsServerType = Guid.Parse("A0002016-E009-B019-E001-ABCD11300206");
            //    var service = db.ServiceInfo.Include(t => t.ServerInfo).
            //        FirstOrDefault(t => t.ServiceTypeId.Equals(ascsServerType) && t.ServerInfo.OrganizationId.Equals(sen.DeviceInfo.OrganizationId));

            //    if (service != null)
            //    {
            //        try
            //        {
            //            ASCSApi ascs = new ASCSApi(service);
            //            var r = ascs.CleanFinger(sen.DeviceInfo.IPDeviceCode, fp.FingerprintNo);
            //            //移除已下发到位台的指纹
            //            db.Set<SentinelFingerPrintMapping>().RemoveRange(
            //                db.Set<SentinelFingerPrintMapping>().Where(t => t.FingerprintId.Equals(fp.FingerprintId)));
            //            _logger.LogInformation("删除下发到哨位的指纹结果：{0}...", r.Success);
            //        }
            //        catch (Exception ex)
            //        {
            //            _logger.LogError("删除下发到哨位的指纹异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
            //        }
            //        return;
            //    }
            //}

            //获取下发了该指纹的哨位台
            var sentinels = db.SentinelFingerPrintMapping.Include(t => t.Sentinel).ThenInclude(t => t.DeviceInfo)
                .ThenInclude(t=>t.Organization)
                .Where(t => t.FingerprintId.Equals(fp.FingerprintId)).ToList().Select(t => t.Sentinel).ToList();
            foreach (var sen in sentinels)
            {
                Guid ascsServerType = Guid.Parse("A0002016-E009-B019-E001-ABCD11300206");
                var deviceOrg = db.Organization.FirstOrDefault(t => t.OrganizationId.Equals(sen.DeviceInfo.OrganizationId));
                var service = db.ServiceInfo.Include(t => t.ServerInfo).
                    FirstOrDefault(t => t.ServiceTypeId.Equals(ascsServerType) && (t.ServerInfo.OrganizationId.Equals(deviceOrg.ParentOrganizationId) ||
                        t.ServerInfo.OrganizationId.Equals(deviceOrg.OrganizationId)));

                if (service != null)
                {
                    try
                    {
                        ASCSApi ascs = new ASCSApi(service);
                        //var sentinelCode = Int32.Parse(sen.DeviceInfo.Organization.OrganizationCode);
                        //var r = ascs.CleanFinger(sentinelCode, fp.FingerprintNo);
                        var r = ascs.CleanFinger(sen.DeviceInfo.IPDeviceCode, fp.FingerprintNo);
                        //移除已下发到位台的指纹
                        db.SentinelFingerPrintMapping.RemoveRange(
                            db.SentinelFingerPrintMapping.Where(t => t.FingerprintId.Equals(fp.FingerprintId)).ToList());
                        _logger.LogInformation("删除下发到哨位的指纹结果：{0}...", r.Success);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("删除下发到哨位的指纹异常：{0}\r\n{1}", ex.Message, ex.StackTrace);
                    }
                    return;
                }
            }
        }
        #endregion

    }
}
