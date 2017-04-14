using System;

namespace Infrastructure.Model
{
    public class EventLog
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid EventLogId
        {
            get;set;
        }

        /// <summary>
        /// 事件级别选项id
        /// </summary>
        public Guid EventLevelId
        {
            get;set;
        }

        /// <summary>
        /// 事件来源选项id
        /// </summary>
        public Guid EventSourceId
        {
            get;set;
        }

        /// <summary>
        /// 事件级别ID
        /// </summary>
        public Guid EventLogTypeId
        {
            get;set;
        }

        /// <summary>
        /// 事件级别
        /// </summary>
        public virtual SystemOption EventLevel { get; set; }

        /// <summary>
        /// 事件来源
        /// </summary>
        public virtual SystemOption EventSource
        {
            get;set;
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        public virtual SystemOption EventLogType
        {
            get; set;
        }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime TimeCreated
        {
            get;set;
        }

        /// <summary>
        /// 事件数据
        /// </summary>
        public string EventData
        {
            get;set;
        }

        /// <summary>
        /// 所属组织Id
        /// </summary>
        public Guid OrganizationId
        {
            get; set;
        }

        /// <summary>
        /// 所属组织
        /// </summary>
        public virtual Organization Organization
        {
            get; set;
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public Guid ApplicationId
        {
            get; set;
        }

        /// <summary>
        /// 应用
        /// </summary>
        public virtual Application Application
        {
            get; set;
        }
    }
}