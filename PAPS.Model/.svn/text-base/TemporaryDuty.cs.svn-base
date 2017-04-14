using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;

namespace PAPS.Model
{
    /// <summary>
    /// 临时勤务
    /// </summary>
    public class TemporaryDuty
    {
        /// <summary>
        /// 临时勤务ID
        /// </summary>
        public Guid TemporaryDutyId
        {
            get;set;
        }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public Guid OrganizationId
        {
            get; set;
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        public virtual Organization Organization
        {
            get;set;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName
        {
            get;set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get;set;
        }

        /// <summary>
        /// 用兵数
        /// </summary>
        public int Troops
        {
            get;set;
        }

        /// <summary>
        /// 携枪带弹
        /// </summary>
        public bool HasBullet
        {
            get;set;
        }

        /// <summary>
        /// 携带装备
        /// </summary>
        public List<Materiel> Equipments
        {
            get;set;
        }

        /// <summary>
        /// 指挥员ID
        /// </summary>
        public Guid CommanderId
        {
            get; set;
        }

        /// <summary>
        /// 指挥员
        /// </summary>
        public virtual Staff Commander
        {
            get;set;
        }

        /// <summary>
        /// 执勤方案
        /// </summary>
        public string DutyProgramme
        {
            get;set;
        }

        /// <summary>
        /// 执勤方案图ID
        /// </summary>
        public Guid DutyProgrammePictureId
        {
            get; set;
        }

        /// <summary>
        /// 执勤方案图
        /// </summary>
        public virtual Attachment DutyProgrammePicture
        {
            get;set;
        }

        /// <summary>
        /// 勤务类型ID
        /// </summary>
        public Guid DutyTypeId
        {
            get;set;
        }

        /// <summary>
        /// 勤务类型
        /// </summary>
        public SystemOption DutyType
        {
            get;set;
        }

        /// <summary>
        /// 携带子弹数
        /// </summary>
        public int Bullets
        {
            get;set;
        }

        /// <summary>
        /// 设哨数
        /// </summary>
        public int Posts
        {
            get;set;
        }

        /// <summary>
        /// 交通工具类型ID
        /// </summary>
        public Guid VehicleTypeId
        {
            get;set;
        }

        /// <summary>
        /// 交通工具类型
        /// </summary>
        public virtual SystemOption VehicleType
        {
            get;set;
        }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact
        {
            get;set;
        }

        /// <summary>
        /// 携带枪支数
        /// </summary>
        public int Guns
        {
            get;set;
        }
    }
}