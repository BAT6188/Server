using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AllInOneContext;

namespace AllInOneContext.Migrations
{
    [DbContext(typeof(AllInOneContext))]
    [Migration("20170104121007_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("AlarmAndPlan.Model.AlarmLog", b =>
                {
                    b.Property<Guid>("AlarmLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlarmLevelId");

                    b.Property<Guid>("AlarmSourceId");

                    b.Property<Guid?>("AlarmStatusId");

                    b.Property<Guid>("AlarmTypeId");

                    b.Property<Guid>("ApplicationId");

                    b.Property<string>("Description");

                    b.Property<Guid?>("OrganizationId");

                    b.Property<DateTime>("TimeCreated");

                    b.Property<int>("UploadCount");

                    b.Property<int>("UploadStatus");

                    b.HasKey("AlarmLogId");

                    b.HasIndex("AlarmLevelId");

                    b.HasIndex("AlarmSourceId");

                    b.HasIndex("AlarmStatusId");

                    b.HasIndex("AlarmTypeId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("AlarmLog");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.AlarmProcessed", b =>
                {
                    b.Property<Guid>("AlarmProcessedId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlarmLogId");

                    b.Property<string>("Conclusion");

                    b.Property<Guid?>("ProcessedByUserId");

                    b.Property<DateTime>("TimeProcessed");

                    b.HasKey("AlarmProcessedId");

                    b.HasIndex("AlarmLogId");

                    b.HasIndex("ProcessedByUserId");

                    b.ToTable("AlarmProcessed");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.AlarmSetting", b =>
                {
                    b.Property<Guid>("AlarmSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlarmLevelId");

                    b.Property<Guid>("AlarmSourceId");

                    b.Property<Guid>("AlarmTypeId");

                    b.Property<Guid?>("BeforePlanId");

                    b.Property<Guid?>("EmergencyPlanId");

                    b.Property<Guid>("ScheduleId");

                    b.HasKey("AlarmSettingId");

                    b.HasIndex("AlarmLevelId");

                    b.HasIndex("AlarmTypeId");

                    b.HasIndex("BeforePlanId");

                    b.HasIndex("EmergencyPlanId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("AlarmSourceId", "AlarmTypeId")
                        .IsUnique();

                    b.ToTable("AlarmSetting");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.DeviceAlarmMapping", b =>
                {
                    b.Property<Guid>("DeviceAlarmMappingId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlarmTypeId");

                    b.Property<Guid>("DeviceTypeId");

                    b.HasKey("DeviceAlarmMappingId");

                    b.HasIndex("AlarmTypeId");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("DeviceAlarmMapping");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.Plan", b =>
                {
                    b.Property<Guid>("PlanId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PlanName");

                    b.Property<Guid?>("PlanTypeId");

                    b.Property<Guid?>("RealVideoRoundSceneId");

                    b.Property<Guid?>("TvVideoRoundSceneId");

                    b.HasKey("PlanId");

                    b.HasIndex("PlanTypeId");

                    b.HasIndex("RealVideoRoundSceneId");

                    b.HasIndex("TvVideoRoundSceneId");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.PlanAction", b =>
                {
                    b.Property<Guid>("PlanActionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PlanDeviceId");

                    b.Property<Guid?>("PlanId");

                    b.HasKey("PlanActionId");

                    b.HasIndex("PlanDeviceId");

                    b.HasIndex("PlanId");

                    b.ToTable("PlanAction");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.PredefinedAction", b =>
                {
                    b.Property<Guid>("PredefinedActionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionArgument");

                    b.Property<Guid>("ActionId");

                    b.Property<Guid?>("PlanActionId");

                    b.HasKey("PredefinedActionId");

                    b.HasIndex("ActionId");

                    b.HasIndex("PlanActionId");

                    b.ToTable("PredefinedAction");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.ServiceEventLog", b =>
                {
                    b.Property<Guid>("ServiceEventLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid>("EventSourceId");

                    b.Property<Guid>("EventTypeId");

                    b.Property<DateTime>("TimeCreated");

                    b.HasKey("ServiceEventLogId");

                    b.HasIndex("EventSourceId");

                    b.HasIndex("EventTypeId");

                    b.ToTable("ServiceEventLog");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.TimerTask", b =>
                {
                    b.Property<Guid>("TimerTaskId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid?>("PlanId");

                    b.Property<Guid>("TaskScheduleId");

                    b.Property<string>("TimerTaskName");

                    b.HasKey("TimerTaskId");

                    b.HasIndex("PlanId");

                    b.HasIndex("TaskScheduleId");

                    b.ToTable("TimerTask");
                });

            modelBuilder.Entity("Infrastructure.Model.Application", b =>
                {
                    b.Property<Guid>("ApplicationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationCode");

                    b.Property<string>("ApplicationName");

                    b.Property<string>("Description");

                    b.HasKey("ApplicationId");

                    b.HasIndex("ApplicationName")
                        .IsUnique();

                    b.ToTable("Application");
                });

            modelBuilder.Entity("Infrastructure.Model.ApplicationCenter", b =>
                {
                    b.Property<Guid>("ApplicationCenterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationCenterCode");

                    b.Property<string>("EndPointsJson");

                    b.Property<string>("ParentApplicationCenterCode");

                    b.Property<string>("RegisterPassword");

                    b.Property<string>("RegisterUser");

                    b.HasKey("ApplicationCenterId");

                    b.ToTable("ApplicationCenter");
                });

            modelBuilder.Entity("Infrastructure.Model.ApplicationResource", b =>
                {
                    b.Property<Guid>("ApplicationResourceId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationId");

                    b.Property<string>("ApplicationResourceName");

                    b.Property<Guid?>("ParentResourceId");

                    b.HasKey("ApplicationResourceId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("ParentResourceId");

                    b.ToTable("ApplicationResource");
                });

            modelBuilder.Entity("Infrastructure.Model.ApplicationSetting", b =>
                {
                    b.Property<Guid>("ApplicationSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationId");

                    b.Property<string>("Description");

                    b.Property<string>("SettingKey");

                    b.Property<string>("SettingValue");

                    b.HasKey("ApplicationSettingId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("SettingKey")
                        .IsUnique();

                    b.ToTable("ApplicationSetting");
                });

            modelBuilder.Entity("Infrastructure.Model.ApplicationSystemOption", b =>
                {
                    b.Property<Guid>("ApplicationId");

                    b.Property<Guid>("SystemOptionId");

                    b.HasKey("ApplicationId", "SystemOptionId");

                    b.HasIndex("SystemOptionId");

                    b.ToTable("ApplicationSystemOption");
                });

            modelBuilder.Entity("Infrastructure.Model.Attachment", b =>
                {
                    b.Property<Guid>("AttachmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttachmentName");

                    b.Property<string>("AttachmentPath");

                    b.Property<int>("AttachmentType");

                    b.Property<double>("AttachmentVersion");

                    b.Property<string>("ContentType");

                    b.Property<DateTime>("Modified");

                    b.Property<Guid?>("ModifiedById");

                    b.HasKey("AttachmentId");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("Infrastructure.Model.ControlResources", b =>
                {
                    b.Property<Guid>("ControlResourcesId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ResourceTypeId");

                    b.Property<Guid?>("UserId");

                    b.HasKey("ControlResourcesId");

                    b.HasIndex("ResourceTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("ControlResources");
                });

            modelBuilder.Entity("Infrastructure.Model.DayPeriod", b =>
                {
                    b.Property<Guid>("DayPeriodId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Day");

                    b.Property<int>("DayOfWeek");

                    b.Property<Guid?>("ScheduleCycleId");

                    b.HasKey("DayPeriodId");

                    b.HasIndex("ScheduleCycleId");

                    b.ToTable("DayPeriod");
                });

            modelBuilder.Entity("Infrastructure.Model.EventLog", b =>
                {
                    b.Property<Guid>("EventLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationId");

                    b.Property<string>("EventData");

                    b.Property<Guid>("EventLevelId");

                    b.Property<Guid>("EventLogTypeId");

                    b.Property<Guid>("EventSourceId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<DateTime>("TimeCreated");

                    b.HasKey("EventLogId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("EventLevelId");

                    b.HasIndex("EventLogTypeId");

                    b.HasIndex("EventSourceId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("EventLog");
                });

            modelBuilder.Entity("Infrastructure.Model.Fingerprint", b =>
                {
                    b.Property<Guid>("FingerprintId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FigureNo");

                    b.Property<byte[]>("FingerprintBuffer");

                    b.Property<int>("FingerprintNo");

                    b.Property<Guid>("StaffId");

                    b.HasKey("FingerprintId");

                    b.HasIndex("StaffId");

                    b.ToTable("Fingerprint");
                });

            modelBuilder.Entity("Infrastructure.Model.OnlineUser", b =>
                {
                    b.Property<Guid>("OnLineUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("KeepAlived");

                    b.Property<Guid>("LoginTerminalId");

                    b.Property<DateTime>("LoginTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("OnLineUserId");

                    b.HasIndex("LoginTerminalId");

                    b.HasIndex("UserId");

                    b.ToTable("OnlineUser");
                });

            modelBuilder.Entity("Infrastructure.Model.Organization", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CenterId");

                    b.Property<string>("Description");

                    b.Property<int>("DutycheckPoints");

                    b.Property<Guid?>("InServiceTypeId");

                    b.Property<string>("OnDutyTarget");

                    b.Property<int>("OrderNo");

                    b.Property<string>("OrganizationCode");

                    b.Property<string>("OrganizationFullName");

                    b.Property<int>("OrganizationLevel");

                    b.Property<string>("OrganizationShortName")
                        .IsRequired();

                    b.Property<Guid?>("OrganizationTypeId");

                    b.Property<Guid?>("ParentOrganizationId");

                    b.Property<string>("Phone");

                    b.HasKey("OrganizationId");

                    b.HasIndex("CenterId");

                    b.HasIndex("InServiceTypeId");

                    b.HasIndex("OrganizationFullName")
                        .IsUnique();

                    b.HasIndex("OrganizationTypeId");

                    b.HasIndex("ParentOrganizationId");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("Infrastructure.Model.Permission", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ResourceId");

                    b.Property<Guid>("ResourcesActionId");

                    b.HasKey("PermissionId");

                    b.HasIndex("ResourceId");

                    b.HasIndex("ResourcesActionId");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("Infrastructure.Model.ResourcesAction", b =>
                {
                    b.Property<Guid>("ResourcesActionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationResourceId");

                    b.Property<string>("ResourcesActionName");

                    b.HasKey("ResourcesActionId");

                    b.HasIndex("ApplicationResourceId");

                    b.ToTable("ResourcesAction");
                });

            modelBuilder.Entity("Infrastructure.Model.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationId");

                    b.Property<Guid?>("ControlResourcesTypeId");

                    b.Property<string>("Description");

                    b.Property<Guid>("OrganizationId");

                    b.Property<string>("RoleName");

                    b.HasKey("RoleId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("ControlResourcesTypeId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("RoleName")
                        .IsUnique();

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Infrastructure.Model.RolePermission", b =>
                {
                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("PermissionId");

                    b.Property<Guid?>("RoleId1");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId1");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("Infrastructure.Model.Schedule", b =>
                {
                    b.Property<Guid>("ScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EffectiveTime");

                    b.Property<DateTime?>("ExpirationTime");

                    b.Property<Guid?>("ScheduleCycleId");

                    b.Property<string>("ScheduleName");

                    b.Property<Guid>("ScheduleTypeId");

                    b.HasKey("ScheduleId");

                    b.HasIndex("ScheduleCycleId");

                    b.HasIndex("ScheduleName")
                        .IsUnique();

                    b.HasIndex("ScheduleTypeId");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("Infrastructure.Model.ScheduleCycle", b =>
                {
                    b.Property<Guid>("ScheduleCycleId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CycleTypeId");

                    b.Property<string>("DaysJson");

                    b.Property<DateTime?>("LastExecute");

                    b.Property<string>("MonthsJson");

                    b.Property<DateTime?>("NextExecute");

                    b.Property<string>("WeekDayJson");

                    b.HasKey("ScheduleCycleId");

                    b.HasIndex("CycleTypeId");

                    b.ToTable("ScheduleCycle");
                });

            modelBuilder.Entity("Infrastructure.Model.Staff", b =>
                {
                    b.Property<Guid>("StaffId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationId");

                    b.Property<string>("ClassRow");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<Guid?>("DegreeOfEducationId");

                    b.Property<string>("Description");

                    b.Property<Guid?>("DutyCheckTypeId");

                    b.Property<string>("EnrolAddress");

                    b.Property<DateTime?>("EnrolTime");

                    b.Property<string>("FamilyPhone");

                    b.Property<Guid?>("MaritalStatusId");

                    b.Property<Guid?>("NationId");

                    b.Property<string>("NativePlace");

                    b.Property<Guid>("OrganizationId");

                    b.Property<DateTime?>("PartyTime");

                    b.Property<string>("Phone");

                    b.Property<Guid?>("PhotoId");

                    b.Property<Guid?>("PhysiclalStatusId");

                    b.Property<Guid?>("PoliticalLandscapeId");

                    b.Property<Guid?>("PositionTypeId");

                    b.Property<string>("PostalZipCode");

                    b.Property<Guid?>("RankTypeId");

                    b.Property<Guid?>("ReignStatusId");

                    b.Property<string>("ReligiousBelief");

                    b.Property<Guid>("SexId");

                    b.Property<int>("StaffCode");

                    b.Property<Guid?>("StaffGroupId");

                    b.Property<string>("StaffName");

                    b.Property<double?>("Stature");

                    b.Property<Guid?>("WorkingPropertyId");

                    b.HasKey("StaffId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("DegreeOfEducationId");

                    b.HasIndex("DutyCheckTypeId");

                    b.HasIndex("MaritalStatusId");

                    b.HasIndex("NationId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("PhotoId");

                    b.HasIndex("PhysiclalStatusId");

                    b.HasIndex("PoliticalLandscapeId");

                    b.HasIndex("PositionTypeId");

                    b.HasIndex("RankTypeId");

                    b.HasIndex("ReignStatusId");

                    b.HasIndex("SexId");

                    b.HasIndex("StaffGroupId");

                    b.HasIndex("StaffName")
                        .IsUnique();

                    b.HasIndex("WorkingPropertyId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("Infrastructure.Model.StaffGroup", b =>
                {
                    b.Property<Guid>("StaffGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationId");

                    b.Property<string>("Description");

                    b.Property<string>("GroupName");

                    b.Property<Guid>("OrganizationId");

                    b.HasKey("StaffGroupId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("StaffGroup");
                });

            modelBuilder.Entity("Infrastructure.Model.SystemOption", b =>
                {
                    b.Property<Guid>("SystemOptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("MappingCode");

                    b.Property<Guid?>("ParentSystemOptionId");

                    b.Property<bool>("Predefine");

                    b.Property<string>("SystemOptionCode");

                    b.Property<string>("SystemOptionName");

                    b.HasKey("SystemOptionId");

                    b.HasIndex("ParentSystemOptionId");

                    b.HasIndex("SystemOptionCode")
                        .IsUnique();

                    b.ToTable("SystemOption");
                });

            modelBuilder.Entity("Infrastructure.Model.TimePeriod", b =>
                {
                    b.Property<Guid>("TimePeriodId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DayPeriodId");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("ExtraJson");

                    b.Property<int>("OrderNo");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("TimePeriodId");

                    b.HasIndex("DayPeriodId");

                    b.ToTable("TimePeriod");
                });

            modelBuilder.Entity("Infrastructure.Model.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailed");

                    b.Property<Guid>("ApplicationId");

                    b.Property<string>("Description");

                    b.Property<bool>("Enable");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTime>("LockoutEndDateUtc");

                    b.Property<Guid>("OrganizationId");

                    b.Property<string>("PasswordHash");

                    b.Property<int>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("UserId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Infrastructure.Model.UserPhoto", b =>
                {
                    b.Property<Guid>("UserPhotoId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Modified");

                    b.Property<byte[]>("PhotoData");

                    b.HasKey("UserPhotoId");

                    b.ToTable("UserPhoto");
                });

            modelBuilder.Entity("Infrastructure.Model.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Infrastructure.Model.UserSetting", b =>
                {
                    b.Property<Guid>("UserSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("SettingKey");

                    b.Property<string>("SettingValue");

                    b.HasKey("UserSettingId");

                    b.ToTable("UserSetting");
                });

            modelBuilder.Entity("Infrastructure.Model.UserSettingMapping", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("UserSettingId");

                    b.HasKey("UserId", "UserSettingId");

                    b.HasIndex("UserSettingId");

                    b.ToTable("UserSettingMapping");
                });

            modelBuilder.Entity("Infrastructure.Model.UserTerminal", b =>
                {
                    b.Property<Guid>("UserTerminalId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserTerminalIP");

                    b.Property<string>("UserTerminalMac");

                    b.Property<Guid>("UserTerminalTypeId");

                    b.HasKey("UserTerminalId");

                    b.HasIndex("UserTerminalTypeId");

                    b.ToTable("UserTerminal");
                });

            modelBuilder.Entity("PAPS.Model.BulletboxLog", b =>
                {
                    b.Property<Guid>("BulletboxLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BulletboxSnapshotId");

                    b.Property<string>("ComfirmInfo");

                    b.Property<string>("Description");

                    b.Property<Guid?>("FrontSnapshotId");

                    b.Property<Guid>("LockStatusId");

                    b.Property<DateTime>("Modified");

                    b.Property<Guid?>("ModifiedById");

                    b.Property<Guid>("SentinelDeviceId");

                    b.HasKey("BulletboxLogId");

                    b.HasIndex("BulletboxSnapshotId");

                    b.HasIndex("FrontSnapshotId");

                    b.HasIndex("LockStatusId");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("SentinelDeviceId");

                    b.ToTable("BulletboxLog");
                });

            modelBuilder.Entity("PAPS.Model.Circular", b =>
                {
                    b.Property<Guid>("CircularId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CircularStaffId");

                    b.Property<DateTime>("CircularTime");

                    b.Property<string>("Description");

                    b.Property<Guid?>("DutyCheckLogId");

                    b.HasKey("CircularId");

                    b.HasIndex("CircularStaffId");

                    b.HasIndex("DutyCheckLogId");

                    b.ToTable("Circular");
                });

            modelBuilder.Entity("PAPS.Model.DailyOnDuty", b =>
                {
                    b.Property<Guid>("DailyOnDutyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("DutyDate");

                    b.Property<Guid>("DutyOfficerTodayId");

                    b.Property<int>("InNumber");

                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid?>("StatusId");

                    b.Property<int>("StrengthNumber");

                    b.Property<Guid>("TomorrowAttendantId");

                    b.HasKey("DailyOnDutyId");

                    b.HasIndex("DutyOfficerTodayId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TomorrowAttendantId");

                    b.ToTable("DailyOnDuty");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckAppraise", b =>
                {
                    b.Property<Guid>("DutyCheckAppraiseId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AppraiseICOId");

                    b.Property<Guid>("AppraiseTypeId");

                    b.Property<string>("Description");

                    b.Property<string>("DutyCheckAppraiseName");

                    b.Property<Guid>("OrganizationId");

                    b.HasKey("DutyCheckAppraiseId");

                    b.HasIndex("AppraiseICOId");

                    b.HasIndex("AppraiseTypeId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("DutyCheckAppraise");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckGroup", b =>
                {
                    b.Property<Guid>("DutyCheckGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DutyGroupName");

                    b.HasKey("DutyCheckGroupId");

                    b.ToTable("DutyCheckGroup");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckLog", b =>
                {
                    b.Property<Guid>("DutyCheckLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DayPeriodId");

                    b.Property<string>("Description");

                    b.Property<Guid?>("DutyCheckOperationId");

                    b.Property<Guid?>("DutyCheckSiteScheduleId");

                    b.Property<Guid?>("DutyCheckStaffId");

                    b.Property<Guid?>("DutycheckSiteId");

                    b.Property<string>("DutycheckSiteName");

                    b.Property<Guid?>("MainAppriseId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<DateTime?>("PlanDate");

                    b.Property<DateTime?>("RecordTime");

                    b.Property<Guid>("RecordTypeId");

                    b.Property<Guid?>("StatusId");

                    b.Property<string>("TimePeriodJson");

                    b.HasKey("DutyCheckLogId");

                    b.HasIndex("DayPeriodId");

                    b.HasIndex("DutyCheckOperationId");

                    b.HasIndex("DutyCheckSiteScheduleId");

                    b.HasIndex("DutyCheckStaffId");

                    b.HasIndex("MainAppriseId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("RecordTypeId");

                    b.HasIndex("StatusId");

                    b.ToTable("DutyCheckLog");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckLogAppraise", b =>
                {
                    b.Property<Guid>("DutyCheckLogAppraiseId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DutyCheckAppraiseId");

                    b.Property<Guid>("DutyCheckLogId");

                    b.HasKey("DutyCheckLogAppraiseId");

                    b.HasIndex("DutyCheckAppraiseId");

                    b.HasIndex("DutyCheckLogId");

                    b.ToTable("DutyCheckLogAppraise");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckLogDispose", b =>
                {
                    b.Property<Guid>("DutyCheckLogDisposeId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DisposeId");

                    b.Property<Guid>("DutyCheckLogId");

                    b.HasKey("DutyCheckLogDisposeId");

                    b.HasIndex("DisposeId");

                    b.HasIndex("DutyCheckLogId");

                    b.ToTable("DutyCheckLogDispose");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckMatter", b =>
                {
                    b.Property<Guid>("DutyCheckMatterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid?>("MatterICOId");

                    b.Property<string>("MatterName");

                    b.Property<int>("MatterScore");

                    b.Property<int>("OrderNo");

                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid?>("VoiceFileId");

                    b.HasKey("DutyCheckMatterId");

                    b.HasIndex("MatterICOId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("VoiceFileId");

                    b.ToTable("DutyCheckMatter");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckOperation", b =>
                {
                    b.Property<Guid>("DutyCheckOperationId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("DutyCheckOperationId");

                    b.ToTable("DutyCheckOperation");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckOperationAttachment", b =>
                {
                    b.Property<Guid>("DutyCheckOperationAttachmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AttachmentId");

                    b.Property<Guid>("AttachmentTypeId");

                    b.Property<Guid>("DutyCheckOperationId");

                    b.HasKey("DutyCheckOperationAttachmentId");

                    b.HasIndex("AttachmentId");

                    b.HasIndex("AttachmentTypeId");

                    b.HasIndex("DutyCheckOperationId");

                    b.ToTable("DutyCheckOperationAttachment");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckPackage", b =>
                {
                    b.Property<Guid>("DutyCheckPackageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndTime");

                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("PackageStatusId");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("DutyCheckPackageId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("PackageStatusId");

                    b.ToTable("DutyCheckPackage");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckPackageLog", b =>
                {
                    b.Property<Guid>("DutyCheckLogId");

                    b.Property<Guid>("DutyCheckPackageId");

                    b.HasKey("DutyCheckLogId", "DutyCheckPackageId");

                    b.HasIndex("DutyCheckPackageId");

                    b.ToTable("DutyCheckPackageLog");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckPackageTimePlan", b =>
                {
                    b.Property<Guid>("DutyCheckPackageTimePlanId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("OrganizationId");

                    b.Property<double>("RandomRate");

                    b.Property<Guid>("ScheduleId");

                    b.HasKey("DutyCheckPackageTimePlanId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("DutyCheckPackageTimePlan");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckSchedule", b =>
                {
                    b.Property<Guid>("DutyCheckScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CheckTimePeriodId");

                    b.Property<Guid?>("DeputyId");

                    b.Property<Guid?>("DutyScheduleDetailId");

                    b.Property<Guid>("LeaderId");

                    b.HasKey("DutyCheckScheduleId");

                    b.HasIndex("CheckTimePeriodId");

                    b.HasIndex("DeputyId");

                    b.HasIndex("DutyScheduleDetailId");

                    b.HasIndex("LeaderId");

                    b.ToTable("DutyCheckSchedule");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckSiteSchedule", b =>
                {
                    b.Property<Guid>("DutyCheckSiteScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CheckDutySiteId");

                    b.Property<Guid?>("CheckManId");

                    b.Property<Guid?>("DutyCheckGroupId");

                    b.Property<Guid?>("DutyGroupScheduleDetailId");

                    b.Property<Guid?>("SiteOrganizationId");

                    b.HasKey("DutyCheckSiteScheduleId");

                    b.HasIndex("CheckDutySiteId");

                    b.HasIndex("CheckManId");

                    b.HasIndex("DutyCheckGroupId");

                    b.HasIndex("DutyGroupScheduleDetailId");

                    b.HasIndex("SiteOrganizationId");

                    b.ToTable("DutyCheckSiteSchedule");
                });

            modelBuilder.Entity("PAPS.Model.DutyGroupSchedule", b =>
                {
                    b.Property<Guid>("DutyGroupScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsCancel");

                    b.Property<Guid>("ListerId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("ScheduleId");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("TabulationTime");

                    b.HasKey("DutyGroupScheduleId");

                    b.HasIndex("ListerId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("DutyGroupSchedule");
                });

            modelBuilder.Entity("PAPS.Model.DutyGroupScheduleDetail", b =>
                {
                    b.Property<Guid>("DutyGroupScheduleDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CheckManId");

                    b.Property<Guid?>("DutyGroupScheduleId");

                    b.Property<int>("OrderNo");

                    b.HasKey("DutyGroupScheduleDetailId");

                    b.HasIndex("CheckManId");

                    b.HasIndex("DutyGroupScheduleId");

                    b.ToTable("DutyGroupScheduleDetail");
                });

            modelBuilder.Entity("PAPS.Model.DutySchedule", b =>
                {
                    b.Property<Guid>("DutyScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid>("ListerId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("ScheduleId");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("TabulationTime");

                    b.HasKey("DutyScheduleId");

                    b.HasIndex("ListerId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("DutySchedule");
                });

            modelBuilder.Entity("PAPS.Model.DutyScheduleDetail", b =>
                {
                    b.Property<Guid>("DutyScheduleDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CadreScheduleId");

                    b.Property<DateTime>("CheckDay");

                    b.Property<Guid?>("DutyScheduleId");

                    b.Property<Guid>("OfficerScheduleId");

                    b.HasKey("DutyScheduleDetailId");

                    b.HasIndex("CadreScheduleId");

                    b.HasIndex("DutyScheduleId");

                    b.HasIndex("OfficerScheduleId");

                    b.ToTable("DutyScheduleDetail");
                });

            modelBuilder.Entity("PAPS.Model.EmergencyTeam", b =>
                {
                    b.Property<Guid>("DutyGroupScheduleId");

                    b.Property<Guid>("StaffId");

                    b.HasKey("DutyGroupScheduleId", "StaffId");

                    b.HasIndex("StaffId");

                    b.ToTable("EmergencyTeam");
                });

            modelBuilder.Entity("PAPS.Model.Fault", b =>
                {
                    b.Property<Guid>("FaultId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CheckDutySiteId");

                    b.Property<Guid?>("CheckManId");

                    b.Property<DateTime>("CircularTime");

                    b.Property<Guid?>("DutyCheckOperationId");

                    b.Property<Guid>("DutyOrganizationId");

                    b.Property<Guid>("FaultTypeId");

                    b.Property<string>("Remark");

                    b.HasKey("FaultId");

                    b.HasIndex("CheckDutySiteId");

                    b.HasIndex("CheckManId");

                    b.HasIndex("DutyCheckOperationId");

                    b.HasIndex("DutyOrganizationId");

                    b.HasIndex("FaultTypeId");

                    b.ToTable("Fault");
                });

            modelBuilder.Entity("PAPS.Model.Feedback", b =>
                {
                    b.Property<Guid>("FeedbackId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CircularId");

                    b.Property<string>("Description");

                    b.Property<Guid?>("FaultId");

                    b.Property<Guid>("FeedbackStaffId");

                    b.Property<DateTime>("FeedbackTime");

                    b.Property<int>("FeedbackType");

                    b.HasKey("FeedbackId");

                    b.HasIndex("CircularId");

                    b.HasIndex("FaultId");

                    b.HasIndex("FeedbackStaffId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("PAPS.Model.InstitutionsDutyCheckSchedule", b =>
                {
                    b.Property<Guid>("InstitutionsDutyCheckScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("EntouragesJson");

                    b.Property<Guid>("InspectedOrganizationId");

                    b.Property<string>("InspectionKey");

                    b.Property<string>("InspectionTargetJson");

                    b.Property<Guid>("LeadId");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("InstitutionsDutyCheckScheduleId");

                    b.HasIndex("InspectedOrganizationId");

                    b.HasIndex("LeadId");

                    b.ToTable("InstitutionsDutyCheckSchedule");
                });

            modelBuilder.Entity("PAPS.Model.PunchLog", b =>
                {
                    b.Property<Guid>("PunchLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AppraiseTypeId");

                    b.Property<Guid?>("BulletboxSnapshotId");

                    b.Property<Guid?>("FrontSnapshotId");

                    b.Property<Guid?>("LogResultId");

                    b.Property<DateTime>("LogTime");

                    b.Property<Guid>("PunchDeviceId");

                    b.Property<Guid?>("PunchTypeId");

                    b.Property<Guid?>("StaffId");

                    b.HasKey("PunchLogId");

                    b.HasIndex("AppraiseTypeId");

                    b.HasIndex("BulletboxSnapshotId");

                    b.HasIndex("FrontSnapshotId");

                    b.HasIndex("LogResultId");

                    b.HasIndex("PunchDeviceId");

                    b.HasIndex("PunchTypeId");

                    b.HasIndex("StaffId");

                    b.ToTable("PunchLog");
                });

            modelBuilder.Entity("PAPS.Model.Reservegroup", b =>
                {
                    b.Property<Guid>("DutyGroupScheduleId");

                    b.Property<Guid>("StaffId");

                    b.HasKey("DutyGroupScheduleId", "StaffId");

                    b.HasIndex("StaffId");

                    b.ToTable("Reservegroup");
                });

            modelBuilder.Entity("PAPS.Model.ShiftHandoverLog", b =>
                {
                    b.Property<Guid>("ShiftHandoverLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("HandoverDate");

                    b.Property<DateTime>("HandoverTime");

                    b.Property<Guid>("OffGoingId");

                    b.Property<Guid>("OnComingId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid?>("StatusId");

                    b.HasKey("ShiftHandoverLogId");

                    b.HasIndex("OffGoingId");

                    b.HasIndex("OnComingId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("StatusId");

                    b.ToTable("ShiftHandoverLog");
                });

            modelBuilder.Entity("PAPS.Model.TemporaryDuty", b =>
                {
                    b.Property<Guid>("TemporaryDutyId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Bullets");

                    b.Property<Guid>("CommanderId");

                    b.Property<string>("Contact");

                    b.Property<string>("DutyProgramme");

                    b.Property<Guid>("DutyProgrammePictureId");

                    b.Property<Guid>("DutyTypeId");

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("Guns");

                    b.Property<bool>("HasBullet");

                    b.Property<Guid>("OrganizationId");

                    b.Property<int>("Posts");

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("TaskName");

                    b.Property<int>("Troops");

                    b.Property<Guid>("VehicleTypeId");

                    b.HasKey("TemporaryDutyId");

                    b.HasIndex("CommanderId");

                    b.HasIndex("DutyProgrammePictureId");

                    b.HasIndex("DutyTypeId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("TemporaryDuty");
                });

            modelBuilder.Entity("Resources.Model.AlarmMainframe", b =>
                {
                    b.Property<Guid>("AlarmMainframeId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DeviceInfoId");

                    b.HasKey("AlarmMainframeId");

                    b.HasIndex("DeviceInfoId");

                    b.ToTable("AlarmMainframe");
                });

            modelBuilder.Entity("Resources.Model.AlarmPeripheral", b =>
                {
                    b.Property<Guid>("AlarmPeripheralId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlarmChannel");

                    b.Property<Guid>("AlarmDeviceId");

                    b.Property<Guid?>("AlarmMainframeId");

                    b.Property<Guid>("AlarmTypeId");

                    b.Property<int>("DefendArea");

                    b.Property<Guid?>("EncoderId");

                    b.HasKey("AlarmPeripheralId");

                    b.HasIndex("AlarmDeviceId")
                        .IsUnique();

                    b.HasIndex("AlarmMainframeId");

                    b.HasIndex("AlarmTypeId");

                    b.HasIndex("EncoderId");

                    b.ToTable("AlarmPeripheral");
                });

            modelBuilder.Entity("Resources.Model.Camera", b =>
                {
                    b.Property<Guid>("CameraId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CameraNo");

                    b.Property<int>("EncoderChannel");

                    b.Property<Guid>("EncoderId");

                    b.Property<Guid>("IPDeviceId");

                    b.Property<Guid?>("SnapshotId");

                    b.Property<Guid?>("VideoForwardId");

                    b.HasKey("CameraId");

                    b.HasIndex("IPDeviceId")
                        .IsUnique();

                    b.HasIndex("SnapshotId");

                    b.HasIndex("VideoForwardId");

                    b.HasIndex("EncoderId", "EncoderChannel");

                    b.ToTable("Camera");
                });

            modelBuilder.Entity("Resources.Model.CruiseScanGroup", b =>
                {
                    b.Property<Guid>("CruiseScanGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CameraId");

                    b.Property<int>("GroupIndex");

                    b.Property<string>("GroupName");

                    b.HasKey("CruiseScanGroupId");

                    b.HasIndex("CameraId", "GroupName")
                        .IsUnique();

                    b.ToTable("CruiseScanGroup");
                });

            modelBuilder.Entity("Resources.Model.CruiseScanGroupPresetSite", b =>
                {
                    b.Property<Guid>("CruiseScanGroupId");

                    b.Property<Guid>("PresetSiteID");

                    b.Property<int>("ScanInterval");

                    b.HasKey("CruiseScanGroupId", "PresetSiteID");

                    b.HasIndex("PresetSiteID");

                    b.ToTable("CruiseScanGroupPresetSite");
                });

            modelBuilder.Entity("Resources.Model.DefenseDevice", b =>
                {
                    b.Property<Guid>("DefenseDeviceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlarmIn");

                    b.Property<bool>("AlarmInNormalOpen");

                    b.Property<int>("AlarmOut");

                    b.Property<Guid?>("DefenseDirectionId");

                    b.Property<int>("DefenseNo");

                    b.Property<Guid>("DeviceInfoId");

                    b.Property<bool>("IsActive");

                    b.Property<Guid>("SentinelId");

                    b.HasKey("DefenseDeviceId");

                    b.HasIndex("DefenseDirectionId");

                    b.HasIndex("DeviceInfoId")
                        .IsUnique();

                    b.HasIndex("SentinelId");

                    b.ToTable("DefenseDevice");
                });

            modelBuilder.Entity("Resources.Model.DeviceChannelSetting", b =>
                {
                    b.Property<Guid>("DeviceChannelSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChannelNo");

                    b.Property<Guid>("ChannelTypeId");

                    b.Property<Guid?>("SentinelId");

                    b.HasKey("DeviceChannelSettingId");

                    b.HasIndex("ChannelTypeId");

                    b.HasIndex("SentinelId");

                    b.ToTable("DeviceChannelSetting");
                });

            modelBuilder.Entity("Resources.Model.DeviceChannelTypeMapping", b =>
                {
                    b.Property<Guid>("DeviceChannelTypeMappingId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChannelTypeId");

                    b.Property<Guid>("DeviceTypeId");

                    b.HasKey("DeviceChannelTypeMappingId");

                    b.HasIndex("ChannelTypeId");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("DeviceChannelTypeMapping");
                });

            modelBuilder.Entity("Resources.Model.DeviceGroup", b =>
                {
                    b.Property<Guid>("DeviceGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(128);

                    b.Property<string>("DeviceGroupName");

                    b.Property<Guid>("DeviceGroupTypeId");

                    b.Property<string>("DeviceListJson");

                    b.Property<Guid?>("ModifiedByUserId");

                    b.Property<DateTime?>("Mondified");

                    b.Property<Guid>("OrganizationId");

                    b.HasKey("DeviceGroupId");

                    b.HasIndex("DeviceGroupTypeId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("OrganizationId", "DeviceGroupName")
                        .IsUnique();

                    b.ToTable("DeviceGroup");
                });

            modelBuilder.Entity("Resources.Model.DeviceGroupIPDevice", b =>
                {
                    b.Property<Guid>("DeviceGroupId");

                    b.Property<Guid>("IPDeviceInfoId");

                    b.HasKey("DeviceGroupId", "IPDeviceInfoId");

                    b.HasIndex("IPDeviceInfoId");

                    b.ToTable("DeviceGroupIPDevice");
                });

            modelBuilder.Entity("Resources.Model.DeviceStatusHistory", b =>
                {
                    b.Property<Guid>("DeviceStatusHistoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("DeviceInfoId");

                    b.Property<Guid>("StatusId");

                    b.HasKey("DeviceStatusHistoryId");

                    b.HasIndex("DeviceInfoId");

                    b.HasIndex("StatusId");

                    b.ToTable("DeviceStatusHistory");
                });

            modelBuilder.Entity("Resources.Model.Encoder", b =>
                {
                    b.Property<Guid>("EncoderId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DeviceInfoId");

                    b.Property<Guid>("EncoderTypeId");

                    b.HasKey("EncoderId");

                    b.HasIndex("DeviceInfoId");

                    b.HasIndex("EncoderTypeId");

                    b.ToTable("Encoder");
                });

            modelBuilder.Entity("Resources.Model.EncoderType", b =>
                {
                    b.Property<Guid>("EncoderTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Channels");

                    b.Property<string>("DefaultPassword");

                    b.Property<int>("DefaultPort");

                    b.Property<string>("DefaultUserName");

                    b.Property<int>("EncoderCode");

                    b.Property<string>("EncoderTypeName");

                    b.Property<int>("OSDLines");

                    b.Property<int>("PTZ3DControl");

                    b.Property<string>("RecordFileExtension");

                    b.HasKey("EncoderTypeId");

                    b.ToTable("EncoderType");
                });

            modelBuilder.Entity("Resources.Model.IPDeviceInfo", b =>
                {
                    b.Property<Guid>("IPDeviceInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DeviceTypeId");

                    b.Property<string>("EndPointsJson");

                    b.Property<int>("IPDeviceCode");

                    b.Property<string>("IPDeviceName");

                    b.Property<DateTime>("Modified");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<string>("Password");

                    b.Property<string>("SeriesNo");

                    b.Property<Guid?>("StatusId");

                    b.Property<string>("UserName");

                    b.HasKey("IPDeviceInfoId");

                    b.HasIndex("DeviceTypeId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("StatusId");

                    b.ToTable("IPDeviceInfo");
                });

            modelBuilder.Entity("Resources.Model.Materiel", b =>
                {
                    b.Property<Guid>("MaterielId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MaterielCode");

                    b.Property<string>("MaterielName");

                    b.Property<Guid?>("TemporaryDutyId");

                    b.Property<Guid?>("UnitSystemOptionId");

                    b.HasKey("MaterielId");

                    b.HasIndex("TemporaryDutyId");

                    b.HasIndex("UnitSystemOptionId");

                    b.ToTable("Materiel");
                });

            modelBuilder.Entity("Resources.Model.MonitorySite", b =>
                {
                    b.Property<Guid>("MonitorySiteId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CameraId");

                    b.Property<string>("Description");

                    b.Property<string>("InstallAddress");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDutycheckSite");

                    b.Property<string>("MonitorySiteName")
                        .HasMaxLength(64);

                    b.Property<int>("OrderNo");

                    b.Property<Guid>("OrganizationId");

                    b.Property<int>("Phone");

                    b.HasKey("MonitorySiteId");

                    b.HasIndex("CameraId")
                        .IsUnique();

                    b.HasIndex("OrganizationId", "MonitorySiteName")
                        .IsUnique();

                    b.ToTable("MonitorySite");
                });

            modelBuilder.Entity("Resources.Model.PresetSite", b =>
                {
                    b.Property<Guid>("PresetSiteId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CameraId");

                    b.Property<byte>("PresetSiteNo");

                    b.Property<string>("PresetSizeName");

                    b.HasKey("PresetSiteId");

                    b.HasIndex("CameraId", "PresetSizeName")
                        .IsUnique();

                    b.ToTable("PresetSite");
                });

            modelBuilder.Entity("Resources.Model.Sentinel", b =>
                {
                    b.Property<Guid>("SentinelId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AudioFileId");

                    b.Property<Guid?>("BulletboxCameraId");

                    b.Property<Guid>("DeviceInfoId");

                    b.Property<Guid?>("FrontCameraId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("Phone");

                    b.Property<Guid?>("SentinelSettingId");

                    b.HasKey("SentinelId");

                    b.HasIndex("AudioFileId");

                    b.HasIndex("BulletboxCameraId");

                    b.HasIndex("DeviceInfoId");

                    b.HasIndex("FrontCameraId");

                    b.HasIndex("SentinelSettingId");

                    b.ToTable("Sentinel");
                });

            modelBuilder.Entity("Resources.Model.SentinelFingerPrintMapping", b =>
                {
                    b.Property<Guid>("SentinelFingerPrintMappingId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("FingerprintId");

                    b.Property<Guid>("SentinelId");

                    b.HasKey("SentinelFingerPrintMappingId");

                    b.HasIndex("FingerprintId");

                    b.HasIndex("SentinelId");

                    b.ToTable("SentinelFingerPrintMapping");
                });

            modelBuilder.Entity("Resources.Model.SentinelLayout", b =>
                {
                    b.Property<Guid>("SentinelLayoutId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MapDataXml");

                    b.Property<string>("Name");

                    b.HasKey("SentinelLayoutId");

                    b.ToTable("SentinelLayout");
                });

            modelBuilder.Entity("Resources.Model.SentinelSetting", b =>
                {
                    b.Property<Guid>("SentinelSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlarmCallType");

                    b.Property<string>("InlinePhone");

                    b.Property<string>("OutlinePhone");

                    b.Property<int>("ScreenViews");

                    b.HasKey("SentinelSettingId");

                    b.ToTable("SentinelSetting");
                });

            modelBuilder.Entity("Resources.Model.SentinelVideo", b =>
                {
                    b.Property<Guid>("SentinelVideoId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CameraId");

                    b.Property<int>("OrderNo");

                    b.Property<bool>("PlayByDevice");

                    b.Property<Guid?>("SentinelId");

                    b.Property<Guid?>("VideoTypeId");

                    b.HasKey("SentinelVideoId");

                    b.HasIndex("CameraId");

                    b.HasIndex("SentinelId");

                    b.HasIndex("VideoTypeId");

                    b.ToTable("SentinelVideo");
                });

            modelBuilder.Entity("Resources.Model.ServerInfo", b =>
                {
                    b.Property<Guid>("ServerInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndPointsJson");

                    b.Property<DateTime>("Modified");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<string>("ServerName")
                        .HasMaxLength(32);

                    b.HasKey("ServerInfoId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("OrganizationId", "ServerName")
                        .IsUnique();

                    b.ToTable("ServerInfo");
                });

            modelBuilder.Entity("Resources.Model.ServiceInfo", b =>
                {
                    b.Property<Guid>("ServiceInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndPointsJson");

                    b.Property<DateTime>("Modified");

                    b.Property<Guid>("ModifiedByUserId");

                    b.Property<string>("Password");

                    b.Property<Guid>("ServerInfoId");

                    b.Property<string>("ServiceName")
                        .HasMaxLength(32);

                    b.Property<Guid>("ServiceTypeId");

                    b.Property<string>("Username");

                    b.HasKey("ServiceInfoId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("ServerInfoId");

                    b.HasIndex("ServiceTypeId");

                    b.ToTable("ServiceInfo");
                });

            modelBuilder.Entity("Resources.Model.TemplateCell", b =>
                {
                    b.Property<Guid>("TemplateCellId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Column");

                    b.Property<int>("ColumnSpan");

                    b.Property<int>("Row");

                    b.Property<int>("RowSpan");

                    b.Property<Guid?>("TemplateLayoutId");

                    b.Property<int>("ViewCount");

                    b.HasKey("TemplateCellId");

                    b.HasIndex("TemplateLayoutId");

                    b.ToTable("TemplateCell");
                });

            modelBuilder.Entity("Resources.Model.TemplateLayout", b =>
                {
                    b.Property<Guid>("TemplateLayoutId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Columns");

                    b.Property<Guid>("LayoutTypeId");

                    b.Property<int>("Rows");

                    b.Property<string>("TemplateLayoutName")
                        .HasMaxLength(32);

                    b.Property<Guid>("TemplateTypeId");

                    b.HasKey("TemplateLayoutId");

                    b.HasIndex("LayoutTypeId");

                    b.HasIndex("TemplateTypeId");

                    b.HasIndex("TemplateLayoutName", "TemplateTypeId")
                        .IsUnique();

                    b.ToTable("TemplateLayout");
                });

            modelBuilder.Entity("Resources.Model.VideoRoundMonitorySiteSetting", b =>
                {
                    b.Property<Guid>("VideoRoundMonitorySiteSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Monitor");

                    b.Property<Guid>("MonitorySiteId");

                    b.Property<Guid?>("PresetSiteId");

                    b.Property<int>("SubView");

                    b.Property<Guid?>("VideoRoundSectionId");

                    b.Property<int>("VideoStream");

                    b.HasKey("VideoRoundMonitorySiteSettingId");

                    b.HasIndex("MonitorySiteId");

                    b.HasIndex("PresetSiteId");

                    b.HasIndex("VideoRoundSectionId");

                    b.ToTable("VideoRoundMonitorySiteSetting");
                });

            modelBuilder.Entity("Resources.Model.VideoRoundScene", b =>
                {
                    b.Property<Guid>("VideoRoundSceneId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Modified");

                    b.Property<Guid?>("ModifiedByUserId");

                    b.Property<Guid>("VideoRoundSceneFlagId");

                    b.Property<string>("VideoRoundSceneName")
                        .HasMaxLength(32);

                    b.HasKey("VideoRoundSceneId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("VideoRoundSceneFlagId");

                    b.HasIndex("VideoRoundSceneName")
                        .IsUnique();

                    b.ToTable("VideoRoundScene");
                });

            modelBuilder.Entity("Resources.Model.VideoRoundSection", b =>
                {
                    b.Property<Guid>("VideoRoundSectionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoundInterval");

                    b.Property<Guid>("TemplateLayoutId");

                    b.Property<Guid?>("VideoRoundSceneId");

                    b.HasKey("VideoRoundSectionId");

                    b.HasIndex("TemplateLayoutId");

                    b.HasIndex("VideoRoundSceneId");

                    b.ToTable("VideoRoundSection");
                });

            modelBuilder.Entity("Surveillance.Model.FaceRecognition", b =>
                {
                    b.Property<Guid>("FaceRecognitionId")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("AageGroup");

                    b.Property<DateTime?>("AbsTime");

                    b.Property<long>("BackgroundPicLen");

                    b.Property<byte[]>("BackgroupBuffer");

                    b.Property<byte>("ByEyeGlass");

                    b.Property<Guid>("DeviceInfoId");

                    b.Property<byte[]>("FaceBuffer");

                    b.Property<long>("FacePicLen");

                    b.Property<int>("FaceScore");

                    b.Property<DateTime>("Modified");

                    b.Property<byte>("Sex");

                    b.HasKey("FaceRecognitionId");

                    b.HasIndex("DeviceInfoId");

                    b.ToTable("FaceRecognition");
                });

            modelBuilder.Entity("Surveillance.Model.LicensePlateRecognition", b =>
                {
                    b.Property<Guid>("LicensePlateRecognitionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AbsTime");

                    b.Property<byte[]>("BackgroupBuffer");

                    b.Property<short>("Color");

                    b.Property<Guid>("DeviceInfoId");

                    b.Property<string>("License");

                    b.Property<DateTime>("Modified");

                    b.Property<long>("PicLen");

                    b.Property<long>("PicPlateLen");

                    b.Property<short>("PlateType");

                    b.Property<byte[]>("VechileNoBuffer");

                    b.Property<short>("VehicleType");

                    b.HasKey("LicensePlateRecognitionId");

                    b.HasIndex("DeviceInfoId");

                    b.ToTable("LicensePlateRecognition");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.AlarmLog", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "AlarmLevel")
                        .WithMany()
                        .HasForeignKey("AlarmLevelId");

                    b.HasOne("Resources.Model.IPDeviceInfo", "AlarmSource")
                        .WithMany()
                        .HasForeignKey("AlarmSourceId");

                    b.HasOne("Infrastructure.Model.SystemOption", "AlarmStatus")
                        .WithMany()
                        .HasForeignKey("AlarmStatusId");

                    b.HasOne("Infrastructure.Model.SystemOption", "AlarmType")
                        .WithMany()
                        .HasForeignKey("AlarmTypeId");

                    b.HasOne("Infrastructure.Model.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.AlarmProcessed", b =>
                {
                    b.HasOne("AlarmAndPlan.Model.AlarmLog", "AlarmLog")
                        .WithMany("Conclusions")
                        .HasForeignKey("AlarmLogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.User", "ProcessedBy")
                        .WithMany()
                        .HasForeignKey("ProcessedByUserId");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.AlarmSetting", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "AlarmLevel")
                        .WithMany()
                        .HasForeignKey("AlarmLevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.IPDeviceInfo", "AlarmSource")
                        .WithMany()
                        .HasForeignKey("AlarmSourceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "AlarmType")
                        .WithMany()
                        .HasForeignKey("AlarmTypeId");

                    b.HasOne("AlarmAndPlan.Model.Plan", "BeforePlan")
                        .WithMany()
                        .HasForeignKey("BeforePlanId");

                    b.HasOne("AlarmAndPlan.Model.Plan", "EmergencyPlan")
                        .WithMany()
                        .HasForeignKey("EmergencyPlanId");

                    b.HasOne("Infrastructure.Model.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlarmAndPlan.Model.DeviceAlarmMapping", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "AlarmType")
                        .WithMany()
                        .HasForeignKey("AlarmTypeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "DeviceType")
                        .WithMany()
                        .HasForeignKey("DeviceTypeId");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.Plan", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "PlanType")
                        .WithMany()
                        .HasForeignKey("PlanTypeId");

                    b.HasOne("Resources.Model.VideoRoundScene", "RealVideoRoundScene")
                        .WithMany()
                        .HasForeignKey("RealVideoRoundSceneId");

                    b.HasOne("Resources.Model.VideoRoundScene", "TvVideoRoundScene")
                        .WithMany()
                        .HasForeignKey("TvVideoRoundSceneId");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.PlanAction", b =>
                {
                    b.HasOne("Resources.Model.IPDeviceInfo", "PlanDevice")
                        .WithMany()
                        .HasForeignKey("PlanDeviceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlarmAndPlan.Model.Plan")
                        .WithMany("Actions")
                        .HasForeignKey("PlanId");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.PredefinedAction", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId");

                    b.HasOne("AlarmAndPlan.Model.PlanAction")
                        .WithMany("PlanActions")
                        .HasForeignKey("PlanActionId");
                });

            modelBuilder.Entity("AlarmAndPlan.Model.ServiceEventLog", b =>
                {
                    b.HasOne("Resources.Model.ServiceInfo", "EventSource")
                        .WithMany()
                        .HasForeignKey("EventSourceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlarmAndPlan.Model.TimerTask", b =>
                {
                    b.HasOne("AlarmAndPlan.Model.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId");

                    b.HasOne("Infrastructure.Model.Schedule", "TaskSchedule")
                        .WithMany()
                        .HasForeignKey("TaskScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.ApplicationResource", b =>
                {
                    b.HasOne("Infrastructure.Model.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.ApplicationResource", "ParentResource")
                        .WithMany()
                        .HasForeignKey("ParentResourceId");
                });

            modelBuilder.Entity("Infrastructure.Model.ApplicationSetting", b =>
                {
                    b.HasOne("Infrastructure.Model.Application")
                        .WithMany("ApplicationSettings")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("Infrastructure.Model.ApplicationSystemOption", b =>
                {
                    b.HasOne("Infrastructure.Model.Application", "Application")
                        .WithMany("ApplicationSystemOptions")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "SystemOption")
                        .WithMany("ApplicationSystemOptions")
                        .HasForeignKey("SystemOptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.Attachment", b =>
                {
                    b.HasOne("Infrastructure.Model.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("Infrastructure.Model.ControlResources", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "ResourceType")
                        .WithMany()
                        .HasForeignKey("ResourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.User")
                        .WithMany("ControlResources")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Infrastructure.Model.DayPeriod", b =>
                {
                    b.HasOne("Infrastructure.Model.ScheduleCycle")
                        .WithMany("DayPeriods")
                        .HasForeignKey("ScheduleCycleId");
                });

            modelBuilder.Entity("Infrastructure.Model.EventLog", b =>
                {
                    b.HasOne("Infrastructure.Model.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "EventLevel")
                        .WithMany()
                        .HasForeignKey("EventLevelId");

                    b.HasOne("Infrastructure.Model.SystemOption", "EventLogType")
                        .WithMany()
                        .HasForeignKey("EventLogTypeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "EventSource")
                        .WithMany()
                        .HasForeignKey("EventSourceId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.Fingerprint", b =>
                {
                    b.HasOne("Infrastructure.Model.Staff")
                        .WithMany("Fingerprints")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.OnlineUser", b =>
                {
                    b.HasOne("Infrastructure.Model.UserTerminal", "LoginTerminal")
                        .WithMany()
                        .HasForeignKey("LoginTerminalId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.Organization", b =>
                {
                    b.HasOne("Infrastructure.Model.ApplicationCenter", "Center")
                        .WithMany()
                        .HasForeignKey("CenterId");

                    b.HasOne("Infrastructure.Model.SystemOption", "InServiceType")
                        .WithMany()
                        .HasForeignKey("InServiceTypeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "OrganizationType")
                        .WithMany()
                        .HasForeignKey("OrganizationTypeId");

                    b.HasOne("Infrastructure.Model.Organization", "ParentOrganization")
                        .WithMany()
                        .HasForeignKey("ParentOrganizationId");
                });

            modelBuilder.Entity("Infrastructure.Model.Permission", b =>
                {
                    b.HasOne("Infrastructure.Model.ApplicationResource", "Resource")
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.ResourcesAction", "ResourcesAction")
                        .WithMany()
                        .HasForeignKey("ResourcesActionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.ResourcesAction", b =>
                {
                    b.HasOne("Infrastructure.Model.ApplicationResource")
                        .WithMany("Actions")
                        .HasForeignKey("ApplicationResourceId");
                });

            modelBuilder.Entity("Infrastructure.Model.Role", b =>
                {
                    b.HasOne("Infrastructure.Model.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "ControlResourcesType")
                        .WithMany()
                        .HasForeignKey("ControlResourcesTypeId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.RolePermission", b =>
                {
                    b.HasOne("Infrastructure.Model.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Infrastructure.Model.Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId1");
                });

            modelBuilder.Entity("Infrastructure.Model.Schedule", b =>
                {
                    b.HasOne("Infrastructure.Model.ScheduleCycle", "ScheduleCycle")
                        .WithMany()
                        .HasForeignKey("ScheduleCycleId");

                    b.HasOne("Infrastructure.Model.SystemOption", "ScheduleType")
                        .WithMany()
                        .HasForeignKey("ScheduleTypeId");
                });

            modelBuilder.Entity("Infrastructure.Model.ScheduleCycle", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "CycleType")
                        .WithMany()
                        .HasForeignKey("CycleTypeId");
                });

            modelBuilder.Entity("Infrastructure.Model.Staff", b =>
                {
                    b.HasOne("Infrastructure.Model.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "DegreeOfEducation")
                        .WithMany()
                        .HasForeignKey("DegreeOfEducationId");

                    b.HasOne("Infrastructure.Model.SystemOption", "DutyCheckType")
                        .WithMany()
                        .HasForeignKey("DutyCheckTypeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "MaritalStatus")
                        .WithMany()
                        .HasForeignKey("MaritalStatusId");

                    b.HasOne("Infrastructure.Model.SystemOption", "Nation")
                        .WithMany()
                        .HasForeignKey("NationId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.UserPhoto", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.HasOne("Infrastructure.Model.SystemOption", "PhysiclalStatus")
                        .WithMany()
                        .HasForeignKey("PhysiclalStatusId");

                    b.HasOne("Infrastructure.Model.SystemOption", "PoliticalLandscape")
                        .WithMany()
                        .HasForeignKey("PoliticalLandscapeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "PositionType")
                        .WithMany()
                        .HasForeignKey("PositionTypeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "RankType")
                        .WithMany()
                        .HasForeignKey("RankTypeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "ReignStatus")
                        .WithMany()
                        .HasForeignKey("ReignStatusId");

                    b.HasOne("Infrastructure.Model.SystemOption", "Sex")
                        .WithMany()
                        .HasForeignKey("SexId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.StaffGroup")
                        .WithMany("Staffs")
                        .HasForeignKey("StaffGroupId");

                    b.HasOne("Infrastructure.Model.SystemOption", "WorkingProperty")
                        .WithMany()
                        .HasForeignKey("WorkingPropertyId");
                });

            modelBuilder.Entity("Infrastructure.Model.StaffGroup", b =>
                {
                    b.HasOne("Infrastructure.Model.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.SystemOption", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "ParentSystemOption")
                        .WithMany()
                        .HasForeignKey("ParentSystemOptionId");
                });

            modelBuilder.Entity("Infrastructure.Model.TimePeriod", b =>
                {
                    b.HasOne("Infrastructure.Model.DayPeriod")
                        .WithMany("TimePeriods")
                        .HasForeignKey("DayPeriodId");
                });

            modelBuilder.Entity("Infrastructure.Model.User", b =>
                {
                    b.HasOne("Infrastructure.Model.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.UserRole", b =>
                {
                    b.HasOne("Infrastructure.Model.Role", "Role")
                        .WithMany("UserManyToRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.User", "User")
                        .WithMany("UserManyToRole")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Infrastructure.Model.UserSettingMapping", b =>
                {
                    b.HasOne("Infrastructure.Model.User", "User")
                        .WithMany("UserSettings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.UserSetting", "UserSetting")
                        .WithMany()
                        .HasForeignKey("UserSettingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infrastructure.Model.UserTerminal", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "UserTerminalType")
                        .WithMany()
                        .HasForeignKey("UserTerminalTypeId");
                });

            modelBuilder.Entity("PAPS.Model.BulletboxLog", b =>
                {
                    b.HasOne("Infrastructure.Model.Attachment", "CartridgeBoxSnapshot")
                        .WithMany()
                        .HasForeignKey("BulletboxSnapshotId");

                    b.HasOne("Infrastructure.Model.Attachment", "FrontSnapshot")
                        .WithMany()
                        .HasForeignKey("FrontSnapshotId");

                    b.HasOne("Infrastructure.Model.SystemOption", "LockStatus")
                        .WithMany()
                        .HasForeignKey("LockStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("Resources.Model.IPDeviceInfo", "SentinelDevice")
                        .WithMany()
                        .HasForeignKey("SentinelDeviceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PAPS.Model.Circular", b =>
                {
                    b.HasOne("Infrastructure.Model.Staff", "CircularStaff")
                        .WithMany()
                        .HasForeignKey("CircularStaffId");

                    b.HasOne("PAPS.Model.DutyCheckLog", "DutyCheckLog")
                        .WithMany()
                        .HasForeignKey("DutyCheckLogId");
                });

            modelBuilder.Entity("PAPS.Model.DailyOnDuty", b =>
                {
                    b.HasOne("Infrastructure.Model.Staff", "DutyOfficerToday")
                        .WithMany()
                        .HasForeignKey("DutyOfficerTodayId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.SystemOption", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("Infrastructure.Model.Staff", "TomorrowAttendant")
                        .WithMany()
                        .HasForeignKey("TomorrowAttendantId");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckAppraise", b =>
                {
                    b.HasOne("Infrastructure.Model.Attachment", "AppraiseICO")
                        .WithMany()
                        .HasForeignKey("AppraiseICOId");

                    b.HasOne("Infrastructure.Model.SystemOption", "AppraiseType")
                        .WithMany()
                        .HasForeignKey("AppraiseTypeId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckLog", b =>
                {
                    b.HasOne("Infrastructure.Model.DayPeriod", "DayPeriod")
                        .WithMany()
                        .HasForeignKey("DayPeriodId");

                    b.HasOne("PAPS.Model.DutyCheckOperation", "DutyCheckOperation")
                        .WithMany()
                        .HasForeignKey("DutyCheckOperationId");

                    b.HasOne("PAPS.Model.DutyCheckSiteSchedule", "DutyCheckSiteSchedule")
                        .WithMany()
                        .HasForeignKey("DutyCheckSiteScheduleId");

                    b.HasOne("Infrastructure.Model.Staff", "DutyCheckStaff")
                        .WithMany()
                        .HasForeignKey("DutyCheckStaffId");

                    b.HasOne("Infrastructure.Model.SystemOption", "MainApprise")
                        .WithMany()
                        .HasForeignKey("MainAppriseId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.SystemOption", "RecordType")
                        .WithMany()
                        .HasForeignKey("RecordTypeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckLogAppraise", b =>
                {
                    b.HasOne("PAPS.Model.DutyCheckAppraise", "DutyCheckAppraise")
                        .WithMany()
                        .HasForeignKey("DutyCheckAppraiseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PAPS.Model.DutyCheckLog", "DutyCheckLog")
                        .WithMany("Apprises")
                        .HasForeignKey("DutyCheckLogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckLogDispose", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "Dispose")
                        .WithMany()
                        .HasForeignKey("DisposeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PAPS.Model.DutyCheckLog", "DutyCheckLog")
                        .WithMany("CircularTypes")
                        .HasForeignKey("DutyCheckLogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckMatter", b =>
                {
                    b.HasOne("Infrastructure.Model.Attachment", "MatterICO")
                        .WithMany()
                        .HasForeignKey("MatterICOId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.Attachment", "VoiceFile")
                        .WithMany()
                        .HasForeignKey("VoiceFileId");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckOperationAttachment", b =>
                {
                    b.HasOne("Infrastructure.Model.Attachment", "Attachment")
                        .WithMany()
                        .HasForeignKey("AttachmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "AttachmentType")
                        .WithMany()
                        .HasForeignKey("AttachmentTypeId");

                    b.HasOne("PAPS.Model.DutyCheckOperation")
                        .WithMany("Attachments")
                        .HasForeignKey("DutyCheckOperationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckPackage", b =>
                {
                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.SystemOption", "PackageStatus")
                        .WithMany()
                        .HasForeignKey("PackageStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckPackageLog", b =>
                {
                    b.HasOne("PAPS.Model.DutyCheckLog", "DutyCheckLog")
                        .WithMany()
                        .HasForeignKey("DutyCheckLogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PAPS.Model.DutyCheckPackage", "DutyCheckPackage")
                        .WithMany("DutyCheckPackLogs")
                        .HasForeignKey("DutyCheckPackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckPackageTimePlan", b =>
                {
                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckSchedule", b =>
                {
                    b.HasOne("Infrastructure.Model.TimePeriod", "CheckTimePeriod")
                        .WithMany()
                        .HasForeignKey("CheckTimePeriodId");

                    b.HasOne("Infrastructure.Model.Staff", "Deputy")
                        .WithMany()
                        .HasForeignKey("DeputyId");

                    b.HasOne("PAPS.Model.DutyScheduleDetail")
                        .WithMany("NetWatcherSchedule")
                        .HasForeignKey("DutyScheduleDetailId");

                    b.HasOne("Infrastructure.Model.Staff", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId");
                });

            modelBuilder.Entity("PAPS.Model.DutyCheckSiteSchedule", b =>
                {
                    b.HasOne("Resources.Model.MonitorySite", "CheckDutySite")
                        .WithMany()
                        .HasForeignKey("CheckDutySiteId");

                    b.HasOne("Infrastructure.Model.Staff", "CheckMan")
                        .WithMany()
                        .HasForeignKey("CheckManId");

                    b.HasOne("PAPS.Model.DutyCheckGroup")
                        .WithMany("CheckDutySiteSchedules")
                        .HasForeignKey("DutyCheckGroupId");

                    b.HasOne("PAPS.Model.DutyGroupScheduleDetail")
                        .WithMany("CheckDutySiteSchedule")
                        .HasForeignKey("DutyGroupScheduleDetailId");

                    b.HasOne("Infrastructure.Model.Organization", "SiteOrganization")
                        .WithMany()
                        .HasForeignKey("SiteOrganizationId");
                });

            modelBuilder.Entity("PAPS.Model.DutyGroupSchedule", b =>
                {
                    b.HasOne("Infrastructure.Model.Staff", "Lister")
                        .WithMany()
                        .HasForeignKey("ListerId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PAPS.Model.DutyGroupScheduleDetail", b =>
                {
                    b.HasOne("Infrastructure.Model.Staff", "CheckMan")
                        .WithMany()
                        .HasForeignKey("CheckManId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PAPS.Model.DutyGroupSchedule")
                        .WithMany("DutyGroupScheduleDetails")
                        .HasForeignKey("DutyGroupScheduleId");
                });

            modelBuilder.Entity("PAPS.Model.DutySchedule", b =>
                {
                    b.HasOne("Infrastructure.Model.Staff", "Lister")
                        .WithMany()
                        .HasForeignKey("ListerId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PAPS.Model.DutyScheduleDetail", b =>
                {
                    b.HasOne("PAPS.Model.DutyCheckSchedule", "CadreSchedule")
                        .WithMany()
                        .HasForeignKey("CadreScheduleId");

                    b.HasOne("PAPS.Model.DutySchedule")
                        .WithMany("DutyScheduleDetails")
                        .HasForeignKey("DutyScheduleId");

                    b.HasOne("PAPS.Model.DutyCheckSchedule", "OfficerSchedule")
                        .WithMany()
                        .HasForeignKey("OfficerScheduleId");
                });

            modelBuilder.Entity("PAPS.Model.EmergencyTeam", b =>
                {
                    b.HasOne("PAPS.Model.DutyGroupSchedule", "DutyGroupSchedule")
                        .WithMany("EmergencyTeam")
                        .HasForeignKey("DutyGroupScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("PAPS.Model.Fault", b =>
                {
                    b.HasOne("Resources.Model.MonitorySite", "CheckDutySite")
                        .WithMany()
                        .HasForeignKey("CheckDutySiteId");

                    b.HasOne("Infrastructure.Model.Staff", "CheckMan")
                        .WithMany()
                        .HasForeignKey("CheckManId");

                    b.HasOne("PAPS.Model.DutyCheckOperation", "DutyCheckOperation")
                        .WithMany()
                        .HasForeignKey("DutyCheckOperationId");

                    b.HasOne("Infrastructure.Model.Organization", "DutyOrganization")
                        .WithMany()
                        .HasForeignKey("DutyOrganizationId");

                    b.HasOne("Infrastructure.Model.SystemOption", "FaultType")
                        .WithMany()
                        .HasForeignKey("FaultTypeId");
                });

            modelBuilder.Entity("PAPS.Model.Feedback", b =>
                {
                    b.HasOne("PAPS.Model.Circular", "Circular")
                        .WithMany()
                        .HasForeignKey("CircularId");

                    b.HasOne("PAPS.Model.Fault", "Fault")
                        .WithMany()
                        .HasForeignKey("FaultId");

                    b.HasOne("Infrastructure.Model.Staff", "FeedbackStaff")
                        .WithMany()
                        .HasForeignKey("FeedbackStaffId");
                });

            modelBuilder.Entity("PAPS.Model.InstitutionsDutyCheckSchedule", b =>
                {
                    b.HasOne("Infrastructure.Model.Organization", "InspectedOrganization")
                        .WithMany()
                        .HasForeignKey("InspectedOrganizationId");

                    b.HasOne("Infrastructure.Model.Staff", "Lead")
                        .WithMany()
                        .HasForeignKey("LeadId");
                });

            modelBuilder.Entity("PAPS.Model.PunchLog", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "AppraiseType")
                        .WithMany()
                        .HasForeignKey("AppraiseTypeId");

                    b.HasOne("Infrastructure.Model.Attachment", "CartridgeBoxSnapshot")
                        .WithMany()
                        .HasForeignKey("BulletboxSnapshotId");

                    b.HasOne("Infrastructure.Model.Attachment", "FrontSnapshot")
                        .WithMany()
                        .HasForeignKey("FrontSnapshotId");

                    b.HasOne("Infrastructure.Model.SystemOption", "LogResult")
                        .WithMany()
                        .HasForeignKey("LogResultId");

                    b.HasOne("Resources.Model.IPDeviceInfo", "PunchDevice")
                        .WithMany()
                        .HasForeignKey("PunchDeviceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "PunchType")
                        .WithMany()
                        .HasForeignKey("PunchTypeId");

                    b.HasOne("Infrastructure.Model.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("PAPS.Model.Reservegroup", b =>
                {
                    b.HasOne("PAPS.Model.DutyGroupSchedule", "DutyGroupSchedule")
                        .WithMany("Reservegroup")
                        .HasForeignKey("DutyGroupScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("PAPS.Model.ShiftHandoverLog", b =>
                {
                    b.HasOne("Infrastructure.Model.Staff", "OffGoing")
                        .WithMany()
                        .HasForeignKey("OffGoingId");

                    b.HasOne("Infrastructure.Model.Staff", "OnComing")
                        .WithMany()
                        .HasForeignKey("OnComingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.SystemOption", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("PAPS.Model.TemporaryDuty", b =>
                {
                    b.HasOne("Infrastructure.Model.Staff", "Commander")
                        .WithMany()
                        .HasForeignKey("CommanderId");

                    b.HasOne("Infrastructure.Model.Attachment", "DutyProgrammePicture")
                        .WithMany()
                        .HasForeignKey("DutyProgrammePictureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "DutyType")
                        .WithMany()
                        .HasForeignKey("DutyTypeId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.SystemOption", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeId");
                });

            modelBuilder.Entity("Resources.Model.AlarmMainframe", b =>
                {
                    b.HasOne("Resources.Model.IPDeviceInfo", "DeviceInfo")
                        .WithMany()
                        .HasForeignKey("DeviceInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Resources.Model.AlarmPeripheral", b =>
                {
                    b.HasOne("Resources.Model.IPDeviceInfo", "AlarmDevice")
                        .WithOne()
                        .HasForeignKey("Resources.Model.AlarmPeripheral", "AlarmDeviceId");

                    b.HasOne("Resources.Model.AlarmMainframe")
                        .WithMany("AlarmPeripherals")
                        .HasForeignKey("AlarmMainframeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "AlarmType")
                        .WithMany()
                        .HasForeignKey("AlarmTypeId");

                    b.HasOne("Resources.Model.Encoder")
                        .WithMany("AlarmPeripherals")
                        .HasForeignKey("EncoderId");
                });

            modelBuilder.Entity("Resources.Model.Camera", b =>
                {
                    b.HasOne("Resources.Model.Encoder", "Encoder")
                        .WithMany()
                        .HasForeignKey("EncoderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.IPDeviceInfo", "IPDevice")
                        .WithOne()
                        .HasForeignKey("Resources.Model.Camera", "IPDeviceId");

                    b.HasOne("Infrastructure.Model.Attachment", "Snapshot")
                        .WithMany()
                        .HasForeignKey("SnapshotId");

                    b.HasOne("Resources.Model.ServiceInfo", "VideoForward")
                        .WithMany()
                        .HasForeignKey("VideoForwardId");
                });

            modelBuilder.Entity("Resources.Model.CruiseScanGroup", b =>
                {
                    b.HasOne("Resources.Model.Camera")
                        .WithMany("CruiseScanGroups")
                        .HasForeignKey("CameraId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Resources.Model.CruiseScanGroupPresetSite", b =>
                {
                    b.HasOne("Resources.Model.CruiseScanGroup", "CruiseScanGroup")
                        .WithMany("PresetSites")
                        .HasForeignKey("CruiseScanGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.PresetSite", "PresetSite")
                        .WithMany()
                        .HasForeignKey("PresetSiteID");
                });

            modelBuilder.Entity("Resources.Model.DefenseDevice", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "DefenseDirection")
                        .WithMany()
                        .HasForeignKey("DefenseDirectionId");

                    b.HasOne("Resources.Model.IPDeviceInfo", "DeviceInfo")
                        .WithOne()
                        .HasForeignKey("Resources.Model.DefenseDevice", "DeviceInfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.Sentinel", "Sentinel")
                        .WithMany("DefenseDevices")
                        .HasForeignKey("SentinelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Resources.Model.DeviceChannelSetting", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "ChannelType")
                        .WithMany()
                        .HasForeignKey("ChannelTypeId");

                    b.HasOne("Resources.Model.Sentinel")
                        .WithMany("AlarmOutputChannels")
                        .HasForeignKey("SentinelId");
                });

            modelBuilder.Entity("Resources.Model.DeviceChannelTypeMapping", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "ChannelType")
                        .WithMany()
                        .HasForeignKey("ChannelTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "DeviceType")
                        .WithMany()
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Resources.Model.DeviceGroup", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "DeviceGroupType")
                        .WithMany()
                        .HasForeignKey("DeviceGroupTypeId");

                    b.HasOne("Infrastructure.Model.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Resources.Model.DeviceGroupIPDevice", b =>
                {
                    b.HasOne("Resources.Model.DeviceGroup", "DeviceGroup")
                        .WithMany()
                        .HasForeignKey("DeviceGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.IPDeviceInfo", "IPDeviceInfo")
                        .WithMany()
                        .HasForeignKey("IPDeviceInfoId");
                });

            modelBuilder.Entity("Resources.Model.DeviceStatusHistory", b =>
                {
                    b.HasOne("Resources.Model.IPDeviceInfo", "DeviceInfo")
                        .WithMany()
                        .HasForeignKey("DeviceInfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.SystemOption", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Resources.Model.Encoder", b =>
                {
                    b.HasOne("Resources.Model.IPDeviceInfo", "DeviceInfo")
                        .WithMany()
                        .HasForeignKey("DeviceInfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.EncoderType", "EncoderType")
                        .WithMany()
                        .HasForeignKey("EncoderTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Resources.Model.IPDeviceInfo", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "DeviceType")
                        .WithMany()
                        .HasForeignKey("DeviceTypeId");

                    b.HasOne("Infrastructure.Model.User", "ModifiedByUser")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Infrastructure.Model.SystemOption", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("Resources.Model.Materiel", b =>
                {
                    b.HasOne("PAPS.Model.TemporaryDuty")
                        .WithMany("Equipments")
                        .HasForeignKey("TemporaryDutyId");

                    b.HasOne("Infrastructure.Model.SystemOption", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitSystemOptionId");
                });

            modelBuilder.Entity("Resources.Model.MonitorySite", b =>
                {
                    b.HasOne("Resources.Model.Camera", "Camera")
                        .WithOne()
                        .HasForeignKey("Resources.Model.MonitorySite", "CameraId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("Resources.Model.PresetSite", b =>
                {
                    b.HasOne("Resources.Model.Camera", "Camera")
                        .WithMany("PresetSites")
                        .HasForeignKey("CameraId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Resources.Model.Sentinel", b =>
                {
                    b.HasOne("Infrastructure.Model.Attachment", "AudioFile")
                        .WithMany()
                        .HasForeignKey("AudioFileId");

                    b.HasOne("Resources.Model.SentinelVideo", "BulletboxCamera")
                        .WithMany()
                        .HasForeignKey("BulletboxCameraId");

                    b.HasOne("Resources.Model.IPDeviceInfo", "DeviceInfo")
                        .WithMany()
                        .HasForeignKey("DeviceInfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.SentinelVideo", "FrontCamera")
                        .WithMany()
                        .HasForeignKey("FrontCameraId");

                    b.HasOne("Resources.Model.SentinelSetting", "SentinelSetting")
                        .WithMany()
                        .HasForeignKey("SentinelSettingId");
                });

            modelBuilder.Entity("Resources.Model.SentinelFingerPrintMapping", b =>
                {
                    b.HasOne("Infrastructure.Model.Fingerprint", "Fingerprint")
                        .WithMany()
                        .HasForeignKey("FingerprintId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.Sentinel", "Sentinel")
                        .WithMany()
                        .HasForeignKey("SentinelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Resources.Model.SentinelVideo", b =>
                {
                    b.HasOne("Resources.Model.Camera", "Camera")
                        .WithMany()
                        .HasForeignKey("CameraId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.Sentinel")
                        .WithMany("SentinelVideos")
                        .HasForeignKey("SentinelId");

                    b.HasOne("Infrastructure.Model.SystemOption", "VideoType")
                        .WithMany()
                        .HasForeignKey("VideoTypeId");
                });

            modelBuilder.Entity("Resources.Model.ServerInfo", b =>
                {
                    b.HasOne("Infrastructure.Model.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Infrastructure.Model.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("Resources.Model.ServiceInfo", b =>
                {
                    b.HasOne("Infrastructure.Model.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Resources.Model.ServerInfo", "ServerInfo")
                        .WithMany()
                        .HasForeignKey("ServerInfoId");

                    b.HasOne("Infrastructure.Model.SystemOption", "ServiceType")
                        .WithMany()
                        .HasForeignKey("ServiceTypeId");
                });

            modelBuilder.Entity("Resources.Model.TemplateCell", b =>
                {
                    b.HasOne("Resources.Model.TemplateLayout")
                        .WithMany("Cells")
                        .HasForeignKey("TemplateLayoutId");
                });

            modelBuilder.Entity("Resources.Model.TemplateLayout", b =>
                {
                    b.HasOne("Infrastructure.Model.SystemOption", "LayoutType")
                        .WithMany()
                        .HasForeignKey("LayoutTypeId");

                    b.HasOne("Infrastructure.Model.SystemOption", "TemplateType")
                        .WithMany()
                        .HasForeignKey("TemplateTypeId");
                });

            modelBuilder.Entity("Resources.Model.VideoRoundMonitorySiteSetting", b =>
                {
                    b.HasOne("Resources.Model.MonitorySite", "MonitorySite")
                        .WithMany()
                        .HasForeignKey("MonitorySiteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.PresetSite", "PresetSite")
                        .WithMany()
                        .HasForeignKey("PresetSiteId");

                    b.HasOne("Resources.Model.VideoRoundSection")
                        .WithMany("RoundMonitorySiteSettings")
                        .HasForeignKey("VideoRoundSectionId");
                });

            modelBuilder.Entity("Resources.Model.VideoRoundScene", b =>
                {
                    b.HasOne("Infrastructure.Model.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Infrastructure.Model.SystemOption", "VideoRoundSceneFlag")
                        .WithMany()
                        .HasForeignKey("VideoRoundSceneFlagId");
                });

            modelBuilder.Entity("Resources.Model.VideoRoundSection", b =>
                {
                    b.HasOne("Resources.Model.TemplateLayout", "TemplateLayout")
                        .WithMany()
                        .HasForeignKey("TemplateLayoutId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Resources.Model.VideoRoundScene")
                        .WithMany("VideoRoundSections")
                        .HasForeignKey("VideoRoundSceneId");
                });

            modelBuilder.Entity("Surveillance.Model.FaceRecognition", b =>
                {
                    b.HasOne("Resources.Model.IPDeviceInfo", "DeviceInfo")
                        .WithMany()
                        .HasForeignKey("DeviceInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Surveillance.Model.LicensePlateRecognition", b =>
                {
                    b.HasOne("Resources.Model.IPDeviceInfo", "DeviceInfo")
                        .WithMany()
                        .HasForeignKey("DeviceInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
