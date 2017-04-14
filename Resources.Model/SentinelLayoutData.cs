using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Resources.Model
{
    /// <summary>
    /// 哨位台布局数据
    /// </summary>
    public class SentinelLayoutData
    {
        /// <summary>
        /// 设备id，播放器id随机生成
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设备类型id
        /// </summary>
        public int Style { get; set; }

       /// <summary>
       /// 设备图标布局
       /// </summary>
        public ElementLayout Crlrect { get; set; }

        /// <summary>
        /// 父容器布局
        /// </summary>
        public ElementLayout Clientrect { get; set; }

        public int Enable { get; set; }

        public int Visable { get; set; }

        public int PostNo { get; set; }

        public string PostName { get; set; }

        public int PostType { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<INFO id=\"").Append(Id).Append("\" style=\"").Append(Style).Append("\" ctrlrect=\"").Append(Crlrect.Left).Append(",").
                              Append(Crlrect.Top).Append(",").Append(Crlrect.Left + Crlrect.Width).Append(",").Append(Crlrect.Height + Crlrect.Top).
                              Append("\" clientrect=\"").Append(Clientrect.Left).Append(",").Append(Clientrect.Top).Append(",").Append(Clientrect.Left + Clientrect.Width).
                              Append(",").Append(Clientrect.Top + Clientrect.Height).Append("\" Enable=\"").Append(Enable).Append("\"  Visable=\"").
                              Append(Visable).Append("\"");
            if (PostNo > 0)
            {
                sb.Append(" PostNo=\"").Append(PostNo).Append("\"");
            }
            if (!string.IsNullOrEmpty(PostName))
            {
                sb.Append(" PostName=\"").Append(PostName).Append("\"");
            }
            if (PostType > 0)
            {
                sb.Append(" PostType=\"").Append(PostType).Append("\"");
            }
            sb.Append("/>");
            return sb.ToString();
        }
    }

    public class ElementLayout
    {
        public double Top { get; set; }

        public double Left { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}
