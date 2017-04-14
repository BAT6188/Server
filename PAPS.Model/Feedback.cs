using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PAPS.Model
{
    /// <summary>
    /// 反馈
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// 反馈ID
        /// </summary>
        public Guid FeedbackId
        {
            get; set;
        }

        /// <summary>
        /// 通报ID
        /// </summary>
        public Guid? CircularId
        {
            get; set;
        }

        /// <summary>
        /// 通报
        /// </summary>
        public virtual Circular Circular
        {
            get; set;
        }


        /// <summary>
        /// 故障Id
        /// </summary>
        public Guid? FaultId
        {
            get;set;
        }
         
        /// <summary>
        /// 故障
        /// </summary>
        public virtual Fault Fault
        {
            get;set;
        }

        /// <summary>
        /// 反馈类型（通报为0，故障为1）
        /// </summary>
        public int FeedbackType
        {
            get;set;
        }


        /// <summary>
        /// 反馈人员ID
        /// </summary>
        public Guid FeedbackStaffId
        {
            get; set;
        }

        /// <summary>
        /// 反馈人员
        /// </summary>
        public virtual Staff FeedbackStaff
        {
            get;set;
        }

        /// <summary>
        /// 反馈时间
        /// </summary>
        public DateTime FeedbackTime
        {
            get;set;
        }

        /// <summary>
        /// 反馈描述
        /// </summary>
        public string Description
        {
            get;set;
        }

        /// <summary>
        ///  反馈内容选项集合实际保存数据
        /// </summary>
        private string FeedbackOptionsJson
        {
            get; set;
        }
        /// <summary>
        /// 反馈选项
        /// </summary>
        [NotMapped]
        public List<SystemOption> FeedbackOptions
        {
            get
            {
                return (List<SystemOption>)JsonConvert.DeserializeObject(FeedbackOptionsJson, typeof(List<SystemOption>));
            }
            set
            {
                string jsontxt = JsonConvert.SerializeObject(value);
                FeedbackOptionsJson = jsontxt;
            }
        }
    }
}