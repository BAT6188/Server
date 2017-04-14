using AlarmAndPlan.Model;
using Infrastructure.Model;
using Microsoft.AspNetCore.Hosting;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Newtonsoft.Json;
using PAPS.Model;


namespace AllInOneContext
{
    public class Program
    {
        public static void Main(string[] args)
        {


            //MyMigration.Update20161214();
            //UpdatePlan();
            //return;
            //AddStaffOptions();
            //return;
            //return;
            //AddAlarmSetting();
            //AddSentinelAlarmSetting();
            //return;
            //MyMigration.Initdata();
            //return;
            //InitEncoder();
            //return;
            //AddDeviceAlarmType();
            //AddDeviceGroup();
            //return;
            //AddAllInOneData();

            #region 基础模块测试
            //GetRole();
            //AddPermission();
            //AddRole();
            //UpdateRole();

            //DeleteRole();
            //AddUser();
            //UpdateUser();
            ////DeleteUser();

            //AddStaff();

            //UpdateOrganization();

            //AddDisposeOption();
            //AddDutyCheckAppraise();
            #endregion
            //AddIpDevice();

            //AddSentry();

            #region 勤务模块数据
            //AddTimePeriods();
            //AddScheduleCycle();
            //AddSchedule();

            //AddDutyCheckPackageTimePlan();

            //AddDutyGroupSchedule();

            //AddDutySchedule();


            //AddSystemOption();
            //AddSystemOptionDispose();

            //AddDutyCheckAppraise();


            //AddDutyCheckLog();
            #endregion
            //AddAlamrSetting();
            //AddAlarmPeripheral();
            //AddTimerTask();

            //    AddAlarmLog();
            MyMigration.Migrate();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine("Hello.use context,Press Enter to quit");
            Console.ReadLine();

        }


        #region 基础模块测试

        private static void AddPermission()
        {
            using (var db = new AllInOneContext())
            {
                //ResourcesAction
                List<ResourcesAction> listResourcesAction = new List<ResourcesAction>();
                ResourcesAction add = new ResourcesAction
                {
                    ResourcesActionId = new Guid("0FDC49D8-C123-4096-A0EB-4C65D2193F82"),
                    ResourcesActionName = "新增权限"
                };

                ResourcesAction update = new ResourcesAction
                {
                    ResourcesActionId = new Guid("77338EE6-D97B-4D02-9008-D9DC06F44EAC"),
                    ResourcesActionName = "修改权限"
                };

                ResourcesAction delete = new ResourcesAction
                {
                    ResourcesActionId = new Guid("2AB62219-66AE-4535-9440-167F41568CED"),
                    ResourcesActionName = "删除权限"
                };

                ResourcesAction view = new ResourcesAction
                {
                    ResourcesActionId = new Guid("43DCA5B8-9AB6-428D-9A32-743CB5A94257"),
                    ResourcesActionName = "查看权限"
                };

                listResourcesAction.Add(add);
                listResourcesAction.Add(update);
                listResourcesAction.Add(delete);
                listResourcesAction.Add(view);

                db.ResourcesAction.AddRange(listResourcesAction);
                db.SaveChanges();

                //ApplicationResource
                List<ApplicationResource> list = new List<ApplicationResource>();

                ApplicationResource applicationResource1 = new ApplicationResource
                {
                    //Actions= listResourcesAction,
                    ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                    ApplicationResourceId = new Guid("E565CCB9-7A83-4AB3-A992-5BC2236B4138"),
                    ApplicationResourceName = "IPBS广播",
                };
                list.Add(applicationResource1);

                ApplicationResource applicationResource2 = new ApplicationResource
                {
                    Actions = listResourcesAction,
                    ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                    ApplicationResourceId = new Guid("9FCA6B7B-1DD7-4EFC-9437-7188599C5F30"),
                    ParentResourceId = new Guid("E565CCB9-7A83-4AB3-A992-5BC2236B4138"),
                    ApplicationResourceName = "权限子集",
                };
                list.Add(applicationResource2);

                db.ApplicationResource.AddRange(list);
                db.SaveChanges();

                //Permission
                List<Permission> listPermission = new List<Permission>();
                Permission Permission1 = new Permission
                {
                    PermissionId = new Guid("403304BF-B382-4100-A847-DC4EB6C89259"),
                    ResourceId = new Guid("9FCA6B7B-1DD7-4EFC-9437-7188599C5F30"),
                    ResourcesActionId = new Guid("0FDC49D8-C123-4096-A0EB-4C65D2193F82"),
                };

                Permission Permission2 = new Permission
                {
                    PermissionId = new Guid("CEF0E464-0DBF-4847-8F00-6E339A26605C"),
                    ResourceId = new Guid("9FCA6B7B-1DD7-4EFC-9437-7188599C5F30"),
                    ResourcesActionId = new Guid("43DCA5B8-9AB6-428D-9A32-743CB5A94257"),
                };
                listPermission.Add(Permission1);
                listPermission.Add(Permission2);
                db.Permission.AddRange(listPermission);
                db.SaveChanges();

            }
        }

        private static void AddRole()
        {
            using (var db = new AllInOneContext())
            {
                List<RolePermission> RolePermissions1 = new List<RolePermission>();
                RolePermissions1.Add(new RolePermission
                {
                    //RolePermissionId=new Guid("5CBD6DC0-640F-4422-9C15-ABC83304E90E"),
                    PermissionId = new Guid("403304BF-B382-4100-A847-DC4EB6C89259"),
                    RoleId = new Guid("2F930EFA-6492-4076-9145-AB0674EE1BFB")
                });
                RolePermissions1.Add(new RolePermission
                {
                    //RolePermissionId = new Guid("4F42AE2A-840A-4F00-9E8F-DED31A162F3C"),
                    PermissionId = new Guid("CEF0E464-0DBF-4847-8F00-6E339A26605C"),
                    RoleId = new Guid("2F930EFA-6492-4076-9145-AB0674EE1BFB")
                });

                Role role1 = new Role
                {
                    ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                    Description = "测试角色1",
                    OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                    RoleId = new Guid("2F930EFA-6492-4076-9145-AB0674EE1BFB"),
                    RoleName = "测试角色1",
                    RolePermissions = RolePermissions1,
                };

                List<RolePermission> RolePermissions2 = new List<RolePermission>();
                RolePermissions2.Add(new RolePermission { PermissionId = new Guid("403304BF-B382-4100-A847-DC4EB6C89259"), RoleId = new Guid("7095D3D3-1638-4B42-84A0-84426531BD03") });
                Role role2 = new Role
                {
                    ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                    Description = "测试角色2",
                    OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                    RoleId = new Guid("7095D3D3-1638-4B42-84A0-84426531BD03"),
                    RoleName = "测试角色2",
                    RolePermissions = RolePermissions2,
                };

                db.Role.Add(role1);
                db.Role.Add(role2);
                db.SaveChanges();


            }
        }


        private static void UpdateRole()
        {
            try
            {
                using (var db = new AllInOneContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Role role = db.Role.Include(t=>t.RolePermissions).FirstOrDefault(p => p.RoleId.Equals(new Guid("2F930EFA-6492-4076-9145-AB0674EE1BFB")));
                        if (role.RolePermissions != null)
                        {
                            List<RolePermission> delList = new List<RolePermission>();
                            foreach (RolePermission rp in role.RolePermissions)
                            {
                                RolePermission del = db.RolePermission
                                    .FirstOrDefault(p => p.PermissionId.Equals(rp.PermissionId) && p.RoleId.Equals(rp.RoleId));
                                if (del != null)
                                {
                                    delList.Add(del);
                                }
                            }
                            db.RolePermission.RemoveRange(delList);
                            db.SaveChanges();
                        }

                        List<RolePermission> RolePermissions2 = new List<RolePermission>();
                        RolePermissions2.Add(new RolePermission { PermissionId = new Guid("403304BF-B382-4100-A847-DC4EB6C89259"), RoleId = new Guid("2F930EFA-6492-4076-9145-AB0674EE1BFB") });
                        role.RolePermissions = RolePermissions2;
                        db.Role.Update(role);
                        db.SaveChanges();

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static void DeleteRole()
        {
            try
            {
                using (var db = new AllInOneContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Role role = db.Role.Include(t => t.RolePermissions).FirstOrDefault(p => p.RoleId.Equals(new Guid("2f930efa-6492-4076-9145-ab0674ee1bfb")));
                        if (role.RolePermissions != null)
                        {
                            List<RolePermission> delList = new List<RolePermission>();
                            foreach (RolePermission rp in role.RolePermissions)
                            {
                                RolePermission del = db.RolePermission
                                    .FirstOrDefault(p => p.PermissionId.Equals(rp.PermissionId) && p.RoleId.Equals(rp.RoleId));
                                if (del != null)
                                {
                                    delList.Add(del);
                                }
                            }
                            db.RolePermission.RemoveRange(delList);
                            db.SaveChanges();
                        }
                        db.Role.Remove(role);
                        db.SaveChanges();

                        transaction.Commit();

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static void AddUser()
        {
            using (var db = new AllInOneContext())
            {
                User user1 = new User
                {
                    ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                    Description = "测试用户",
                    OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                    PasswordHash = "666",
                    UserId = new Guid("69C2152C-1EE7-4741-8183-D3B0B89F2CBB"),
                    UserName = "666",
                };

                User user2 = new User
                {
                    ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                    Description = "测试用户",
                    OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                    PasswordHash = "777",
                    UserId = new Guid("BEE6B361-6B09-420D-8F2C-B9B817DDFC42"),
                    UserName = "777",
                };

                db.User.Add(user1);
                db.User.Add(user2);
                db.SaveChanges();
            }
        }

        private static void UpdateUser()
        {
            try
            {
                using (var db = new AllInOneContext())
                {


                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static void DeleteUser()
        {
            using (var db = new AllInOneContext())
            {
                User delUser = db.User.First(p => p.UserId.Equals(new Guid("69C2152C-1EE7-4741-8183-D3B0B89F2CBB")));
                if (delUser != null)
                    db.User.Remove(delUser);
                db.SaveChanges();
            }
        }


        private static void AddStaff()
        {
            Staff staff = new Staff
            {
                ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                Description = "xxxxxxxxx",
                OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                //PhotoId = new Guid("6E64A5A3-5D0C-4A21-AB43-10E757C28852"),
                PositionTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                RankTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                SexId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                StaffId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"),
                StaffName = "张三",
                StaffCode = 1,
                //Photo = new UserPhoto
                //{
                //    PhotoData=new byte[] { 1,2,2,2,3},
                //    //UserPhotoId=new Guid("FC950E80-9FE7-400C-93B5-727FBC7BBF56"),
                    
                //},

            };

            Staff staff1 = new Staff
            {
                ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                Description = "xxxxxxxxx",
                OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                //PhotoId = new Guid("6E64A5A3-5D0C-4A21-AB43-10E757C28852"),
                PositionTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                RankTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                SexId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                StaffId = new Guid("03D3CE5C-C7FB-4317-8A30-8A58B1F82FA4"),
                StaffName = "李四",
                StaffCode = 2,
                //Photo = new UserPhoto
                //{
                //    PhotoData = new byte[] { 1, 2, 2, 2, 3 },
                //    UserPhotoId = new Guid("FC950E80-9FE7-400C-93B5-727FBC7BBF56"),

                //},

            };

            Staff staff2 = new Staff
            {
                ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                Description = "xxxxxxxxx",
                OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                //PhotoId = new Guid("6E64A5A3-5D0C-4A21-AB43-10E757C28852"),
                PositionTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                RankTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                SexId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                StaffId =Guid.NewGuid(),
                StaffName = "王五",
                StaffCode = 3,
                //Photo = new UserPhoto
                //{
                //    PhotoData = new byte[] { 1, 2, 2, 2, 3 },
                //    UserPhotoId = new Guid("FC950E80-9FE7-400C-93B5-727FBC7BBF56"),

                //},

            };


            Staff staff3 = new Staff
            {
                ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                Description = "xxxxxxxxx",
                OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                //PhotoId = new Guid("6E64A5A3-5D0C-4A21-AB43-10E757C28852"),
                PositionTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                RankTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                SexId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                StaffId = Guid.NewGuid(),
                StaffName = "123",
                StaffCode = 4,
                //Photo = new UserPhoto
                //{
                //    PhotoData = new byte[] { 1, 2, 2, 2, 3 },
                //    UserPhotoId = new Guid("FC950E80-9FE7-400C-93B5-727FBC7BBF56"),

                //},

            };

            Staff staff4 = new Staff
            {
                ApplicationId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                Description = "xxxxxxxxx",
                OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                //PhotoId = new Guid("6E64A5A3-5D0C-4A21-AB43-10E757C28852"),
                PositionTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                RankTypeId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                SexId = new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                StaffId = Guid.NewGuid(),
                StaffName = "456",
                StaffCode = 5,
                //Photo = new UserPhoto
                //{
                //    PhotoData = new byte[] { 1, 2, 2, 2, 3 },
                //    UserPhotoId = new Guid("FC950E80-9FE7-400C-93B5-727FBC7BBF56"),

                //},

            };

            using (var db = new AllInOneContext())
            {
                db.Staff.Add(staff);
                db.Staff.Add(staff1);
                db.Staff.Add(staff2);
                db.Staff.Add(staff3);
                db.Staff.Add(staff4);
                db.SaveChanges();
            }
        }


        public static void UpdateOrganization()
        {
            try
            {
                using (var db = new AllInOneContext())
                {
                    Organization org = db.Organization.Include(t=>t.Center)
                        .First(p => p.OrganizationId.Equals(new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8")));
                    if (org != null)
                    {
                        List<EndPointInfo> list = new List<EndPointInfo>();
                        list.Add(new EndPointInfo { IPAddress = "127.0.0.1", Port = 5000 });
                        org.Center = new ApplicationCenter
                        {
                            ApplicationCenterId = new Guid("8DB3D774-5F99-4AA5-BA30-73E401137837"),
                            ApplicationCenterCode = "777",
                            EndPoints = list,
                            RegisterUser = "PAPS",
                            RegisterPassword = "PAPS"
                        };
                        org.OrganizationTypeId = db.SystemOption.First().SystemOptionId;

                        db.Organization.Update(org);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void GetRole()
        {
            using (var db = new AllInOneContext())
            {
                var role = db.Role
                    .Include(t => t.RolePermissions).Include(t => t.UserManyToRole)
                    .Include(t => t.Organization).Include(t => t.Application)
                    .Include(t => t.ControlResourcesType);

            }
        }


        //public static void Update

        public static void AddDisposeOption()
        {
            try
            {
                using (var db = new AllInOneContext())
                {
                    db.SystemOption.AddRange(DataCache.GetFeedbackTypeSystemOption());
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void AddDutyCheckAppraise()
        {
            //try
            //{
            //    using (var db = new AllInOneContext())
            //    {
            //        List<DutyCheckAppraise> list = DataCache.GetDutyCheckAppraise();
            //        db.DutyCheckAppraise.AddRange(list);
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        #endregion


        #region 勤务模块数据测试

        #region TimePeriods
        private static void AddTimePeriods()
        {
            using (var db = new AllInOneContext())
            {
                TimePeriod TimePeriod1 = new TimePeriod
                {
                    StartTime = new DateTime(2016, 1, 1, 0, 0, 0),
                    EndTime= new DateTime(2016, 1, 1, 4, 0, 0),
                    OrderNo=1,
                    TimePeriodId=new Guid("7434C64A-477E-49F5-AEFB-6D7A1FBA21DB"),
                    TimePeriodExtra=new TimePeriodExtra
                    {
                        AbsoluteValue=10,
                        PercentValue=0,
                        ValueType=1
                    }
                };

                TimePeriod TimePeriod2 = new TimePeriod
                {
                    StartTime = new DateTime(2016, 1, 1, 4, 0, 0),
                    EndTime = new DateTime(2016, 1, 1, 8, 0, 0),
                    OrderNo = 2,
                    TimePeriodId = new Guid("E4795A2A-E10B-4341-A6E6-D725104212B1"),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 10,
                        PercentValue = 0,
                        ValueType = 1
                    }
                };

                TimePeriod TimePeriod3 = new TimePeriod
                {
                    StartTime = new DateTime(2016, 1, 1, 8, 0, 0),
                    EndTime = new DateTime(2016, 1, 1, 12, 0, 0),
                    OrderNo = 3,
                    TimePeriodId = new Guid("C8AC5A2E-A4E3-4038-83CF-9A8352287657"),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 10,
                        PercentValue = 0,
                        ValueType = 1
                    }
                };

                TimePeriod TimePeriod4 = new TimePeriod
                {
                    StartTime = new DateTime(2016, 1, 1, 16, 0, 0),
                    EndTime = new DateTime(2016, 1, 1, 20, 0, 0),
                    OrderNo = 4,
                    TimePeriodId = new Guid("F8EAAA7F-B80B-4508-999B-929F3E406C7C"),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 10,
                        PercentValue = 0,
                        ValueType = 1
                    }
                };

                TimePeriod TimePeriod5 = new TimePeriod
                {
                    StartTime = new DateTime(2016, 1, 1, 20, 0, 0),
                    EndTime = new DateTime(2016, 1, 1, 0, 0, 0),
                    OrderNo = 5,
                    TimePeriodId = new Guid("A7EF4B05-9B81-481A-AE1C-D3C155D87981"),
                    TimePeriodExtra = new TimePeriodExtra
                    {
                        AbsoluteValue = 10,
                        PercentValue = 0,
                        ValueType = 1
                    }
                };

                db.Set<TimePeriod>().Add(TimePeriod1);
                db.Set<TimePeriod>().Add(TimePeriod2);
                db.Set<TimePeriod>().Add(TimePeriod3);
                db.Set<TimePeriod>().Add(TimePeriod4);
                db.Set<TimePeriod>().Add(TimePeriod5);
                db.SaveChanges();
            }
        }




        #endregion

        #region ScheduleCycle
        //private static void AddScheduleCycle()
        //{
        //    using (var db = new AllInOneContext())
        //    {
        //        ScheduleCycle ScheduleCycle = new ScheduleCycle
        //        {
        //            CycleTypeId = new Guid("A0002016-E009-B019-E001-ABCD13700002"), //周
        //            ScheduleCycleId = new Guid("D2C9E91C-8D58-445C-B412-AAEF47F74FB2"),
        //            TimePeriods = db.Set<TimePeriod>().ToList(),
        //        };

        //        db.Set<ScheduleCycle>().Add(ScheduleCycle);
        //        db.SaveChanges();
        //    }
        //}

        //private static void UpdateScheduleCycle()
        //{
        //    using (var db = new AllInOneContext())
        //    {

        //        List<TimePeriod> list = new List<TimePeriod>();
        //        list.Add(new TimePeriod
        //        {
        //            StartTime = new DateTime(2016, 1, 1, 20, 0, 0),
        //            EndTime = new DateTime(2016, 1, 1, 0, 0, 0),
        //            OrderNo = 1,
        //            TimePeriodId = new Guid("BE4043A5-CD75-48C4-ACBD-B0DA6CDFDC10"),
        //        });

        //        list.Add(new TimePeriod
        //        {
        //            StartTime = new DateTime(2016, 1, 1, 0, 0, 0),
        //            EndTime = new DateTime(2016, 1, 1, 0, 20, 0),
        //            OrderNo = 2,
        //            TimePeriodId = new Guid("B861B224-6409-4332-A701-CBC20F7E5073"),
        //        });

        //        ScheduleCycle ScheduleCycle = db.Set<ScheduleCycle>().FirstOrDefault(p => p.ScheduleCycleId.Equals(new Guid("D2C9E91C-8D58-445C-B412-AAEF47F74FB2")));

        //        ScheduleCycle.TimePeriods = list;


        //        db.Set<ScheduleCycle>().Update(ScheduleCycle);
        //        db.SaveChanges();
        //    }
        //}



        #endregion

        #region Schedule

        private static void AddSchedule()
        {
            using (var db = new AllInOneContext())
            {
                Schedule Schedule = new Schedule
                {
                    ScheduleTypeId = new Guid("A0002016-E009-B019-E001-ABCD13200003"), //排程
                    ScheduleId = new Guid("62CEC38E-7E63-42EE-BF2C-CD95448A1B45"),
                    ScheduleName = "勤务排程",
                    ScheduleCycle = db.Set<ScheduleCycle>().FirstOrDefault(),
                    EffectiveTime=DateTime.Now,
                    ExpirationTime=DateTime.Now.AddMonths(1)
                };
                db.Schedule.Add(Schedule);
                db.SaveChanges();
            }
        }



        #endregion


        #region DutyCheckPackageTimePlan
        private static void AddDutyCheckPackageTimePlan()
        {
            using (var db = new AllInOneContext())
            {
                DutyCheckPackageTimePlan plan = new DutyCheckPackageTimePlan
                {
                    DutyCheckPackageTimePlanId = new Guid("5B12F776-023F-4660-859B-636968988FC0"),
                    OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                    RandomRate = Convert.ToDouble(0.25),
                    ScheduleId = new Guid("62CEC38E-7E63-42EE-BF2C-CD95448A1B45"),
                };

                db.DutyCheckPackageTimePlan.Add(plan);
                db.SaveChanges();
            }
        }

        #endregion


        #region DutySchedule
        private static void AddDutySchedule()
        {
            //
            //List<DutyCheckSchedule> dcs = new List<DutyCheckSchedule>();
            //dcs.Add(new DutyCheckSchedule { LeaderId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), CheckDay = DateTime.Now, CheckTimePeriodId = new Guid("7434C64A-477E-49F5-AEFB-6D7A1FBA21DB"), DutyCheckScheduleId = Guid.NewGuid() });
            //List<DutyCheckSchedule> dcs1 = new List<DutyCheckSchedule>();
            //dcs1.Add(new DutyCheckSchedule { LeaderId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), CheckDay = DateTime.Now, CheckTimePeriodId = new Guid("F8EAAA7F-B80B-4508-999B-929F3E406C7C"), DutyCheckScheduleId = Guid.NewGuid() });


            //List <DutyScheduleDetail> dsd = new List<DutyScheduleDetail>();
            //dsd.Add(new DutyScheduleDetail
            //{
            //    CadreSchedule = new DutyCheckSchedule { LeaderId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), CheckDay = DateTime.Now, CheckTimePeriodId = new Guid("7434C64A-477E-49F5-AEFB-6D7A1FBA21DB"), DutyCheckScheduleId = Guid.NewGuid() },
            //    DutyScheduleDetailId = Guid.NewGuid(),
            //    OfficerSchedule = new DutyCheckSchedule { LeaderId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), CheckDay = DateTime.Now, CheckTimePeriodId = new Guid("7434C64A-477E-49F5-AEFB-6D7A1FBA21DB"), DutyCheckScheduleId = Guid.NewGuid() },
            //    NetWatcherSchedule = dcs
            //});
            ////
            //dsd.Add(new DutyScheduleDetail
            //{
            //    CadreSchedule = new DutyCheckSchedule { LeaderId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), CheckDay = DateTime.Now, CheckTimePeriodId = new Guid("F8EAAA7F-B80B-4508-999B-929F3E406C7C"), DutyCheckScheduleId = Guid.NewGuid() },
            //    DutyScheduleDetailId = Guid.NewGuid(),
            //    OfficerSchedule = new DutyCheckSchedule { LeaderId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), CheckDay = DateTime.Now, CheckTimePeriodId = new Guid("F8EAAA7F-B80B-4508-999B-929F3E406C7C"), DutyCheckScheduleId = Guid.NewGuid() },
            //    NetWatcherSchedule = dcs1
            //});



            //try
            //{

            //    using (var db = new AllInOneContext())
            //    {
            //        DutySchedule ds = new DutySchedule
            //        {
            //            DutyScheduleId = Guid.NewGuid(),
            //            ListerId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"),
            //            EndDate = DateTime.Now.AddDays(10),
            //            OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
            //            ScheduleId = new Guid("62CEC38E-7E63-42EE-BF2C-CD95448A1B45"),
            //            StartDate = DateTime.Now.AddDays(-1),
            //            TabulationTime = DateTime.Now.AddDays(-2),
            //            //DutyScheduleDetails = dsd
            //        };

            //        db.DutySchedule.Add(ds);
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }




        #endregion


        #region DutyGroupSchedule

        //private static void AddSentry()
        //{
        //    using (var db = new AllInOneContext())
        //    {
        //        Sentinel sentinel = new Sentinel
        //        {
        //            BreakoutSwitch=1,
        //            SentinelId=new Guid("88342E95-A641-449E-977A-9CF13176D899"),
        //            DeviceInfoId=new Guid("DF16B48C-0B22-42D6-A068-EAFE73F5B946"),
                    
                    

        //        };

        //        Sentinel sentinel1 = new Sentinel
        //        {
        //            BreakoutSwitch = 1,
        //            SentinelId = new Guid("3096E153-B739-438A-807A-1BD64A2C7D46"),
        //            DeviceInfoId = new Guid("370AE732-F6D3-416C-9A6C-2C322FC68CF0"),


        //        };


        //        db.Sentinel.Add(sentinel);
        //        db.Sentinel.Add(sentinel1);
        //        db.SaveChanges();
        //    }
        //}



        private static void AddDutyGroupSchedule()
        {
            try
            {
                using (var db = new AllInOneContext())
                {
                    //
                    List<DutyCheckSiteSchedule> dcss = new List<DutyCheckSiteSchedule>();
                    dcss.Add(new DutyCheckSiteSchedule { DutyCheckSiteScheduleId = Guid.NewGuid(), CheckManId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), CheckDutySiteId = new Guid("88342E95-A641-449E-977A-9CF13176D899") });


                    //
                    List<DutyGroupScheduleDetail> details = new List<DutyGroupScheduleDetail>();
                    details.Add(new DutyGroupScheduleDetail { DutyGroupScheduleDetailId = Guid.NewGuid(), OrderNo = 1, CheckManId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), CheckDutySiteSchedule = dcss, });

                    //
                    List<EmergencyTeam> et = new List<EmergencyTeam>();
                    et.Add(new EmergencyTeam { StaffId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), DutyGroupScheduleId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A611") });

                    //
                    List<Reservegroup> rg = new List<Reservegroup>();
                    rg.Add(new Reservegroup { StaffId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"), DutyGroupScheduleId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A611") });


                    DutyGroupSchedule dgs = new DutyGroupSchedule
                    {
                        DutyGroupScheduleId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A611"),
                        EndDate = DateTime.Now.AddDays(10),
                        ListerId = new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"),
                        OrganizationId = new Guid("8865F9A3-EC17-4D13-9616-88496E8C51E8"),
                        ScheduleId = new Guid("62CEC38E-7E63-42EE-BF2C-CD95448A1B45"),
                        StartDate = DateTime.Now.AddDays(-1),
                        TabulationTime = DateTime.Now.AddDays(-2),
                        DutyGroupScheduleDetails = details,
                        EmergencyTeam = et,
                        Reservegroup = rg,
                    };

                    db.DutyGroupSchedule.Add(dgs);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {


            }
        }
        #endregion


        #region 评价类型
        private static void AddSystemOption()
        {
            List<SystemOption> _SystemOptions = new List<SystemOption>();

            SystemOption cycleOption = new SystemOption()
            {
                SystemOptionId = CreateGuid3("200"),
                SystemOptionCode = "200",
                SystemOptionName = "评价",
            };
            SystemOption dayCycle = new SystemOption()
            {

                SystemOptionId = CreateGuid8("20000001"),
                SystemOptionCode = "20000001",
                SystemOptionName = "好",
                ParentSystemOption = cycleOption
            };
            SystemOption weekCycle = new SystemOption()
            {

                SystemOptionId = CreateGuid8("20000002"),
                SystemOptionCode = "20000002",
                SystemOptionName = "中",
                ParentSystemOption = cycleOption
            };
            SystemOption monthCycle = new SystemOption()
            {

                SystemOptionId = CreateGuid8("20000003"),
                SystemOptionCode = "20000003",
                SystemOptionName = "差",
                ParentSystemOption = cycleOption
            };
            _SystemOptions.Add(cycleOption);
            _SystemOptions.Add(dayCycle);
            _SystemOptions.Add(weekCycle);
            _SystemOptions.Add(monthCycle);

            using (var db = new AllInOneContext())
            {
                db.SystemOption.AddRange(_SystemOptions);
                db.SaveChanges();
            }
        }


        private static void AddSystemOptionDispose()
        {
            List<SystemOption> _SystemOptions = new List<SystemOption>();

            SystemOption cycleOption = new SystemOption()
            {
                SystemOptionId = CreateGuid3("201"),
                SystemOptionCode = "201",
                SystemOptionName = "查勤处理方式",
            };
            SystemOption dayCycle = new SystemOption()
            {

                SystemOptionId = CreateGuid8("20100001"),
                SystemOptionCode = "20100001",
                SystemOptionName = "电话反馈给执勤人员",
                ParentSystemOption = cycleOption
            };
            SystemOption weekCycle = new SystemOption()
            {

                SystemOptionId = CreateGuid8("20100002"),
                SystemOptionCode = "20100002",
                SystemOptionName = "电话告知中队网络查勤员",
                ParentSystemOption = cycleOption
            };
            SystemOption monthCycle = new SystemOption()
            {

                SystemOptionId = CreateGuid8("20100003"),
                SystemOptionCode = "20100003",
                SystemOptionName = "电话告知支队网络查勤员",
                ParentSystemOption = cycleOption
            };
            _SystemOptions.Add(cycleOption);
            _SystemOptions.Add(dayCycle);
            _SystemOptions.Add(weekCycle);
            _SystemOptions.Add(monthCycle);

            using (var db = new AllInOneContext())
            {
                db.SystemOption.AddRange(_SystemOptions);
                db.SaveChanges();
            }
        }
        #endregion


        #region DutyCheckAppraise

        private static void AddDutyCheckAppraiseEx()
        {

            DutyCheckAppraise DutyCheckAppraise0 = new DutyCheckAppraise
            {
                AppraiseICO = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    AttachmentName = "无",
                    AttachmentPath = "",
                    AttachmentType = 1,
                    Modified = DateTime.Now,
                    ModifiedById = new Guid("3D9D3E6B-86C5-4043-8B4B-22F494D124A6"),
                },
                AppraiseTypeId = new Guid("A0002016-E009-B019-E001-ABCD20000001"),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000000"),
                DutyCheckAppraiseName = "无",
                OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),

            };


            DutyCheckAppraise DutyCheckAppraise = new DutyCheckAppraise
            {
                AppraiseICO = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    AttachmentName = "测1",
                    AttachmentPath = "",
                    AttachmentType = 1,
                    Modified = DateTime.Now,
                    ModifiedById = new Guid("3D9D3E6B-86C5-4043-8B4B-22F494D124A6"),
                },
                AppraiseTypeId = new Guid("A0002016-E009-B019-E001-ABCD20000002"),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000001"),
                DutyCheckAppraiseName = "执勤画面不清晰",
                OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),

            };

            DutyCheckAppraise DutyCheckAppraise1 = new DutyCheckAppraise
            {
                AppraiseICO = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    AttachmentName = "测11",
                    AttachmentPath = "",
                    AttachmentType = 1,
                    Modified = DateTime.Now,
                    ModifiedById = new Guid("3D9D3E6B-86C5-4043-8B4B-22F494D124A6"),
                },
                AppraiseTypeId = new Guid("A0002016-E009-B019-E001-ABCD20000002"),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000002"),
                DutyCheckAppraiseName = "姿态不端正",
                OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),

            };

            DutyCheckAppraise DutyCheckAppraise2 = new DutyCheckAppraise
            {
                AppraiseICO = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    AttachmentName = "测211",
                    AttachmentPath = "",
                    AttachmentType = 1,
                    Modified = DateTime.Now,
                    ModifiedById = new Guid("3D9D3E6B-86C5-4043-8B4B-22F494D124A6"),
                },
                AppraiseTypeId = new Guid("A0002016-E009-B019-E001-ABCD20000003"),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000003"),
                DutyCheckAppraiseName = "打瞌睡",
                OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),

            };

            DutyCheckAppraise DutyCheckAppraise3 = new DutyCheckAppraise
            {
                AppraiseICO = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    AttachmentName = "测211",
                    AttachmentPath = "",
                    AttachmentType = 1,
                    Modified = DateTime.Now,
                    ModifiedById = new Guid("3D9D3E6B-86C5-4043-8B4B-22F494D124A6"),
                },
                AppraiseTypeId = new Guid("A0002016-E009-B019-E001-ABCD20000003"),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000004"),
                DutyCheckAppraiseName = "执勤画面无图像",
                OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),

            };

            DutyCheckAppraise DutyCheckAppraise5 = new DutyCheckAppraise
            {
                AppraiseICO = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    AttachmentName = "测211",
                    AttachmentPath = "",
                    AttachmentType = 1,
                    Modified = DateTime.Now,
                    ModifiedById = new Guid("3D9D3E6B-86C5-4043-8B4B-22F494D124A6"),
                },
                AppraiseTypeId = new Guid("A0002016-E009-B019-E001-ABCD20000003"),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000005"),
                DutyCheckAppraiseName = "做与执勤无关的事情",
                OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),

            };

            DutyCheckAppraise DutyCheckAppraise6 = new DutyCheckAppraise
            {
                AppraiseICO = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    AttachmentName = "测211",
                    AttachmentPath = "",
                    AttachmentType = 1,
                    Modified = DateTime.Now,
                    ModifiedById = new Guid("3D9D3E6B-86C5-4043-8B4B-22F494D124A6"),
                },
                AppraiseTypeId = new Guid("A0002016-E009-B019-E001-ABCD20000003"),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000006"),
                DutyCheckAppraiseName = "不按规定携带武器装备",
                OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),

            };

            DutyCheckAppraise DutyCheckAppraise7 = new DutyCheckAppraise
            {
                AppraiseICO = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    AttachmentName = "测211",
                    AttachmentPath = "",
                    AttachmentType = 1,
                    Modified = DateTime.Now,
                    ModifiedById = new Guid("3D9D3E6B-86C5-4043-8B4B-22F494D124A6"),
                },
                AppraiseTypeId = new Guid("A0002016-E009-B019-E001-ABCD20000003"),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000007"),
                DutyCheckAppraiseName = "不按规定着装",
                OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),

            };

            DutyCheckAppraise DutyCheckAppraise8 = new DutyCheckAppraise
            {
                AppraiseICO = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    AttachmentName = "测211",
                    AttachmentPath = "",
                    AttachmentType = 1,
                    Modified = DateTime.Now,
                    ModifiedById = new Guid("3D9D3E6B-86C5-4043-8B4B-22F494D124A6"),
                },
                AppraiseTypeId = new Guid("A0002016-E009-B019-E001-ABCD20000003"),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000008"),
                DutyCheckAppraiseName = "语音联络不畅通",
                OrganizationId = new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),

            };

            using (var db = new AllInOneContext())
            {
                db.DutyCheckAppraise.Add(DutyCheckAppraise0);
                db.DutyCheckAppraise.Add(DutyCheckAppraise);
                db.DutyCheckAppraise.Add(DutyCheckAppraise1);
                db.DutyCheckAppraise.Add(DutyCheckAppraise2);
                db.DutyCheckAppraise.Add(DutyCheckAppraise3);
                db.DutyCheckAppraise.Add(DutyCheckAppraise5);
                db.DutyCheckAppraise.Add(DutyCheckAppraise6);
                db.DutyCheckAppraise.Add(DutyCheckAppraise7);
                db.DutyCheckAppraise.Add(DutyCheckAppraise8);
                db.SaveChanges();
            }
        }


        public static Guid CreateGuid3(string code)
        {
            return Guid.Parse("A0002016-E009-B019-E001-ABCDEF000" + code);
        }

        public static Guid CreateGuid8(string code)
        {
            return Guid.Parse("A0002016-E009-B019-E001-ABCD" + code);
        }
        #endregion

        #region DutyCheckLogDispose
        private static void AddDutyCheckLogDispose()
        {



        }



        #endregion


        #region DutyCheckLog

        private static void AddDutyCheckLog()
        {

            List<DutyCheckLogAppraise> appraises = new List<DutyCheckLogAppraise>();

            appraises.Add(new DutyCheckLogAppraise
            {
                DutyCheckLogAppraiseId = Guid.NewGuid(),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000005"),
                DutyCheckLogId = new Guid("8194ECB5-8538-4A13-B1E9-CAC61808797B")

            });

            appraises.Add(new DutyCheckLogAppraise
            {
                DutyCheckLogAppraiseId = Guid.NewGuid(),
                DutyCheckAppraiseId = new Guid("D0002016-E009-B019-E001-ABCD20000008"),
                DutyCheckLogId = new Guid("8194ECB5-8538-4A13-B1E9-CAC61808797B")

            });
            //
            List<DutyCheckLogDispose> disposes = new List<DutyCheckLogDispose>();
            disposes.Add(new DutyCheckLogDispose
            {
                DutyCheckLogDisposeId = Guid.NewGuid(),
                DisposeId = new Guid("A0002016-E009-B019-E001-ABCD20100001")
            });



            DutyCheckLog dutychecklog = new DutyCheckLog
            {
                Apprises= appraises,
                CircularTypes= disposes,
                Description="XXXXXXXXXXXX",
                DutyCheckLogId=Guid.NewGuid(),
                DutyCheckStaffId=new Guid("9CD482F3-39AC-42F2-93D2-F63E62F5A602"),
                MainAppriseId= new Guid("A0002016-E009-B019-E001-ABCD20000001"),
                OrganizationId=new Guid("1DFD13C6-820F-488D-84E0-A458CAF77B31"),
                RecordTime=DateTime.Now,
                RecordTypeId=new Guid("359A58FA-0BAB-45A3-ACAF-98EB73228B95"),
                StatusId=new Guid("24AC9875-C463-47B6-8147-5845874C3CAF"),
            };

            string txt = JsonConvert.SerializeObject(dutychecklog);

            using (var db = new AllInOneContext())
            {
                try
                {
                    //db.DutyCheckLog.Add(dutychecklog);
                    //db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }


        }


        #endregion




        #endregion


        private static void AddAlamrSetting()
        {
            using (var db = new AllInOneContext())
            {
              int[] weekdays = new int[] { 1, 2, 3, 4, 5 };
                List<DayPeriod> dayPeriods = new List<DayPeriod>();
                foreach (var weekday in weekdays)
                {
                    List<TimePeriod> times = new List<TimePeriod>();
                    for (int i = 0; i < 12; ++i)
                    {
                        TimePeriod mp = new TimePeriod();
                        mp.TimePeriodId = Guid.NewGuid();
                        mp.StartTime = DateTime.Now.Date.AddHours(i * 2);
                        mp.EndTime = DateTime.Now.Date.AddHours((i + 1) * 2);
                        times.Add(mp);
                    }
                    DayPeriod dp = new DayPeriod() {
                        DayPeriodId = Guid.NewGuid(),
                        DayOfWeek = weekday,
                        TimePeriods = times
                    };
                    dayPeriods.Add(dp);
                }

                ScheduleCycle scycle = new ScheduleCycle();
                scycle.ScheduleCycleId = Guid.NewGuid();
                scycle.DayPeriods = dayPeriods;
                scycle.WeekDays = new int[] { 1,2,3,4,5};
                scycle.CycleType = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("13700002"));

                Schedule schedule = new Schedule()
                {
                    ScheduleId = Guid.NewGuid(),
                    EffectiveTime = DateTime.Now,
                    ExpirationTime = DateTime.Now.AddYears(3),
                    ScheduleName = "多目标跟踪",
                    ScheduleCycle = scycle,
                    ScheduleType = db.SystemOption.First(t => t.SystemOptionCode.Equals("13200001"))
                };

                List<PredefinedAction> linkages = new List<PredefinedAction>();
                linkages.Add(new PredefinedAction() {
                    PredefinedActionId = Guid.NewGuid(),
                    Action = db.SystemOption.First(t=>t.SystemOptionName.Equals("视频预览")),
                    ActionArgument = "{.................................}",
                });
                List<PlanAction> planactions = new List<PlanAction>();
                planactions.Add(new PlanAction()
                {
                    PlanActionId = Guid.NewGuid(),
                    PlanDevice = db.IPDeviceInfo.First(),
                    PlanActions = linkages
                });
                Plan emergencyPlan = new Plan()
                {
                    PlanId = Guid.NewGuid(),
                    //Schedule = schedule,
                    Actions = planactions
                };

                AlarmSetting setting = new AlarmSetting() {
                    AlarmSettingId = Guid.NewGuid(),
                    AlarmLevel = db.SystemOption.First(t => t.SystemOptionCode.Equals("12900001")),
                    Schedule = schedule,
                    AlarmSourceId = db.Set<Camera>().First().IPDeviceId,
                    AlarmType = db.SystemOption.First(t=>t.SystemOptionName.Equals("移动侦测")),
                    EmergencyPlan = emergencyPlan
                };
                db.AlarmSetting.Add(setting);
                db.SaveChanges();
            }
        }

        //static void AddAlarmPeripheral()
        //{
        //    using (var db = new AllInOneContext())
        //    {
        //        SystemOption mainFrameDeviceType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11000003"));
        //        SystemOption alarmDeviceType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11000005"));

        //        IPDeviceInfo alarmMainframe = db.IPDeviceInfo.FirstOrDefault(t => t.IPDeviceName.Equals("编码器-192.168.20.101"));

        //        IPDeviceInfo alarmDevice1 = new IPDeviceInfo()
        //        {
        //            IPDeviceInfoId = Guid.NewGuid(),
        //            DeviceType = alarmDeviceType,
        //            ModifiedByUser = db.User.First(),
        //            Organization = db.Organization.First(),
        //            IPDeviceName = "编码器外接-1",
        //        };

        //        AlarmPeripheral hongwai1 = new AlarmPeripheral()
        //        {
        //            AlarmChannel = 3,
        //            AlarmDevice = alarmDevice1,
        //            AlarmMainframe = alarmMainframe,
        //            DefendArea = 0,
        //            AlarmPeripheralId = Guid.NewGuid(),
        //            AlarmType = db.SystemOption.First(t => t.SystemOptionCode.Equals("4009"))
        //        };

        //        IPDeviceInfo alarmDevice2 = new IPDeviceInfo()
        //        {
        //            IPDeviceInfoId = Guid.NewGuid(),
        //            DeviceType = alarmDeviceType,
        //            ModifiedByUser = db.User.First(),
        //            Organization = db.Organization.First(),
        //            IPDeviceName = "编码器外接-2",
        //        };

        //        AlarmPeripheral hongwai2 = new AlarmPeripheral()
        //        {
        //            AlarmChannel = 4,
        //            AlarmDevice = alarmDevice2,
        //            AlarmMainframe = alarmMainframe,
        //            DefendArea = 0,
        //            AlarmPeripheralId = Guid.NewGuid(),
        //            AlarmType = db.SystemOption.First(t => t.SystemOptionCode.Equals("4009"))
        //        };

        //        db.AlarmPeripheral.Add(hongwai1);
        //        db.AlarmPeripheral.Add(hongwai2);
        //        db.SaveChanges();
        //    }
        //}

        private static void AddTimerTask()
        {
            using (var db = new AllInOneContext())
            {
                int[] weekDays = new int[] { 0, 1, 2, 3, 4, 5 };
                List<DayPeriod> dayPeriods = new List<DayPeriod>();
                foreach (var weekDay in weekDays)
                {
                    DayPeriod p = new DayPeriod();

                    List<TimePeriod> times = new List<TimePeriod>();
                    for (int i = 0; i < 8; ++i)
                    {
                        TimePeriod mp = new TimePeriod();
                        mp.TimePeriodId = Guid.NewGuid();
                        mp.StartTime = DateTime.Now.Date.AddHours(i * 2);
                        mp.EndTime = DateTime.Now.Date.AddHours((i + 1) * 2);
                        times.Add(mp);
                    }
                    p.DayOfWeek = weekDay;
                    p.DayPeriodId = Guid.NewGuid();
                    p.TimePeriods = times;
                    dayPeriods.Add(p);
                }

                ScheduleCycle scycle = new ScheduleCycle();
                scycle.ScheduleCycleId = Guid.NewGuid();
                scycle.DayPeriods = dayPeriods;
                scycle.WeekDays = weekDays;
                scycle.CycleType = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("13700002"));

                Schedule schedule = new Schedule()
                {
                    ScheduleId = Guid.NewGuid(),
                    EffectiveTime = DateTime.Now,
                    ExpirationTime = DateTime.Now.AddYears(3),
                    ScheduleName = "定时预案计划周1-6",
                    ScheduleCycle = scycle,
                    ScheduleType = db.SystemOption.First(t => t.SystemOptionCode.Equals("13200001"))
                };

                string deviceid1 = db.IPDeviceInfo.First(t => t.IPDeviceName.Equals("camera-1")).IPDeviceInfoId.ToString();
                string deviceid2 = db.IPDeviceInfo.First(t => t.IPDeviceName.Equals("camera-2")).IPDeviceInfoId.ToString();
                string realPlay1Arg = "{ \"VideoDeviceId\":\"" + deviceid1 + "\",\"StreamType\":1,\"RoundInterval\":10}";
                string tvPlay1Arg = "{ \"VideoDeviceId\":\"" + deviceid1 + "\",\"Monitor\":1,\"SubView\":1,\"RoundInterval\":10,\"StreamType\":0}";
                string realPlay2Arg = "{ \"VideoDeviceId\":\"" + deviceid2 + "\",\"StreamType\":1,\"RoundInterval\":10}";
                string tvPlay2Arg = "{ \"VideoDeviceId\":\"" + deviceid2 + "\",\"Monitor\":2,\"SubView\":1,\"RoundInterval\":10,\"StreamType\":0}";

                List<PredefinedAction> linkages1 = new List<PredefinedAction>();
                linkages1.Add(new PredefinedAction()
                {
                    PredefinedActionId = Guid.NewGuid(),
                    Action = db.SystemOption.First(t => t.SystemOptionCode.Equals("13000001")),
                    ActionArgument =realPlay1Arg,
                });
                linkages1.Add(new PredefinedAction()
                {
                    PredefinedActionId = Guid.NewGuid(),
                    Action = db.SystemOption.First(t => t.SystemOptionCode.Equals("13000003")),
                    ActionArgument = tvPlay1Arg,
                });

                List<PredefinedAction> linkages2 = new List<PredefinedAction>();
                linkages2.Add(new PredefinedAction()
                {
                    PredefinedActionId = Guid.NewGuid(),
                    Action = db.SystemOption.First(t => t.SystemOptionCode.Equals("13000001")),
                    ActionArgument = realPlay2Arg,
                });
                linkages2.Add(new PredefinedAction()
                {
                    PredefinedActionId = Guid.NewGuid(),
                    Action = db.SystemOption.First(t => t.SystemOptionCode.Equals("13000003")),
                    ActionArgument =tvPlay2Arg,
                });

                List<PlanAction> planactions = new List<PlanAction>();
                planactions.Add(new PlanAction()
                {
                    PlanActionId = Guid.NewGuid(),
                    PlanDevice = db.IPDeviceInfo.First(t=>t.IPDeviceName.Equals("camera-1")),
                    PlanActions = linkages1
                });
                planactions.Add(new PlanAction()
                {
                    PlanActionId = Guid.NewGuid(),
                    PlanDevice = db.IPDeviceInfo.First(t => t.IPDeviceName.Equals("camera-2")),
                    PlanActions = linkages2
                });
                Plan tvPlan = new Plan()
                {
                    PlanId = Guid.NewGuid(),
                    //Schedule = schedule,
                    PlanName = "定时预案测试。。。",
                    Actions = planactions
                };

                TimerTask task = new TimerTask()
                {
                    TimerTaskId = Guid.NewGuid(),
                    TimerTaskName = "测试任务",
                    Plan = tvPlan,
                    TaskSchedule = schedule

                };
                db.TimerTask.Add(task);
                db.SaveChanges();
            }
        }


        static void AddAlarmLog()
        {
            using (var db = new AllInOneContext())
            {
                IPDeviceInfo alarmSource = db.IPDeviceInfo.Include(t=>t.Organization).First(t => t.IPDeviceName.Equals("camera-1"));
                SystemOption alarmStatus = db.SystemOption.First(t => t.SystemOptionCode.Equals("13100001"));
                SystemOption alarmType = db.SystemOption.First(t => t.SystemOptionCode.Equals("2001"));
                SystemOption alarmLevel = db.SystemOption.First(t => t.SystemOptionCode.Equals("12900001"));
                Application app = db.Application.First();

                AlarmLog log = new AlarmLog() {
                    AlarmLogId = Guid.NewGuid(),
                    TimeCreated = DateTime.Now,
                    AlarmType = alarmType,
                    AlarmSource = alarmSource,
                    //Organization = alarmSource.Organization,
                    AlarmLevel = alarmLevel,
                    Application = app
                };
                db.AlarmLog.Add(log);
                db.SaveChanges();
            }
        }

        static void AddServerTypeOption()
        {
            using (var db = new AllInOneContext())
            {
                var list = db.SystemOption.Where(t => t.SystemOptionCode.Contains("11300")).ToList();
                db.SystemOption.RemoveRange(list);
                var serverType = db.SystemOption.First(t => t.SystemOptionCode.Equals("113"));
                db.SaveChanges();

                #region 服务类型
                SystemOption sipOption = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "11300200",
                    SystemOptionName = "网关服务器",
                    SystemOptionId = DataCache.CreateGuid8("11300200"),
                    ParentSystemOption = serverType
                };
                SystemOption webServerOption = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "11300201",
                    SystemOptionName = "WEB应用服务",
                    SystemOptionId = DataCache.CreateGuid8("11300201"),
                    ParentSystemOption = serverType
                };
                SystemOption vfsOption = new SystemOption()
                {
                    Description = "媒体分发服务",
                    SystemOptionCode = "11300202",
                    SystemOptionName = "媒体分发服务",
                    SystemOptionId = DataCache.CreateGuid8("11300202"),
                    ParentSystemOption = serverType
                };

                SystemOption vssOption = new SystemOption()
                {
                    Description = "媒体存储服务",
                    SystemOptionCode = "11300204",
                    SystemOptionName = "媒体存储服务",
                    SystemOptionId = DataCache.CreateGuid8("11300204"),
                    ParentSystemOption = serverType
                };
                SystemOption dvmOption = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "11300205",
                    SystemOptionName = "数字矩阵中心",
                    SystemOptionId = DataCache.CreateGuid8("11300205"),
                    ParentSystemOption = serverType
                };

                SystemOption scsOption = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "11300206",
                    SystemOptionName = "哨位中心服务",
                    SystemOptionId = DataCache.CreateGuid8("11300206"),
                    ParentSystemOption = serverType
                };
                SystemOption dcpOption = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "11300207",
                    SystemOptionName = "设备控制代理",
                    SystemOptionId = DataCache.CreateGuid8("11300207"),
                    ParentSystemOption = serverType
                };
                db.SystemOption.AddRange(new SystemOption[] { sipOption, webServerOption, vfsOption, vssOption, scsOption, dvmOption, dcpOption });

                #endregion


                db.SaveChanges();
            }
        }


        static void AddDeviceStatusOption()
        {
            using (var db = new AllInOneContext())
            {
                SystemOption deviceStatuOption = new SystemOption()
                {
                    SystemOptionId = DataCache.CreateGuid8("00000138"),
                    SystemOptionCode = "138",
                    SystemOptionName = "设备状态",
                };
                SystemOption discNetwork = new SystemOption()
                {
                    SystemOptionId = DataCache.CreateGuid8("13800001"),
                    SystemOptionCode = "13800001",
                    SystemOptionName = "离线",
                    ParentSystemOption = deviceStatuOption
                };
                SystemOption novideo = new SystemOption()
                {
                    SystemOptionId = DataCache.CreateGuid8("13800002"),
                    SystemOptionCode = "13800002",
                    SystemOptionName = "无视频",
                    ParentSystemOption = deviceStatuOption
                };
                SystemOption videoNormal = new SystemOption()
                {
                    SystemOptionId = DataCache.CreateGuid8("13800003"),
                    SystemOptionCode = "13800003",
                    SystemOptionName = "视频正常",
                    ParentSystemOption = deviceStatuOption
                };
                SystemOption online = new SystemOption()
                {
                    SystemOptionId = DataCache.CreateGuid8("13800004"),
                    SystemOptionCode = "13800004",
                    SystemOptionName = "在线",
                    ParentSystemOption = deviceStatuOption
                };
                db.SystemOption.Add(deviceStatuOption);
                db.SystemOption.Add(discNetwork);
                db.SystemOption.Add(novideo);
                db.SystemOption.Add(videoNormal);
                db.SystemOption.Add(online);
                db.SaveChanges();
            }

        }

        static void AddAlarmStatusOption()
        {
            using (var db = new AllInOneContext())
            {
                var t1 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("131"));
                var t2 = db.SystemOption.Where(t => t.ParentSystemOptionId.Equals(t1.SystemOptionId)).ToList();
                db.SystemOption.RemoveRange(t2);
                db.SystemOption.Remove(t1);
                db.SaveChanges();
                SystemOption alarmStatus = new SystemOption()
                {
                    Description = "报警状态",
                    SystemOptionCode = "131",
                    SystemOptionName = "报警状态",
                    SystemOptionId = DataCache.CreateGuid8("00000131"),
                };

                SystemOption notDealStatus = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "13100001",
                    SystemOptionName = "未确认",
                    SystemOptionId = DataCache.CreateGuid8("13100001"),
                    ParentSystemOption = alarmStatus
                };
                SystemOption dealStatus = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "13100002",
                    SystemOptionName = "已确认",
                    SystemOptionId = DataCache.CreateGuid8("13100002"),
                    ParentSystemOption = alarmStatus
                };
                SystemOption closeStauts = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "13100003",
                    SystemOptionName = "已关闭",
                    SystemOptionId = DataCache.CreateGuid8("13100003"),
                    ParentSystemOption = alarmStatus
                };
                db.SystemOption.Add(alarmStatus);
                db.SystemOption.Add(notDealStatus);
                db.SystemOption.Add(dealStatus);
                db.SystemOption.Add(closeStauts);
                db.SaveChanges();
            }
        }

        static void AddDeviceGroup()
        {
            using (var db = new AllInOneContext())
            {
                //var tlist = JsonConvert.SerializeObject(db.IPDeviceInfo.
                //        Take(2).Select(t => t.IPDeviceInfoId).ToList());
                //Console.WriteLine(tlist);
                //Console.ReadLine();
                var cameraGroupType = db.SystemOption.First(t => t.SystemOptionCode.Equals("10900001"));

                DeviceGroup dg = new DeviceGroup() {
                    Description = "监控中心分组",
                    DeviceGroupId = Guid.NewGuid(),
                    DeviceGroupName = "一中队值班室",
                    DeviceGroupType = cameraGroupType,
                    Organization = db.Organization.First(),
                    Mondified = DateTime.Now,
                    ModifiedBy = db.User.First(),
                    DeviceListJson = JsonConvert.SerializeObject( db.IPDeviceInfo.Where(t => t.DeviceTypeId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD11000000"))).
                        Take(2).Select(t => t.IPDeviceInfoId).ToList()),
                    
                };
                db.DeviceGroup.Add(dg);
                db.SaveChanges();
            }
        }

        static void AddDeviceAlarmType()
        {
            using (var db = new AllInOneContext())
            {
                SystemOption alarmType = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("108"));

                SystemOption videoMoveAlarm = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "1001",
                    SystemOptionName = "移动侦测",
                    SystemOptionId = DataCache.CreateGuid8("00001001"),
                    ParentSystemOption = alarmType
                };
                SystemOption shelterAlarm = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "1002",
                    SystemOptionName = "视频遮挡",
                    SystemOptionId = DataCache.CreateGuid8("00001002"),
                    ParentSystemOption = alarmType
                };
                SystemOption videolostAlarm = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "1003",
                    SystemOptionName = "视频丢失",
                    SystemOptionId = DataCache.CreateGuid8("00001003"),
                    ParentSystemOption = alarmType
                };
                SystemOption banxianAlarm = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "1004",
                    SystemOptionName = "拌线报警",
                    SystemOptionId = DataCache.CreateGuid8("00001004"),
                    ParentSystemOption = alarmType
                };

                var cameraDeviceType = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("11000000"));
                DeviceAlarmMapping cam1 = new DeviceAlarmMapping() {
                    DeviceAlarmMappingId = Guid.NewGuid(),
                    AlarmType = videolostAlarm,
                    DeviceType = cameraDeviceType
                };
                DeviceAlarmMapping cam2 = new DeviceAlarmMapping()
                {
                    DeviceAlarmMappingId = Guid.NewGuid(),
                    AlarmType = videoMoveAlarm,
                    DeviceType = cameraDeviceType
                };
                DeviceAlarmMapping cam3 = new DeviceAlarmMapping()
                {
                    DeviceAlarmMappingId = Guid.NewGuid(),
                    AlarmType = shelterAlarm,
                    DeviceType = cameraDeviceType
                };
                DeviceAlarmMapping cam4 = new DeviceAlarmMapping()
                {
                    DeviceAlarmMappingId = Guid.NewGuid(),
                    AlarmType = banxianAlarm,
                    DeviceType = cameraDeviceType
                };

                db.Set<DeviceAlarmMapping>().Add(cam1);
                db.Set<DeviceAlarmMapping>().Add(cam2);
                db.Set<DeviceAlarmMapping>().Add(cam3);

                db.Set<DeviceAlarmMapping>().Add(cam4);
                db.SaveChanges();
            }
        }

        static void InitEncoder()
        {
            using (var db = new AllInOneContext())
            {
                var encoderType = db.Set<EncoderType>().FirstOrDefault(t => t.EncoderCode == 102002);
                IPDeviceInfo device20154 = new IPDeviceInfo()
                {
                    IPDeviceInfoId = Guid.NewGuid(),
                    IPDeviceName = "编码器192.168.20.154",
                    EndPointsJson = "[{IPAddress:\"192.168.20.154\",Port:8000}]",
                    Modified = DateTime.Now,
                    ModifiedByUserId = db.User.FirstOrDefault().UserId,
                    OrganizationId = db.Organization.First().OrganizationId,
                    Password = "123456",
                    UserName = "admin",
                    DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD11000001")
                };
                Resources.Model.Encoder encoder20154 = new Resources.Model.Encoder()
                {
                    EncoderId = Guid.NewGuid(),
                    DeviceInfo = device20154,
                    EncoderType = encoderType,
                };


                IPDeviceInfo device2088 = new IPDeviceInfo()
                {
                    IPDeviceInfoId = Guid.NewGuid(),
                    IPDeviceName = "编码器192.168.20.88",
                    EndPointsJson = "[{IPAddress:\"192.168.20.88\",Port:8000}]",
                    Modified = DateTime.Now,
                    ModifiedByUserId = db.User.FirstOrDefault().UserId,
                    OrganizationId = db.Organization.First().OrganizationId,
                    Password = "123456",
                    UserName = "admin",
                    DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD11000001")
                };
                Resources.Model.Encoder encoder2088 = new Resources.Model.Encoder()
                {
                    EncoderId = Guid.NewGuid(),
                    DeviceInfo = device2088,
                    EncoderType = encoderType,
                };

                db.Encoder.Add(encoder20154);
                db.Encoder.Add(encoder2088);
                db.SaveChanges();
            }
        }

        static void AddTemplate()
        {
            using (var db = new AllInOneContext())
            {
                List<TemplateCell> tcList = new List<TemplateCell>();
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        TemplateCell tcl = new TemplateCell()
                        {
                            TemplateCellId = Guid.NewGuid(),
                            Column = j,
                            Row = i,
                            ColumnSpan = 1,
                            RowSpan = 1
                        };
                        tcList.Add(tcl);
                    }
                }

                TemplateLayout tll = new TemplateLayout()
                {
                    Columns = 3,
                    Rows = 4,
                    TemplateLayoutId = Guid.NewGuid(),
                    TemplateLayoutName = "4x3",
                    LayoutType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11400001")),
                    TemplateType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11500001")),
                    Cells = tcList
                };
                db.TemplateLayout.Add(tll);
                db.SaveChanges();

                db.SaveChanges();
            }
        }

        static void AddIpDevice()
        {
            using (var db = new AllInOneContext())
            {
                SystemOption hujiaozhan = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "11000006",
                    SystemOptionName = "对讲呼叫站",
                    SystemOptionId = DataCache.CreateGuid8("11000006"),
                    ParentSystemOptionId = Guid.Parse("A0002016-E009-B019-E001-ABCDEF000110"),
                };

                SystemOption yijianqiuzhu = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "11000007",
                    SystemOptionName = "一键求助器",
                    SystemOptionId = DataCache.CreateGuid8("11000007"),
                    ParentSystemOptionId = Guid.Parse("A0002016-E009-B019-E001-ABCDEF000111"),
                };
                SystemOption yinpinbofang = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "11000008",
                    SystemOptionName = "音频播放器",
                    SystemOptionId = DataCache.CreateGuid8("11000008"),
                    ParentSystemOptionId = Guid.Parse("A0002016-E009-B019-E001-ABCDEF000112"),
                };

                db.SystemOption.Add(hujiaozhan);
                db.SystemOption.Add(yijianqiuzhu);
                db.SystemOption.Add(yinpinbofang);
                db.SaveChanges();
                IPDeviceInfo h1 = new IPDeviceInfo() {
                    IPDeviceInfoId = Guid.NewGuid(),
                    
                    DeviceTypeId = hujiaozhan.SystemOptionId,
                    IPDeviceName = "测试呼叫站",
                    OrganizationId = db.Organization.First().OrganizationId,
                    ModifiedByUserId = db.User.First().UserId,
                    EndPointsJson = "[{IPAddress:\"192.168.20.5\",Port:6000}]"
                };

                IPDeviceInfo h2 = new IPDeviceInfo()
                {
                    IPDeviceInfoId = Guid.NewGuid(),
                    DeviceTypeId = yijianqiuzhu.SystemOptionId,
                    IPDeviceName = "一件求助测试设备",
                    OrganizationId = db.Organization.First().OrganizationId,
                    ModifiedByUserId = db.User.First().UserId,
                    EndPointsJson = "[{IPAddress:\"192.168.20.5\",Port:6000}]"
                };

                IPDeviceInfo h3 = new IPDeviceInfo()
                {
                    IPDeviceInfoId = Guid.NewGuid(),
                    DeviceTypeId = yinpinbofang.SystemOptionId,
                    IPDeviceName = "音频播放测试设备",
                    OrganizationId = db.Organization.First().OrganizationId,
                    ModifiedByUserId = db.User.First().UserId,
                    EndPointsJson = "[{IPAddress:\"192.168.20.5\",Port:6000}]"
                };

                db.IPDeviceInfo.Add(h1);
                db.IPDeviceInfo.Add(h2);
                db.IPDeviceInfo.Add(h3);
                db.SaveChanges();
            }
        }

        static void AddPlanAction()
        {
            using (var db = new AllInOneContext())
            {
                SystemOption planAction = db.SystemOption.First(t => t.SystemOptionCode.Equals("130"));
                SystemOption openGunWarning = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "13000012",
                    SystemOptionName = "鸣枪警告",
                    SystemOptionId = DataCache.CreateGuid8("13000012"),
                    ParentSystemOption = planAction
                };
                SystemOption pushText = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "13000013",
                    SystemOptionName = "推送文字(",
                    SystemOptionId = DataCache.CreateGuid8("13000013"),
                    ParentSystemOption = planAction
                };
                SystemOption voiceReport = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "13000014",
                    SystemOptionName = "播报音频(",
                    SystemOptionId = DataCache.CreateGuid8("13000014"),
                    ParentSystemOption = planAction
                };

                SystemOption openAlarmLight = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "13000015",
                    SystemOptionName = "开启警灯(",
                    SystemOptionId = DataCache.CreateGuid8("13000015"),
                    ParentSystemOption = planAction
                };
                db.SystemOption.Add(openAlarmLight);
                db.SystemOption.Add(openGunWarning);
                db.SystemOption.Add(voiceReport);
                db.SystemOption.Add(pushText);
                db.SaveChanges();
            }

        }

     
        static void AddSentinel()
        {
            using (var db = new AllInOneContext())
            { 
                SentinelSetting sst = new SentinelSetting()
                {
                    SentinelSettingId = Guid.NewGuid(),
                    InlinePhone = "1002",
                    OutlinePhone = "1004",
                    ScreenViews = 4

                };

                IPDeviceInfo ip1 = new IPDeviceInfo() {
                    IPDeviceInfoId = Guid.NewGuid(),
                    DeviceTypeId = Guid.Parse("a0002016-e009-b019-e001-ab1100000401"),
                    EndPointsJson = "[{IPAddress:\"192.168.80.74\",Port:9999}]",
                    IPDeviceCode = 4,
                    IPDeviceName = "四号哨",
                    ModifiedByUserId = db.User.FirstOrDefault().UserId,
                    Modified = DateTime.Now,
                    OrganizationId = db.Organization.FirstOrDefault().OrganizationId
                };
                List<SentinelVideo> centerCam = new List<SentinelVideo>();
                SentinelVideo cam1 = new SentinelVideo() {
                    SentinelVideoId = Guid.NewGuid(),
                    CameraId = db.Set<Camera>().ToList()[2].CameraId,
                    VideoTypeId = db.SystemOption.FirstOrDefault().SystemOptionId
                };
                centerCam.Add(cam1);
                Sentinel sen = new Sentinel() {
                    SentinelId = Guid.NewGuid(),
                    //BreakoutSwitch =1,
                    //LeftArea=2,
                    //RightArea=3,
                    //RaidSwitch=4,
                    //RebellionSwitch=5,
                    //DisasterSwitch=6,
                    Phone = 1002,
                    DeviceInfo = ip1,
                    BulletboxCamera = new SentinelVideo() { PlayByDevice = false, OrderNo = 1, Camera = db.Set<Camera>().FirstOrDefault(), SentinelVideoId=Guid.NewGuid() },
                    FrontCamera = new SentinelVideo() { PlayByDevice = false, OrderNo = 1, Camera = db.Set<Camera>().FirstOrDefault(), SentinelVideoId = Guid.NewGuid() },
                    SentinelVideos = centerCam,
                    SentinelSetting = sst
                };

                DefenseDevice dd = new DefenseDevice();
                dd.DefenseDeviceId = Guid.NewGuid();
                dd.AlarmInNormalOpen = true;
                dd.AlarmIn = 1;
                dd.AlarmOut = 2;
                dd.SentinelId = sen.SentinelId;
               // dd.DefenseDirectionId = db.SystemOption.First(t => t.SystemOptionCode.Equals("13900001")).SystemOptionId;
                dd.DefenseNo = 2;
                dd.DeviceInfo = new IPDeviceInfo()
                {
                    IPDeviceCode = 3,
                    DeviceTypeId = db.SystemOption.First(t => t.SystemOptionCode.Equals("1100001101")).SystemOptionId,
                    ModifiedByUserId = db.User.First().UserId,
                    OrganizationId = db.Organization.First().OrganizationId,
                    IPDeviceName = "一号哨-红外1"
                };
                db.Sentinel.Add(sen);
                db.DefenseDevice.Add(dd);
               
                db.SaveChanges();
            }
        }

        static void AddEncoderType()
        {
            using (var db = new AllInOneContext())
            {
                db.Set<EncoderType>().AddRange(DataCache._EncoderTypes);
                db.SaveChanges();
            }
        }

        static void AddEncoder()
        {
            using (var db = new AllInOneContext())
            {
                Guid EncoderTypeId = db.Set<EncoderType>().FirstOrDefault(t => t.EncoderCode == 102005).EncoderTypeId;
                for (int i = 0; i < 10; i++)
                {
                    IPDeviceInfo deviceInfo = new IPDeviceInfo()
                    {
                        IPDeviceInfoId = Guid.NewGuid(),
                        IPDeviceName = "Test编码器" + i,
                        DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD11000001"),
                        OrganizationId = db.Organization.First().OrganizationId,
                        UserName = "admin",
                        Password = "123456",
                        ModifiedByUserId = db.User.First().UserId,
                        EndPointsJson = "[{\"IPAddress\":\"192.168.20.71\",\"Port\":8000}]"
                    };
                    Resources.Model.Encoder en = new Resources.Model.Encoder()
                    {
                        EncoderId = Guid.NewGuid(),//"34010000001130000001",
                        EncoderTypeId = EncoderTypeId,
                        DeviceInfo = deviceInfo
                    };
                    db.Encoder.Add(en);
                    db.SaveChanges();
                }
            }
        }

        static void AddVideoRoundScene()
        {
            using (var db = new AllInOneContext())
            {
                List<VideoRoundMonitorySiteSetting> settings = new List<VideoRoundMonitorySiteSetting>();
                int monitor = 1;
                foreach (MonitorySite ms2 in db.MonitorySite)
                {
                    VideoRoundMonitorySiteSetting mss = new VideoRoundMonitorySiteSetting()
                    {
                        Monitor = monitor++,
                        SubView = 1,
                        MonitorySite = ms2
                    };
                    settings.Add(mss);
                }

                VideoRoundSection vrs = new VideoRoundSection()
                {
                    VideoRoundSectionId = Guid.NewGuid(),
                    RoundInterval = 10,
                    RoundMonitorySiteSettings = settings.Take(9).ToList(),
                    TemplateLayout = db.TemplateLayout.FirstOrDefault(t=>t.TemplateLayoutName.Equals("预览模板-3x3"))
                };

                List<VideoRoundSection> sections = new List<VideoRoundSection>();
                sections.Add(vrs);
                VideoRoundScene scene = new VideoRoundScene()
                {
                    Modified = DateTime.Now,
                    ModifiedBy = db.User.First(),
                    VideoRoundSceneFlagId = db.SystemOption.First().SystemOptionId,
                    VideoRoundSceneId = Guid.NewGuid(),
                    VideoRoundSceneName = "视频轮巡场景1",
                    VideoRoundSections = sections
                };
                db.VideoRoundScene.Add(scene);
                db.SaveChanges();
            }
        }

        static void AddResourceControlSystemOptions()
        {
            using (var db = new AllInOneContext())
            {
                SystemOption guankongziyuan = new SystemOption()
                {
                    SystemOptionId = DataCache.CreateGuid3("144"),
                    SystemOptionCode = "144",
                    SystemOptionName = "管控资源",
                };
                SystemOption jiankongdian = new SystemOption()
                {
                    SystemOptionId = DataCache.CreateGuid8("14400001"),
                    SystemOptionCode = "14400001",
                    SystemOptionName = "监控点",
                    ParentSystemOption = guankongziyuan
                };
                db.SystemOption.Add(guankongziyuan);
                db.SystemOption.Add(jiankongdian);
                db.SaveChanges();
            }
        }

        private static void AddSentinelAlarmSetting()
        {
            using (var db = new AllInOneContext())
            {
                int[] weekdays = new int[] { 1, 2, 3, 4, 5,6 };
                List<DayPeriod> dayPeriods = new List<DayPeriod>();
                foreach (var weekday in weekdays)
                {
                    List<TimePeriod> times = new List<TimePeriod>();
                    for (int i = 0; i < 6; ++i)
                    {
                        TimePeriod mp = new TimePeriod();
                        mp.TimePeriodId = Guid.NewGuid();
                        mp.StartTime = DateTime.Now.Date.AddHours(i * 4);
                        if (i == 5)
                            mp.EndTime = DateTime.Now.Date.AddHours((i + 1) * 4).AddSeconds(-1);
                        else
                            mp.EndTime = DateTime.Now.Date.AddHours((i + 1) * 4);
                        times.Add(mp);
                    }
                    DayPeriod dp = new DayPeriod()
                    {
                        DayPeriodId = Guid.NewGuid(),
                        DayOfWeek = weekday,
                        TimePeriods = times
                    };
                    dayPeriods.Add(dp);
                }

                ScheduleCycle scycle = new ScheduleCycle();
                scycle.ScheduleCycleId = Guid.NewGuid();
                scycle.DayPeriods = dayPeriods;
                scycle.WeekDays = weekdays;
                scycle.CycleType = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("13700002"));

                Schedule schedule = new Schedule()
                {
                    ScheduleId = Guid.NewGuid(),
                    EffectiveTime = DateTime.Now,
                    ExpirationTime = DateTime.Now.AddYears(3),
                    ScheduleName = "哨位越狱报警",
                    ScheduleCycle = scycle,
                    ScheduleType = db.SystemOption.First(t => t.SystemOptionCode.Equals("13200001"))
                };

                var cams = db.Set<Camera>().Take(3).ToList();
           
                //视频联动
                List<PlanAction> planactions = new List<PlanAction>();
                foreach (var cam in cams)
                {
                    List<PredefinedAction> linkages = new List<PredefinedAction>();
                    string arg = "{ VideoDeviceId:\"" + cam.CameraId + "\",StreamType:0,RoundInterval:10,PresetSiteNo:0,Snapshot:true}";
                    linkages.Add(new PredefinedAction()
                    {
                        PredefinedActionId = Guid.NewGuid(),
                        Action = db.SystemOption.First(t => t.SystemOptionCode.Equals("13000001")),
                        ActionArgument = arg
                    });
                    planactions.Add(new PlanAction()
                    {
                        PlanActionId = Guid.NewGuid(),
                        PlanActions = linkages,
                        PlanDeviceId = cam.IPDeviceId
                    });
                }

                //打开子弹箱
                PredefinedAction openBullet = new PredefinedAction()
                {
                    ActionId = Guid.Parse("A0002016-E009-B019-E001-ABCD13000008"),
                    ActionArgument = "{ SentinelId:\"" + db.Sentinel.First().DeviceInfoId + "\",PasswordConfirm:true}",
                    PredefinedActionId = Guid.NewGuid(),
                };
                List<PredefinedAction> sentinelLinkage = new List<PredefinedAction>();
                sentinelLinkage.Add(openBullet);
                planactions.Add(new PlanAction()
                {
                    PlanActionId = Guid.NewGuid(),
                    PlanDeviceId = db.Sentinel.First().DeviceInfoId,
                    PlanActions = sentinelLinkage
                });
                
                Plan beforePlan = new Plan()
                {
                    PlanId = Guid.NewGuid(),
                    //Schedule = schedule,
                    Actions = planactions
                };

                AlarmSetting setting = new AlarmSetting()
                {
                    AlarmSettingId = Guid.NewGuid(),
                    AlarmLevel = db.SystemOption.First(t => t.SystemOptionCode.Equals("12900001")),
                    Schedule = schedule,
                    AlarmSourceId = db.Sentinel.FirstOrDefault().DeviceInfoId,
                    AlarmType = db.SystemOption.First(t => t.SystemOptionName.Equals("暴狱")),
                    BeforePlan = beforePlan
                };
                db.AlarmSetting.Add(setting);
                db.SaveChanges();
            }
        }

        static void AddSoundAndLightDEVICE()
        {
            using (var db = new AllInOneContext())
            {
                var sen = db.Sentinel.Include(t => t.DeviceInfo).FirstOrDefault();
                var devicetype = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("11000006"));
                var device = new IPDeviceInfo() {
                    IPDeviceInfoId = Guid.NewGuid(),
                    DeviceTypeId = devicetype.SystemOptionId,
                    EndPointsJson = "[{\"IPAddress\":\"192.168.80.209\",\"Port\":\"9999\"}]",
                    IPDeviceName = "声光报警209",
                    Modified = DateTime.Now,
                    ModifiedByUserId = db.User.First().UserId,
                    OrganizationId = sen.DeviceInfo.OrganizationId,
                }
                ;
                db.IPDeviceInfo.Add(device);
                db.SaveChanges();
            }
        }

        static void AddAlarmSetting()
        {
            using (var db = new AllInOneContext())
            {
                var plan = db.Plan.Include(t => t.Actions).ThenInclude(t => t.PlanActions).ThenInclude(t => t.Action).FirstOrDefault(t =>
                t.PlanId.Equals(Guid.Parse("20FCCB84-8F31-43B3-A04B-C4C4905FF356")));

                Guid soundAndLightDeviceId = db.IPDeviceInfo.FirstOrDefault(t => t.DeviceTypeId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD11000006"))).IPDeviceInfoId;

                PredefinedAction pushTextAction = new PredefinedAction()
                {
                    PredefinedActionId = Guid.NewGuid(),
                    ActionId = Guid.Parse("A0002016-E009-B019-E001-ABCD13000013"),
                    ActionArgument = "{SoundLightDeviceId:\"" + soundAndLightDeviceId.ToString() + "\"}"
                };
                List<PredefinedAction> actions = new List<PredefinedAction>();
                actions.Add(pushTextAction);
                PlanAction pc = new PlanAction() {
                    PlanActionId = Guid.NewGuid(),
                    PlanDeviceId = db.IPDeviceInfo.FirstOrDefault(t=>t.DeviceTypeId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCD11000006"))).IPDeviceInfoId,
                    PlanActions = actions
                };
                plan.Actions.Add(pc);
                db.SaveChanges();
            }
        }

        static void AddDutyServiceTypeOption()
        {
            using (var db = new AllInOneContext())
            {
                var baseOpt = DataCache._SystemOptions.FirstOrDefault(t => t.SystemOptionCode.Equals("181"));
                db.SystemOption.AddRange(baseOpt);
                db.SystemOption.AddRange(DataCache._SystemOptions.Where(t =>t.ParentSystemOption != null  && t.ParentSystemOption.Equals(baseOpt)).ToList());
                db.SaveChanges();
            }
        }

        static void AddDoubleDevice()
        {
            using (var db = new AllInOneContext())
            {
                var so = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("110"));
                SystemOption doubleLinkage = new SystemOption()
                {
                    Description = "",
                    SystemOptionCode = "11000012",
                    SystemOptionName = "两警联动",
                    SystemOptionId = CreateGuid8("11000012"),
                    ParentSystemOptionId = so.SystemOptionId
                };
                db.SystemOption.AddRange(doubleLinkage);
                db.SaveChanges();
            }
        }

        static void UpdatePlan()
        {
            using (var db = new AllInOneContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //先移除上一配置的Action
                        var dbPlan = db.Plan.Include(t => t.Actions).ThenInclude(t => t.PlanActions).ThenInclude(t => t.Action).
                        Include(t => t.Actions).ThenInclude(t => t.PlanDevice).ThenInclude(t => t.DeviceType).
                        FirstOrDefault(t => t.PlanId.Equals(Guid.Parse("355a2f70-4dca-b9fa-fca0-848525ba1bd3")));

                        dbPlan.Actions.ForEach(t => db.Set<PredefinedAction>().RemoveRange(t.PlanActions));
                        db.Set<PlanAction>().RemoveRange(dbPlan.Actions);
                        //db.Plan.Remove(dbPlan);
                        db.SaveChanges();

                        //重新赋值
                        List<PlanAction> actions = new List<PlanAction>();
                        actions.Add(new PlanAction() {
                            PlanActionId = Guid.NewGuid(),
                            PlanDeviceId = db.IPDeviceInfo.FirstOrDefault().IPDeviceInfoId
                        });
                        dbPlan.Actions = actions;
                        db.Plan.Update(dbPlan);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

    }
}
