using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Sample.ApiControllers.Models
{
    /// <summary>
    /// 保存、删除返回请求
    /// </summary>
    public class SuccessResponse : BaseResponse
    {
        /// <summary>
        /// 成功/失败
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public string ErrCode { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }


    }
}
