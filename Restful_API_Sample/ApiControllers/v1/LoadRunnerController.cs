using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Restful_API_Sample.ApiControllers.v1
{
    /// <summary>
    /// 性能测试
    /// </summary>
    [EnableCors("AllowAll")]
    [Route("api/v1/[controller]")]
    public class LoadRunnerController : Controller
    {
        /// <summary>
        /// 查询列表1000条
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string[] strs = new string[1000];
            for (int i = 0; i < 1000; i++)
            {
                strs[i] = "{'staffId':123,'deviceId':56001,'happenTime':1504750285000,'mapId':21,'areaId':42,'locationType':1,'x':325.6068080208332,'y':602.1246197164348,'z':" + i.ToString() + "}";
            }
            return strs;
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return id.ToString();
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
