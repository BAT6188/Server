using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AlarmMainframe amf = new AlarmMainframe();
            IPDeviceInfo amfDeviceInfo = new IPDeviceInfo() {
                IPDeviceCode= 1,
                DeviceTypeId = Guid.Parse("a0002016-e009-b019-e001-abcd11000003"),
                Modified = DateTime.Now,
                ModifiedByUserId = Guid.Parse("609955c4-7f32-308b-5343-2eb3ebf373ea"),
                IPDeviceName = "报警主机12",
                OrganizationId = Guid.Parse("b31f22c1-bcd8-4b5a-ad5b-70760a1a9d74"),
            };
            amf.AlarmMainframeId = Guid.NewGuid();
            amf.DeviceInfo = amfDeviceInfo;
            List<AlarmPeripheral> aps = new List<AlarmPeripheral>();
            var alarmDevice1 = new IPDeviceInfo() {
                IPDeviceInfoId = Guid.NewGuid(),
                ModifiedByUserId = Guid.Parse("609955c4-7f32-308b-5343-2eb3ebf373ea"),
                IPDeviceName = "报警主机12",
                OrganizationId = Guid.Parse("b31f22c1-bcd8-4b5a-ad5b-70760a1a9d74"),
                DeviceTypeId = Guid.Parse("a0002016-e009-b019-e001-abcd11000005"),
            };
            aps.Add(new AlarmPeripheral() {
                AlarmChannel = 1,
                AlarmDevice = alarmDevice1,
                DefendArea = 1,
                AlarmPeripheralId = Guid.NewGuid(),
                AlarmMainframeId = amf.AlarmMainframeId,
                AlarmTypeId = Guid.Parse("a0002016-e009-b019-e001-ab1100001103"),
            });
            amf.AlarmPeripherals = aps;
            Console.WriteLine(JsonConvert.SerializeObject(amf));
            Console.ReadLine();

        }
    }
}
