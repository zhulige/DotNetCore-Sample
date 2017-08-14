using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Restful_API_Sample.ApiControllers.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restful_API_Sample.ApiControllers.v1
{
    /// <summary>
    /// WebApi Demo
    /// </summary>
    [Authorize]
    [Route("api/v1/[controller]")]
    public class DemoController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagingRequset"></param>
        /// <returns></returns>
        [Authorize, HttpGet("Paging")]
        public PagingResponse Paging(PagingRequset pagingRequset)
        {
            return new PagingResponse();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <returns></returns>
        [Authorize, HttpGet("Query")]
        public object Query(QueryRequest queryRequest)
        {
            return new PagingResponse();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public SuccessResponse Post([FromBody]string value)
        {
            return new SuccessResponse();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public SuccessResponse Put(int id, [FromBody]string value)
        {
            return new SuccessResponse();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public SuccessResponse Delete(int id)
        {
            return new SuccessResponse();
        }

    }
}
