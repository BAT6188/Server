using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 故障
    /// </summary>
    public class Fault
    {
        /// <summary>
        /// 故障ID
        /// </summary>
        public Guid FaultId
        {
            get;set;
        }


        /// <summary>
        /// 故障类型ID
        /// </summary>
        public Guid FaultTypeId
        {
            get;set;
        }

        /// <summary>
        /// 故障类型
        /// </summary>
        public virtual SystemOption FaultType
        {
            get;set;
        }


        /// <summary>
        /// 执勤单位ID
        /// </summary>
        public Guid DutyOrganizationId
        {
            get;set;
        }


        /// <summary>
        /// 执勤单位
        /// </summary>
        public virtual Organization DutyOrganization
        {
            get;set;
        }

        /// <summary>
        /// 检查点ID
        /// </summary>
        public Guid CheckDutySiteId
        {
            get; set;
        }

        /// <summary>
        /// 检查点
        /// </summary>
        public virtual MonitorySite CheckDutySite
        {
            get; set;
        }


        /// <summary>
        /// 检查人ID
        /// </summary>
        public Guid? CheckManId
        {
            get; set;
        }

        /// <summary>
        /// 检查人
        /// </summary>
        public virtual Staff CheckMan
        {
            get; set;
        }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;set;
        }

        /// <summary>
        /// 通报时间
        /// </summary>
        public DateTime CircularTime
        {
            get;set;
        }


        /// <summary>
        /// 检查操作ID
        /// </summary>
        public Guid? DutyCheckOperationId
        {
            get; set;
        }

        /// <summary>
        /// 检查操作
        /// </summary>
        public virtual DutyCheckOperation DutyCheckOperation
        {
            get; set;
        }


    }
}
