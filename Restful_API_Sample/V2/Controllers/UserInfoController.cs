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
    public class UserInfoController : Controller
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
        /// <response code="200">成功获取用户信息</response>
        /// <response code="400">未获取到用户信息</response>
        [HttpGet("{id:int}", Name = ByIdRouteName)]
        [ProducesResponseType(typeof(UserInfo), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id) =>
            Ok(new UserInfo()
            {
                Id = id,
                UserName = "ZhuLige",
                Mobile = "13580000001"
            }
            );

        /// <summary>
        /// 添加用户信息.
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

        /// <summary>
        /// 修改用户信息.
        /// </summary>
        /// <param name="id">The person to create.</param>
        /// <param name="userInfo">The person to create.</param>
        /// <returns>The created person.</returns>
        /// <response code="201">The person was successfully created.</response>
        /// <response code="400">The person was invalid.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserInfo), 201)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userInfo.Id = id;

            return CreatedAtRoute(ByIdRouteName, new { id = userInfo.Id }, userInfo);
        }

        /// <summary>
        /// 删除用户信息.
        /// </summary>
        /// <param name="id">The person to create.</param>
        /// <returns>The created person.</returns>
        /// <response code="201">The person was successfully created.</response>
        /// <response code="400">The person was invalid.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(id);
        }
        
    }

    

}
