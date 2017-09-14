using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Restful_API_Sample.V2.Models;
using System;

namespace Restful_API_Sample.V2.Controllers
{
    /// <summary>
    /// 样板API
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
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
        /// <returns>Token</returns>
        /// <response code="200">返回Token</response>
        /// <response code="400">用户名密码不能为空！</response>
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(400)]
        [HttpPost, Route("Login")]
        public IActionResult Login([FromBody]Login login)
        {
            if (login.UserName == null || login.Password == null)
            {
                return BadRequest();
            }
            return Ok(Guid.NewGuid());
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

    

}
