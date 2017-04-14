using Infrastructure.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlarmAndPlan.Model
{
    public class AlarmLog
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid AlarmLogId
        {
            get;set;
        }

        public Guid AlarmLevelId
        {
            get;set;
        }

        /// <summary>
        /// 事件触发设备id
        /// </summary>
        public Guid AlarmSourceId
        {
            get;set;
        }

        /// <summary>
        /// 报警状态，新增的报警状态可能为null，等效于未处理状态
        /// </summary>
        public Guid? AlarmStatusId
        {
            get;set;
        }

        public Guid ApplicationId
        {
            get;set;
        }

        public Guid AlarmTypeId
        {
            get;set;
        }

        /// <summary>
        /// 报警级别 ,从AlarmSetting配置
        /// </summary>
        public virtual SystemOption AlarmLevel { get; set; }

        /// <summary>
        /// 事件来源
        /// </summary>
        public virtual IPDeviceInfo AlarmSource
        {
            get;set;
        }

        /// <summary>
        /// 报警状态
        /// </summary>
        public virtual SystemOption AlarmStatus
        {
            get;set;
        }

        /// <summary>
        /// 报警类型
        /// </summary>
        public virtual SystemOption AlarmType
        {
            get;set;
        }

        /// <summary>
        /// 所属组织
        /// </summary>
        public Organization Organization
        {
            get; set;
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public virtual Application Application
        {
            get; set;
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public List<AlarmProcessed> Conclusions { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime TimeCreated
        {
            get;set;
        }
        
        public string Description
        {
            get;set;
        }
        
        /// <summary>
        /// 报警上传状态
        /// </summary>
        public int UploadStatus
        {
            get;set;
        } 
        
        /// <summary>
        /// 报警上传次数，超过多少次不再上传
        /// </summary>
        public int UploadCount
        {
            get;set;
        } 
        
        /// <summary>
        /// 是否为下级转发的报警
        /// </summary>
        [NotMapped]
        public bool IsForwardAlarm
        {
            get;set;
        }
        ///// <summary>
        ///// 转发到上级服务
        ///// </summary>
        //[NotMapped]
        //public bool ToTopServer
        //{
        //    get;set;
        //}             
    }
}