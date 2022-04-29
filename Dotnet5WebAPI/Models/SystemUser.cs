using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet5WebAPI.Models
{
    //todo The entity type 'SystemUser' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'. For more information on keyless entity types, see https://go.microsoft.com/fwlink/?linkid=2141943.
    public class SystemUser
    {
        public int SysNo { get; set; }

        /// <summary>
        /// 用于判断用户cookie的随机码
        /// </summary>
        public string RandomCode { get; set; }

        /// <summary>
        /// 要求全局唯一
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoginPassword { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string UserFullName { get; set; }


        /// <summary>
        /// 职位
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CellPhone { get; set; }


        //public List<SystemOrganization> Organizations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string QQ { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string AvatarImageUrl { get; set; }


        /// <summary>
        /// 用户默认组织机构编号
        /// </summary>
        public int OrganizationSysNo { get; set; }

        /// <summary>
        /// 用户默认组织机构编码
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 区分使用哪个系统
        /// </summary>
        public int SystemUserType { get; set; }

        /// <summary>
        /// Gets or sets the last change password date.
        /// </summary>
        /// <value>
        /// The last change password date.
        /// </value>
        public DateTime? LastChangePasswordDate { get; set; }

        /// <summary>
        /// 分供商编号
        /// </summary>
        public int SupplierSysNo { get; set; }


        /// <summary>
        /// 公司信息系统编号(目前保存的是供应商系统编号)
        /// </summary>
        public int CompanySysNo { get; set; }

        /// <summary>
        /// 用户类型（086:国内，---:国外）
        /// </summary>
        public string CountryCode { get; set; }


        /// <summary>
        /// 最近一次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        public string DingTalkDepartmentName { get; set; }

    }

}
