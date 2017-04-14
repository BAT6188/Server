using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveillance.ViewModel.Enum
{
    /// <summary>
    /// 云台控制动作
    /// </summary>
    public enum PtzControlAction:int
    {
        /// <summary>
        /// 上
        /// </summary>
        PTZ_UP = 1,         
        /// <summary>
        /// 下
        /// </summary>
        PTZ_DOWN,       
        /// <summary>
        /// 左
        /// </summary>
        PTZ_LEFT,       
        /// <summary>
        /// 右
        /// </summary>
        PTZ_RIGHT,
        /// <summary>
        /// 左上
        /// </summary>
        PTZ_LEFT_UP,    
        /// <summary>
        /// 左下
        /// </summary>
        PTZ_LEFT_DOWN,       
        /// <summary>
        /// 右上
        /// </summary>
        PTZ_RIGHT_UP,       
        /// <summary>
        /// 右下
        /// </summary>
        PTZ_RIGHT_DOWN,
        /// <summary>
        /// 设置预置点
        /// </summary>
        PTZ_SET_PRESET,
        /// <summary>
        /// 清除预置点
        /// </summary>
        PTZ_CLE_PRESET,
        /// <summary>
        /// 调用预置点
        /// </summary>
        PTZ_CALL_PRESET,
        /// <summary>
        /// 放大
        /// </summary>
        PTZ_ZOOM_IN,
        /// <summary>
        /// 缩小
        /// </summary>
        PTZ_ZOOM_OUT,
        /// <summary>
        /// 焦点近
        /// </summary>
        PTZ_FOCUS_NEAR,
        /// <summary>
        /// 焦点远
        /// </summary>
        PTZ_FOCUS_FAR,
        /// <summary>
        /// 光圈大
        /// </summary>
        PTZ_IRIS_OPEN,
        /// <summary>
        /// 光圈小
        /// </summary>
        PTZ_IRIS_CLOSE,
        /// <summary>
        /// 视频切换上墙（针对模拟矩阵）
        /// </summary>
        PTZ_SET_MONITOR,     
        PTZ_AUTO,              
        /// <summary>
        /// 鼠标拖动放大功能
        /// </summary>
        PTZ_MOUSE_CLICK_MOVE_ZOOM, 
        /// <summary>
        /// 云台自动巡航
        /// </summary>
        PTZ_AUTO_SCAN_PRESET, 
        /// <summary>
        /// 线扫
        /// </summary>
        PTZ_LINESCAN,            
        /// <summary>
        /// 灯光
        /// </summary>
        PTZ_LIGHTCONTROL,
        /// <summary>
        /// 红外
        /// </summary>
        PTZ_INFRAED,                
        PTZ_WIPERS
    }
}
