using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Sample.ApiControllers.Models
{
    public class QueryRequest : BaseRequset
    {
        /// <summary>
        /// select显示列
        /// </summary>
        public string select { get; set; }

        /// <summary>
        /// where条件
        /// </summary>
        public string where { get; set; }

        /// <summary>
        /// where参数
        /// </summary>
        public object param { get; set; }

        /// <summary>
        /// Top
        /// </summary>
        public int top { get { return _top; } set { _top = value; } }
        private int _top = -1;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public orderValue order { get; set; }


        public enum orderValue
        {
            /// <summary>
            /// 正序
            /// </summary>
            desc,
            /// <summary>
            /// 倒序
            /// </summary>
            asc
        }
    }
}
