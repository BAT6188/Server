using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.Model
{
    /// <summary>
    /// 人脸识别
    /// </summary>
    public class FaceRecognition
    {
        public Guid FaceRecognitionId
        {
            get; set;
        }

        /// <summary>
        ///  抓拍摄像机
        /// </summary>
        public Guid DeviceInfoId
        {
            get; set;
        }

        public virtual IPDeviceInfo DeviceInfo
        {
            get;set;
        }

        /// <summary>
        ///  绝对时间 
        /// </summary>
        public DateTime? AbsTime
        {
            get; set;
        }

        /// <summary>
        /// 人脸评分，范围：0~100
        /// </summary>
        public int FaceScore
        {
            get; set;
        }

        /// <summary>
        /// 人脸子图的长度，为0表示没有图片，大于0表示有图片 
        /// </summary>
        public long FacePicLen
        {
            get; set;
        }

        /// <summary>
        ///  背景图的长度，为0表示没有图片，大于0表示有图片(保留)
        /// </summary>
        public long BackgroundPicLen
        {
            get; set;
        }

        /// <summary>
        /// 人脸子图的图片数据 
        /// </summary>
        public byte[] FaceBuffer
        {
            get; set;
        }

        /// <summary>
        /// 背景图的图片数据 
        /// </summary>
        public byte[] BackgroupBuffer
        {
            get; set;
        }

        /// <summary>
        /// 年龄段
        /// </summary>
        public short AageGroup
        {
            get; set;
        }

        /// <summary>
        ///  性别：1- 男，2- 女 
        /// </summary>
        public byte Sex
        {
            get; set;
        }

        /// <summary>
        /// 是否戴眼镜：1- 不戴，2- 戴 
        /// </summary>
        public byte ByEyeGlass
        {
            get; set;
        }


        public DateTime Modified
        {
            get; set;
        }
    }
}
