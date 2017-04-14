using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resources.Model
{
    /// <summary>
    /// 视频轮巡场景 
    /// 适用于视频轮巡，上墙轮巡
    /// </summary>
    public class VideoRoundScene
    {
        public Guid VideoRoundSceneId
        {
            get;set;
        }

        [MaxLength(32)]
        public string VideoRoundSceneName
        {
            get;set;
        }

        public List<VideoRoundSection> VideoRoundSections
        {
            get;set;
        }

        public Guid VideoRoundSceneFlagId
        {
            get;set;
        }

        /// <summary>
        /// 场景应用方式（视频预览，视频上墙）
        /// </summary>
        public virtual SystemOption VideoRoundSceneFlag
        {
            get;set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Modified
        {
            get; set;
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public User ModifiedBy
        {
            get; set;
        }

    }
}
