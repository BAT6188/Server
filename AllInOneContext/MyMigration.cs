using AlarmAndPlan.Model;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Resources.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllInOneContext
{
    public class MyMigration
    {
        /// <summary>
        /// 数据迁移
        /// </summary>
        public static void Migrate()
        {
            using (AllInOneContext db = new AllInOneContext())
            {
                try
                {
                    //if (!db.Database.EnsureCreated())
                    //    db.Database.Migrate();
                    int count = db.SystemOption.Count();
                    //db.Organization.ToList().Where(t => t.OrganizationFullName.Equals("1"));
                }
                catch (Exception ex)
                {
                    db.Database.Migrate();
                }
            }
            InitData();
            InitDisposSystemOption();
            InitDutyCheckAppraise();
            InitDutyCheckTimePlan();
        }

        private static void InitDutyCheckTimePlan()
        {
            using (AllInOneContext db = new AllInOneContext())
            {
                var plan = db.DutyCheckPackageTimePlan;
                if (plan == null || plan.Count() == 0)
                {
                    DataCache.InitDutyCheckTimePlan();
                }
            }
        }

        private static void InitDisposSystemOption()
        {
            using (AllInOneContext db = new AllInOneContext())
            {
                var so = db.SystemOption.FirstOrDefault(p => p.SystemOptionCode == "250");
                if (so == null)
                {
                    var list = DataCache.GetFeedbackTypeSystemOption();
                    db.SystemOption.AddRange(list);
                    db.SaveChanges();
                }

            }
        }

        private static void InitDutyCheckAppraise()
        {
            using (AllInOneContext db = new AllInOneContext())
            {
                //确认没有就保存
                if (db.DutyCheckAppraise.Count() == 0)
                {
                    Organization org = db.Organization.OrderBy(p => p.OrderNo).ToList()[0];
                    db.DutyCheckAppraise.AddRange(DataCache.GetDutyCheckAppraise(org.OrganizationId));
                    db.SaveChanges();
                }
            }
        }

        static void InitData()
        {
            using (AllInOneContext db = new AllInOneContext())
            {
                if (db.SystemOption.Count() > 0)
                    return;
                var sentinelChannelMappings = DataCache.InitSentinelChannelOption();
                var er = DataCache._SystemOptions.GroupBy(t => t.SystemOptionId).Where(t => t.ToList().Count > 1).Select(t => t.Key).ToList();
                db.SystemOption.AddRange(DataCache._SystemOptions);
                db.Set<EncoderType>().AddRange(DataCache._EncoderTypes);
                db.Organization.AddRange(DataCache._Organizations);
                db.ApplicationSetting.AddRange(DataCache._ApplicationSettings);
                db.Application.AddRange(DataCache._Applications);
                db.User.AddRange(DataCache._Users);

                db.SaveChanges();

                AddStandarTemplateLayout(2, 2, Guid.Parse("A0002016-E009-B019-E001-ABCD11500001"));
                AddStandarTemplateLayout(3, 3, Guid.Parse("A0002016-E009-B019-E001-ABCD11500001"));
                AddStandarTemplateLayout(4, 4, Guid.Parse("A0002016-E009-B019-E001-ABCD11500001"));
                AddStandarTemplateLayout(1, 1, Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                AddStandarTemplateLayout(2, 2, Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                AddStandarTemplateLayout(3, 3, Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                AddStandarTemplateLayout(4, 4, Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                AddStandarTemplateLayout(5, 5, Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                AddStandarTemplateLayout(6, 6, Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                Add4and9RealTemplateLayout(Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                Add1and12RealTemplateLayout(Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                Add1and1and10RealTemplateLayout(Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                Add1and7RealTemplateLayout(Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                Add2and8RealTemplateLayout(Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));
                Add5and1TemplateLayout(Guid.Parse("A0002016-E009-B019-E001-ABCD11500002"));

                AddSentinelDeviceAlarmTypeMapping();
                AddDefneceDeviceAlarmTypeMapping();
                AddCameraAlarmTypeMapping();
                AddQiangzhiligangAlarmTypeMapping();
                db.SaveChanges();

                db.DeviceChannelTypeMapping.AddRange(sentinelChannelMappings);
                db.SaveChanges();



            }
        }

        #region 设备类型与报警
        /// <summary>
        /// 枪支离岗与报警关系
        /// </summary>
        static void AddQiangzhiligangAlarmTypeMapping()
        {
            using (var db = new AllInOneContext())
            {
                var qiangzhiligangAlarmType = db.SystemOption.Where(t => t.ParentSystemOptionId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCDEF000108")) &&
                    t.SystemOptionCode.CompareTo("4002") >= 0 && t.SystemOptionCode.CompareTo("4008") <= 0).ToList();
                List<DeviceAlarmMapping> list = new List<DeviceAlarmMapping>();
                qiangzhiligangAlarmType.ForEach(t => {
                    DeviceAlarmMapping atm = new DeviceAlarmMapping()
                    {
                        DeviceAlarmMappingId = Guid.NewGuid(),
                        DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-AB1100000301"),
                        AlarmTypeId = t.SystemOptionId
                    };
                    list.Add(atm);
                });
                db.AddRange(list);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 添加哨位台报警类型
        /// </summary>
        static void AddSentinelDeviceAlarmTypeMapping()
        {
            using (var db = new AllInOneContext())
            {
                var alarmTypeMappings = new List<DeviceAlarmMapping>();
                string[] zhinengalarmcode = new string[] { "2001", "2002", "2003", "2004" }; //暴逃袭灾
                string[] jingweialarmcode = new string[] { "2005", "2006", "2003", "2004" }; //冲破袭灾
                var sen1 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100000401"));
                var sen2 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100000402"));
                var sen3 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100000403"));
                var sen4 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100000404"));
                var sen5 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100000405"));
                var sen6 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100000406"));
                var liangjing = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("11000012")); 
                var zhinengAlarm = db.SystemOption.Where(t => zhinengalarmcode.Contains(t.SystemOptionCode)).ToList();
                zhinengAlarm.ForEach(t =>
                {
                    alarmTypeMappings.Add(new DeviceAlarmMapping()
                    {
                        DeviceAlarmMappingId = Guid.NewGuid(),
                        AlarmTypeId = t.SystemOptionId,
                        DeviceTypeId = sen1.SystemOptionId
                    });
                    alarmTypeMappings.Add(new DeviceAlarmMapping()
                    {
                        DeviceAlarmMappingId = Guid.NewGuid(),
                        AlarmTypeId = t.SystemOptionId,
                        DeviceTypeId = sen2.SystemOptionId
                    });
                    alarmTypeMappings.Add(new DeviceAlarmMapping()
                    {
                        DeviceAlarmMappingId = Guid.NewGuid(),
                        AlarmTypeId = t.SystemOptionId,
                        DeviceTypeId = sen5.SystemOptionId
                    });
                    alarmTypeMappings.Add(new DeviceAlarmMapping()
                    {
                        DeviceAlarmMappingId = Guid.NewGuid(),
                        AlarmTypeId = t.SystemOptionId,
                        DeviceTypeId = sen6.SystemOptionId
                    });
                    alarmTypeMappings.Add(new DeviceAlarmMapping()
                    {
                        DeviceAlarmMappingId = Guid.NewGuid(),
                        AlarmTypeId = t.SystemOptionId,
                        DeviceTypeId = liangjing.SystemOptionId
                    });
                });

                var jingweiAlarm = db.SystemOption.Where(t => jingweialarmcode.Contains(t.SystemOptionCode)).ToList();
                jingweiAlarm.ForEach(t =>
                {
                    alarmTypeMappings.Add(new DeviceAlarmMapping()
                    {
                        DeviceAlarmMappingId = Guid.NewGuid(),
                        AlarmTypeId = t.SystemOptionId,
                        DeviceTypeId = sen3.SystemOptionId
                    });
                    alarmTypeMappings.Add(new DeviceAlarmMapping()
                    {
                        DeviceAlarmMappingId = Guid.NewGuid(),
                        AlarmTypeId = t.SystemOptionId,
                        DeviceTypeId = sen4.SystemOptionId
                    });

                });
                db.DeviceAlarmMapping.AddRange(alarmTypeMappings);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 哨位防区设备与报警类型关系
        /// </summary>
        static void AddDefneceDeviceAlarmTypeMapping()
        {
            using (var db = new AllInOneContext())
            {
                List<DeviceAlarmMapping> alarmTypeMappings = new List<DeviceAlarmMapping>();
                alarmTypeMappings.Add(new DeviceAlarmMapping()
                {
                    DeviceAlarmMappingId = Guid.NewGuid(),
                    AlarmTypeId = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("2007")).SystemOptionId,
                    DeviceTypeId = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100001101")).SystemOptionId,
                });
                alarmTypeMappings.Add(new DeviceAlarmMapping()
                {
                    DeviceAlarmMappingId = Guid.NewGuid(),
                    AlarmTypeId = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("2009")).SystemOptionId,
                    DeviceTypeId = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100001102")).SystemOptionId,
                });
                alarmTypeMappings.Add(new DeviceAlarmMapping()
                {
                    DeviceAlarmMappingId = Guid.NewGuid(),
                    AlarmTypeId = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("2011")).SystemOptionId,
                    DeviceTypeId = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100001103")).SystemOptionId,
                });
                db.DeviceAlarmMapping.AddRange(alarmTypeMappings);
                db.SaveChanges();
            }
        }

        static void AddCameraAlarmTypeMapping()
        {
            using (var db = new AllInOneContext())
            {
                var cameraAlarmType = db.SystemOption.Where(t => t.ParentSystemOptionId.Equals(Guid.Parse("A0002016-E009-B019-E001-ABCDEF000108")) &&
                    t.SystemOptionCode.CompareTo("1001") >= 0 && t.SystemOptionCode.CompareTo("1021") <= 0).ToList();
                List<DeviceAlarmMapping> list = new List<DeviceAlarmMapping>();
                cameraAlarmType.ForEach(t => {
                    DeviceAlarmMapping atm = new DeviceAlarmMapping()
                    {
                        DeviceAlarmMappingId = Guid.NewGuid(),
                        DeviceTypeId = Guid.Parse("A0002016-E009-B019-E001-ABCD11000000"),
                        AlarmTypeId = t.SystemOptionId
                    };
                    list.Add(atm);
                });
                db.AddRange(list);
                db.SaveChanges();
            }
        }
        #endregion

        #region 模板初始数据
        /// <summary>
        /// 1+1+10
        /// </summary>
        /// <param name="TemplateTypeGuid"></param>
        static void Add1and1and10RealTemplateLayout(Guid TemplateTypeGuid)
        {
            using (var db = new AllInOneContext())
            {
                List<TemplateCell> tcList = new List<TemplateCell>();
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        TemplateCell tcl = new TemplateCell();
                        if (i == 0 && j == 0)
                        {
                            tcl.TemplateCellId = Guid.NewGuid();
                            tcl.Column = j;
                            tcl.Row = i;
                            tcl.RowSpan = 3;
                            tcl.ColumnSpan = 3;
                        }
                        else if (i == 0 && j == 3)
                        {
                            tcl.TemplateCellId = Guid.NewGuid();
                            tcl.Column = j;
                            tcl.Row = i;
                            tcl.RowSpan = 2;
                            tcl.ColumnSpan = 2;
                        }
                        else if (i <= 2 && j <= 2)
                        {
                            continue;
                        }
                        else if (i <= 1 && j >= 2)
                        {
                            continue;
                        }
                        else
                        {
                            tcl = new TemplateCell()
                            {
                                TemplateCellId = Guid.NewGuid(),
                                Column = j,
                                Row = i,
                                RowSpan = 1,
                                ColumnSpan = 1,
                            };
                        }
                        tcList.Add(tcl);
                    }
                }


                TemplateLayout tll = new TemplateLayout()
                {
                    Columns = 5,
                    Rows = 5,
                    TemplateLayoutId = Guid.NewGuid(),

                    LayoutType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11400002")),
                    TemplateType = db.SystemOption.First(t => t.SystemOptionId.Equals(TemplateTypeGuid)),
                    Cells = tcList
                };
                tll.TemplateLayoutName = "1+1+10";
                db.TemplateLayout.Add(tll);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 1+1+10
        /// </summary>
        /// <param name="TemplateTypeGuid"></param>
        static void Add1and7RealTemplateLayout(Guid TemplateTypeGuid)
        {
            using (var db = new AllInOneContext())
            {
                List<TemplateCell> tcList = new List<TemplateCell>();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        TemplateCell tcl = new TemplateCell();
                        if (i == 0 && j == 0)
                        {
                            tcl.TemplateCellId = Guid.NewGuid();
                            tcl.Column = j;
                            tcl.Row = i;
                            tcl.RowSpan = 3;
                            tcl.ColumnSpan = 3;
                        }
                        else if (i <= 2 && j <= 2)
                        {
                            continue;
                        }
                        else
                        {
                            tcl = new TemplateCell()
                            {
                                TemplateCellId = Guid.NewGuid(),
                                Column = j,
                                Row = i,
                                RowSpan = 1,
                                ColumnSpan = 1,
                            };
                        }
                        tcList.Add(tcl);
                    }
                }


                TemplateLayout tll = new TemplateLayout()
                {
                    Columns = 4,
                    Rows = 4,
                    TemplateLayoutId = Guid.NewGuid(),

                    LayoutType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11400002")),
                    TemplateType = db.SystemOption.First(t => t.SystemOptionId.Equals(TemplateTypeGuid)),
                    Cells = tcList
                };
                tll.TemplateLayoutName = "1+7";
                db.TemplateLayout.Add(tll);
                db.SaveChanges();
            }
        }


        /// <summary>
        /// 1+1+10
        /// </summary>
        /// <param name="TemplateTypeGuid"></param>
        static void Add1and12RealTemplateLayout(Guid TemplateTypeGuid)
        {
            using (var db = new AllInOneContext())
            {
                List<TemplateCell> tcList = new List<TemplateCell>();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        TemplateCell tcl = new TemplateCell();
                        if (i == 1 && j == 1)
                        {
                            tcl.TemplateCellId = Guid.NewGuid();
                            tcl.Column = j;
                            tcl.Row = i;
                            tcl.RowSpan = 2;
                            tcl.ColumnSpan = 2;
                        }
                        else if (i >= 1 && i <= 2 && j >= 1 && j <= 2)
                        {
                            continue;
                        }
                        else
                        {
                            tcl = new TemplateCell()
                            {
                                TemplateCellId = Guid.NewGuid(),
                                Column = j,
                                Row = i,
                                RowSpan = 1,
                                ColumnSpan = 1,
                            };
                        }
                        tcList.Add(tcl);
                    }
                }


                TemplateLayout tll = new TemplateLayout()
                {
                    Columns = 4,
                    Rows = 4,
                    TemplateLayoutId = Guid.NewGuid(),

                    LayoutType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11400002")),
                    TemplateType = db.SystemOption.First(t => t.SystemOptionId.Equals(TemplateTypeGuid)),
                    Cells = tcList
                };
                tll.TemplateLayoutName = "1+12";
                db.TemplateLayout.Add(tll);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 1+1+10
        /// </summary>
        /// <param name="TemplateTypeGuid"></param>
        static void Add2and8RealTemplateLayout(Guid TemplateTypeGuid)
        {
            using (var db = new AllInOneContext())
            {
                List<TemplateCell> tcList = new List<TemplateCell>();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        TemplateCell tcl = new TemplateCell();
                        if ((i == 0 && j == 0) || (i == 0 && j == 2))
                        {
                            tcl.TemplateCellId = Guid.NewGuid();
                            tcl.Column = j;
                            tcl.Row = i;
                            tcl.RowSpan = 2;
                            tcl.ColumnSpan = 2;
                        }
                        else if (i < 2 && j < 4)
                        {
                            continue;
                        }
                        else
                        {
                            tcl = new TemplateCell()
                            {
                                TemplateCellId = Guid.NewGuid(),
                                Column = j,
                                Row = i,
                                RowSpan = 1,
                                ColumnSpan = 1,
                            };
                        }
                        tcList.Add(tcl);
                    }
                }


                TemplateLayout tll = new TemplateLayout()
                {
                    Columns = 4,
                    Rows = 4,
                    TemplateLayoutId = Guid.NewGuid(),

                    LayoutType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11400002")),
                    TemplateType = db.SystemOption.First(t => t.SystemOptionId.Equals(TemplateTypeGuid)),
                    Cells = tcList
                };
                tll.TemplateLayoutName = "2+8";
                db.TemplateLayout.Add(tll);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 1+1+10
        /// </summary>
        /// <param name="TemplateTypeGuid"></param>
        static void Add4and9RealTemplateLayout(Guid TemplateTypeGuid)
        {
            using (var db = new AllInOneContext())
            {
                List<TemplateCell> tcList = new List<TemplateCell>();
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        TemplateCell tcl = new TemplateCell();
                        if ((i == 0 && j == 0) || (i == 0 && j == 2) || (i == 2 && j == 2) || (i == 2 && j == 0))
                        {
                            tcl.TemplateCellId = Guid.NewGuid();
                            tcl.Column = j;
                            tcl.Row = i;
                            tcl.RowSpan = 2;
                            tcl.ColumnSpan = 2;
                        }
                        else if (i < 4 && j < 4)
                        {
                            continue;
                        }
                        else
                        {
                            tcl = new TemplateCell()
                            {
                                TemplateCellId = Guid.NewGuid(),
                                Column = j,
                                Row = i,
                                RowSpan = 1,
                                ColumnSpan = 1,
                            };
                        }
                        tcList.Add(tcl);
                    }
                }


                TemplateLayout tll = new TemplateLayout()
                {
                    Columns = 5,
                    Rows = 5,
                    TemplateLayoutId = Guid.NewGuid(),

                    LayoutType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11400002")),
                    TemplateType = db.SystemOption.First(t => t.SystemOptionId.Equals(TemplateTypeGuid)),
                    Cells = tcList
                };
                tll.TemplateLayoutName = "4+9";
                db.TemplateLayout.Add(tll);
                db.SaveChanges();
            }
        }

        static void Add5and1TemplateLayout(Guid TemplateTypeGuid)
        {
            using (var db = new AllInOneContext())
            {
                List<TemplateCell> tcList = new List<TemplateCell>();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        TemplateCell tcl = new TemplateCell();
                        if (i == 0 && j == 0)
                        {
                            tcl.TemplateCellId = Guid.NewGuid();
                            tcl.Column = j;
                            tcl.Row = i;
                            tcl.RowSpan = 2;
                            tcl.ColumnSpan = 2;
                        }
                        else if (i <= 1 && j <= 1)
                        {
                            continue;
                        }
                        else
                        {
                            tcl = new TemplateCell()
                            {
                                TemplateCellId = Guid.NewGuid(),
                                Column = j,
                                Row = i,
                                RowSpan = 1,
                                ColumnSpan = 1,
                            };
                        }
                        tcList.Add(tcl);
                    }
                }


                TemplateLayout tll = new TemplateLayout()
                {
                    Columns = 3,
                    Rows = 3,
                    TemplateLayoutId = Guid.NewGuid(),

                    LayoutType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11400002")),
                    TemplateType = db.SystemOption.First(t => t.SystemOptionId.Equals(TemplateTypeGuid)),
                    Cells = tcList
                };
                tll.TemplateLayoutName = "1+5";
                db.TemplateLayout.Add(tll);
                db.SaveChanges();
            }
        }

        static void AddStandarTemplateLayout(int row, int column, Guid TemplateTypeGuid)
        {
            using (var db = new AllInOneContext())
            {
                List<TemplateCell> tcList = new List<TemplateCell>();
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        TemplateCell tcl = new TemplateCell()
                        {
                            TemplateCellId = Guid.NewGuid(),
                            Column = j,
                            Row = i,
                            RowSpan = 1,
                            ColumnSpan = 1,
                        };
                        tcList.Add(tcl);
                    }
                }

                TemplateLayout tll = new TemplateLayout()
                {
                    Columns = column,
                    Rows = row,
                    TemplateLayoutId = Guid.NewGuid(),

                    LayoutType = db.SystemOption.First(t => t.SystemOptionCode.Equals("11400001")),
                    TemplateType = db.SystemOption.First(t => t.SystemOptionId.Equals(TemplateTypeGuid)),
                    Cells = tcList
                };
                tll.TemplateLayoutName = string.Format("{0}", row * column);
                db.TemplateLayout.Add(tll);
                db.SaveChanges();
            }
        }

        public static void Update20161214()
        {
            //防区名称调整
            using (var db = new AllInOneContext())
            {
                var left1 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("13900001"));
                left1.SystemOptionName = "左防区";

                var right = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("13900002"));
                right.SystemOptionName = "右防区";


                var alarmMappings = db.Set<DeviceAlarmMapping>().ToList();
                db.Set<DeviceAlarmMapping>().RemoveRange(alarmMappings);
                db.SaveChanges();

                var alarm1 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("2008"));
                if (alarm1 != null)
                    db.SystemOption.Remove(alarm1);
                var alarm2 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("2010"));
                if (alarm2 != null)
                    db.SystemOption.Remove(alarm2);
                var alarm3 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("2012"));
                if (alarm3 != null)
                    db.SystemOption.Remove(alarm3);

                var at1 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("2007"));
                at1.SystemOptionName = "红外";

                var at2 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("2007"));
                at2.SystemOptionName = "高压";

                var at3 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("2007"));
                at3.SystemOptionName = "栅栏";
                db.SaveChanges();

                //设备类型
                var dt1 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100001101"));
                dt1.SystemOptionName = "红外";
                var dt2 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100001102"));
                dt2.SystemOptionName = "高压";
                var dt3 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100001103"));
                dt3.SystemOptionName = "栅栏";
                db.SaveChanges();

                var dt4 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100001104"));
                if (dt4 != null)
                    db.SystemOption.Remove(dt4);
                var dt5 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100001105"));
                if (dt5 != null)
                    db.SystemOption.Remove(dt5);
                var dt6 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("1100001106"));
                if (dt6 != null)
                    db.SystemOption.Remove(dt6);
                db.SaveChanges();
                AddSentinelDeviceAlarmTypeMapping();
                AddQiangzhiligangAlarmTypeMapping();
                AddCameraAlarmTypeMapping();

                var deleteStatus = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("13800005"));
                if (deleteStatus == null)
                {
                    deleteStatus = new Infrastructure.Model.SystemOption() {
                        Predefine=true,
                        ParentSystemOptionId = Guid.Parse("a0002016-e009-b019-e001-abcdef000138"),
                        SystemOptionId = Guid.Parse("a0002016-e009-b019-e001-abcd13800005"),
                        SystemOptionCode = "13800005",
                        SystemOptionName = "已删除",
                    };
                    db.SystemOption.Add(deleteStatus);
                    db.SaveChanges();
                }
            }
        }

        public static void Update20161219()
        {
            //防区名称调整
            using (var db = new AllInOneContext())
            {
                var baseOption = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("103"));
                if (baseOption == null)
                {
                    var bs = new SystemOption() {
                        SystemOptionCode = "103",
                        SystemOptionId = DataCache.CreateGuid3("103"),
                        SystemOptionName = "日志分类",
                        Description = "日志分类"
                    };
                    db.SystemOption.Add(bs);
                    db.SaveChanges();
                }

                var baguid = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("103")).SystemOptionId;
                var b1 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("10300001"));
                if (b1 != null)
                {
                    b1.ParentSystemOptionId = baguid;
                    db.SystemOption.Update(b1);
                }

                var b2 = db.SystemOption.FirstOrDefault(t => t.SystemOptionCode.Equals("10300002"));
                if (b2 != null)
                {
                    b2.ParentSystemOptionId = baguid;
                    db.SystemOption.Update(b2);
                }
                db.SaveChanges();
            }
        }

        public static void InitPhoneline()
        {
            using (var db = new AllInOneContext())
            {
                string baseOptionName = "报警拨号";
                string baseOptionCode = "147";
                string[] options = new string[] { "都不拨打", "哨位对讲", "内线", "都拨打" };
                string[] mappingcodes = new string[] { "0", "1", "2", "3" };
                int optionCode = 14700001;
                List<SystemOption> sysoptions = new List<SystemOption>();
                DataCache.NewSystemOptions(sysoptions, baseOptionName, baseOptionCode, options, mappingcodes, optionCode);
                db.AddRange(sysoptions);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
