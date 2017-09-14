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
        /// <value>The person's first name.</value>
        [Required]
        [StringLength(25)]
        public string UserName { get; set; }
        
    }
}
