using System;

namespace Infrastructure.Model
{
    /// <summary>
    /// 导航属性
    /// </summary>
    public class UserClaim:User
    {

        /// <summary>
        /// 导航属性ID
        /// </summary>
        public Guid ClaimID
        {
            get;set;
        }

        /// <summary>
        /// 导航属性类型
        /// </summary>
        public string ClaimType
        {
            get;set;
        }

        public string ClaimValue
        {
            get;set;
        }
    }
}