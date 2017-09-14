using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Restful_API_Sample.V2.Models;
using System;
using System.Collections.Generic;

namespace Restful_API_Sample.V2.Controllers
{
    /// <summary>
    /// 样板API
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class SampleController : Controller
    {
        const string ByIdRouteName = "GetById" + nameof(V2);

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <returns>用户信息列表.</returns>
        /// <response code="200">成功获取用户信息列表</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserInfo>), 200)]
        public IActionResult Get()
        {
            var _UserName = new[]
            {
                new UserInfo()
                {
                    Id = 1,
                    UserName = "ZhuLige"
                },
                new UserInfo()
                {
                    Id = 2,
                    UserName = "Bob"
                },
                new UserInfo()
                {
                    Id = 3,
                    UserName = "Jane",
                }
            };

            return Ok(_UserName);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns>用户信息</returns>
        /// <response code="200">成功获取用户信息</response>
        /// <response code="400">未获取到用户信息</response>
        [HttpGet("{id:int}", Name = ByIdRouteName)]
        [ProducesResponseType(typeof(UserInfo), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id) =>
            Ok(new UserInfo()
            {
                Id = id,
                UserName = "ZhuLige"
            }
            );

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="userInfo">The person to create.</param>
        /// <returns>The created person.</returns>
        /// <response code="201">The person was successfully created.</response>
        /// <response code="400">The person was invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserInfo), 201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userInfo.Id = 42;

            return CreatedAtRoute(ByIdRouteName, new { id = userInfo.Id }, userInfo);
        }

        ///// <summary>
        ///// 用户登陆
        ///// </summary>
        ///// <remarks>
        ///// Note that the key is a GUID and not an integer.
        /////  
        /////     POST /Login
        /////     {
        /////        "UserName": "用户名",
        /////        "Password": "密码"
        /////     }
        ///// 
        ///// </remarks>
        ///// <param name="login">Username:"用户名"<br/>Password:"密码"</param>
        ///// <returns>Token</returns>
        ///// <response code="200">返回Token</response>
        ///// <response code="400">用户名密码不能为空！</response>
        //[ProducesResponseType(typeof(Guid), 200)]
        //[ProducesResponseType(400)]
        //[HttpPost, Route("Login")]
        //public IActionResult Login([FromBody]Login login)
        //{
        //    if (login.UserName == null || login.Password == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(Guid.NewGuid());
        //}

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
