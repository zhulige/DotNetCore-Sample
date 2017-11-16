using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restful_API_Sample.V1.Models;
using System;
using System.Collections.Generic;

namespace Restful_API_Sample.V1.Controllers
{
    /// <summary>
    /// 样板API
    /// </summary>
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class UserInfoController : Controller
    {
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <returns>用户信息列表.</returns>
        /// <response code="200">成功</response>
        /// <response code="400">失败</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserInfo>), 200)]
        [ProducesResponseType(400)]
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
        /// <response code="200">成功</response>
        /// <response code="400">失败</response>
        [Authorize]
        [HttpGet("{id:int}")]
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
        /// 用户登陆
        /// </summary>
        ///// <remarks>
        ///// 用户登陆 
        ///// 
        /////     POST /LoginRequest
        /////     {
        /////        "UserName": "用户名",
        /////        "Password": "密码"
        /////     }
        ///// 
        ///// </remarks>
        /// <param name="loginRequest">UserName:用户名<br/>Password:密码<br/></param>
        /// <returns>用户信息</returns>
        /// <response code="201">成功</response>
        /// <response code="400">失败</response>
        /// <response code="401">安全认证失败</response>
        /// <response code="404">地址不存在</response>
        /// <response code="500">服务器程序出错了</response>
        /// <response code="503">服务器性能达到瓶颈了</response>
        [HttpPost, Route("Login")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(503)]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("用户名密码不能为空！");
            }
            //LoginResponse _LoginResponse = new LoginResponse();
            //_LoginResponse.Token = Guid.NewGuid().ToString();
            //_LoginResponse.UserName = loginRequest.UserName;
            //return CreatedAtRoute(Get, _UserInfo);
            return Created("Token", Guid.NewGuid());
        }

    }
    
}