using AllInOneContext;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Infrastructure.Services
{
    [Route("Infrastructure/[controller]")]
    /// <summary>
    /// 授权信息控制类
    /// </summary>
    public class AuthorizationInformationController : Controller
    {

        /// <summary>
        /// 获取所有授权信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AuthorizationInformation> GetAll()
        {
            var list = DataCache._AuthorizationInformations;

            return list;
        }

        /// <summary>
        /// 根据公司名称获取授权信息
        /// </summary>
        /// <param name="id">公司名称</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetAuthorizationInformation(string id)
        {
            var list = DataCache._AuthorizationInformations;

            return Ok(list);
        }



    }
}
