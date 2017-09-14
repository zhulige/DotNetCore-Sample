using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_API_Sample.V2.Models
{
    /// <summary>
    /// 用户登陆实体
    /// </summary>
    public class Login
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
