using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveillance.Model
{
    public class LicensePlateRecognition
    {
        public Guid LicensePlateRecognitionId
        {
            get; set;
        }

        /// <summary>
        /// 抓拍摄像机
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
        /// 车辆类型：0- 未知，1- 客车，2- 货车，3- 轿车，4- 面包车，5- 小货车
        /// </summary>
        public short VehicleType
        {
            get; set;
        }

        /// <summary>
        /// 绝对时间，精确到毫秒，yyyymmddhhmmssxxx，例如20090810235959999
        /// </summary>
        public DateTime AbsTime
        {
            get; set;
        }

        /// <summary>
        /// 图片长度（背景图） 
        /// </summary>
        public long PicLen
        {
            get; set;
        }

        /// <summary>
        /// 车牌小图片长度（车牌图）
        /// </summary>
        public long PicPlateLen
        {
            get; set;
        }

        /// <summary>
        ///  当上传的是图片(背景图)信息时，指针指向图片信息，图片长度为dwPicLen；当上传的是录像时，指针指向录像信息，录像长度为dwVideoLen 
        /// </summary>
        public byte[] BackgroupBuffer
        {
            get; set;
        }


        /// <summary>
        /// 当上传的是图片(车牌图)信息时，指针指向车牌小图片信息，车牌小图片的长度为dwPicPlateLen 
        /// </summary>
        public byte[] VechileNoBuffer
        {
            get; set;
        }

        /// <summary>
        /// 车牌类型
        /// </summary>
        public short PlateType
        {
            get; set;
        }

        /// <summary>
        ///  车牌颜色
        /// </summary>
        public short Color
        {
            get; set;
        }

        /// <summary>
        /// 车牌号码
        /// </summary>
        public String License
        {
            get; set;
        }


        public DateTime Modified
        {
            get; set;
        }
    }
}
