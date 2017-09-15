using System.ComponentModel.DataAnnotations;

namespace Restful_API_Sample.V2.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        /// <value>The person's unique identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        /// <value>登陆所用用户名</value>
        [Required]
        [StringLength(25)]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        /// <value>登陆所用手机号</value>
        [StringLength(13)]
        public string Mobile { get; set; }

    }
}
