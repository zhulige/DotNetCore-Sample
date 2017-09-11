using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restful_API_Sample.ApiControllers.v1
{
    /// <summary>
    /// Consul 客户端
    /// </summary>
    [Route("api/v1/[controller]")]
    public class ConsulClientController : Controller
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Startup._apiClient.Initialize().Wait();
            return Startup._apiClient.Get("ConsulServer").Result;
            //return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
