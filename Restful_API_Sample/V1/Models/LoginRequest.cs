using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Sample.V1.Models
{
    /// <summary>
    /// 用户登陆请求实体
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName;

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password;
    }
}
