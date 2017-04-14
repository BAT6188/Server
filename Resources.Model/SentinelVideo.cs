using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Model
{
    public class SentinelVideo
    {
        [Key]
        public Guid SentinelVideoId
        {
            get;set;
        }

        public Guid CameraId
        {
            get;set;
        }

        public virtual Camera Camera
        {
            get;set;
        }

        public Guid? VideoTypeId
        {
            get;set;
        }

        /// <summary>
        /// 视频类型，哨位中心视频...
        /// </summary>
        public virtual SystemOption VideoType
        {
            get;set;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 是否直连设备预览视频
        /// </summary>
        public bool PlayByDevice { get; set; }
    }
}
