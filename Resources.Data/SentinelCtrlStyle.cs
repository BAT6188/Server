using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Data
{
    public enum CtrlStyle
    {
        /// <summary>
        /// //空
        /// <summary>
        STYLE_NULL=0,
        /// <summary>
        /// //哨位
        /// <summary>
        STYLE_STATION=1,
        /// <summary>
        /// //哨位报警
        /// <summary>
        STYLE_STATION_ALARM=2,
        /// <summary>
        /// //栅栏横
        /// <summary>
        STYLE_BARRIER_H=3,
        /// <summary>
        /// //栅栏报警横
        /// <summary>
        STYLE_BARRIER_ALARM_H=4,
        /// <summary>
        /// //栅栏竖
        /// <summary>
        STYLE_BARRIER_V=5,
        /// <summary>
        /// //栅栏报警竖
        /// <summary>
        STYLE_BARRIER_ALARM_V=6,
        /// <summary>
        /// //DVR
        /// <summary>
        STYLE_DVR=7,
        /// <summary>
        /// //DVR报警
        /// <summary>
        STYLE_DVR_ALARM=8,
        /// <summary>
        /// //弹夹
        /// <summary>
        STYLE_CARTRIDGE_CLIP=9,
        /// <summary>
        /// //弹夹报警
        /// <summary>
        STYLE_CARTRIDGE_CLIP_ALARM=10,
        /// <summary>
        /// //红外横
        /// <summary>
        STYLE_INFRARED_H=11,
        /// <summary>
        /// //红外报警横
        /// <summary>
        STYLE_INFRARED_ALARM_H=12,
        /// <summary>
        /// //红外竖
        /// <summary>
        STYLE_INFRARED_V=13,
        /// <summary>
        /// //红外报警竖
        /// <summary>
        STYLE_INFRARED_ALARM_V=14,
        /// <summary>
        /// //高压横
        /// <summary>
        STYLE_HIGH_PRESSURE_H=15,
        /// <summary>
        /// //高压报警横
        /// <summary>
        STYLE_HIGH_PRESSURE_ALARM_H=16,
        /// <summary>
        /// //高压竖
        /// <summary>
        STYLE_HIGH_PRESSURE_V=17,
        /// <summary>
        /// //高压报警竖
        /// <summary>
        STYLE_HIGH_PRESSURE_ALARM_V=18,
        /// <summary>
        /// //手动报警-正常状态横
        /// <summary>
        STYLE_HANDLE_NORMAL_H=19,
        /// <summary>
        /// //手动报警-报警状态横
        /// <summary>
        STYLE_HANDLE_ALARM_H=20,
        /// <summary>
        /// //手动报警-正常状态竖
        /// <summary>
        STYLE_HANDLE_NORMAL_V=21,
        /// <summary>
        /// //手动报警-报警状态竖
        /// <summary>
        STYLE_HANDLE_ALARM_V=22,
        /// <summary>
        /// //视频窗口
        /// <summary>
        STYLE_VIDEO=23,
        /// <summary>
        /// //语言警告
        /// <summary>
        STYLE_ALARM_SOUND=24,
        /// <summary>
        /// //鸣枪警告
        /// <summary>
        STYLE_ALARM_SHOT=25,
        /// <summary>
        /// //子弹箱用钥匙打开报警
        /// <summary>
        STYLE_ALARM_DOOR,
        /// <summary>
        /// //子弹箱强制打开报警
        /// <summary>
        STYLE_ALARM_DOOR_DESTROY,
        /// <summary>
        /// //标签水平
        /// <summary>
        STYLE_LABEL_H,
        /// <summary>
        /// 标签垂直
        /// </summary>
        STYLE_LABEL_V,
        /// <summary>
        /// //来电声音
        /// <summary>
        STYLE_PHONE,
        /// <summary>
        /// //消息提示音
        /// <summary>
        STYLE_MESSAGE,
        /// <summary>
        /// //子弹箱打开
        /// <summary>
        STYLE_CARTRIDGE_OPEN,
        /// <summary>
        /// //子弹箱关闭
        /// <summary>
        STYLE_CARTRIDGE_CLOSE,
        /// <summary>
        /// //哨兵喊话
        /// <summary>
        STYLE_PIQUET,
        /// <summary>
        /// //警情发布终端
        /// <summary>
        STYLE_ALARM_DIFFUSE,
        /// <summary>
        /// //声光报警器
        /// <summary>
        STYLE_SOUND_LIGHT_ALARM,
        /// <summary>
        /// //手动声光报警器
        /// <summary>
        STYLE_SOUND_LIGHT_ALARM_HANDLE,
        /// <summary>
        /// //防瞌睡声音
        /// <summary>
        STYLE_ANTI_SNOOZE,
    }
}
