using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Sample.ApiControllers.Models
{
    public class BaseResponse
    {
        IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();

        public BaseResponse()
        {
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            this.StartTime = DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString();
        }

        /// <summary>
        /// 请求触发时间
        /// </summary>
        private string _StartTime = DateTime.Now.ToString() + ":" + DateTime.Now.Millisecond.ToString();
        public string StartTime { get { return _StartTime; } set { _StartTime = value; } }

        /// <summary>
        /// 请求响应时间
        /// </summary>
        public string EndTime { get; set; }
    }
}
