using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resources.Model
{
    /// <summary>
    /// 设备基本信息
    /// </summary>
    public class IPDeviceInfo
    {
        /// <summary>
        /// pk ,设备id
        /// </summary>
        public Guid IPDeviceInfoId
        {
            get; set;
        }

        /// <summary>
        /// 设备编号，使用于哨位台，声光报警，防区
        /// </summary>
        public int IPDeviceCode
        {
            get; set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string IPDeviceName
        {
            get; set;
        }

        public Guid DeviceTypeId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public virtual SystemOption DeviceType
        {
            get; set;
        }

        public string EndPointsJson
        {
            get;set;
        }

        /// <summary>
        /// 访问终结点
        /// </summary>
        [NotMapped]
        //[JsonIgnore]
        public List<EndPointInfo> EndPoints
        {
            get
            {
                if (string.IsNullOrEmpty(EndPointsJson))
                {
                    return null;
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<EndPointInfo>>(EndPointsJson);
                }
            }
            set
            {
                EndPointsJson = JsonConvert.SerializeObject(value);
            }
        }

        //public JsonObject<EndPointInfo[]> EndPoints
        //{
        //    get;set;
        //}

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        public Guid OrganizationId
        {
            get;set;
        }

        /// <summary>
        /// 所属机构
        /// </summary>
        public virtual Organization Organization
        {
            get; set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Modified
        {
            get; set;
        }

        public Guid ModifiedByUserId
        {
            get;set;
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public virtual User ModifiedByUser
        {
            get; set;
        }

        //public List<DeviceGroupIPDevice> DeviceGroupIPDevices
        //{
        //    get;set;
        //}

            /// <summary>
            /// 设备状态
            /// </summary>
        public Guid? StatusId
        {
            get;set;
        }

        public  virtual SystemOption Status
        {
            get;set;
        }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public string SeriesNo
        {
            get;set;
        }
    }
}
