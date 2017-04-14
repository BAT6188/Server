using M2Mqtt.Messages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    public class MQPulish
    {
        static ILogger<MQPulish> _logger = Logger.CreateLogger<MQPulish>();
        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="msgObj">消息对象</param>
        public static void PublishMessage(string topic, object msgObj)
        {
            M2Mqtt.MqttClient client = new M2Mqtt.MqttClient(GlobalSetting.MqBrokerHost);
            client.Connect(Guid.NewGuid().ToString());
            string msg = JsonConvert.SerializeObject(msgObj);
            ushort result = client.Publish(topic, UTF8Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            //client.Disconnect();//关闭可能导致消息推送失败。。抓包数据分析，调用Disconnect可能会在发布消息前就导致链接断开。//后续跟进 2016-11-05
            _logger.LogInformation("连接MQ服务{0}广播消息,Topic:{1} ,Result:{2}\r\nMessage{3}", GlobalSetting.MqBrokerHost, topic, result, msg);
        }
    }
}
