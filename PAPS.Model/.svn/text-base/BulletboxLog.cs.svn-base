using Infrastructure.Model;
using Resources.Data;
using Resources.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAPS.Model
{
    /// <summary>
    /// 子弹箱开启
    /// </summary>
    public class BulletboxLog
    {
        /// <summary>
        /// PK
        /// </summary>
        public Guid BulletboxLogId { get; set; }

        /// <summary>
        /// 哨位台设备Id
        /// </summary>
        public Guid SentinelDeviceId { get; set; }

        public virtual IPDeviceInfo SentinelDevice { get; set; }

        public Guid LockStatusId { get; set; }

        /// <summary>
        /// 子弹箱状态（供弹申请/开/关/超时未关闭。。。。）
        /// </summary>
        public SystemOption LockStatus { get; set; }


        /// <summary>
        /// 消息确认
        /// </summary>
        public string ComfirmInfo { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public DateTime Modified { get; set; }

        /// <summary>
        /// 在客户端打开，需记录
        /// </summary>
        public Guid? ModifiedById { get; set; }

        public virtual User ModifiedBy { get; set; }

        /// <summary>
        /// 哨位数据视图
        /// </summary>
        [NotMapped]
        public SentinelView SentinelViewInfo { get; set; }

        public Guid? FrontSnapshotId { get; set; }

        /// <summary>
        /// 前端抓拍，通常指抓拍打卡人员
        /// </summary>
        public Attachment FrontSnapshot { get; set; }

        public Guid? BulletboxSnapshotId { get; set; }

        /// <summary>
        /// 子弹箱抓拍
        /// </summary>
        public Attachment BulletboxSnapshot { get; set; }

        /// <summary>
        /// 是否打开子弹箱
        /// </summary>
        [NotMapped]
        public bool IsOpen { get; set; }

    }
}
