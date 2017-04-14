using AllInOneContext;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*********************************************************
 * 模板元素要排序，否则如果客户端设置模板时没有排序，则播放控件顺序不对
 * ******************************************************/
namespace Resources.Services
{
    [Route("Resources/[controller]")]
    public class TemplateLayoutController : Controller
    {
        ILogger<TemplateLayoutController> _logger;

        public TemplateLayoutController(ILogger<TemplateLayoutController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] TemplateLayout templateLayout)
        {
            if (templateLayout == null)
                return BadRequest("TemplateLayout object can not null!");
            if (ExistsName(templateLayout.TemplateLayoutName, templateLayout.TemplateLayoutId))
                return BadRequest("TemplateLayout name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.TemplateLayout.Add(templateLayout);
                    db.SaveChanges();
                    return CreatedAtAction("", templateLayout);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加模板异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加模板异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] TemplateLayout templateLayout)
        {
            if (templateLayout == null)
                return BadRequest("TemplateLayout object can not null!");
            if (ExistsName(templateLayout.TemplateLayoutName, templateLayout.TemplateLayoutId))
                return BadRequest("TemplateLayout name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.TemplateLayout.Update(templateLayout);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("更新模板异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("更新模板异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                TemplateLayout deleteObj = db.TemplateLayout.FirstOrDefault(t => t.TemplateLayoutId.Equals(id));
                if (deleteObj == null)
                    return NotFound();
                db.TemplateLayout.Remove(deleteObj);
                db.SaveChanges();
                return NoContent();
            }
        }

        /// <summary>
        /// 根据类型获取模板
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/TemplateLayout/systemOptionId={systemOptionId}")]
        public IEnumerable<TemplateLayout> GetByTemplateType(Guid systemOptionId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var templates = GetDbQuery(db).Where(t => t.TemplateTypeId.Equals(systemOptionId)).ToList();
                templates.ForEach(t => t.Cells = (from c in t.Cells ?? new List<TemplateCell>() orderby c.Row, c.Column select c).ToList());
                return templates;
            }
        }

        //  [HttpGet("{id}", Name = "GetById")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                TemplateLayout tl = GetDbQuery(db).FirstOrDefault(t => t.TemplateLayoutId.Equals(id));
                if (tl == null)
                    return NotFound();
                tl.Cells = (from c in (tl.Cells ?? new List<TemplateCell>()) orderby c.Row, c.Column select c).ToList();
                return new ObjectResult(tl);
            }
        }

        [HttpGet]
        public IEnumerable<TemplateLayout> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var templates = GetDbQuery(db).ToList();
                templates.ForEach(t => t.Cells = (from c in (t.Cells ?? new List<TemplateCell>()) orderby c.Row, c.Column select c).ToList());
                return templates;
            }
        }

        private IQueryable<TemplateLayout> GetDbQuery(AllInOneContext.AllInOneContext dbContext)
        {
            return dbContext.TemplateLayout.Include(t => t.TemplateType).Include(t => t.Cells);
        }

        //private bool Exists(Guid TemplateLayoutid)
        //{
        //    //return _cacheTemplateLayoutList.Exists(d => d.TemplateLayoutId.ToString().Equals(TemplateLayoutid));
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        return db.TemplateLayout.Where(d => d.TemplateLayoutId.ToString().Equals(TemplateLayoutid)).Count() > 0;
        //    }
        //}

        /// <summary>
        /// 判断模板名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="templateLayoutId"></param>
        /// <returns></returns>
        public bool ExistsName(string name, Guid templateLayoutId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.TemplateLayout.Where(t => t.TemplateLayoutName.Equals(name)
                    && !t.TemplateLayoutId.Equals(templateLayoutId)).Count() > 0;
            }
        }
    }
}
