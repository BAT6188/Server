using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Model
{
    /// <summary>
    /// 哨位模板
    /// </summary>
    public class SentinelLayout
    {
        public Guid SentinelLayoutId { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public List<SentinelLayoutData> MapDatas
        {
            get
            {
                if (string.IsNullOrEmpty(MapDataXml))
                {
                    return null;
                }
                List<SentinelLayoutData> mapDatas = new List<SentinelLayoutData>();
                string temp = MapDataXml;
                temp = temp.Replace("\"", "");
                while (temp.Contains("<INFO"))
                {
                    string info = temp.Substring(temp.IndexOf("<INFO"), temp.IndexOf("/>") -7);
                    mapDatas.Add(ToSentinelLayoutData(info));
                    temp = temp.Replace(info, "");
                }
                return mapDatas;
            }
            set
            {
                StringBuilder sb = new StringBuilder();
                if (value != null && value.Count > 0)
                {
                    sb.Append("<MAPDATA>");

                    foreach (SentinelLayoutData d in value)
                    {
                        sb.Append(d.ToString());
                    }
                    sb.Append("</MAPDATA>");
                }
                MapDataXml = sb.ToString();
            }
        }

        private SentinelLayoutData ToSentinelLayoutData(string xml)
        {
            string datas = xml.Replace("<", "").Replace("/>", "");
            var attrs = datas.Split(' ');
            SentinelLayoutData mapData = new SentinelLayoutData();
            foreach (var attr in attrs)
            {
                var keyvalue = attr.Split('=');
                if (keyvalue.Length == 2)
                {
                    if (keyvalue[0].Equals("id"))
                    {
                        mapData.Id = keyvalue[1];
                    }
                    else if (keyvalue[0].Equals("style"))
                    {
                        mapData.Style = Int32.Parse(keyvalue[1]);
                    }
                    else if (keyvalue[0].Equals("ctrlrect"))
                    {
                        var rects = keyvalue[1].Split(',');
                        foreach (var rect in rects)
                            mapData.Crlrect = new ElementLayout()
                            {
                                Left = Int32.Parse(rects[0]),
                                Top = Int32.Parse(rects[1]),
                                Width = Int32.Parse(rects[2]) - Int32.Parse(rects[0]),
                                Height = Int32.Parse(rects[3]) - Int32.Parse(rects[1]),
                            };
                    }
                    else if (keyvalue[0].Equals("clientrect"))
                    {
                        var rects = keyvalue[1].Split(',');
                        foreach (var rect in rects)
                            mapData.Clientrect = new ElementLayout()
                            {
                                Left = Int32.Parse(rects[0]),
                                Top = Int32.Parse(rects[1]),
                                Width = Int32.Parse(rects[2]) - Int32.Parse(rects[0]),
                                Height = Int32.Parse(rects[3]) - Int32.Parse(rects[1]),
                            };
                    }
                    else if (keyvalue[0].Equals("Enable"))
                    {
                        mapData.Enable = Int32.Parse(keyvalue[1]);
                    }
                    else if (keyvalue[0].Equals("Visable"))
                    {
                        mapData.Visable = Int32.Parse(keyvalue[1]);
                    }
                    else if (keyvalue[0].Equals("PostNo"))
                    {
                        mapData.PostNo = Int32.Parse(keyvalue[1]);
                    }
                    else if (keyvalue[0].Equals("PostName"))
                    {
                        mapData.PostName = keyvalue[1];
                    }
                    else if (keyvalue[0].Equals("PostType"))
                    {
                        mapData.PostType = Int32.Parse(keyvalue[1]);
                    }
                }
            }
            return mapData;
        }


        /// <summary>
        /// 发送到哨位台的数据
        /// </summary>
        public string MapDataXml
        {
            get;set;
        }
    }
}
