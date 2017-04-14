using Infrastructure.Model;
using Resources.Data;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Model
{
    /// <summary>
    /// 哨位指纹仪打卡记录
    /// </summary>
    public class PunchLog
    {
        public Guid PunchLogId { get; set; }

        /// <summary>
        /// 打卡设备id[哨位终端]
        /// </summary>
        public Guid PunchDeviceId { get; set; }

        public  virtual IPDeviceInfo PunchDevice { get; set; }

        /// <summary>
        /// 打卡遵循两次推送
        /// </summary>
        public Guid? PunchTypeId { get; set; }

        /// <summary>
        /// 考勤类型【查哨(实地查勤)，上哨，下哨】
        /// </summary>
        public virtual SystemOption PunchType { get; set; }

        [NotMapped]
        public int FigureNo { get; set; }

        /// <summary>
        /// 打卡人员
        /// </summary>
        public Guid? StaffId { get; set; }

        public virtual Staff Staff { get; set; }

        public DateTime LogTime { get; set; }

        /// <summary>
        /// 查勤评价
        /// </summary>
        public Guid? AppraiseTypeId { get; set; }

        public SystemOption AppraiseType { get; set; }

        /// <summary>
        /// 换岗结果
        /// </summary>
        public Guid? LogResultId { get; set; }

        /// <summary>
        /// 打卡结果
        /// </summary>
        public SystemOption LogResult { get; set; }

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
        /// 值勤人/接班人
        /// </summary>
        [NotMapped]
        public Staff OnDutyStaff { get; set; }

        /// <summary>
        /// 交班人
        /// </summary>
        [NotMapped]
        public Staff OffDutyStaff { get; set; }

        /// <summary>
        /// 哨位数据视图
        /// </summary>
        [NotMapped]
        public SentinelView SentinelViewInfo { get; set; }

        /// <summary>
        /// 是否为下级推送的查勤
        /// </summary>
        [NotMapped]
        public bool IsForward { get; set; }
    }
}
