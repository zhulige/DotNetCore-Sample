using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Restful_API_Sample.ApiControllers.v1
{
    /// <summary>
    /// 样板API
    /// </summary>
    [Route("api/v1/[controller]")]
    public class SampleController : Controller
    {
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <remarks>
        /// Note that the key is a GUID and not an integer.
        ///  
        ///     POST /Login
        ///     {
        ///        "UserName": "用户名",
        ///        "Password": "密码"
        ///     }
        /// 
        /// </remarks>
        /// <param name="login">Username:"用户名"<br/>Password:"密码"</param>
        /// <returns></returns>
        /// <response code="200">用户名</response>
        /// <response code="400">用户名密码不能为空！</response>
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Login), 400)]
        [HttpPost, Route("Login")]
        public IActionResult Login([FromBody]Login login)
        {
            if (login.UserName == null || login.Password == null)
            {
                return BadRequest();
            }
            //return CreatedAtRoute("Login", new { id = item.Key }, item);
            return Ok(login.UserName);
        }

        //// GET: api/Sample
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Sample/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        //// POST: api/Sample
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}
        
        //// PUT: api/Sample/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        
        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }

    /// <summary>
    /// 用户登陆实体
    /// </summary>
    public class Login
    {
        /// <summary>
        /// 用户名
        /// </summary>
        //[Required]  //此配置可以约束(Swagger )传进来的参数值 不能为空
        public string UserName;

        /// <summary>
        /// 密码
        /// </summary>
        //[Required]  //此配置可以约束(Swagger )传进来的参数值 不能为空
        public string Password;
    }

}
