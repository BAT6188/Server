using System;

namespace Resources.Model
{
    /// <summary>
    /// 编码器类型
    /// </summary>
    public class EncoderType
    {
        public Guid EncoderTypeId
        {
            get;set;
        }

        public string EncoderTypeName
        {
            get;set;
        }

        public int EncoderCode
        {
            get;set;
        }

        public string DefaultUserName
        {
            get;set;
        }

        public string DefaultPassword
        {
            get;set;
        }

        public int OSDLines
        {
            get;set;
        }


        public int PTZ3DControl
        {
            get;set;
        }

        /// <summary>
        /// 缺省通道
        /// </summary>
        public int Channels { get; set; }

        /// <summary>
        /// 录像文件后缀
        /// </summary>
        public string RecordFileExtension { get; set; }

        /// <summary>
        /// 默认端口
        /// </summary>
        public int DefaultPort { get; set; }
    }
}
