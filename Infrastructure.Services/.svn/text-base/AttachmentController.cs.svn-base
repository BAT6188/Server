using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    [Route("Infrastructure/[controller]")]
    /// <summary>
    /// 附件程序控制类
    /// </summary>
    public class AttachmentController : Controller
    {
        private readonly ILogger<AttachmentController> _logger;
        public AttachmentController(ILogger<AttachmentController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 根据参数获取附件集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="type">附件类型</param>
        /// <param name="userId">修改人ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Infrastructure/Attachment")]
        public IActionResult GetAttachmentByParameter(DateTime startTime, DateTime endTime, int currentPage,
            int pageSize, int type, Guid userId)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    var query = from p in db.Attachment
                                .Include(t => t.ModifiedBy)
                                orderby p.Modified descending
                                where p.Modified >= startTime && p.Modified <= endTime
                                && p.ModifiedBy.UserId == userId && p.AttachmentType == type
                                select p;
                    if (currentPage == 0)
                        currentPage = 1;
                    if (pageSize <= 0)
                        pageSize = 10;

                    var data = query.Skip(pageSize * (currentPage - 1)).Take(pageSize * currentPage).ToList();

                    if (data.Count() == 0)
                    {
                        return NoContent();
                    }
                    return new ObjectResult(data.ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAttachmentByParameter：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 根据附件ID获取附件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Attachment data = db.Attachment.Include(t => t.ModifiedBy).FirstOrDefault(p => p.AttachmentId.Equals(id));
                    if (data == null)
                    {
                        return NoContent();
                    }
                    return new ObjectResult(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
            }
        }


        /// <summary>
        /// 新增附件
        /// </summary>
        /// <param name="model">附件实体</param>
        /// <returns>返回值</returns>
        [HttpPost]
        public IActionResult Add([FromBody]Attachment model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Attachment.Add(model);
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
        /// 修改附件
        /// </summary>
        /// <param name="model">附件实体</param>
        /// <returns>返回值</returns>
        [HttpPut]
        public IActionResult Update([FromBody]Attachment model)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    db.Attachment.Update(model);
                    db.SaveChanges();
                    return new NoContentResult();
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
        ///  根据附件ID删除附件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <returns>返回值</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var db = new AllInOneContext.AllInOneContext())
                {
                    Attachment data = db.Attachment.FirstOrDefault(p => p.AttachmentId == id);
                    if (data == null)
                    {
                        return NoContent();
                    }
                    db.Attachment.Remove(data);
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
        /// 文件上传，暂不验证文件格式
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [Route("~/Infrastructure/Attachment/File")]
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] Attachment attach)
        {
            try
            {
                if (attach == null)
                {
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = "attach can not be null!" });
                }
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("处理上传截图 Begin");

                    if (attach.File != null && attach.File.Length > 0)//以FormFile形式上传
                    {
                        var File = attach.File;
                        var fileName = ContentDispositionHeaderValue.Parse(File.ContentDisposition).FileName.Trim('"');
                        var appFileName = Process.GetCurrentProcess().MainModule.FileName;
                        var uploadFolder = Path.GetDirectoryName(appFileName);
                        uploadFolder = Path.Combine(uploadFolder, "attach");
                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);
                        await File.SaveAsAsync(Path.Combine(uploadFolder, fileName));
                        using (var db = new AllInOneContext.AllInOneContext())
                        {
                            //如果截图记录不存在则更新
                            var dbAtt = db.Attachment.FirstOrDefault(t => t.AttachmentId.Equals(attach.AttachmentId));
                            if (attach.AttachmentId.Equals(Guid.Empty))
                                attach.AttachmentId = Guid.NewGuid();

                            if (dbAtt != null)
                            {
                                dbAtt.AttachmentPath = "/attach/";
                                dbAtt.ContentType = File.ContentType;
                                dbAtt.AttachmentName = fileName;
                                dbAtt.Modified = DateTime.Now;
                                dbAtt.ModifiedBy = db.User.FirstOrDefault();
                                db.Attachment.Update(dbAtt);  //打卡的情况，截图记录先保存后刷新
                            }
                            else
                            {
                                attach.AttachmentPath = "/attach/";
                                attach.ContentType = File.ContentType;
                                attach.AttachmentName = fileName;
                                attach.Modified = DateTime.Now;
                                attach.ModifiedBy = db.User.FirstOrDefault();
                                db.Attachment.Add(attach);
                            }
                            db.SaveChanges();
                            _logger.LogInformation("处理上传截图 End");
                        }
                    }
                    else if (attach.FileData != null && attach.FileData.Length > 0)//以字节流形式上传
                    {
                        var appFileName = Process.GetCurrentProcess().MainModule.FileName;
                        var uploadFolder = Path.GetDirectoryName(appFileName);
                        uploadFolder = Path.Combine(uploadFolder, "attach");
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }
                        System.IO.File.WriteAllBytes(Path.Combine(uploadFolder, attach.AttachmentName), attach.FileData);

                        using (var db = new AllInOneContext.AllInOneContext())
                        {
                            //如果截图记录不存在则更新
                            var dbAtt = db.Attachment.FirstOrDefault(t => t.AttachmentId.Equals(attach.AttachmentId));
                            if (attach.AttachmentId.Equals(Guid.Empty))
                                attach.AttachmentId = Guid.NewGuid();

                            if (dbAtt != null)
                            {
                                dbAtt.AttachmentPath = "/attach/";
                                dbAtt.ContentType = attach.ContentType;
                                dbAtt.AttachmentName = attach.AttachmentName;
                                dbAtt.Modified = DateTime.Now;
                                dbAtt.ModifiedBy = db.User.FirstOrDefault();
                                db.Attachment.Update(dbAtt);  //打卡的情况，截图记录先保存后刷新
                            }
                            else
                            {
                                attach.AttachmentPath = "/attach/";
                                attach.ContentType = attach.ContentType;
                                attach.AttachmentName = attach.AttachmentName;
                                attach.Modified = DateTime.Now;
                                attach.ModifiedBy = db.User.FirstOrDefault();
                                db.Attachment.Add(attach);
                            }
                            db.SaveChanges();
                            _logger.LogInformation("处理上传截图 End");
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 文件上传，以字节数组形式上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [Route("~/Infrastructure/Attachment/FileByByte")]
        [HttpPost]
        public async Task<IActionResult> UploadFileByByteArray([FromBody] Attachment attach)
        {
            try
            {
                if (attach == null)
                {
                    return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = "attach can not be null!" });
                }
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("处理上传截图 Begin");

                    if (attach.File != null && attach.File.Length > 0)//以FormFile形式上传
                    {
                        var File = attach.File;
                        var fileName = ContentDispositionHeaderValue.Parse(File.ContentDisposition).FileName.Trim('"');
                        var appFileName = Process.GetCurrentProcess().MainModule.FileName;
                        var uploadFolder = Path.GetDirectoryName(appFileName);
                        uploadFolder = Path.Combine(uploadFolder, "attach");
                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);
                        await File.SaveAsAsync(Path.Combine(uploadFolder, fileName));
                        using (var db = new AllInOneContext.AllInOneContext())
                        {
                            //如果截图记录不存在则更新
                            var dbAtt = db.Attachment.FirstOrDefault(t => t.AttachmentId.Equals(attach.AttachmentId));
                            if (attach.AttachmentId.Equals(Guid.Empty))
                                attach.AttachmentId = Guid.NewGuid();

                            if (dbAtt != null)
                            {
                                dbAtt.AttachmentPath = "/attach/";
                                dbAtt.ContentType = File.ContentType;
                                dbAtt.AttachmentName = fileName;
                                dbAtt.Modified = DateTime.Now;
                                dbAtt.ModifiedBy = db.User.FirstOrDefault();
                                db.Attachment.Update(dbAtt);  //打卡的情况，截图记录先保存后刷新
                            }
                            else
                            {
                                attach.AttachmentPath = "/attach/";
                                attach.ContentType = File.ContentType;
                                attach.AttachmentName = fileName;
                                attach.Modified = DateTime.Now;
                                attach.ModifiedBy = db.User.FirstOrDefault();
                                db.Attachment.Add(attach);
                            }
                            db.SaveChanges();
                            _logger.LogInformation("处理上传截图 End");
                        }
                    }
                    else if (attach.FileData != null && attach.FileData.Length > 0)//以字节流形式上传
                    {
                        var appFileName = Process.GetCurrentProcess().MainModule.FileName;
                        var uploadFolder = Path.GetDirectoryName(appFileName);
                        uploadFolder = Path.Combine(uploadFolder, "attach");
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }
                        System.IO.File.WriteAllBytes(Path.Combine(uploadFolder, attach.AttachmentName), attach.FileData);

                        using (var db = new AllInOneContext.AllInOneContext())
                        {
                            //如果截图记录不存在则更新
                            var dbAtt = db.Attachment.FirstOrDefault(t => t.AttachmentId.Equals(attach.AttachmentId));
                            if (attach.AttachmentId.Equals(Guid.Empty))
                                attach.AttachmentId = Guid.NewGuid();

                            if (dbAtt != null)
                            {
                                dbAtt.AttachmentPath = "/attach/";
                                dbAtt.ContentType = attach.ContentType;
                                dbAtt.AttachmentName = attach.AttachmentName;
                                dbAtt.Modified = DateTime.Now;
                                dbAtt.ModifiedBy = db.User.FirstOrDefault();
                                db.Attachment.Update(dbAtt);  //打卡的情况，截图记录先保存后刷新
                            }
                            else
                            {
                                attach.AttachmentPath = "/attach/";
                                attach.ContentType = attach.ContentType;
                                attach.AttachmentName = attach.AttachmentName;
                                attach.Modified = DateTime.Now;
                                attach.ModifiedBy = db.User.FirstOrDefault();
                                db.Attachment.Add(attach);
                            }
                            db.SaveChanges();
                            _logger.LogInformation("处理上传截图 End");
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApplicationException() { ErrorCode = "Unknow", ErrorMessage = ex.Message });
            }
        }


        [Route("~/Infrastructure/Attachment/File/id={id}")]
        [HttpGet]
        public FileStreamResult Download(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var attach = db.Attachment.FirstOrDefault(t => t.AttachmentId.Equals(id));
                if (attach != null)
                {
                    var appFileName = Process.GetCurrentProcess().MainModule.FileName;
                    var uploadFolder = Path.GetDirectoryName(appFileName);
                    uploadFolder = Path.Combine(uploadFolder, "attach");
                    var fileName = Path.Combine(uploadFolder, attach.AttachmentName);
                    var stream = new FileStream(fileName, FileMode.Open);
                    return File(stream, attach.ContentType, attach.AttachmentName);
                }
            }
            return null;
        }
    }
}