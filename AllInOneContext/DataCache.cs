using AlarmAndPlan.Model;
using Infrastructure.Model;
using PAPS.Model;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllInOneContext
{
    public class DataCache
    {
        public static List<IPDeviceInfo> _cacheDeviceinfoList = new List<IPDeviceInfo>();

        public static List<Encoder> _cacheEncoderList = new List<Encoder>();

        public static List<ServiceInfo> _cacheServiceList = new List<ServiceInfo>();

        public static List<MonitorySite> _cacheMonitorySiteList = new List<MonitorySite>();

        public static List<DeviceGroup> _cacheDevicegroupList = new List<DeviceGroup>();

        public static List<TemplateLayout> _cacheTemplateLayoutList = new List<TemplateLayout>();

        public static List<Sentinel> _cacheSentinelList = new List<Sentinel>();

        public static List<SystemOption> _SystemOptions = new List<SystemOption>();

        public static List<Organization> _Organizations = new List<Organization>();

        public static List<Application> _Applications = new List<Application>();

        public static List<ApplicationSetting> _ApplicationSettings = new List<ApplicationSetting>();
        public static List<AuthorizationInformation> _AuthorizationInformations = new List<AuthorizationInformation>();
        public static List<EventLog> _EventLogs = new List<EventLog>();
        public static List<OnlineUser> _OnlineUsers = new List<OnlineUser>();
        public static List<Permission> _Permissions = new List<Permission>();
        public static List<ResourcesAction> _ResourcesActions = new List<ResourcesAction>();
        public static List<Role> _Roles = new List<Role>();
        public static List<Schedule> _Schedules = new List<Schedule>();
        public static List<Staff> _Staffs = new List<Staff>();
        public static List<StaffGroup> _StaffGroups = new List<StaffGroup>();
        public static List<User> _Users = new List<User>();
        public static List<ApplicationResource> _ApplicationResources = new List<ApplicationResource>();
        public static List<Attachment> _Attachments = new List<Attachment>();
        public static List<ServerInfo> _ServerInfos = new List<ServerInfo>();


        public static List<Circular> _Circulars = new List<Circular>();
        public static List<DailyOnDuty> _DailyOnDutys = new List<DailyOnDuty>();
        public static List<DutyCheckAppraise> _DutyCheckAppraises = new List<DutyCheckAppraise>();
        public static List<DutyCheckGroup> _DutyCheckGroups = new List<DutyCheckGroup>();
        public static List<DutyCheckLog> _DutyCheckLogs = new List<DutyCheckLog>();
        public static List<DutyCheckMatter> _DutyCheckMatters = new List<DutyCheckMatter>();
        public static List<DutyCheckPackage> _DutyCheckPackages = new List<DutyCheckPackage>();
        public static List<DutyCheckPackageTimePlan> _DutyCheckPackageTimePlans = new List<DutyCheckPackageTimePlan>();
        public static List<DutySchedule> _DutySchedules = new List<DutySchedule>();
        public static List<DutyGroupSchedule> _DutyGroupSchedules = new List<DutyGroupSchedule>();
        public static List<Feedback> _Feedbacks = new List<Feedback>();
        public static List<InstitutionsDutyCheckSchedule> _InstitutionsDutyCheckSchedules = new List<InstitutionsDutyCheckSchedule>();
        public static List<ShiftHandoverLog> _ShiftHandoverLogs = new List<ShiftHandoverLog>();
        public static List<TemporaryDuty> _TemporaryDutys = new List<TemporaryDuty>();

        public static List<AlarmPeripheral> _AlarmPeripherals = new List<AlarmPeripheral>();

        public static List<DeviceAlarmMapping> _DeviceAlarmMapping = new List<DeviceAlarmMapping>();

        public static int autoIndex = 1;


        /// <summary>
        /// 组织机构心跳
        /// </summary>
        public static Dictionary<Guid, DateTime> _AllOrganizationHeart = new Dictionary<Guid, DateTime>();

        /// <summary>
        /// 包含直属下级的所有监控点
        /// </summary>
        public static List<MonitorySite> _AllMonitorySite = new List<MonitorySite>();



        static void InitOrganizationNode()
        {
            #region 组织机构类型
            SystemOption organizationTypeOption = new SystemOption()
            {
                Description = "", SystemOptionCode = "100", SystemOptionName = "组织机构",
                SystemOptionId = CreateGuid3("100"),
            };
            SystemOption zhibanshiNode = new SystemOption()
            {
                Description = "", SystemOptionCode = "10000001", SystemOptionName = "值班室", ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000001"),
            };
            SystemOption shaoweiNode = new SystemOption()
            {
                Description = "", SystemOptionCode = "10000002", SystemOptionName = "哨位", ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000002"),
            };
            SystemOption yingquNode = new SystemOption()
            {
                Description = "", SystemOptionCode = "10000003", SystemOptionName = "营区", ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000003"),
            };
            SystemOption jianquNode = new SystemOption()
            {
                Description = "", SystemOptionCode = "10000004", SystemOptionName = "监区", ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000004"),
            };
            SystemOption jiyaoshiNode = new SystemOption()
            {
                Description = "", SystemOptionCode = "10000005", SystemOptionName = "机要室", ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000005"),
            };
            SystemOption junxiekuNode = new SystemOption()
            {
                Description = "", SystemOptionCode = "10000006", SystemOptionName = "军械库", ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000006"),
            };
            SystemOption danyaokuNode = new SystemOption()
            {
                Description = "", SystemOptionCode = "10000007", SystemOptionName = "弹药库", ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000007"),
            };
            SystemOption jiayouzhanNode = new SystemOption()
            {
                Description = "", SystemOptionCode = "10000008", SystemOptionName = "加油站", ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000008"),
            };
            SystemOption chechangNode = new SystemOption()
            {
                Description = "", SystemOptionCode = "10000009", SystemOptionName = "车场", ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000009"),
            };
            SystemOption targgetNode = new SystemOption() //两警联动设备挂载节点
            {
                Description = "",
                SystemOptionCode = "10000010",
                SystemOptionName = "目标单位",
                ParentSystemOption = organizationTypeOption,
                SystemOptionId = CreateGuid8("10000010"),
            };

            _SystemOptions.AddRange(new SystemOption[] {
                organizationTypeOption,chechangNode,
                danyaokuNode, jianquNode,
                jiayouzhanNode, jiyaoshiNode,
                junxiekuNode, shaoweiNode,
                yingquNode, zhibanshiNode,targgetNode
            });
            #endregion

            #region 组织机构
            Organization zhiduiOrg = new Organization()
            {
                OrganizationId = Guid.NewGuid(), 
                //new Guid("B31F22C1-BCD8-4B5A-AD5B-70760A1A9D74"),
                OrganizationFullName = "xx中队",
                OrganizationShortName = "xx中队",
                OrderNo = 1,
                //OrganizationType = organizationTypeOption,
                //OrganizationTypeId = organizationTypeOption.SystemOptionId,
            };
            _Organizations.Add(zhiduiOrg);

            //Organization jinanzhiduiOrg = new Organization()
            //{
            //    OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
            //    OrganizationFullName = "山东省总队.济南支队",
            //    OrganizationShortName = "济南支队",
            //    OrderNo = 2, ParentOrganization = shandongzongduiOrg,
            //    //OrganizationType = organizationTypeOption,
            //    //OrganizationTypeId = organizationTypeOption.SystemOptionId,
            //};

            //Organization yizhongduiOrg = new Organization()
            //{
            //    OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),
            //    OrganizationFullName = "山东省总队.济南支队.一中队",
            //    OrganizationShortName = "一中队", OrderNo = 3, ParentOrganization = jinanzhiduiOrg,
            //    //OrganizationType = organizationTypeOption,
            //    //OrganizationTypeId = organizationTypeOption.SystemOptionId,
            //};

            //Organization zuozhanqinwuzhibanshi = new Organization()
            //{
            //    OrganizationId = new Guid("081D0BB5-770F-44A6-A038-EB1D5F144A96"),
            //    OrganizationFullName = "山东省总队.济南支队.一中队.作战勤务值班室",
            //    OrganizationShortName = "作战勤务值班室",
            //    OrderNo = 3, ParentOrganization = yizhongduiOrg, OrganizationType = zhibanshiNode,
            //    //OrganizationTypeId = zhibanshiNode.SystemOptionId
            //};

            //Organization monitorPoint1 = new Organization
            //{
            //    OrderNo = 4, OrganizationId = new Guid("D37DA08C-1526-4C55-86C9-5BCEF2880285"),
            //    Description = "1号哨",
            //    OrganizationFullName = "山东省总队.济南支队.一中队.1号哨",
            //    OrganizationLevel = 4, OrganizationShortName = "1号哨", OrganizationType = shaoweiNode,
            //    //OrganizationTypeId = shaoweiNode.SystemOptionId,
            //    ParentOrganization = yizhongduiOrg,
            //};

            //_Organizations.Add(monitorPoint1);

            //Organization monitorPoint2 = new Organization
            //{
            //    OrderNo = 6,
            //    OrganizationId = new Guid("39C3B8A5-E08C-4BD0-A516-7B8AC4F38507"),
            //    Description = "2号哨",
            //    OrganizationFullName = "山东省总队.济南支队.一中队.2号哨",
            //    OrganizationLevel = 4,
            //    OrganizationShortName = "2号哨",
            //    OrganizationType = shaoweiNode,
            //    //OrganizationTypeId = shaoweiNode.SystemOptionId,
            //    ParentOrganization = yizhongduiOrg
            //};

            //_Organizations.Add(monitorPoint2);

            //Organization camp = new Organization
            //{
            //    OrderNo = 7,
            //    OrganizationId = new Guid("23A29D12-8A99-4711-A711-C08D7F46E8E0"),
            //    Description = "营区",
            //    OrganizationFullName = "山东省总队.济南支队.一中队.营区",
            //    OrganizationLevel = 4,
            //    OrganizationShortName = "营区",
            //    OrganizationType = yingquNode,
            //    //OrganizationTypeId = yingquNode.SystemOptionId,
            //    ParentOrganization = yizhongduiOrg
            //};
            //_Organizations.AddRange(new Organization[] { shandongzongduiOrg, jinanzhiduiOrg, yizhongduiOrg,
            //    zuozhanqinwuzhibanshi,monitorPoint1, monitorPoint2,camp});
            #endregion
        }


        static void ServicesTypeSystemOptions()
        {
            string baseOptionName = "服务器类型";
            string baseOptionCode = "113";
            string[] servers = new string[] {"网关服务器", "WEB应用服务","媒体转发服务","设备控制代理","媒体存储服务","数字矩阵中心", "哨位中心服务"
            ,"智能分析服务","消息推送","检测代理","媒体点播服务"};
            string[] mappingcodes = new string[] { "5060", "8080", "554","9000", "8900", "9100", "5002", "10098", null, null, "8910" };
            int optionCode = 11300200;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, servers, mappingcodes, optionCode);
        }

        static void DeviceGroupSystemOptions()
        {
            #region  设备分组
            SystemOption deviceGroupOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "109",
                SystemOptionName = "设备分组",
                SystemOptionId = CreateGuid3("109"),
            };

            SystemOption cameraGroupType = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "10900001",
                SystemOptionName = "监控点",
                SystemOptionId = CreateGuid8("10900001"),
                ParentSystemOption = deviceGroupOption
            };

            SystemOption sentinelGroupType = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "10900002",
                SystemOptionName = "哨位台",
                SystemOptionId = CreateGuid8("10900002"),
                ParentSystemOption = deviceGroupOption
            };
            _SystemOptions.Add(deviceGroupOption);
            _SystemOptions.Add(cameraGroupType);
            _SystemOptions.Add(sentinelGroupType);
            #endregion
        }

        static void AlarmTypeSystemOptions()
        {
            #region 报警类型
            SystemOption alarmType = new SystemOption()
            {
                Description = "报警类型",
                SystemOptionCode = "108",
                SystemOptionName = "报警类型",
                SystemOptionId = CreateGuid3("108"),
            };

            SystemOption yidongzhence = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1001",
                SystemOptionName = "移动侦测",
                SystemOptionId = DataCache.CreateGuid8("00001001"),
                ParentSystemOption = alarmType,
                MappingCode = "2"
            };
            SystemOption shipinzhedang = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1002",
                SystemOptionName = "视频遮挡",
                SystemOptionId = DataCache.CreateGuid8("00001002"),
                ParentSystemOption = alarmType,
                MappingCode = "3"
            };
            SystemOption shipindiushi = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1003",
                SystemOptionName = "视频丢失",
                SystemOptionId = DataCache.CreateGuid8("00001003"),
                ParentSystemOption = alarmType,
                MappingCode = "4",
            };
            SystemOption banxian = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1004",
                SystemOptionName = "拌线报警",
                SystemOptionId = DataCache.CreateGuid8("00001004"),
                ParentSystemOption = alarmType,
                MappingCode = "8"
            };
            SystemOption yuejiezhengce = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1009",
                SystemOptionName = "越界侦测",
                SystemOptionId = DataCache.CreateGuid8("00001009"),
                ParentSystemOption = alarmType,
                MappingCode = "9"
            };
            SystemOption mubiaojinru = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1010",
                SystemOptionName = "目标进入区域",
                SystemOptionId = DataCache.CreateGuid8("00001010"),
                ParentSystemOption = alarmType,
                MappingCode = "10",
            };
            SystemOption mubiaolikai = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1011",
                SystemOptionName = "目标离开区域",
                SystemOptionId = DataCache.CreateGuid8("00001011"),
                ParentSystemOption = alarmType,
                MappingCode = "11"
            };
            SystemOption zhoujierujin = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1012",
                SystemOptionName = "周界入侵",
                SystemOptionId = DataCache.CreateGuid8("00001012"),
                ParentSystemOption = alarmType,
                MappingCode = "12",
            };
            SystemOption tingche = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1013",
                SystemOptionName = "停车",
                SystemOptionId = DataCache.CreateGuid8("00001013"),
                ParentSystemOption = alarmType,
                MappingCode = "13"
            };
            SystemOption kuaisuyidong = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1014",
                SystemOptionName = "快速移动",
                SystemOptionId = DataCache.CreateGuid8("00001014"),
                ParentSystemOption = alarmType,
                MappingCode = "14",
            };
            SystemOption quyuneirenyuanjuji = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1015",
                SystemOptionName = "区域内人员聚集",
                SystemOptionId = DataCache.CreateGuid8("00001015"),
                ParentSystemOption = alarmType,
                MappingCode = "15"
            };
            SystemOption wupinyiliunaqu = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1016",
                SystemOptionName = "物品遗留拿取",
                SystemOptionId = DataCache.CreateGuid8("00001016"),
                ParentSystemOption = alarmType,
                MappingCode = "16",
            };
            SystemOption wupinyiliu = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1017",
                SystemOptionName = "物品遗留",
                SystemOptionId = DataCache.CreateGuid8("00001017"),
                ParentSystemOption = alarmType,
                MappingCode = "17",
            };
            SystemOption wupinnaqu = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1018",
                SystemOptionName = "物品拿取",
                SystemOptionId = DataCache.CreateGuid8("00001018"),
                ParentSystemOption = alarmType,
                MappingCode = "18",
            };
            SystemOption paihuai = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1019",
                SystemOptionName = "徘徊",
                SystemOptionId = DataCache.CreateGuid8("00001019"),
                ParentSystemOption = alarmType,
                MappingCode = "19"
            };
            SystemOption xujiao = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1020",
                SystemOptionName = "虚焦",
                SystemOptionId = DataCache.CreateGuid8("00001020"),
                ParentSystemOption = alarmType,
                MappingCode = "20"
            };
            SystemOption changjingbianhuan = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1021",
                SystemOptionName = "场景变换",
                SystemOptionId = DataCache.CreateGuid8("00001021"),
                ParentSystemOption = alarmType,
                MappingCode = "21"
            };

            SystemOption prisonBreakAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "2001",
                SystemOptionName = "越狱",
                SystemOptionId = CreateGuid8("00002001"),
                ParentSystemOption = alarmType,
                MappingCode = "243"
            };
            SystemOption baoyuAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "2002",
                SystemOptionName = "暴狱",
                SystemOptionId = CreateGuid8("00002002"),
                ParentSystemOption = alarmType,
                MappingCode = "242",
            };
            SystemOption xijiAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "2003",
                SystemOptionName = "袭击",
                SystemOptionId = CreateGuid8("00002003"),
                ParentSystemOption = alarmType,
                MappingCode = "244"
            };
            SystemOption zaihaiBreakAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "2004",
                SystemOptionName = "灾害",
                SystemOptionId = CreateGuid8("00002004"),
                ParentSystemOption = alarmType,
                MappingCode = "245"
            };
            SystemOption chongguangAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "2005",
                SystemOptionName = "冲闯",
                SystemOptionId = CreateGuid8("00002005"),
                ParentSystemOption = alarmType,
                MappingCode = "242"
            };

            SystemOption pohuaiAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "2006",
                SystemOptionName = "破坏",
                SystemOptionId = CreateGuid8("00002006"),
                ParentSystemOption = alarmType,
                MappingCode = "243"
            };
            //SystemOption lefthongwaiAlarm = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "2007",
            //    SystemOptionName = "左红外报警",
            //    SystemOptionId = CreateGuid8("00002007"),
            //    ParentSystemOption = alarmType,
            //    MappingCode = "248"
            //};
            //SystemOption leftgaoyaAlarm = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "2009",
            //    SystemOptionName = "左高压报警",
            //    SystemOptionId = CreateGuid8("00002009"),
            //    ParentSystemOption = alarmType,
            //    MappingCode = "250"
            //};
            //SystemOption leftzhalanAlarm = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "2011",
            //    SystemOptionName = "左栅栏报警",
            //    SystemOptionId = CreateGuid8("00002011"),
            //    ParentSystemOption = alarmType,
            //    MappingCode = "246"
            //};
            //SystemOption righthongwaiAlarm = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "2008",
            //    SystemOptionName = "右红外报警",
            //    SystemOptionId = CreateGuid8("00002008"),
            //    ParentSystemOption = alarmType,
            //    MappingCode = "249"
            //};
            //SystemOption rightgaoyaAlarm = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "2010",
            //    SystemOptionName = "右高压报警",
            //    SystemOptionId = CreateGuid8("00002010"),
            //    ParentSystemOption = alarmType,
            //    MappingCode = "251"
            //};
            //SystemOption rightzhalanAlarm = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "2012",
            //    SystemOptionName = "右栅栏报警",
            //    SystemOptionId = CreateGuid8("00002012"),
            //    ParentSystemOption = alarmType,
            //    MappingCode = "247"
            //};
            //SystemOption perimeteralarm = new SystemOption()
            //{
            //    SystemOptionCode = "4009",
            //    SystemOptionName = "周界报警",
            //    SystemOptionId = CreateGuid8("00004009"),
            //    ParentSystemOption = alarmType,
            //};
            SystemOption hongwaiAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "2007",
                SystemOptionName = "红外",
                SystemOptionId = CreateGuid8("00002007"),
                ParentSystemOption = alarmType,
                MappingCode = "248"
            };
            SystemOption gaoyaAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "2009",
                SystemOptionName = "高压",
                SystemOptionId = CreateGuid8("00002009"),
                ParentSystemOption = alarmType,
                MappingCode = "250"
            };
            SystemOption zhalanAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "2011",
                SystemOptionName = "栅栏",
                SystemOptionId = CreateGuid8("00002011"),
                ParentSystemOption = alarmType,
                MappingCode = "246"
            };

            //枪支离岗报警
            SystemOption biaoqiankaqianya = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "4002",
                SystemOptionName = "标签卡欠压",
                SystemOptionId = CreateGuid8("00004002"),
                ParentSystemOption = alarmType,
                MappingCode = "2"
            };
            SystemOption fangchaiAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "4004",
                SystemOptionName = "防拆异常",
                SystemOptionId = CreateGuid8("00004004"),
                ParentSystemOption = alarmType,
                MappingCode = "4"
            };
            SystemOption qiangzhiligangAlarm = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "4008",
                SystemOptionName = "枪支离岗",
                SystemOptionId = CreateGuid8("00004008"),
                ParentSystemOption = alarmType,
                MappingCode = "8"
            };

            _SystemOptions.AddRange(new SystemOption[] { yuejiezhengce,mubiaojinru,mubiaolikai,zhoujierujin,tingche,kuaisuyidong,quyuneirenyuanjuji,
                wupinyiliunaqu,wupinnaqu,wupinyiliu,paihuai,xujiao,changjingbianhuan});
            _SystemOptions.AddRange(new SystemOption[] { alarmType,yidongzhence,shipinzhedang,shipindiushi,banxian, prisonBreakAlarm, baoyuAlarm,
                chongguangAlarm, zaihaiBreakAlarm,xijiAlarm,pohuaiAlarm,hongwaiAlarm,gaoyaAlarm,zhalanAlarm,
                });
            _SystemOptions.AddRange(new SystemOption[] { biaoqiankaqianya, fangchaiAlarm, qiangzhiligangAlarm });
            #endregion
        }

        static void DeviceTypeSystemOptions()
        {
            #region 设备类型
            SystemOption deviceTypeOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "110",
                SystemOptionName = "设备类型",
                SystemOptionId = CreateGuid3("110"),
            };

            SystemOption cameraDeviceType = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000000",
                SystemOptionName = "摄像机",
                SystemOptionId = CreateGuid8("11000000"),
                ParentSystemOption = deviceTypeOption
            };

            SystemOption gunCameraType = new SystemOption()
            {
                Description = "枪机",
                SystemOptionCode = "1100000001",
                SystemOptionName = "枪机",
                SystemOptionId = DataCache.CreateGuid10("1100000001"),
                ParentSystemOption = cameraDeviceType
            };

            SystemOption ptzCameraType = new SystemOption()
            {
                Description = "球机",
                SystemOptionCode = "1100000002",
                SystemOptionName = "球机",
                SystemOptionId = CreateGuid10("1100000002"),
                ParentSystemOption = cameraDeviceType
            };
            _SystemOptions.AddRange(new SystemOption[] { gunCameraType, ptzCameraType });

            SystemOption encoderDeivcetypeOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000001",
                SystemOptionName = "编码器",
                SystemOptionId = CreateGuid8("11000001"),
                ParentSystemOption = deviceTypeOption
            };
            SystemOption dncoderDeivcetypeOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000002",
                SystemOptionName = "解码器",
                SystemOptionId = CreateGuid8("11000002"),
                ParentSystemOption = deviceTypeOption
            };
            SystemOption alarmMachineDeivcetypeOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000003",
                SystemOptionName = "报警主机",
                SystemOptionId = CreateGuid8("11000003"),
                ParentSystemOption = deviceTypeOption
            };

            SystemOption qiangzhiligang = new SystemOption()
            {
                Description = "枪支离岗报警主机",
                SystemOptionCode = "1100000301",
                SystemOptionName = "枪支离岗报警主机",
                SystemOptionId = CreateGuid10("1100000301"),
                ParentSystemOption = alarmMachineDeivcetypeOption,
                MappingCode="0"
            };

            SystemOption normalBaojingzhuji = new SystemOption()
            {
                Description = "防区报警主机",
                SystemOptionCode = "1100000302",
                SystemOptionName = "防区报警主机",
                SystemOptionId = CreateGuid10("1100000302"),
                ParentSystemOption = alarmMachineDeivcetypeOption,
                MappingCode = "1"
            };

            SystemOption sentinelDeivcetypeOption = new SystemOption()
            {
                Description = "哨位台",
                SystemOptionCode = "11000004",
                SystemOptionName = "哨位台",
                SystemOptionId = CreateGuid8("11000004"),
                ParentSystemOption = deviceTypeOption
            };

            //智能哨类型
            SystemOption zhinengExpandSentinelType = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1100000401",
                SystemOptionName = "智能哨位信息化终端（拓展型）",
                SystemOptionId = CreateGuid10("1100000401"),
                ParentSystemOption = sentinelDeivcetypeOption,
                MappingCode = "1"
            };

            SystemOption zhinengSimpleSentinelType = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1100000402",
                SystemOptionName = "智能哨位信息化终端（简约型）",
                SystemOptionId = CreateGuid10("1100000402"),
                ParentSystemOption = sentinelDeivcetypeOption,
                MappingCode = "2"
            };

            SystemOption jingweiExpandSentinelType = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1100000403",
                SystemOptionName = "智能警卫信息化终端（拓展型）",
                SystemOptionId = CreateGuid10("1100000403"),
                ParentSystemOption = sentinelDeivcetypeOption,
                MappingCode = "3"
            };

            SystemOption jingweiSimpleSentinelType = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1100000404",
                SystemOptionName = "智能警卫信息化终端（简约型）",
                SystemOptionId = CreateGuid10("1100000404"),
                ParentSystemOption = sentinelDeivcetypeOption,
                MappingCode = "4"
            };
            SystemOption jichengxiangExpandSentinelType = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1100000405",
                SystemOptionName = "哨位集成箱（拓展型）",
                SystemOptionId = CreateGuid10("1100000405"),
                ParentSystemOption = sentinelDeivcetypeOption,
                MappingCode = "5"
            };

            SystemOption jichengxiangSimpleSentinelType = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1100000406",
                SystemOptionName = "哨位集成箱（简约型）",
                SystemOptionId = CreateGuid10("1100000406"),
                ParentSystemOption = sentinelDeivcetypeOption,
                MappingCode = "6"
            };

            SystemOption alarmPeripheralDeivcetypeOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000005",
                SystemOptionName = "报警外设",
                SystemOptionId = CreateGuid8("11000005"),
                ParentSystemOption = deviceTypeOption
            };
            _SystemOptions.AddRange(new SystemOption[] {
                deviceTypeOption,
                cameraDeviceType,
                encoderDeivcetypeOption,
                alarmMachineDeivcetypeOption,
                sentinelDeivcetypeOption,
                zhinengExpandSentinelType, zhinengSimpleSentinelType,
                jingweiExpandSentinelType, jingweiSimpleSentinelType,
                jichengxiangExpandSentinelType, jichengxiangSimpleSentinelType,alarmPeripheralDeivcetypeOption
            });

            SystemOption shengguang = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000006",
                SystemOptionName = "声光报警",
                SystemOptionId = DataCache.CreateGuid8("11000006"),
                ParentSystemOption = deviceTypeOption
            };

            SystemOption jingqing = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000007",
                SystemOptionName = "警情发布",
                SystemOptionId = DataCache.CreateGuid8("11000007"),
                ParentSystemOption = deviceTypeOption
            };

            #region IPdevice type
            SystemOption hujiaozhan = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000008",
                SystemOptionName = "对讲呼叫站",
                SystemOptionId = CreateGuid8("11000008"),
                ParentSystemOption = deviceTypeOption
            };

            SystemOption yijianqiuzhu = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000009",
                SystemOptionName = "一键求助器",
                SystemOptionId = CreateGuid8("11000009"),
                ParentSystemOption = deviceTypeOption
            };
            SystemOption yinpinbofang = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000010",
                SystemOptionName = "音频播放器",
                SystemOptionId = CreateGuid8("11000010"),
                ParentSystemOption = deviceTypeOption
            };

            SystemOption defenseDeviceOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000011",
                SystemOptionName = "防区设备",
                SystemOptionId = CreateGuid8("11000011"),
                ParentSystemOption = deviceTypeOption
            };
            //SystemOption lefthongwaiOption = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "1100001101",
            //    SystemOptionName = "左红外",
            //    SystemOptionId = CreateGuid10("1100001101"),
            //    ParentSystemOption = defenseDeviceOption,
            //    MappingCode = "13"
            //};
            //SystemOption leftgaoyaOption = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "1100001102",
            //    SystemOptionName = "左高压",
            //    SystemOptionId = CreateGuid10("1100001102"),
            //    ParentSystemOption = defenseDeviceOption,
            //    MappingCode = "17"
            //};
            //SystemOption leftzhalanOption = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "1100001103",
            //    SystemOptionName = "左栅栏",
            //    SystemOptionId = CreateGuid10("1100001103"),
            //    ParentSystemOption = defenseDeviceOption,
            //    MappingCode = "5"
            //};
            //SystemOption righthongwaiOption = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "1100001104",
            //    SystemOptionName = "右红外",
            //    SystemOptionId = CreateGuid10("1100001104"),
            //    ParentSystemOption = defenseDeviceOption,
            //    MappingCode = "11"
            //};
            //SystemOption rightgaoyaOption = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "1100001105",
            //    SystemOptionName = "右高压",
            //    SystemOptionId = CreateGuid10("1100001105"),
            //    ParentSystemOption = defenseDeviceOption,
            //    MappingCode = "15"
            //};
            //SystemOption rightzhalanOption = new SystemOption()
            //{
            //    Description = "",
            //    SystemOptionCode = "1100001106",
            //    SystemOptionName = "右栅栏",
            //    SystemOptionId = CreateGuid10("1100001106"),
            //    ParentSystemOption = defenseDeviceOption,
            //    MappingCode = "3"
            //};
            SystemOption hongwaiOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1100001101",
                SystemOptionName = "红外",
                SystemOptionId = CreateGuid10("1100001101"),
                ParentSystemOption = defenseDeviceOption,
                MappingCode = "13"
            };
            SystemOption gaoyaOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1100001102",
                SystemOptionName = "高压",
                SystemOptionId = CreateGuid10("1100001102"),
                ParentSystemOption = defenseDeviceOption,
                MappingCode = "17"
            };
            SystemOption zhalanOption = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "1100001103",
                SystemOptionName = "栅栏",
                SystemOptionId = CreateGuid10("1100001103"),
                ParentSystemOption = defenseDeviceOption,
                MappingCode = "5"
            };
          
            _SystemOptions.AddRange(new SystemOption[] {qiangzhiligang , normalBaojingzhuji, shengguang,jingqing, defenseDeviceOption, hongwaiOption, gaoyaOption, zhalanOption});
            _SystemOptions.Add(hujiaozhan);
            _SystemOptions.Add(yijianqiuzhu);
            _SystemOptions.Add(yinpinbofang);

            SystemOption doubleLinkage = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "11000012",
                SystemOptionName = "两警联动",
                SystemOptionId = CreateGuid8("11000012"),
                ParentSystemOption = deviceTypeOption
            };
            _SystemOptions.Add(doubleLinkage);
            #endregion
            #endregion
        }
        static void InitSystemOptions()
        {
            ServicesTypeSystemOptions();

            DeviceGroupSystemOptions();

            DeviceTypeSystemOptions();

            AlarmTypeSystemOptions();

            #region 视频模板
            SystemOption layoutType = new SystemOption()
            {
                Description = "模板布局",SystemOptionCode = "114",SystemOptionId = CreateGuid3("114"), SystemOptionName = "模板布局",
            };

            SystemOption standarLayoutType = new SystemOption()
            {
                Description = "标准分割",SystemOptionCode = "11400001",SystemOptionId =CreateGuid8("11400001"),SystemOptionName = "标准分割",ParentSystemOption = layoutType
            };

            SystemOption wideScreenLayoutType = new SystemOption()
            {
                Description = "宽屏分割",SystemOptionCode = "11400002", SystemOptionId = CreateGuid8("11400002"), SystemOptionName = "宽屏分割", ParentSystemOption = layoutType
            };

            SystemOption userDefinedLayoutType = new SystemOption()
            {
                Description = "",SystemOptionCode = "11400003",SystemOptionId = CreateGuid8("11400003"),SystemOptionName = "自定义分割",ParentSystemOption = layoutType
            };

            SystemOption templateType = new SystemOption()
            {
                Description = "模板类型", SystemOptionCode = "115",SystemOptionId =CreateGuid3("115"),SystemOptionName = "模板类型",
            };

            SystemOption tvTemplateType = new SystemOption()
            {
                Description = "", SystemOptionCode = "11500001",SystemOptionId = CreateGuid8("11500001"),SystemOptionName = "上墙模板", ParentSystemOption = templateType
            };

            SystemOption videoTemplateType = new SystemOption()
            {
                Description = "",SystemOptionCode = "11500002",SystemOptionId = CreateGuid8("11500002"),SystemOptionName = "预览模板",ParentSystemOption = templateType
            };
            _SystemOptions.AddRange(new SystemOption[] {
                layoutType, standarLayoutType, wideScreenLayoutType, userDefinedLayoutType,
                templateType, tvTemplateType, videoTemplateType
            });
            #endregion

            #region 报警级别
            SystemOption alarmLevelType = new SystemOption()
            {
                Description = "报警级别",
                SystemOptionCode = "129",
                SystemOptionName = "报警级别",
                SystemOptionId = CreateGuid3("129"),
            };

            SystemOption alarmHighLevel = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "12900001",
                SystemOptionName = "高",
                SystemOptionId = CreateGuid8("12900001"),
                ParentSystemOption = alarmLevelType
            };
            SystemOption alarmNormalLevel = new SystemOption()
            {
                Description = "",
                SystemOptionCode = "12900002",
                SystemOptionName = "普通",
                SystemOptionId = CreateGuid8("12900002"),
                ParentSystemOption = alarmLevelType
            };
            _SystemOptions.AddRange(new SystemOption[] { alarmLevelType, alarmHighLevel, alarmNormalLevel });
            #endregion

            #region 预案动作
            string baseOptionName = "预案动作";
            string baseOptionCode = "130";
            string[] options = new string[] { "预览视频", "录像", "视频上墙", "截图", "语音提示", "地图定位","对讲",
                "打开子弹箱","群呼", "警情发布", "开启喊话", "鸣枪警告","推送文字","播报音频","开启警灯"};
            int optionCode = 13000001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, optionCode);
            #endregion

            #region 报警状态
            baseOptionName = "报警状态";
            baseOptionCode = "131";
            options = new string[] { "未确认", "已确认", "已取消","已关闭" };
            optionCode = 13100001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, optionCode);
            #endregion


            baseOptionName = "排程";
             baseOptionCode = "132";
            options = new string[] { "报警布防", "勤务排程", "值班排程","定时任务" };
             optionCode = 13200001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, optionCode);

            baseOptionName = "勤务交班状态";
            baseOptionCode = "122";
            options = new string[] { "交班正常", "交班异常"};
            optionCode = 12200001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, optionCode);

            baseOptionName = "报警拨号";
            baseOptionCode = "147";
            options = new string[] { "都不拨打", "哨位对讲", "内线", "都拨打" };
            string[] mappingcodes = new string[] { "0", "1", "2", "3" };
            optionCode = 14700001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, mappingcodes,optionCode);


            #region 性别
            SystemOption sexOption = new SystemOption()
            {
                SystemOptionId = CreateGuid3("133"),
                SystemOptionCode = "133",
                SystemOptionName = "性别",
            };
            SystemOption maleOption = new SystemOption()
            {
                SystemOptionId = CreateGuid8("13300001"),
                SystemOptionCode = "13300001",
                SystemOptionName = "男",
                ParentSystemOption = sexOption
            };
            SystemOption femaleOption = new SystemOption()
            {
                SystemOptionId = CreateGuid8("13300002"),
                SystemOptionCode = "13300002",
                SystemOptionName = "女",
                ParentSystemOption = sexOption
            };
            _SystemOptions.Add(sexOption);
            _SystemOptions.Add(femaleOption); _SystemOptions.Add( maleOption);
            #endregion

            #region 周期
            SystemOption cycleOption = new SystemOption()
            {
                SystemOptionId = CreateGuid3("137"),
                SystemOptionCode = "137",
                SystemOptionName = "周期",
            };
            SystemOption dayCycle = new SystemOption()
            {

                SystemOptionId = CreateGuid8("13700001"),
                SystemOptionCode = "13700001",
                SystemOptionName = "每天",
                ParentSystemOption = cycleOption
            };
            SystemOption weekCycle = new SystemOption()
            {

                SystemOptionId = CreateGuid8("13700002"),
                SystemOptionCode = "13700002",
                SystemOptionName = "周",
                ParentSystemOption = cycleOption
            };
            SystemOption monthCycle = new SystemOption()
            {

                SystemOptionId = CreateGuid8("13700003"),
                SystemOptionCode = "13700003",
                SystemOptionName = "月",
                ParentSystemOption = cycleOption
            };
            _SystemOptions.Add(cycleOption);
            _SystemOptions.Add(dayCycle);
            _SystemOptions.Add(weekCycle);
            _SystemOptions.Add(monthCycle);
            #endregion

            #region 查勤状态
            SystemOption dutyCheckStatusOption = new SystemOption()
            {
                SystemOptionId = new Guid("9F11B800-AB40-49B0-B14B-9CFF0D6502AD"),
                SystemOptionCode = "160",
                SystemOptionName = "查勤状态",
            };
            SystemOption duty1 = new SystemOption()
            {
                SystemOptionId = new Guid("361ADFE9-E58A-4C88-B191-B742CC212443"),
                SystemOptionCode = "16000001",
                SystemOptionName = "未开始",
                ParentSystemOption = dutyCheckStatusOption
            };
            SystemOption duty2 = new SystemOption()
            {
                SystemOptionId = new Guid("E07A7F37-031A-4834-BAE3-8276E162DA51"),
                SystemOptionCode = "16000002",
                SystemOptionName = "已获取",
                ParentSystemOption = dutyCheckStatusOption
            };
            SystemOption duty3 = new SystemOption()
            {
                SystemOptionId =new Guid("24AC9875-C463-47B6-8147-5845874C3CAF"),
                SystemOptionCode = "16000003",
                SystemOptionName = "已结束",
                ParentSystemOption = dutyCheckStatusOption
            };
            SystemOption duty4 = new SystemOption()
            {
                SystemOptionId = new Guid("124A8562-EAC8-4C09-8758-A6E312974552"),
                SystemOptionCode = "16000004",
                SystemOptionName = "已作废",
                ParentSystemOption = dutyCheckStatusOption
            };
            _SystemOptions.Add(dutyCheckStatusOption);
            _SystemOptions.Add(duty1);
            _SystemOptions.Add(duty2);
            _SystemOptions.Add(duty3);
            _SystemOptions.Add(duty4);
            #endregion

            #region 查勤类型
             baseOptionName = "查勤类型";
             baseOptionCode = "180";
            options = new string[] { "网络查勤", "实地查勤"};
             optionCode = 18000001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, optionCode);
            #endregion

            #region 设备状态
             baseOptionName = "设备状态";
             baseOptionCode = "138";
             options = new string[] { "离线", "无视频", "视频正常", "在线","删除" };
             mappingcodes = new string[] {"-1","1","0","2","-2" };
             optionCode = 13800001;
             NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, mappingcodes,optionCode);
            #endregion

            #region 终端类型
            SystemOption terminalType = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("107"),
                SystemOptionCode = "107",
                SystemOptionName = "用户终端类型",
            };
            SystemOption pc = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("10700001"),
                SystemOptionCode = "10700001",
                SystemOptionName = "PC端",
                ParentSystemOption = terminalType
            };
            SystemOption mobile = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("10700002"),
                SystemOptionCode = "10700002",
                SystemOptionName = "移动端",
                ParentSystemOption = terminalType
            };
            _SystemOptions.Add(terminalType);
            _SystemOptions.Add(pc);
            _SystemOptions.Add(mobile);
            #endregion

            #region 防区
            SystemOption defenceDirection = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("139"),
                SystemOptionCode = "139",
                SystemOptionName = "防区方向",
            };
            SystemOption horizotion = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("13900001"),
                SystemOptionCode = "13900001",
                SystemOptionName = "左防区",
                ParentSystemOption = defenceDirection,
                MappingCode="1"
            };
            SystemOption vertical = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("13900002"),
                SystemOptionCode = "13900002",
                SystemOptionName = "右防区",
                ParentSystemOption = defenceDirection,
                MappingCode = "0"
            };
            _SystemOptions.AddRange(new SystemOption[] { defenceDirection, horizotion, vertical });
            #endregion

            #region 预案类型
            SystemOption planOptions = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("140"),
                SystemOptionCode = "140",
                SystemOptionName = "预案类型",
            };
            SystemOption normalPlan = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14000001"),
                SystemOptionCode = "14000001",
                SystemOptionName = "普通预案",
                ParentSystemOption = planOptions
            };
            SystemOption alarmPlan = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14000002"),
                SystemOptionCode = "14000002",
                SystemOptionName = "报警预案",
                ParentSystemOption = planOptions
            };
            _SystemOptions.Add(normalPlan);
            _SystemOptions.Add(planOptions);
            _SystemOptions.Add(alarmPlan);
            #endregion

            #region 子弹箱状态
            SystemOption cartidgeStatusOption = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("141"),
                SystemOptionCode = "141",
                SystemOptionName = "子弹箱状态",
            };
            SystemOption applicationOpen = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14100001"),
                SystemOptionCode = "14100001",
                SystemOptionName = "供弹申请",
                ParentSystemOption = cartidgeStatusOption
            };
            SystemOption cartidgeOpen = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14100002"),
                SystemOptionCode = "14100002",
                SystemOptionName = "打开",
                ParentSystemOption = cartidgeStatusOption
            };
            SystemOption cartidgeClose = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14100003"),
                SystemOptionCode = "14100003",
                SystemOptionName = "关闭",
                ParentSystemOption = cartidgeStatusOption
            };
            SystemOption cartidgeCloseTimeout = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14100004"),
                SystemOptionCode = "14100004",
                SystemOptionName = "超时未关闭",
                ParentSystemOption = cartidgeStatusOption
            };
            _SystemOptions.AddRange(new SystemOption[] { cartidgeStatusOption, applicationOpen, cartidgeOpen, cartidgeClose, cartidgeCloseTimeout });
            #endregion

            #region 哨位告警
            SystemOption sentinelWarning = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("142"),
                SystemOptionCode = "142",
                SystemOptionName = "哨位告警"
            };
            SystemOption voiceWarning = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14200001"),
                SystemOptionCode = "14200001",
                SystemOptionName = "语音警告",
                ParentSystemOption = sentinelWarning,
                MappingCode ="160"
            };
            SystemOption hanhuaWarning = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14200002"),
                SystemOptionCode = "14200002",
                SystemOptionName = "哨兵喊话",
                ParentSystemOption = sentinelWarning,
                MappingCode = "162"
            };
            SystemOption mingqiangWarning = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14200003"),
                SystemOptionCode = "14200003",
                SystemOptionName = "鸣枪警告",
                ParentSystemOption = sentinelWarning,
                MappingCode = "161"
            };
            SystemOption keshuiWarning = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14200004"),
                SystemOptionCode = "14200004",
                SystemOptionName = "防瞌睡反馈",
                ParentSystemOption = sentinelWarning,
                MappingCode = "163"
            };
            SystemOption powerstatus = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14200005"),
                SystemOptionCode = "14200005",
                SystemOptionName = "电源状态改变",
                ParentSystemOption = sentinelWarning,
                MappingCode = "164"
            };
            SystemOption zidanxiangnotclose = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14200006"),
                SystemOptionCode = "14200006",
                SystemOptionName = "子弹箱长时间被打开",
                ParentSystemOption = sentinelWarning,
                MappingCode = "165"
            };
            _SystemOptions.AddRange(new SystemOption[] { sentinelWarning, voiceWarning, hanhuaWarning, mingqiangWarning, keshuiWarning,
                powerstatus,zidanxiangnotclose
            });
            #endregion

            #region 场景方式
            SystemOption roundScene = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("124"),
                SystemOptionCode = "124",
                SystemOptionName = "场景",
            };
            SystemOption tvRoundScene  = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("12400001"),
                SystemOptionCode = "12400001",
                SystemOptionName = "视频上墙",
                ParentSystemOption = roundScene
            };
            SystemOption realRoundScene = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("12400002"),
                SystemOptionCode = "12400002",
                SystemOptionName = "视频预览",
                ParentSystemOption = roundScene
            };
            _SystemOptions.AddRange(new SystemOption[] { roundScene,realRoundScene,tvRoundScene});
            #endregion

            #region 查哨换岗
            SystemOption chashaohuangang = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("143"),
                SystemOptionCode = "143",
                SystemOptionName = "查哨换岗",
            };
            SystemOption chashao = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14300001"),
                SystemOptionCode = "14300001",
                SystemOptionName = "查哨",
                ParentSystemOption = chashaohuangang,
                MappingCode="0",
            };
            SystemOption huangang = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14300002"),
                SystemOptionCode = "14300002",
                SystemOptionName = "换岗",
                ParentSystemOption = chashaohuangang,
                MappingCode = "3",
            };
            SystemOption shangshao = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14300003"),
                SystemOptionCode = "14300003",
                SystemOptionName = "上哨",
                ParentSystemOption = chashaohuangang,
                MappingCode = "1",
            };
            SystemOption xiashao = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14300004"),
                SystemOptionCode = "14300004",
                SystemOptionName = "下哨",
                ParentSystemOption = chashaohuangang,
                MappingCode = "2",
            };
            _SystemOptions.AddRange(new SystemOption[] { chashaohuangang, chashao, huangang, xiashao, shangshao });
            #endregion

            #region 管控资源
            SystemOption guankongziyuan = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("144"),
                SystemOptionCode = "144",
                SystemOptionName = "管控资源",
            };
            SystemOption jiankongdian  = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14400001"),
                SystemOptionCode = "14400001",
                SystemOptionName = "监控点",
                ParentSystemOption = guankongziyuan
            };
            _SystemOptions.Add(guankongziyuan);
            _SystemOptions.Add(jiankongdian);
            #endregion

            #region 实地查勤评价
            SystemOption shidichaqinpingjia = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("145"),
                SystemOptionCode = "145",
                SystemOptionName = "实地查勤评价",
            };
            SystemOption hao = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14500001"),
                SystemOptionCode = "14500001",
                SystemOptionName = "好",
                ParentSystemOption = shidichaqinpingjia,
                MappingCode = "1",
            };
            SystemOption zhong = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14500002"),
                SystemOptionCode = "14500002",
                SystemOptionName = "中",
                ParentSystemOption = shidichaqinpingjia,
                MappingCode = "2",
            };
            SystemOption cha = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14500003"),
                SystemOptionCode = "14500003",
                SystemOptionName = "差",
                ParentSystemOption = shidichaqinpingjia,
                MappingCode = "3",
            };
            _SystemOptions.AddRange(new SystemOption[] { shidichaqinpingjia,hao,zhong,cha});
            #endregion

            #region 换岗结果
            SystemOption huangangqingkuang = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid3("146"),
                SystemOptionCode = "146",
                SystemOptionName = "换岗结果",
            };
            SystemOption zhengchange = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14600001"),
                SystemOptionCode = "14600001",
                SystemOptionName = "正常",
                ParentSystemOption = huangangqingkuang,
                MappingCode = "1",
            };
            SystemOption yichang = new SystemOption()
            {
                SystemOptionId = DataCache.CreateGuid8("14600002"),
                SystemOptionCode = "14600002",
                SystemOptionName = "异常",
                ParentSystemOption = huangangqingkuang,
                MappingCode = "2",
            };
            _SystemOptions.AddRange(new SystemOption[] { huangangqingkuang, zhengchange, yichang });
            #endregion

            baseOptionName = "勤务类型";
            baseOptionCode = "181";
            options = new string[] { "警卫", "看押", "看守", "守卫", "守护", "巡逻" };
            optionCode = 18100001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, optionCode);
        }

        //static void InitIPDevice()
        //{
        //    Organization yizhongdui = _Organizations[3]; //_Organizations.First(t => t.OrganizationId.Equals("37010101"));
        //    SystemOption encoderDeivcetypeOption = _SystemOptions.First(t => t.SystemOptionCode.Equals("11000001"));
        //    SystemOption zhinengExpandSentinelType = _SystemOptions.First(t => t.SystemOptionCode.Equals("1100000401"));
        //    User user = _Users[0];
        //    //new User() {
        //    //    UserId = Guid.NewGuid(),
        //    //    UserName = "admin",
        //    //    PasswordHash = "admin",
        //    //    Organization = yizhongdui,
        //    //    Application = _Applications.FirstOrDefault(),
        //    //};

        //    IPDeviceInfo yihaoshao = new IPDeviceInfo()
        //    {
        //        IPDeviceInfoId = Guid.NewGuid(), //"34010000005000000001",
        //        IPDeviceName = "一号哨",
        //        IPDeviceCode = 1,
        //        Organization = yizhongdui,
        //        EndPoints = new List<EndPointInfo>(new EndPointInfo[] {
        //               new EndPointInfo() {
        //                   IPAddress = "192.168.20.5",
        //                   Port = 6000
        //               }
        //        }),
        //        Modified = DateTime.Now,
        //        DeviceType = zhinengExpandSentinelType,
        //        //ModifiedByUser = user
        //    };

        //    IPDeviceInfo erhaoshao = new IPDeviceInfo()
        //    {
        //        IPDeviceInfoId = Guid.NewGuid(), //"34010000005000000002",
        //        IPDeviceName = "二号哨",
        //        IPDeviceCode = 1,
        //        Organization = yizhongdui,
        //        EndPoints = new List<EndPointInfo>(new EndPointInfo[] {
        //               new EndPointInfo() {
        //                   IPAddress = "192.168.20.6",
        //                   Port = 6000
        //               }
        //        }),
        //        Modified = DateTime.Now,
        //        DeviceType = zhinengExpandSentinelType,
        //        //ModifiedByUser = user
        //    };

        //    IPDeviceInfo shengguang1 =
        //        new IPDeviceInfo()
        //        {
        //            IPDeviceInfoId = Guid.NewGuid(),
        //            IPDeviceName = "声光报警设备1",
        //            Modified = DateTime.Now,
        //            Organization = yizhongdui,
        //            Password = "123456",
        //            UserName = "admin",
        //            EndPoints = new List<EndPointInfo>(new EndPointInfo[] {
        //               new EndPointInfo() {
        //                    IPAddress = "192.168.20.101", Port = 8000
        //               }
        //       }),
        //            DeviceType = _SystemOptions.First(t => t.SystemOptionCode.Equals("11000006")),
        //            IPDeviceCode = 22,
        //            //ModifiedByUser = user,
        //        };

        //    IPDeviceInfo jingqing2 =
        //           new IPDeviceInfo()
        //           {
        //               IPDeviceInfoId = Guid.NewGuid(), //"34010000001130000002",
        //               IPDeviceName = "警情设备2",
        //               Modified = DateTime.Now,
        //               Organization = yizhongdui,
        //               Password = "123456",
        //               UserName = "admin",
        //               EndPoints = new List<EndPointInfo>(new EndPointInfo[] {
        //               new EndPointInfo() {
        //                   IPAddress = "192.168.20.129",Port = 8000
        //               }
        //          }),
        //               DeviceType = _SystemOptions.FirstOrDefault(t => t.SystemOptionCode.Equals("11000007")),
        //               IPDeviceCode = 23,
        //               //ModifiedByUser =user,
        //           };
        //    _cacheDeviceinfoList.Add(yihaoshao);
        //    _cacheDeviceinfoList.Add(erhaoshao);
        //    _cacheDeviceinfoList.Add(jingqing2);
        //    _cacheDeviceinfoList.Add(shengguang1);
        //}

        //static void InitEncoder()
        //{
        //    Organization yizhongdui = _Organizations[3];
        //    SystemOption encoderDeivcetypeOption = _SystemOptions.First(t => t.SystemOptionCode.Equals("11000001"));
        //    SystemOption zhinengExpandSentinelType = _SystemOptions.First(t => t.SystemOptionCode.Equals("1100000401"));
        //    Guid EncoderTypeId = _EncoderTypes.First(t => t.EncoderCode == 102005).EncoderTypeId;
         
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int lastip = 60 + i;
        //        IPDeviceInfo encoder2 =
        //             new IPDeviceInfo()
        //             {
        //                 IPDeviceInfoId = Guid.NewGuid(),
        //                 IPDeviceName = "编码器-192.168.20." + lastip,
        //                 Modified = DateTime.Now,
        //                 Organization = yizhongdui,
        //                 Password = "123456",
        //                 UserName = "admin",
        //                 EndPoints = new List<EndPointInfo>(new EndPointInfo[] {
        //               new EndPointInfo() {
        //                    IPAddress = "192.168.20." + lastip, Port = 8000
        //               }
        //            }),
        //                 DeviceType = encoderDeivcetypeOption,
        //                 IPDeviceCode = -1,
        //                 //ModifiedByUser = _Users[0]
        //             };
        //        _cacheEncoderList.Add(new Encoder()
        //        {
        //            EncoderId = Guid.NewGuid(), //"34010000001130000002",
        //            EncoderTypeId = EncoderTypeId,
        //            DeviceInfo = encoder2
        //        });
        //    }
        //}

        //static void InitServer()
        //{
        //    Organization yizhongdui = _Organizations[2];//.First(t => t.OrganizationId.Equals("37010101"));
        //    SystemOption vfsOption = _SystemOptions.First(t => t.SystemOptionCode.Equals("11300202"));
        //    SystemOption vssOption = _SystemOptions.First(t => t.SystemOptionCode.Equals("11300204"));
        //    SystemOption dvmOption = _SystemOptions.First(t => t.SystemOptionCode.Equals("11300205"));

        //    User user = new User()
        //    {
        //        UserId = Guid.NewGuid(),
        //        UserName = "test",
        //        PasswordHash = "test",
        //        Organization = yizhongdui,
        //        Application = _Applications.FirstOrDefault(),
        //    };

        //    ServerInfo appServer = new ServerInfo() {
        //        ServerInfoId = Guid.NewGuid(),
        //        ServerName = "测试服务器",
        //        EndPoints = new List<EndPointInfo>(new EndPointInfo[] { new EndPointInfo() {
        //                IPAddress = "192.168.20.229",
        //                Port = 0
        //            } }),
        //        Organization = yizhongdui,
        //        Modified = DateTime.Now,
        //        ModifiedBy = user
        //    };

        //    _cacheServiceList.Add(new ServiceInfo()
        //    {
        //        ServiceInfoId = Guid.NewGuid(), //"34010000002020000001",
        //            ServerInfo = appServer,
        //            Modified = DateTime.Now,
        //            ModifiedBy = user,
        //            Password = "admin",
        //            Username = "admin",
        //            ServiceName = "转发服务器1",
        //            ServiceType = vfsOption,
        //        EndPoints = new List<EndPointInfo>(new EndPointInfo[] { new EndPointInfo() {
        //                IPAddress = "192.168.20.229",
        //                Port = 8800
        //            } }),
        //    });

        //    _cacheServiceList.Add(new ServiceInfo()
        //    {
        //        ServiceInfoId = Guid.NewGuid(), //"34010000001180000001",
        //        ServiceName = "视频存储服务器1",
        //        Modified = DateTime.Now,
        //        ModifiedBy = user,
        //        Password = "admin",
        //        Username = "admin",
        //        ServiceType = vssOption,
        //        EndPoints = new List<EndPointInfo>(new EndPointInfo[] { new EndPointInfo() {
        //                IPAddress = "192.168.20.229",
        //                Port = 8090
        //            } }),
        //        ServerInfo = appServer
        //    });

        //    _cacheServiceList.Add(new ServiceInfo()
        //    {
        //        ServiceInfoId = Guid.NewGuid(), //"34010000001180000001",
        //        ServiceName = "矩阵服务器",
        //        Modified = DateTime.Now,
        //        ModifiedBy = user,
        //        Password = "admin",
        //        Username = "admin",
        //        ServiceType = dvmOption,
        //        EndPoints = new List<EndPointInfo>(new EndPointInfo[] { new EndPointInfo() {
        //                IPAddress = "192.168.20.229",
        //                Port = 9100
        //            } }),
        //        ServerInfo = appServer
        //    });
        //}

        //static void InitMonitorySite()
        //{
        //    Organization yizhongdui = _Organizations[4];//.First(t => t.OrganizationId.Equals("37010101"));
        //    Organization zuozhanqinwuzhibanshi = _Organizations[4];//_Organizations.First(t => t.OrganizationId.Equals("3701010101"));
        //    SystemOption ptzOption = _SystemOptions.First(t => t.SystemOptionCode.Equals("1100000001"));
        //    SystemOption gunOption = _SystemOptions.First(t => t.SystemOptionCode.Equals("1100000002"));
        //    ServiceInfo vfs = _cacheServiceList.FirstOrDefault(t => t.ServiceType.SystemOptionCode.Equals("11300202"));


        //    for (int i = 1; i < 10; i++)
        //    {
        //        Encoder encoder = _cacheEncoderList[i - 1];
        //        IPDeviceInfo cameraDevice1 = new IPDeviceInfo()
        //        {
        //            IPDeviceInfoId = Guid.NewGuid(),
        //            IPDeviceName = i + "号哨",
        //            Organization = encoder.DeviceInfo.Organization,
        //            DeviceType = gunOption,
        //            //ModifiedByUser = encoder.DeviceInfo.ModifiedByUser
        //        };
        //        Camera cam1 = new Camera()
        //        {
        //            CameraId = Guid.NewGuid(),
        //            Encoder = encoder,
        //            IPDevice = cameraDevice1,
        //            EncoderChannel = 1,
        //            CameraNo = 0,
        //            VideoForward = vfs,
        //        };
        //        _cacheMonitorySiteList.Add(new MonitorySite()
        //        {
        //            MonitorySiteId = Guid.NewGuid(),//"34010000001320000002",
        //            MonitorySiteName = cameraDevice1.IPDeviceName,
        //            Organization = yizhongdui,
        //            //EncoderChannel = 2,
        //            Camera = cam1
        //        });
        //    }
        //}

        //static void InitDeviceGroup()
        //{
        //    Organization yizhongdui = _Organizations[2];//.First(t => t.OrganizationId.Equals("37010101"));
        //    _cacheDevicegroupList.Add(new DeviceGroup()
        //    {
        //        DeviceGroupId = Guid.NewGuid(), //"34010000005010000001",
        //        DeviceGroupName = "一中队设备组1",
        //       // IPDevices = _cacheDeviceinfoList,
        //        Organization = yizhongdui
        //    });
        //}

        //static void InitTemplateLayout()
        //{
        //    SystemOption standarLayoutType = _SystemOptions.First(t => t.SystemOptionCode.Equals("11400001"));
        //    SystemOption videoTemplateType = _SystemOptions.First(t => t.SystemOptionCode.Equals("11500001"));
        //    List<TemplateCell> cells = new List<TemplateCell>();
        //    cells.Add(
        //        new TemplateCell()
        //        {
        //            Column = 0,
        //            Row = 0,
        //            RowSpan = 1,
        //            ColumnSpan = 1,
        //        });
        //    cells.Add(new TemplateCell()
        //    {
        //        Column = 1,
        //        Row = 0,
        //        RowSpan = 1,
        //        ColumnSpan = 1,
        //    });
        //    cells.Add(new TemplateCell()
        //    {
        //        Column = 0,
        //        Row = 1,
        //        RowSpan = 1,
        //        ColumnSpan = 1,
        //    });
        //    cells.Add(new TemplateCell()
        //    {
        //        Column = 1,
        //        Row = 1,
        //        RowSpan = 1,
        //        ColumnSpan = 1,
        //    });

        //    _cacheTemplateLayoutList.Add(new TemplateLayout()
        //    {
        //        TemplateLayoutId = Guid.NewGuid(),
        //        Columns = 2,
        //        Rows = 2,
        //        TemplateLayoutName = "Standard2x2",
        //        LayoutType = standarLayoutType,
        //        TemplateType = videoTemplateType,
        //        Cells = cells
        //    });
        //}

        static DataCache()
        {
            InitOrganizationNode();
            InitSystemOptions();
            _SystemOptions.AddRange(GetFeedbackTypeSystemOption());
            _SystemOptions.AddRange(GetStaffSystemOption());
            _SystemOptions.ForEach(t => t.Predefine = true);
            InitEncoderType();
            InitData();
            //InitIPDevice();
            //InitEncoder();
            //InitServer();
            //InitMonitorySite();
            //InitDeviceGroup();
            //InitTemplateLayout();
            InitDeviceAlarmMapping();
            //
            

            #region sentinel
            //_cacheSentinelList.Add(new Sentinel()
            //{
            //    SentinelId =  Guid.NewGuid(),//Guid.NewGuid (),
            //    //BreakoutSwitch = 1,
            //    //RaidSwitch = 2,
            //    //DisasterSwitch = 3,
            //    //RebellionSwitch = 4,
            //    //LeftArea = 5,
            //    //RightArea = 6,
            //    DeviceInfo = _cacheDeviceinfoList[2]
            //});
            //_cacheSentinelList.Add(new Sentinel()
            //{
            //    SentinelId = Guid.NewGuid(),
            //    //BreakoutSwitch = 1,
            //    //RaidSwitch = 2,
            //    //DisasterSwitch = 3,
            //    //RebellionSwitch = 4,
            //    //LeftArea = 5,
            //    //RightArea = 6,
            //    DeviceInfo = _cacheDeviceinfoList[3]
            //});
            #endregion  
        }

       

        public static void InitData()
        {

            #region _ApplicationSettings
            _ApplicationSettings.AddRange(new ApplicationSetting[]
                {
                    new ApplicationSetting { ApplicationSettingId=Guid.NewGuid (),SettingKey="1",SettingValue="1",Description="1" },
                    new ApplicationSetting { ApplicationSettingId=Guid.NewGuid (),SettingKey="2",SettingValue="2",Description="2"},
                    new ApplicationSetting { ApplicationSettingId=Guid.NewGuid (),SettingKey="3",SettingValue="3",Description="3"},
                });
            #endregion

            #region _Applications
            Application papsApplication = new Application
            {
                ApplicationCode = "PAPS",
                Description = "武警部队可视化勤务应急指挥调度平台",
                ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                ApplicationName = "武警部队可视化勤务应急指挥调度平台",
                ApplicationSettings = _ApplicationSettings
            };

            _Applications.Add(papsApplication);

            _Applications.Add(new Application
            {
                ApplicationCode = "IPBS",
                Description = "网络广播系统",
                ApplicationId = new Guid("0F74D13E-2E96-4E64-9566-CED48AFDFBEC"),
                ApplicationName = "网络广播系统",
              //  ApplicationSettings = _ApplicationSettings
            });

            _Applications.Add(new Application
            {
                ApplicationCode = "VSMP",
                Description = "视频监控平台",
                ApplicationId = new Guid("0E2ABB6D-5DBA-4A1F-8377-1A47D60119B6"),
                ApplicationName = "视频监控平台",
             //   ApplicationSettings = _ApplicationSettings
            });
            #endregion

            #region _SystemOptions
            List<Application> applicationList = new List<Application>();
            applicationList.Add(papsApplication);

            SystemOption eventLevel = new SystemOption { SystemOptionId = Guid.NewGuid(), SystemOptionCode = "101", Predefine = true, SystemOptionName = "事件级别", Description = "事件级别" };
            SystemOption log0 = new SystemOption { SystemOptionId = Guid.NewGuid (), SystemOptionCode = "10100001", Predefine = true, SystemOptionName = "错误", Description = "错误", ParentSystemOption = eventLevel };
            SystemOption log1 = new SystemOption { SystemOptionId = Guid.NewGuid (), SystemOptionCode = "10100002", Predefine = true, SystemOptionName = "警告", Description = "警告", ParentSystemOption = eventLevel };
            SystemOption log2 = new SystemOption { SystemOptionId = Guid.NewGuid (), SystemOptionCode = "10100003", Predefine = true, SystemOptionName = "信息", Description = "信息", ParentSystemOption = eventLevel };

            string baseOptionName = "日志分类";
            string baseOptionCode = "103";
            string[] options = new string[] { "报警", "事件"};
            int optionCode = 10300001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, optionCode);

            baseOptionName = "事件级别";
             baseOptionCode = "101";
             options = new string[] { "错误", "警告","信息" };
             optionCode = 10100001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, optionCode);

            baseOptionName = "来源";
            baseOptionCode = "102";
            options = new string[] { "服务上报" };
            optionCode = 10200001;
            NewSystemOptions(_SystemOptions, baseOptionName, baseOptionCode, options, optionCode);
            #endregion

            #region _AuthorizationInformations
            _AuthorizationInformations.Add(new AuthorizationInformation { ProjectName = "勤务演示项目", Deadline = Convert.ToDateTime("2016-8-24 19:17:49"), CompanyName = "美电贝尔", MaxCameras = 5000, MaxOnlineUsers = 500, Applications = applicationList });
            #endregion

            #region _EventLogs
            //_EventLogs.Add(new EventLog { Application = papsApplication, EventData = "报警", EventLevel = log0, EventLogId = Guid.NewGuid(), EventLogType = logType0, EventSource = eventSourceCatalog, Organization = _Organizations[0], TimeCreated = Convert.ToDateTime("2016-8-24 19:33:08") });
            //_EventLogs.Add(new EventLog { Application = papsApplication, EventData = "服务", EventLevel = log1, EventLogId = Guid.NewGuid(), EventLogType = logType1, EventSource = eventSourceType, Organization = _Organizations[0], TimeCreated = Convert.ToDateTime("2016-8-24 19:43:01") });
            #endregion

            #region ResourcesAction
            ResourcesAction ResourcesActionAdd = new ResourcesAction { ResourcesActionId = Guid.NewGuid(), ResourcesActionName = "增加" };
            ResourcesAction ResourcesActionUpdate = new ResourcesAction { ResourcesActionId = Guid.NewGuid(), ResourcesActionName = "修改" };
            ResourcesAction ResourcesActionDelete = new ResourcesAction { ResourcesActionId = Guid.NewGuid(), ResourcesActionName = "删除" };
            ResourcesAction ResourcesActionView = new ResourcesAction { ResourcesActionId = Guid.NewGuid(), ResourcesActionName = "查看" };
            ResourcesAction ResourcesActionTabulate = new ResourcesAction { ResourcesActionId = Guid.NewGuid(), ResourcesActionName = "制表" };
            _ResourcesActions.Add(ResourcesActionAdd);
            _ResourcesActions.Add(ResourcesActionUpdate);
            _ResourcesActions.Add(ResourcesActionDelete);
            _ResourcesActions.Add(ResourcesActionView);
            _ResourcesActions.Add(ResourcesActionTabulate);
            #endregion

            #region _ApplicationResources
            ApplicationResource applicationResourceDutySchedule = new ApplicationResource
            {
                Actions = new List<ResourcesAction>(new ResourcesAction[]
                {
                    ResourcesActionAdd,ResourcesActionUpdate,ResourcesActionDelete,ResourcesActionView,ResourcesActionTabulate
                }),
                Application = papsApplication,
                ApplicationResourceId = Guid.NewGuid(),
                ApplicationResourceName = "值班安排表",
            };

            ApplicationResource applicationResourceDutyCheckGroup = new ApplicationResource
            {
                Actions = new List<ResourcesAction>( new ResourcesAction[]
                {
                    new ResourcesAction { ResourcesActionId=Guid.NewGuid(),ResourcesActionName="增加"},
                    new ResourcesAction { ResourcesActionId=Guid.NewGuid(),ResourcesActionName="修改"},
                    new ResourcesAction { ResourcesActionId=Guid.NewGuid(),ResourcesActionName="删除"},
                    new ResourcesAction { ResourcesActionId=Guid.NewGuid(),ResourcesActionName="查看"},
                    new ResourcesAction { ResourcesActionId=Guid.NewGuid(),ResourcesActionName="制表"},
                }),
                Application = papsApplication,
                ApplicationResourceId = Guid.NewGuid(),
                ApplicationResourceName = "值班勤务分组安排表",
            };

            _ApplicationResources.Add(applicationResourceDutySchedule);
            _ApplicationResources.Add(applicationResourceDutyCheckGroup);

            #endregion

            #region _Permissions
            _Permissions.Add(new Permission { PermissionId = Guid.NewGuid(), Resource = applicationResourceDutySchedule, ResourcesAction = ResourcesActionAdd });
            _Permissions.Add(new Permission { PermissionId = Guid.NewGuid(), Resource = applicationResourceDutySchedule, ResourcesAction = ResourcesActionUpdate });
            _Permissions.Add(new Permission { PermissionId = Guid.NewGuid(), Resource = applicationResourceDutySchedule, ResourcesAction = ResourcesActionDelete });
            _Permissions.Add(new Permission { PermissionId = Guid.NewGuid(), Resource = applicationResourceDutySchedule, ResourcesAction = ResourcesActionView });


            #endregion

            #region _Roles
            Role roleadmin = new Role
            {
                Application = papsApplication,
                ControlResourcesType = _SystemOptions.FirstOrDefault(t=>t.SystemOptionCode.Equals("14400001")),
                Description = "超级管理员",
                Organization = _Organizations[0],
                //Permissions = _Permissions,
                RoleId = Guid.NewGuid(),
                RoleName = "超级管理员"
            };

            Role roleTourist = new Role
            {
                Application = papsApplication,
                ControlResourcesType = _SystemOptions.FirstOrDefault(t => t.SystemOptionCode.Equals("14400001")),
                Description = "游客",
                Organization = _Organizations[0],
                //Permissions = _Permissions.ToArray(),
                RoleId = Guid.NewGuid(),
                RoleName = "游客"
            };

            _Roles.Add(roleadmin);
            _Roles.Add(roleTourist);


            #endregion

            #region _Users
            User userAdmin = new User
            {
                AccessFailed = 0,
                ApplicationId = papsApplication.ApplicationId,
                Description = "默认用户",
                Enable = true,
                LockoutEnabled = false,
                OrganizationId = _Organizations[0].OrganizationId,
                PasswordHash = "admin",
                //Roles = new List<Role>(new Role[] { roleadmin, roleTourist }),
                UserId = Guid.Parse("5ab5fb9b-6bbc-4d32-ba77-6ed6b1ba061a"),
                UserName = "admin"
            };

            User userTourist = new User
            {
                AccessFailed = 0,
                ApplicationId = papsApplication.ApplicationId,
                Description = "默认用户",
                Enable = true,
                LockoutEnabled = false,
                OrganizationId = _Organizations[0].OrganizationId,
                PasswordHash = "tourist",
                //Roles = new List<Role>(new Role[] { roleTourist }),
                UserId = Guid.Parse("afa6cab1-b805-42ca-83fc-98d33ae1c660"),
                UserName = "tourist"
            };

            _Users.Add(userAdmin);
            _Users.Add(userTourist);

            #endregion

            #region _OnlineUsers
            _OnlineUsers.Add(new OnlineUser
            {
                User = userAdmin,
                KeepAlived = DateTime.Now.AddSeconds(-2),
                LoginTerminal = new UserTerminal { UserTerminalIP = "5.5.5.5", UserTerminalMac = "00-23-5A-15-99-42", UserTerminalType = _SystemOptions[0] },
                LoginTime = DateTime.Now.AddHours(-1),
                OnLineUserId = Guid.NewGuid ()
            });

            #endregion

            #region _ScheduleCycle
            List<DayPeriod> dplist = new List<DayPeriod>();
            dplist.Add(new DayPeriod()
            {
                TimePeriods = new List<TimePeriod>(new TimePeriod[]
            {
                new TimePeriod { StartTime=new DateTime(2016,8,25,0,0,0),EndTime=new DateTime(2016,8,25,2,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,2,0,0),EndTime=new DateTime(2016,8,25,4,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,4,0,0),EndTime=new DateTime(2016,8,25,6,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,6,0,0),EndTime=new DateTime(2016,8,25,8,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,8,0,0),EndTime=new DateTime(2016,8,25,10,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,10,0,0),EndTime=new DateTime(2016,8,25,12,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,12,0,0),EndTime=new DateTime(2016,8,25,14,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,14,0,0),EndTime=new DateTime(2016,8,25,16,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,16,0,0),EndTime=new DateTime(2016,8,25,18,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,18,0,0),EndTime=new DateTime(2016,8,25,20,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,20,0,0),EndTime=new DateTime(2016,8,25,22,0,0)},
                new TimePeriod { StartTime=new DateTime(2016,8,25,22,0,0),EndTime=new DateTime(2016,8,26,0,0,0)},
            }),
                DayPeriodId = Guid.NewGuid()
            });
            ScheduleCycle scheduleCycle = new ScheduleCycle
            {
               DayPeriods = dplist
            };
            #endregion

            //#region _Schedules

            //Schedule schedules = new Schedule
            //{
            //    ScheduleCycle = scheduleCycle,
            //    EffectiveTime = DateTime.Now.AddDays(-1),
            //    ExpirationTime = DateTime.Now.AddYears(1),
            //    ScheduleId = Guid.NewGuid (),
            //    ScheduleName = "任务排程",
            //    ScheduleType = logType0,
            //};
            //_Schedules.Add(schedules);

            //#endregion

            #region _Staffs

            SystemOption male = _SystemOptions.FirstOrDefault(t => t.SystemOptionCode.Equals("13300002"));
            _Staffs.Add(new Staff { Application = papsApplication, Organization = _Organizations[0], PositionType = _SystemOptions[0], RankType = _SystemOptions[1], Sex = male, StaffCode = 1, StaffId = Guid.NewGuid (), StaffName = "张三" });
            _Staffs.Add(new Staff { Application = papsApplication, Organization = _Organizations[0], PositionType = _SystemOptions[0], RankType = _SystemOptions[1], Sex = male, StaffCode = 2, StaffId = Guid.NewGuid (), StaffName = "李四" });
            _Staffs.Add(new Staff { Application = papsApplication, Organization = _Organizations[0], PositionType = _SystemOptions[0], RankType = _SystemOptions[1], Sex = male, StaffCode = 3, StaffId =Guid.NewGuid (), StaffName = "王五" });
            _Staffs.Add(new Staff { Application = papsApplication, Organization = _Organizations[0], PositionType = _SystemOptions[0], RankType = _SystemOptions[1], Sex = male, StaffCode = 4, StaffId = Guid.NewGuid (), StaffName = "赵六" });


            #endregion

            #region _StaffGroups
            _StaffGroups.Add(new StaffGroup { Application = papsApplication, StaffGroupId = Guid.NewGuid(), GroupName = "勤务分组", Organization = _Organizations[0], Staffs = _Staffs });



            #endregion

            #region _Attachments
            _Attachments.Add(new Attachment { AttachmentId = Guid.NewGuid (), AttachmentName = "aspnet-ef-latest.pdf", AttachmentPath = "../aspnet-ef-latest.pdf", AttachmentType = 0, Modified = DateTime.Now.AddMonths(-1), ModifiedBy = userAdmin });
            _Attachments.Add(new Attachment { AttachmentId = Guid.NewGuid (), AttachmentName = "aspnet-aspnet-latest.pdf", AttachmentPath = "../aspnet-aspnet-latest.pdf", AttachmentType = 0, Modified = DateTime.Now.AddMonths(-2), ModifiedBy = userAdmin });
            #endregion


            #region _AllMonitorySite

            #endregion


        }

        public static List<EncoderType> _EncoderTypes = new List<EncoderType>();
        public static void InitEncoderType()
        {
            _EncoderTypes.Add(CreateEncoderType("00100000", 100000, "海康DVR系列", "admin", "12345", 8, "mp4",8000));
            _EncoderTypes.Add(CreateEncoderType("00100001", 100001, "AEBELL-DVR-A-H265系列", "admin", "12345", 8, "mp4",8000));
            _EncoderTypes.Add(CreateEncoderType("00100100", 100100, "海康DVS系列", "admin", "12345", 8, "mp4",8000));
            _EncoderTypes.Add(CreateEncoderType("00100200", 100200, "海康IPC系列", "admin", "12345", 1, "mp4",8000));
            _EncoderTypes.Add(CreateEncoderType("00100201", 100201, "AEBELL-IPC-A-H265系列", "admin", "12345", 1, "mp4",8000));
            _EncoderTypes.Add(CreateEncoderType("00100202", 100202, "AEBELL-IPC-AQ系列", "admin", "12345", 1, "mp4",8000));
            _EncoderTypes.Add(CreateEncoderType("00100203", 100203, "AEBELL-IPC-AQ-H265系列", "admin", "12345", 1, "mp4",8000));
            _EncoderTypes.Add(CreateEncoderType("00101000", 101000, "大华DVR系列", "super", "super", 16, "dav",37777));
            _EncoderTypes.Add(CreateEncoderType("00101100", 101100, "大华DVS系列", "super", "super", 16, "dav",37777));
            _EncoderTypes.Add(CreateEncoderType("00101200", 101200, "大华IPC系列", "super", "super", 1, "dav",37777));
            _EncoderTypes.Add(CreateEncoderType("00102000", 102000, "AEBELL-DVR-E系列", "admin", "admin", 16, "dav",37777));
            _EncoderTypes.Add(CreateEncoderType("00102001", 102001, "AEBELL-DVR-M系列", "admin", "admin", 16, "dav",37777));
            _EncoderTypes.Add(CreateEncoderType("00102002", 102002, "AEBELL-DVR-S系列", "admin", "123456", 32, "zl",8000));
            _EncoderTypes.Add(CreateEncoderType("00102003", 102003, "AEBELL-DVR-S-3G系列", "admin", "123456", 32, "zl",8000));
            _EncoderTypes.Add(CreateEncoderType("00102004", 102004, "虚拟DVR", "admin", "admin", 300, "zl",8000));
            _EncoderTypes.Add(CreateEncoderType("00102005", 102005, "AEBELL-IPC-S系列", "admin", "admin", 16, "zl",8000));
            _EncoderTypes.Add(CreateEncoderType("00102006", 102006, "AEBELL-DVR-S-H265系列", "admin", "123456", 32, "zl",8000));
            _EncoderTypes.Add(CreateEncoderType("00102007", 102007, "AEBELL-DVR-S-3G-H265系列", "admin", "123456", 32, "zl",8000));
            _EncoderTypes.Add(CreateEncoderType("00102100", 102100, "AEBELL-DVS系列", "admin", "admin", 4, "264",36688));
            _EncoderTypes.Add(CreateEncoderType("00102201", 102201, "AEBELL-IPC系列", "admin", "admin", 1, "264",36688));
            _EncoderTypes.Add(CreateEncoderType("00102208", 102208, "AEBELL-C50系列", "admin", "12345", 1, "avi",8091));
            _EncoderTypes.Add(CreateEncoderType("00102209", 102209, "AEBELL-CH20系列", "admin", "123456", 1, "aps",6002));
            _EncoderTypes.Add(CreateEncoderType("00102210", 102210, "AEBELL-SVAC系列", "admin", "123456", 1, "zx",34567));
            _EncoderTypes.Add(CreateEncoderType("00102211", 102211, "AEBELL-IPC-S-H265系列", "admin", "123456", 1, "zl",8000));
            _EncoderTypes.Add(CreateEncoderType("00103200", 103200, "科达IPC系列", "admin", "admin", 1, "asf",5510));
            _EncoderTypes.Add(CreateEncoderType("00104000", 104000, "BSR-DVR7系列", "admin", "admin", 16, "bsr",3721));
            _EncoderTypes.Add(CreateEncoderType("00104001", 104001, "BSR-DVR6系列", "admin", "admin", 16, "bsr",3721));
            _EncoderTypes.Add(CreateEncoderType("00105200", 105200, "亿维7100HR-IPC", "admin", "admin", 1, "yw",3000));
            _EncoderTypes.Add(CreateEncoderType("00106100", 106100, "十五所NV-1100HA", "admin", "admin", 1, "15s",80));
            _EncoderTypes.Add(CreateEncoderType("00106101", 106101, "十五所NV-001", "admin", "admin", 1, "15s",80));
            _EncoderTypes.Add(CreateEncoderType("00107000", 107000, "BL-ONVIF系列", "", "", 1, "mp4",8080));
            _EncoderTypes.Add(CreateEncoderType("00108000", 108000, "米卡IPC", "", "", 1, "mk",1));
            _EncoderTypes.Add(CreateEncoderType("00109000", 109000, "景阳IPC", "admin", "admin", 1, "jy", 30001));
            _EncoderTypes.Add(CreateEncoderType("00110000", 110000, "朗驰IPC", "888888", "888888", 1, "lc", 3000));
            _EncoderTypes.Add(CreateEncoderType("00111000", 111000, "三星IPC", "admin", "4321", 1, "xns",4520));
            _EncoderTypes.Add(CreateEncoderType("00112000", 112000, "电子哨兵", "admin", "admin", 1, "mp4",554));
            _EncoderTypes.Add(CreateEncoderType("00113000", 113000, "宇视IPC", "admin", "admin", 1, "mp4",554));
            _EncoderTypes.Add(CreateEncoderType("00114000", 114000, "智能交通系列", "admin", "admin", 32, "td",3000));
            _EncoderTypes.Add(CreateEncoderType("00115000", 115000, "GB28181", "admin", "admin", 1, "mp4",5060));
            _EncoderTypes.Add(CreateEncoderType("00116000", 116000, "银海NVR", "admin", "12345", 16, "yh",8000));
            _EncoderTypes.Add(CreateEncoderType("00117000", 117000, "汉邦DVR", "admin", "888888", 16, "hb",8101));
            _EncoderTypes.Add(CreateEncoderType("00118000", 118000, "佳信捷IPC", "admin", "admin", 1, "jxj",6080));
            _EncoderTypes.Add(CreateEncoderType("00120000", 120000, "手机摄像头", "admin", "admin", 1, "mp4",9999));
            _EncoderTypes.Add(CreateEncoderType("00121000", 121000, "可视对讲", "admin", "123456", 1, "mp4",5540));
            _EncoderTypes.Add(CreateEncoderType("00122000", 122000, "慧眼通车载", null, null, 3, "hyt",0));
            _EncoderTypes.Add(CreateEncoderType("0A100000", 100000, "	AEBELL-DVR-A系列", "admin", "12345", 8, "mp4",80));
            _EncoderTypes.Add(CreateEncoderType("0A100100", 100100, "AEBELL-IPC-AK系列", "admin", "12345", 1, "mp4",8000));
            _EncoderTypes.Add(CreateEncoderType("0A100200", 100200, "AEBELL-IPC-A-H265系列", "admin", "12345", 1, "mp4",8000));
        }

        static EncoderType CreateEncoderType(string id, int encoderCode,string name, string username, string password, int channels, string ext,int defaultport)
        {
            EncoderType et = new EncoderType()
            {
                EncoderTypeId = CreateGuid8(id),
                EncoderTypeName = name,
                Channels = 8,
                DefaultUserName =username,
                DefaultPassword = password,
                OSDLines = 1,
                PTZ3DControl = 0,
                EncoderCode = encoderCode,
                RecordFileExtension = ext,
                DefaultPort = defaultport
            };
            return et;
        }

        static void InitDeviceAlarmMapping()
        {
            //哨位拓展
            DeviceAlarmMapping m1 = new DeviceAlarmMapping() {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002001"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000401"),
            };
            DeviceAlarmMapping m2 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002002"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000401"),
            };
            DeviceAlarmMapping m3 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002003"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000401"),
            };
            DeviceAlarmMapping m4 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002004"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000401"),
            };
            //哨位简约
            DeviceAlarmMapping f1 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002001"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000402"),
            };
            DeviceAlarmMapping f2 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002002"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000402"),
            };
            DeviceAlarmMapping f3 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002003"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000402"),
            };
            DeviceAlarmMapping f4 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002004"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000402"),
            };
            //智能哨拓展
            DeviceAlarmMapping d1 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002005"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000403"),
            };
            DeviceAlarmMapping d2 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002006"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000403"),
            };
            DeviceAlarmMapping d3 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002003"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000403"),
            };
            DeviceAlarmMapping d4 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002004"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000403"),
            };
            // //智能哨简约
            DeviceAlarmMapping e1 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002005"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000404"),
            };
            DeviceAlarmMapping e2 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002006"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000404"),
            };
            DeviceAlarmMapping e3 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002003"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000404"),
            };
            DeviceAlarmMapping e4 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002004"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000404"),
            };
            //哨位集成箱拓展
            DeviceAlarmMapping g1 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002001"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000405"),
            };
            DeviceAlarmMapping g2 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002002"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000405"),
            };
            DeviceAlarmMapping g3 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002003"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000405"),
            };
            DeviceAlarmMapping g4 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002004"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000405"),
            };
            //哨位简约
            DeviceAlarmMapping h1 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002001"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000406"),
            };
            DeviceAlarmMapping h2 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002002"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000406"),
            };
            DeviceAlarmMapping h3 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002003"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000406"),
            };
            DeviceAlarmMapping h4 = new DeviceAlarmMapping()
            {
                DeviceAlarmMappingId = Guid.NewGuid(),
                AlarmTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD00002004"),
                DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000406"),
            };
            _DeviceAlarmMapping.AddRange(new DeviceAlarmMapping[] { m1,m2,m3,m4,d1,d2,d3,d4,e1,e2,e3,e4,f1,f2,f3,f4,g1,
            g2,g3,g4,h1,h2,h3,h4});
        }

        public static Guid CreateGuid3(string code)
        {
            return Guid.Parse("A0002016-E009-B019-E001-ABCDEF000" + code);
        }

        public static Guid CreateGuid10(string code)
        {
            return Guid.Parse("A0002016-E009-B019-E001-AB" + code);
        }

        public static Guid CreateGuid8(string code)
        {
            return Guid.Parse("A0002016-E009-B019-E001-ABCD" + code);
        }

        /// <summary>
        /// 人员字典定义
        /// </summary>
        public static List<SystemOption> GetStaffSystemOption()
        {
            List<SystemOption> staffOptions = new List<SystemOption>();
            //200开头
            //文化程序
            string baseOptionName, baseOptionsCode; string[] systemoptions; int optionCode;
            #region 文化程度
            baseOptionName = "文化程度";
            baseOptionsCode = "200";
            optionCode = 20000001;
            systemoptions = new string[] { "初中", "职高", "高中", "中专", "大专", "本科", "研究生", "博士", "其他" };
            NewSystemOptions(staffOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);
            #endregion

            #region 身体状况
            baseOptionName = "身体状况";
            baseOptionsCode = "201";
            optionCode = 20100001;
            systemoptions = new string[] { "良好", "一般", "很好"};
            NewSystemOptions(staffOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);
            #endregion

            #region 工作性质
            baseOptionName = "工作性质";
            baseOptionsCode = "202";
            optionCode = 20200001;
            systemoptions = new string[] { "指挥", "执勤", "机动", "保障", "其他" };
            NewSystemOptions(staffOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);

            #endregion

            #region 职务
            baseOptionName = "职务";
            baseOptionsCode = "203";
            optionCode = 20300001;
            systemoptions = new string[] { "中队长", "指导员", "副中队长", "副指导员", "排长", "代理排长", "司务长", "班长", "副班长", "军械员兼文书", "通信员", "卫生员", "炊事员", "驾驶员", "战士" };
            NewSystemOptions(staffOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);

            #endregion

            #region 警衔
            baseOptionName = "警衔";
            baseOptionsCode = "204";
            optionCode = 20400001;
            systemoptions = new string[] { "列兵", "上等兵", "下士", "中士", "上士", "四级警士长", "三级警士长", "二级警士长", "一级警士长", "少尉", "中尉", "上尉", "少校", "中校", "上校" };
            NewSystemOptions(staffOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);
            #endregion

            #region 政治面貌
            baseOptionName = "政治面貌";
            baseOptionsCode = "205";
            optionCode = 20500001;
            systemoptions = new string[] { "群众", "团员", "党员", "其它" };
            NewSystemOptions(staffOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);
            #endregion

            baseOptionName = "婚姻状况";
            baseOptionsCode = "206";
            optionCode = 20600001;
            systemoptions = new string[] { "未婚", "已婚", "离异", "丧偶" };
            NewSystemOptions(staffOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);

            baseOptionName = "人员在位情况";
            baseOptionsCode = "207";
            optionCode = 20700001;
            systemoptions = new string[] { "在位", "休假", "培训", "住院", "公差", "借调","退伍","退休","离职" };
            NewSystemOptions(staffOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);

            baseOptionName = "民族";
            baseOptionsCode = "208";
            optionCode = 20800001;
            systemoptions = new string[] { "汉族", "壮族", "回族", "满族", "维吾尔族", "苗族", "彝族", "土家族","藏族", "蒙古族", "侗族", "布依族", "瑶族", "白族", "朝鲜族", "哈尼族",
                "黎族", "哈萨克族", "傣族", "畲族", "傈僳族", "东乡族", "仡佬族", "拉祜族", "佤族", "水族", "纳西族", "羌族", "土族", "仫佬族", "锡伯族", "柯尔克孜族", "景颇族", "达斡尔族",
                "撒拉族", "布朗族", "毛南族", "塔吉克族", "普米族", "阿昌族", "怒族", "鄂温克族", "京族", "基诺族", "德昂族", "保安族", "俄罗斯族", "裕固族", "乌孜别克族", "门巴族",
                "鄂伦春族", "独龙族", "赫哲族", "高山族", "珞巴族", "塔塔尔族", "穿青族" };
            NewSystemOptions(staffOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);

            return staffOptions;
        }

        public static void NewSystemOptions(List<SystemOption> options, string baseOptionName, string baseOptionsCode, string[] systemoptions, int optionCode)
        {
            SystemOption baseOptions = new SystemOption()
            {
                Description = baseOptionName,
                SystemOptionCode = baseOptionsCode,
                SystemOptionId = DataCache.CreateGuid3(baseOptionsCode),
                SystemOptionName = baseOptionName
            };
            options.Add(baseOptions);
            int i = optionCode;
            foreach (var s in systemoptions)
            {
                var so = new SystemOption()
                {
                    Description = s,
                    SystemOptionCode = i.ToString(),
                    SystemOptionId = CreateGuid8(i.ToString()),
                    SystemOptionName = s,
                    ParentSystemOption = baseOptions
                };
                options.Add(so);
                i++;
            }
        }

        public static void NewSystemOptions(List<SystemOption> options, string baseOptionName, string baseOptionsCode, string[] systemoptions,string[] mappingcodes, int optionCode)
        {
            SystemOption baseOptions = new SystemOption()
            {
                Description = baseOptionName,
                SystemOptionCode = baseOptionsCode,
                SystemOptionId = DataCache.CreateGuid3(baseOptionsCode),
                SystemOptionName = baseOptionName
            };
            options.Add(baseOptions);
            int i = optionCode;
            int j = 0;
            foreach (var s in systemoptions)
            {
                var so = new SystemOption()
                {
                    Description = s,
                    SystemOptionCode = i.ToString(),
                    SystemOptionId = CreateGuid8(i.ToString()),
                    SystemOptionName = s,
                    ParentSystemOption = baseOptions,
                    MappingCode = mappingcodes[j]
                };
                options.Add(so);
                i++;
                j++;
            }
        }

        public static List<DeviceChannelTypeMapping> InitSentinelChannelOption()
        {
            List<SystemOption> deviceChannelOptions = new List<SystemOption>();
            string baseOptionName = "设备通道类型";
            string baseOptionsCode = "220";
            int optionCode = 22000001;
            string[] systemoptions = new string[] { "暴狱输出通道", "越狱输出通道", "袭击输出通道", "灾害输出通道", "冲闯输出通道", "破坏输出通道" };
            string[] mappingCodes = new string[] { "1", "2", "3", "4", "1", "2" };
            NewSystemOptions(deviceChannelOptions, baseOptionName, baseOptionsCode, systemoptions, mappingCodes, optionCode);
            _SystemOptions.AddRange(deviceChannelOptions);

            //爆逃袭灾
            List<DeviceChannelTypeMapping> mappings = new List<DeviceChannelTypeMapping>();
            string[] sentineloptions = new string[] { "1100000401", "1100000402", "1100000405", "1100000406" };

            int mappingGuid = 1100000401;
            foreach (string sentinelTypeCode in sentineloptions)
            {
                for (int i = 1; i <=4; i++)
                {
                    DeviceChannelTypeMapping m = new DeviceChannelTypeMapping()
                    {
                        DeviceChannelTypeMappingId = CreateGuid10(mappingGuid.ToString()),
                        DeviceTypeId = _SystemOptions.First(t => t.SystemOptionCode.Equals(sentinelTypeCode)).SystemOptionId,
                        ChannelTypeId = deviceChannelOptions[i].SystemOptionId
                    };
                    mappings.Add(m);
                    mappingGuid++;
                }
            }
            //冲破袭灾
            sentineloptions = new string[] { "1100000403", "1100000404" };

            foreach (string sentinelTypeCode in sentineloptions)
            {
                for (int i = 3; i <= 6; i++)
                {
                    DeviceChannelTypeMapping m = new DeviceChannelTypeMapping()
                    {
                        DeviceChannelTypeMappingId = CreateGuid10(mappingGuid.ToString()),
                        DeviceTypeId = _SystemOptions.First(t => t.SystemOptionCode.Equals(sentinelTypeCode)).SystemOptionId,
                        ChannelTypeId = deviceChannelOptions[i].SystemOptionId
                    };
                    mappings.Add(m);
                    mappingGuid++;
                }
            }
            return mappings;
        }

        /// <summary>
        /// 查勤反馈方式定义
        /// </summary>
        /// <returns></returns>
        public static List<SystemOption> GetFeedbackTypeSystemOption()
        {
            List<SystemOption> feedbackTypeOptions = new List<SystemOption>();
            // 250开始
            string baseOptionName, baseOptionsCode; string[] systemoptions; int optionCode;
            baseOptionName = "查勤反馈方式";
            baseOptionsCode = "250";
            optionCode = 25000001;
            systemoptions = new string[] { "无","反馈", "电话反馈给执勤人员", "电话告知中队网络查勤员", "电话告知支队网络查勤员" };
            NewSystemOptions(feedbackTypeOptions, baseOptionName, baseOptionsCode, systemoptions, optionCode);

            return feedbackTypeOptions;
        }

        /// <summary>
        /// 初始化查勤评价选项
        /// </summary>
        /// <returns></returns>
        public static List<DutyCheckAppraise> GetDutyCheckAppraise(Guid organizationId)
        {

            List<DutyCheckAppraise> list = new List<DutyCheckAppraise>();
            list.Add(new DutyCheckAppraise
            {
                AppraiseTypeId = new Guid("a0002016-e009-b019-e001-abcd14500001"),
                DutyCheckAppraiseId = CreateGuidAppraise("25000001"),
                DutyCheckAppraiseName = "无",
                OrganizationId = organizationId,
                OrderNo=1,
            });
            //
            list.Add(new DutyCheckAppraise
            {
                AppraiseTypeId = new Guid("a0002016-e009-b019-e001-abcd14500002"),
                DutyCheckAppraiseId = CreateGuidAppraise("25000002"),
                DutyCheckAppraiseName = "姿态不端正",
                OrganizationId = organizationId,
                OrderNo = 2,
            });
            list.Add(new DutyCheckAppraise
            {
                AppraiseTypeId = new Guid("a0002016-e009-b019-e001-abcd14500002"),
                DutyCheckAppraiseId = CreateGuidAppraise("25000003"),
                DutyCheckAppraiseName = "报告词不规范",
                OrganizationId = organizationId,
                OrderNo = 3,
            });
            //
            list.Add(new DutyCheckAppraise
            {
                AppraiseTypeId = new Guid("a0002016-e009-b019-e001-abcd14500003"),
                DutyCheckAppraiseId = CreateGuidAppraise("25000004"),
                DutyCheckAppraiseName = "打瞌睡",
                OrganizationId = organizationId,
                OrderNo = 4,
            });
            list.Add(new DutyCheckAppraise
            {
                AppraiseTypeId = new Guid("a0002016-e009-b019-e001-abcd14500003"),
                DutyCheckAppraiseId = CreateGuidAppraise("25000005"),
                DutyCheckAppraiseName = "做与执勤无关的事情",
                OrganizationId = organizationId,
                OrderNo = 5,
            });
            list.Add(new DutyCheckAppraise
            {
                AppraiseTypeId = new Guid("a0002016-e009-b019-e001-abcd14500003"),
                DutyCheckAppraiseId = CreateGuidAppraise("25000006"),
                DutyCheckAppraiseName = "不按规定携带武器装备",
                OrganizationId = organizationId,
                OrderNo = 6,
            });
            list.Add(new DutyCheckAppraise
            {
                AppraiseTypeId = new Guid("a0002016-e009-b019-e001-abcd14500003"),
                DutyCheckAppraiseId = CreateGuidAppraise("25000007"),
                DutyCheckAppraiseName = "不按规定着装",
                OrganizationId = organizationId,
                OrderNo = 7,
            });
            list.Add(new DutyCheckAppraise
            {
                AppraiseTypeId = new Guid("a0002016-e009-b019-e001-abcd14500003"),
                DutyCheckAppraiseId = CreateGuidAppraise("25000008"),
                DutyCheckAppraiseName = "语音联络不畅通",
                OrganizationId = organizationId,
                OrderNo = 8,
            });
            list.Add(new DutyCheckAppraise
            {
                AppraiseTypeId = new Guid("a0002016-e009-b019-e001-abcd14500003"),
                DutyCheckAppraiseId = CreateGuidAppraise("25000009"),
                DutyCheckAppraiseName = "监控图像不清晰",
                OrganizationId = organizationId,
                OrderNo = 9,
            });


            return list;

        }

        public static Guid CreateGuidAppraise(string code)
        {
            return Guid.Parse("A0002017-E001-B007-E001-ABCD" + code);
        }


        public static Attachment GetAttachment()
        {
           return new Attachment
           {
               AttachmentId = Guid.NewGuid(),
               AttachmentPath = "",
               AttachmentType = 1,
               Modified = DateTime.Now,
               ModifiedById = _Users[0].UserId,
           };
        }

        public static void InitDutyCheckTimePlan()
        {
            using (var db = new AllInOneContext())
            {
                TimePeriod TimePeriod1 = new TimePeriod
                {
                    StartTime = new DateTime(2017, 1, 1, 8, 0, 0),
                    EndTime = new DateTime(2017, 1, 1, 12, 0, 0),
                    OrderNo = 1,
                    TimePeriodId = Guid.NewGuid(),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 0,
                        PercentValue = 0,
                        ValueType = 0
                    }
                };

                TimePeriod TimePeriod2 = new TimePeriod
                {
                    StartTime = new DateTime(2017,1, 1, 12, 0, 0),
                    EndTime = new DateTime(2017, 1, 1, 16, 0, 0),
                    OrderNo = 2,
                    TimePeriodId = Guid.NewGuid(),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 0,
                        PercentValue = 0,
                        ValueType = 0
                    }
                };

                TimePeriod TimePeriod3 = new TimePeriod
                {
                    StartTime = new DateTime(2017, 1, 1, 16, 0, 0),
                    EndTime = new DateTime(2017, 1, 1, 20, 0, 0),
                    OrderNo = 3,
                    TimePeriodId = Guid.NewGuid(),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 0,
                        PercentValue = 0,
                        ValueType = 0
                    }
                };

                TimePeriod TimePeriod4 = new TimePeriod
                {
                    StartTime = new DateTime(2017,1, 1, 20, 0, 0),
                    EndTime = new DateTime(2017, 1, 1, 1, 0, 0),
                    OrderNo = 4,
                    TimePeriodId = Guid.NewGuid(),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 0,
                        PercentValue = 0,
                        ValueType = 0
                    }
                };

                TimePeriod TimePeriod5 = new TimePeriod
                {
                    StartTime = new DateTime(2017,1,1, 1, 0, 0),
                    EndTime = new DateTime(2017, 1,1, 4, 0, 0),
                    OrderNo = 5,
                    TimePeriodId = Guid.NewGuid(),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 0,
                        PercentValue = 0,
                        ValueType = 0
                    }
                };

                TimePeriod TimePeriod6 = new TimePeriod
                {
                    StartTime = new DateTime(2017, 1, 1, 4, 0, 0),
                    EndTime = new DateTime(2017, 1, 1, 8, 0, 0),
                    OrderNo = 6,
                    TimePeriodId = Guid.NewGuid(),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 0,
                        PercentValue = 0,
                        ValueType = 0
                    }
                };

                List<TimePeriod> listTimePeriod = new List<TimePeriod>();
                listTimePeriod.Add(TimePeriod1);
                listTimePeriod.Add(TimePeriod2);
                listTimePeriod.Add(TimePeriod3);
                listTimePeriod.Add(TimePeriod4);
                listTimePeriod.Add(TimePeriod5);
                listTimePeriod.Add(TimePeriod6);
                //
                List<DayPeriod> listDayPeriod = new List<DayPeriod>();
                DayPeriod DayPeriod1 = new DayPeriod
                {
                    TimePeriods = listTimePeriod
                };
                listDayPeriod.Add(DayPeriod1);
                //
                ScheduleCycle ScheduleCycle = new ScheduleCycle
                {
                    CycleTypeId = new Guid("A0002016-E009-B019-E001-ABCD13700001"), //周
                    ScheduleCycleId = Guid.NewGuid(),
                    DayPeriods = listDayPeriod,
                    

                };
                //
                Schedule Schedule = new Schedule
                {
                    ScheduleId = Guid.NewGuid(),
                    ScheduleCycle = ScheduleCycle,
                    ScheduleName = "查勤包排程",
                    ScheduleTypeId = new Guid("A0002016-E009-B019-E001-ABCD13200002"),
                    EffectiveTime=DateTime.Now,
                   
                };


                Organization org = db.Organization.OrderBy(p => p.OrderNo).ToList()[0];

                DutyCheckPackageTimePlan plan = new DutyCheckPackageTimePlan
                {
                    DutyCheckPackageTimePlanId = Guid.NewGuid(),
                    OrganizationId = org.OrganizationId,
                    RandomRate = 0,
                    Schedule = Schedule
                };


                    try
                    {
                        db.DutyCheckPackageTimePlan.Add(plan);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                    }
            }
        }

    }
}
