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
        /// <response code="200">成功获取用户信息列表</response>
        [Authorize]
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
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserInfo), 200)]
        [ProducesResponseType(404)]
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
        /// <remarks>
        /// 用户登陆 
        /// 
        ///     POST /LoginRequest
        ///     {
        ///        "UserName": "用户名",
        ///        "Password": "密码"
        ///     }
        /// 
        /// </remarks>
        /// <param name="loginRequest">UserName:用户名<br/>Password:密码<br/></param>
        /// <returns>用户信息</returns>
        /// <response code="201">用户信息</response>
        /// <response code="400">用户名密码不能为空！</response>
        [HttpPost, Route("Login")]
        [ProducesResponseType(typeof(LoginResponse), 201)]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest();
            }
            LoginResponse _LoginResponse = new LoginResponse();
            _LoginResponse.Token = Guid.NewGuid().ToString();
            _LoginResponse.UserName = loginRequest.UserName;
            //return CreatedAtRoute(Get, _UserInfo);
            return Ok(_LoginResponse);
        }

    }
    
}