using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Restful_API_Sample.V2.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Restful_API_Sample.V2.Controllers
{
    /// <summary>
    /// 样板API
    /// </summary>
    [Authorize]
    [EnableCors("AllowAll")]
    [ApiVersion("2.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class UserInfoController : Controller
    {
        //const string ByIdRouteName = "GetById" + nameof(V2);

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <returns>用户信息列表.</returns>
        /// <response code="200">成功</response>
        /// <response code="400">失败</response>
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
                    UserName = "ZhuLige",
                    Mobile = "13580000001"
                },
                new UserInfo()
                {
                    Id = 2,
                    UserName = "Bob",
                    Mobile = "13580000001"
                },
                new UserInfo()
                {
                    Id = 3,
                    UserName = "Jane",
                    Mobile = "13580000001"
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
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserInfo), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id) =>
            Ok(new UserInfo()
            {
                Id = id,
                UserName = "ZhuLige",
                Mobile = "13580000001"
            }
            );

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo">The person to create.</param>
        /// <returns>The created person.</returns>
        /// <response code="201">成功</response>
        /// <response code="400">失败</response>
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

            return Created("UserInfo", userInfo);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <param name="userInfo">实体数据</param>
        /// <returns>The created person.</returns>
        /// <response code="201">成功</response>
        /// <response code="400">失败</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserInfo), 201)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody]UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userInfo.Id = id;

            return Created("UserInfo", userInfo);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id">The person to create.</param>
        /// <returns>The created person.</returns>
        /// <response code="204">成功</response>
        /// <response code="400">失败</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
        
    }

    

}
