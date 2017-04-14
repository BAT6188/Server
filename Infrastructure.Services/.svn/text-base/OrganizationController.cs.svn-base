/**
 * 2016-12-22 zhrx 增加批量添加节点接口，哨位节点分配编号
 */
using AllInOneContext;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Services
{
    [Route("Infrastructure/[controller]")]
    /// <summary>
    /// 组织机构控制类
    /// </summary>
    public class OrganizationController : Controller
    {
        private readonly ILogger<OrganizationController> _logger;

        private static List<string> s_ArabicNo = new List<string> { "1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16",
            "17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32"};

        private static List<string> s_ChineseNo = new List<string> { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五", "十六",
            "十七", "十八", "十九", "二十", "二十一", "二十二", "二十三", "二十四", "二十五", "二十六", "二十七", "二十八", "二十九", "三十", "三十一", "三十二"  };

        public OrganizationController(ILogger<OrganizationController> logger)
        {
            _logger = logger;
        }


        public IEnumerable<Organization> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var data = db.Organization
                    .Include(t => t.OrganizationType)
                    .Include(t=>t.Center);
                return data.ToList();
            }
        }


        /// <summary>
        /// 获取组织机构
        /// </summary>
        /// <param name="isOrganizationType">是否取组织机构类型不为空</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Organization/isOrganizationType={isOrganizationType}")]
        public IEnumerable<Organization> GetAll(bool isOrganizationType)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                if (isOrganizationType)
                {
                    var data = db.Organization.Include(t=>t.OrganizationType)
                        .Include(t => t.Center).Where(p => p.OrganizationType != null);
                    return data.ToList();
                }
                else
                {
                    var data = db.Organization.Include(t=>t.Center).Where(p=>p.OrganizationType==null);
                    return data.ToList();
                }
            }
        }


        /// <summary>
        /// 根据组织机构ID获取所属组织机构
        /// </summary>
        /// <param name="id">组织机构ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                Organization data = db.Organization.First(p => p.OrganizationId.Equals(id));
                if (data == null)
                {
                    return NoContent();
                }
                return new ObjectResult(data);
            }
        }


        /// <summary>
        /// 根据组织机构名称获取直属组织机构集合
        /// </summary>
        /// <param name="organizationFullName">组织机构全称</param>
        /// <param name="include">是否包括下级</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Organization/organizationFullName={organizationFullName}&include={include}")]
        public IActionResult GetOrganizationByOrganizationFullName(string organizationFullName,bool include)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    if (include)
                    {
                        var data = db.Organization.Include(t => t.ParentOrganization)
                            .Include(t => t.Center).Where(p => p.ParentOrganization.OrganizationFullName.Equals(organizationFullName));
                        if (data.Count() == 0)
                        {
                            return NoContent();
                        }
                        return new ObjectResult(data.ToList());
                    }
                    else
                    {
                        Organization data = db.Organization.Include(t => t.ParentOrganization).First(p => p.OrganizationFullName.Equals(organizationFullName));
                        if (data == null)
                        {
                            return NoContent();
                        }
                        return new ObjectResult(data);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetOrganizationByOrganizationFullName：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 根据父组织机构名称获取直属组织机构集合
        /// </summary>
        /// <param name="organizationFullName">组织机构全称</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Organization/parentOrganizationId={parentOrganizationId}")]
        public IActionResult GetOrganizationByParentOrganizationId(Guid parentOrganizationId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.Organization.Include(t => t.ParentOrganization)
                        .Include(t => t.Center).Where(p => p.ParentOrganizationId.Equals(parentOrganizationId));
                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }
                    return new ObjectResult(data.ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetOrganizationByParentOrganizationId：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 根据父组织机构名称获取直属节点类型集合
        /// </summary>
        /// <param name="parentOrganizationId">父节点组织ID</param>
        /// <param name="typeid">类型ID</param> 
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Organization/typeCode={typeCode}&parentOrganizationId={parentOrganizationId}")]
        public IActionResult GetOrganizationByParentOrganizationIdAndType(Guid parentOrganizationId, string typeCode)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var data = db.Organization
                               .Include(t => t.ParentOrganization)
                               .Include(t=>t.OrganizationType)
                               .Include(t => t.Center)
                               .Where(p => p.ParentOrganizationId.Equals(parentOrganizationId)
                               && p.OrganizationType!=null
                               && p.OrganizationType.SystemOptionCode.Equals(typeCode));
                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }
                    return new ObjectResult(data.ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetOrganizationByParentOrganizationIdAndType：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }



        /// <summary>
        /// 新增组织机构
        /// </summary>
        /// <param name="model">组织机构实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]Organization model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    //如果是哨位节点，更新编号
                    var sentinelNodeTypes = db.SystemOption.Where(t => t.SystemOptionCode.Equals("10000002")
                    || t.SystemOptionCode.Equals("10000010")).Select(t=>t.SystemOptionId).ToList();
                    if (sentinelNodeTypes != null && model.OrganizationTypeId!= null  && sentinelNodeTypes.Contains(model.OrganizationTypeId.Value))
                    {
                        var codes = GetOrganizationCodeList(db, model.ParentOrganizationId.Value);
                        int code = -1;
                        if (model.OrganizationShortName.Contains("号"))
                        {
                            var no = model.OrganizationShortName.Substring(0, model.OrganizationShortName.IndexOf("号"));
                            if (s_ArabicNo.Contains(no))
                                code = s_ArabicNo.IndexOf(no) + 1;
                            else if (s_ChineseNo.Contains(no))
                                code = s_ChineseNo.IndexOf(no) + 1;
                        }
                        if (codes.Contains(code) || code == -1)
                            code = NewOrganizationCode(codes);
                        if (code == -1)
                            return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = "哨位节点已达到最大值" });
                        model.OrganizationCode = code.ToString(); //NewOrganizationCode(codes).ToString();
                    }
                    db.Organization.Add(model);
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
        /// 修改组织机构
        /// </summary>
        /// <param name="model">组织机构实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]Organization model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    //建立事务
                    using (var transaction = db.Database.BeginTransaction())
                    {

                        //转换一般数据
                        Organization org = db.Organization.FirstOrDefault(p => p.OrganizationId.Equals(model.OrganizationId));
                        //检查名称是否修改
                        if (!org.OrganizationShortName.Equals(model.OrganizationShortName))
                        {
                            db.Organization.Where(t => !t.OrganizationId.Equals(model.OrganizationId)).ToList().
                                ForEach(t => t.OrganizationFullName = t.OrganizationFullName.Replace(org.OrganizationFullName,
                                model.OrganizationFullName));
                        }
                        org.Description = model.Description;
                        org.OrderNo = model.OrderNo;
                        org.OrganizationCode = model.OrganizationCode;
                        org.OrganizationTypeId = model.OrganizationTypeId;
                        org.OrganizationFullName = model.OrganizationFullName;
                        org.OrganizationLevel = model.OrganizationLevel;
                        org.OrganizationShortName = model.OrganizationShortName;
                        org.ParentOrganizationId = model.ParentOrganizationId;
                        org.InServiceTypeId = model.InServiceTypeId;
                        org.OnDutyTarget = model.OnDutyTarget;
                        org.Phone = model.Phone;

                        //检测应用中心
                        if (model.Center != null)
                        {
                            ApplicationCenter center = db.ApplicationCenter.FirstOrDefault(p => p.ApplicationCenterId.Equals(model.Center.ApplicationCenterId));
                            if (center != null)
                            {
                                center.ApplicationCenterCode = model.Center.ApplicationCenterCode;
                                center.EndPointsJson = model.Center.EndPointsJson;
                                center.ParentApplicationCenterCode = model.Center.ParentApplicationCenterCode;
                                center.RegisterPassword = model.Center.RegisterPassword;
                                center.RegisterUser = model.Center.RegisterUser;
                                db.ApplicationCenter.Update(center);
                                db.SaveChanges();
                                //
                                org.CenterId = center.ApplicationCenterId;
                            }
                            else
                            {
                                //代表为新增的数据
                                db.ApplicationCenter.Add(model.Center);
                                db.SaveChanges();

                                org.CenterId = model.Center.ApplicationCenterId;
                            }
                        }

                        db.Organization.Update(org);
                        db.SaveChanges();

                        transaction.Commit();
                        return new NoContentResult();
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
        ///  根据组织机构ID删除组织机构
        /// </summary>
        /// <param name="id">组织机构ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Organization data = db.Organization.First(p => p.OrganizationId== id);
                    if (data==null)
                    {
                        return NoContent();
                    }
                    db.Organization.Remove(data);
                    db.SaveChanges();
                    return new NoContentResult();
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Delete：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 组织机构s刷新心跳
        /// </summary>
        /// <param name="heartId">组织机构ID</param>
        /// <returns></returns>
        [HttpPut("Heart")]
        [Route("~/Infrastructure/Organization/heartId={heartId}")]
        public IActionResult Heart(Guid heartId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Organization data = db.Organization.First(p => p.OrganizationId == heartId);
                    if (data == null)
                    {
                        return NoContent();
                    }


                    var org = DataCache._AllOrganizationHeart.First(p =>p.Key == heartId);
                    if (org.Key == null)
                    {
                        DataCache._AllOrganizationHeart.Add(heartId, DateTime.Now);
                    }
                    else
                    {
                        DataCache._AllOrganizationHeart[heartId] = DateTime.Now;
                    }
                    return new NoContentResult();
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Heart：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 根据组织机构名称获取组织机构状态
        /// </summary>
        /// <param name="organizationFullName">组织机构全称</param>
        /// <param name="include">是否包括下级</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Organization/fullName={fullName}&include={include}")]
        public IActionResult GetAllOrganizationByOrganizationFullName(string organizationFullName, bool include)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    if (include)
                    {
                        List<Organization> data = new List<Organization>();
                        List<Guid> online = GetNormalOrganization();
                        var query = db.Organization.Include(t => t.ParentOrganization).Where(p => p.ParentOrganization.OrganizationFullName.Equals(organizationFullName));
                        foreach (Guid guid in online)
                        {
                            Organization org = db.Organization.First(p => p.OrganizationId.Equals(guid));
                            if (org != null)
                            {
                                data.Add(org);
                            }
                        }
                        if (data.Count() == 0)
                        {
                            return NoContent();
                        }
                        return new ObjectResult(data);
                    }
                    else
                    {
                        List<Guid> online = GetNormalOrganization();
                        Organization data = null;
                        Organization org = db.Organization.First(p => p.OrganizationFullName.Equals(organizationFullName));
                        if (org == null)
                        {
                            return NoContent();
                        }
                        else
                        {
                            foreach (Guid guid in online)
                            {
                                if (guid == org.OrganizationId)
                                {
                                    data = org;
                                }
                            }
                        }
                        if (data == null)
                        {
                            return NoContent();
                        }
                        return new ObjectResult(data);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllOrganizationByOrganizationFullName：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        private List<Guid> GetNormalOrganization()
        {
            List<Guid> list = new List<Guid>();

            foreach (var data in DataCache._AllOrganizationHeart)
            {
                TimeSpan time = DateTime.Now - data.Value;
                if (time.TotalMinutes <= 10)
                {
                    list.Add(data.Key);
                }
            }
            return list;
        }

        /// <summary>
        /// 批量初始化节点
        /// 编号查找规则：1.从哨位节点名称获取，看是否找到符合规则的编号；2.判断找到的编号是否已分配；3.编号已分配，重新获取
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Infrastructure/Organization/Multi")]
        public ActionResult BatchAdd([FromBody]List<Organization> nodes)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    Organization Organization = db.Organization.First(p => p.OrganizationId == nodes[0].ParentOrganizationId);
                    var sentinelNodeTypes = db.SystemOption.Where(t => t.SystemOptionCode.Equals("10000002") 
                        || t.SystemOptionCode.Equals("10000010")).Select(t=>t.SystemOptionId).ToList(); //哨位节点 or 目标单位
                    var codes = GetOrganizationCodeList(db, Organization.OrganizationId);
                    foreach (var node in nodes)
                    {
                        if (sentinelNodeTypes != null && node.OrganizationTypeId  != null && sentinelNodeTypes.Contains(node.OrganizationTypeId.Value))
                        {
                            int code = -1;
                            if (node.OrganizationShortName.Contains("号"))
                            {
                                var no = node.OrganizationShortName.Substring(0, node.OrganizationShortName.IndexOf("号"));
                                if (s_ArabicNo.Contains(no))
                                    code = s_ArabicNo.IndexOf(no) + 1;
                                else if (s_ChineseNo.Contains(no))
                                    code = s_ChineseNo.IndexOf(no) + 1;
                            }
                            //if (codes.Contains(code) || code== -1)
                            //    code = NewOrganizationCode(codes);]
                            //常规哨位编号已分配
                            if (codes.Contains(code))
                            {
                                sb.Append(node.OrganizationShortName).Append("、");
                                continue;
                            }
                            if (code == -1)
                                code = NewOrganizationCode(codes);

                            if (code == -1)
                                continue;
                                //return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = "哨位节点已达到最大值" });
                            node.OrganizationCode = code.ToString();
                            codes.Add(code);
                        }
                        node.OrganizationFullName = Organization.OrganizationFullName + '.' + node.OrganizationShortName;
                        if (db.Organization.FirstOrDefault(t => t.OrganizationFullName.Equals(node.OrganizationFullName)) != null)
                        {
                            sb.Append(node.OrganizationShortName).Append("、");
                            continue;
                        }
                        db.Organization.Add(node);
                    }
                    //db.AddRange(nodes);
                    db.SaveChanges();
                    string message = "";
                    if (sb.Length > 0)
                    {
                        sb.Length = sb.Length - 1;
                         message =  sb.ToString() + "已添加！";
                        //return BadRequest(new ApplicationException() { ErrorCode = "数据重复", ErrorMessage = message });
                    }
                    return Ok(message);
                }
                catch (Exception ex)
                {
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
                }
            }
        }

        /// <summary>
        /// 获取可用的哨位编号
        /// </summary>
        /// <param name="existsCodes">已经添加的编号</param>
        /// <returns></returns>
        private int NewOrganizationCode(List<int> existsCodes)
        {
            for (int i = 32; i >= 1; i--)
            {
                if (existsCodes.Contains(i))
                    continue;
                else
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 获取已添加节点编号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        private List<int> GetOrganizationCodeList(AllInOneContext.AllInOneContext db,Guid organizationId)
        {
            //自定义分配哨位编号...1-32
            var sentinelNums = db.Organization.Where(t => organizationId.Equals(t.ParentOrganizationId) && !string.IsNullOrEmpty(t.OrganizationCode)).
                OrderBy(t => t.OrganizationCode).Select(t => t.OrganizationCode).ToList();

            List<int> codes = new List<int>();
            foreach (var num in sentinelNums)
            {
                int n = 0;
                if (Int32.TryParse(num, out n))
                    codes.Add(n);
            }
            return codes;
        }

        /// <summary>
        /// 根据组织机构名称获取组织机构状态
        /// </summary>
        /// <param name="organizationFullName">组织机构全称</param>
        /// <param name="include">是否包括下级</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Organization/GetRoot")]
        public IActionResult GetRootOrganization()
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Organization org = db.Organization
                                      .Include(t => t.OrganizationType)
                                      .FirstOrDefault(p => p.ParentOrganizationId == null);

                    if (org == null)
                    {
                        return NoContent();
                    }
                    return new ObjectResult(org);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 统计查勤点总数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Organization/DutyCheckPoint/Sum")]
        public int SumDutycheckPoint()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.Organization.Select(t => t.DutycheckPoints).Sum();
            }
        }
    }


}
