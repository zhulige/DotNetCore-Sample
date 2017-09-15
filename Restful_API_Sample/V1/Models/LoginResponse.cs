using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Sample.V1.Models
{
    /// <summary>
    /// 用户登陆返回实体
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Token;

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName;
    }
}
