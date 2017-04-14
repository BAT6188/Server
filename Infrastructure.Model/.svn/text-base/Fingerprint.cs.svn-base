using Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Infrastructure.Model
{
    /// <summary>
    /// 指纹
    /// </summary>
    public class Fingerprint
    {
        /// <summary>
        /// 指纹ID
        /// </summary>
        public Guid FingerprintId
        {
            get;set;
        }

        /// <summary>
        /// 指纹编号
        /// </summary>
        public int FingerprintNo
        {
            get;set;
        }

        /// <summary>
        /// 手指号（从左到右，1-10代表对应的手指）
        /// </summary>
        public int FigureNo { get; set; }

        /// <summary>
        /// 指纹特征
        /// </summary>
        public byte[] FingerprintBuffer
        {
            get; set;
        }

        public Guid StaffId { get; set; }

        //public virtual Staff Staff { get; set; }

    }
}