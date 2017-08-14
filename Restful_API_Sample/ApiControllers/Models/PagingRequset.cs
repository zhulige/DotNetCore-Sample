using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Sample.ApiControllers.Models
{
    public class PagingRequset : BaseRequset
    {
        private string _select = " * ";
        /// <summary>
        /// select显示列
        /// </summary>
        public string select { get { return _select; } set { _select = value; } }


        /// <summary>
        /// where条件
        /// </summary>
        public string where { get; set; }

        /// <summary>
        /// where参数
        /// </summary>
        public object param { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string search { get; set; }
        private int _rows = 20;
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int rows { get { return _rows; } set { _rows = value; } }
        private int _page = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get { return _page; } set { _page = value; } }
        private string _sort = "InTime";
        /// <summary>
        /// 排序字段
        /// </summary>
        public string sort { get { return _sort; } set { _sort = value; } }

        /// <summary>
        /// 排序方式
        /// </summary>
        public orderValue order { get; set; }
        /// <summary>
        /// 其它值
        /// </summary>
        public string Data { get; set; }


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
