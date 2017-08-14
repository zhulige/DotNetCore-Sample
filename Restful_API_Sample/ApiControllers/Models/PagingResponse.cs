using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Sample.ApiControllers.Models
{
    public class PagingResponse : BaseResponse
    {
        /// <summary>
        /// 分页查询中总记录数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 分页查询中结果集合
        /// </summary>
        public object rows { get; set; }
    }
}
