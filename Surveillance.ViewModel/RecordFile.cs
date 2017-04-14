using System;
using Surveillance.ViewModel.Enum;
using Newtonsoft.Json;

namespace Surveillance.ViewModel
{
    /// <summary>
    /// 录像文件信息
    /// </summary>
    public class RecordFile
    {
        /// <summary>
        /// 录像文件id
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// 录像文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 录像开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 录像结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 录像触发方式
        /// </summary>
        public RecordFileType RecordType { get; set; }

        /// <summary>
        /// 文件大小，单位byte
        /// </summary>
        public long RecordSize { get; set; }

        public int Channel { get; set; }

        public int DriveNo { get; set; }

        public int StartCluster { get; set; }

        [JsonIgnore]
        public string FileSizeText
        {
            get
            {
                if (RecordSize > 1024 * 1024 * 1024) //G
                {
                    return string.Format("{0:f2} G", (double)RecordSize / (1024 * 1024 * 1024));
                }
                else if (RecordSize > 1024 * 1024) //M
                {
                    return string.Format("{0:f2} M", (double)RecordSize / (1024 * 1024));
                }
                else if (RecordSize > 1024) //KB
                {
                    return string.Format("{0:f2} Kb", (double)RecordSize / 1024);
                }
                else
                {
                    return string.Format("{0:d2} b", RecordSize);
                }
            }
        }
    }
}
