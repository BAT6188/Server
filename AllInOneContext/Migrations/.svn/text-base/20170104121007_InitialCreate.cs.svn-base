using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllInOneContext.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''");

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    ApplicationId = table.Column<Guid>(nullable: false),
                    ApplicationCode = table.Column<string>(nullable: true),
                    ApplicationName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.ApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCenter",
                columns: table => new
                {
                    ApplicationCenterId = table.Column<Guid>(nullable: false),
                    ApplicationCenterCode = table.Column<string>(nullable: true),
                    EndPointsJson = table.Column<string>(nullable: true),
                    ParentApplicationCenterCode = table.Column<string>(nullable: true),
                    RegisterPassword = table.Column<string>(nullable: true),
                    RegisterUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCenter", x => x.ApplicationCenterId);
                });

            migrationBuilder.CreateTable(
                name: "SystemOption",
                columns: table => new
                {
                    SystemOptionId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MappingCode = table.Column<string>(nullable: true),
                    ParentSystemOptionId = table.Column<Guid>(nullable: true),
                    Predefine = table.Column<bool>(nullable: false),
                    SystemOptionCode = table.Column<string>(nullable: true),
                    SystemOptionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOption", x => x.SystemOptionId);
                    table.ForeignKey(
                        name: "FK_SystemOption_SystemOption_ParentSystemOptionId",
                        column: x => x.ParentSystemOptionId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPhoto",
                columns: table => new
                {
                    UserPhotoId = table.Column<Guid>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    PhotoData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhoto", x => x.UserPhotoId);
                });

            migrationBuilder.CreateTable(
                name: "UserSetting",
                columns: table => new
                {
                    UserSettingId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SettingKey = table.Column<string>(nullable: true),
                    SettingValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSetting", x => x.UserSettingId);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckGroup",
                columns: table => new
                {
                    DutyCheckGroupId = table.Column<Guid>(nullable: false),
                    DutyGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckGroup", x => x.DutyCheckGroupId);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckOperation",
                columns: table => new
                {
                    DutyCheckOperationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckOperation", x => x.DutyCheckOperationId);
                });

            migrationBuilder.CreateTable(
                name: "EncoderType",
                columns: table => new
                {
                    EncoderTypeId = table.Column<Guid>(nullable: false),
                    Channels = table.Column<int>(nullable: false),
                    DefaultPassword = table.Column<string>(nullable: true),
                    DefaultPort = table.Column<int>(nullable: false),
                    DefaultUserName = table.Column<string>(nullable: true),
                    EncoderCode = table.Column<int>(nullable: false),
                    EncoderTypeName = table.Column<string>(nullable: true),
                    OSDLines = table.Column<int>(nullable: false),
                    PTZ3DControl = table.Column<int>(nullable: false),
                    RecordFileExtension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncoderType", x => x.EncoderTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SentinelLayout",
                columns: table => new
                {
                    SentinelLayoutId = table.Column<Guid>(nullable: false),
                    MapDataXml = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentinelLayout", x => x.SentinelLayoutId);
                });

            migrationBuilder.CreateTable(
                name: "SentinelSetting",
                columns: table => new
                {
                    SentinelSettingId = table.Column<Guid>(nullable: false),
                    AlarmCallType = table.Column<int>(nullable: false),
                    InlinePhone = table.Column<string>(nullable: true),
                    OutlinePhone = table.Column<string>(nullable: true),
                    ScreenViews = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentinelSetting", x => x.SentinelSettingId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationResource",
                columns: table => new
                {
                    ApplicationResourceId = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    ApplicationResourceName = table.Column<string>(nullable: true),
                    ParentResourceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationResource", x => x.ApplicationResourceId);
                    table.ForeignKey(
                        name: "FK_ApplicationResource_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationResource_ApplicationResource_ParentResourceId",
                        column: x => x.ParentResourceId,
                        principalTable: "ApplicationResource",
                        principalColumn: "ApplicationResourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSetting",
                columns: table => new
                {
                    ApplicationSettingId = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SettingKey = table.Column<string>(nullable: true),
                    SettingValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSetting", x => x.ApplicationSettingId);
                    table.ForeignKey(
                        name: "FK_ApplicationSetting_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceAlarmMapping",
                columns: table => new
                {
                    DeviceAlarmMappingId = table.Column<Guid>(nullable: false),
                    AlarmTypeId = table.Column<Guid>(nullable: false),
                    DeviceTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceAlarmMapping", x => x.DeviceAlarmMappingId);
                    table.ForeignKey(
                        name: "FK_DeviceAlarmMapping_SystemOption_AlarmTypeId",
                        column: x => x.AlarmTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceAlarmMapping_SystemOption_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSystemOption",
                columns: table => new
                {
                    ApplicationId = table.Column<Guid>(nullable: false),
                    SystemOptionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSystemOption", x => new { x.ApplicationId, x.SystemOptionId });
                    table.ForeignKey(
                        name: "FK_ApplicationSystemOption_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationSystemOption_SystemOption_SystemOptionId",
                        column: x => x.SystemOptionId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    CenterId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DutycheckPoints = table.Column<int>(nullable: false),
                    InServiceTypeId = table.Column<Guid>(nullable: true),
                    OnDutyTarget = table.Column<string>(nullable: true),
                    OrderNo = table.Column<int>(nullable: false),
                    OrganizationCode = table.Column<string>(nullable: true),
                    OrganizationFullName = table.Column<string>(nullable: true),
                    OrganizationLevel = table.Column<int>(nullable: false),
                    OrganizationShortName = table.Column<string>(nullable: false),
                    OrganizationTypeId = table.Column<Guid>(nullable: true),
                    ParentOrganizationId = table.Column<Guid>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organization_ApplicationCenter_CenterId",
                        column: x => x.CenterId,
                        principalTable: "ApplicationCenter",
                        principalColumn: "ApplicationCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organization_SystemOption_InServiceTypeId",
                        column: x => x.InServiceTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organization_SystemOption_OrganizationTypeId",
                        column: x => x.OrganizationTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organization_Organization_ParentOrganizationId",
                        column: x => x.ParentOrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleCycle",
                columns: table => new
                {
                    ScheduleCycleId = table.Column<Guid>(nullable: false),
                    CycleTypeId = table.Column<Guid>(nullable: true),
                    DaysJson = table.Column<string>(nullable: true),
                    LastExecute = table.Column<DateTime>(nullable: true),
                    MonthsJson = table.Column<string>(nullable: true),
                    NextExecute = table.Column<DateTime>(nullable: true),
                    WeekDayJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleCycle", x => x.ScheduleCycleId);
                    table.ForeignKey(
                        name: "FK_ScheduleCycle_SystemOption_CycleTypeId",
                        column: x => x.CycleTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTerminal",
                columns: table => new
                {
                    UserTerminalId = table.Column<Guid>(nullable: false),
                    UserTerminalIP = table.Column<string>(nullable: true),
                    UserTerminalMac = table.Column<string>(nullable: true),
                    UserTerminalTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTerminal", x => x.UserTerminalId);
                    table.ForeignKey(
                        name: "FK_UserTerminal_SystemOption_UserTerminalTypeId",
                        column: x => x.UserTerminalTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceChannelTypeMapping",
                columns: table => new
                {
                    DeviceChannelTypeMappingId = table.Column<Guid>(nullable: false),
                    ChannelTypeId = table.Column<Guid>(nullable: false),
                    DeviceTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceChannelTypeMapping", x => x.DeviceChannelTypeMappingId);
                    table.ForeignKey(
                        name: "FK_DeviceChannelTypeMapping_SystemOption_ChannelTypeId",
                        column: x => x.ChannelTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceChannelTypeMapping_SystemOption_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateLayout",
                columns: table => new
                {
                    TemplateLayoutId = table.Column<Guid>(nullable: false),
                    Columns = table.Column<int>(nullable: false),
                    LayoutTypeId = table.Column<Guid>(nullable: false),
                    Rows = table.Column<int>(nullable: false),
                    TemplateLayoutName = table.Column<string>(maxLength: 32, nullable: true),
                    TemplateTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateLayout", x => x.TemplateLayoutId);
                    table.ForeignKey(
                        name: "FK_TemplateLayout_SystemOption_LayoutTypeId",
                        column: x => x.LayoutTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemplateLayout_SystemOption_TemplateTypeId",
                        column: x => x.TemplateTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourcesAction",
                columns: table => new
                {
                    ResourcesActionId = table.Column<Guid>(nullable: false),
                    ApplicationResourceId = table.Column<Guid>(nullable: true),
                    ResourcesActionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcesAction", x => x.ResourcesActionId);
                    table.ForeignKey(
                        name: "FK_ResourcesAction_ApplicationResource_ApplicationResourceId",
                        column: x => x.ApplicationResourceId,
                        principalTable: "ApplicationResource",
                        principalColumn: "ApplicationResourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventLog",
                columns: table => new
                {
                    EventLogId = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    EventData = table.Column<string>(nullable: true),
                    EventLevelId = table.Column<Guid>(nullable: false),
                    EventLogTypeId = table.Column<Guid>(nullable: false),
                    EventSourceId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLog", x => x.EventLogId);
                    table.ForeignKey(
                        name: "FK_EventLog_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventLog_SystemOption_EventLevelId",
                        column: x => x.EventLevelId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventLog_SystemOption_EventLogTypeId",
                        column: x => x.EventLogTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventLog_SystemOption_EventSourceId",
                        column: x => x.EventSourceId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventLog_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    ControlResourcesTypeId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Role_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_SystemOption_ControlResourcesTypeId",
                        column: x => x.ControlResourcesTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Role_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffGroup",
                columns: table => new
                {
                    StaffGroupId = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffGroup", x => x.StaffGroupId);
                    table.ForeignKey(
                        name: "FK_StaffGroup_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffGroup_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    AccessFailed = table.Column<int>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEndDateUtc = table.Column<DateTime>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<int>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckPackage",
                columns: table => new
                {
                    DutyCheckPackageId = table.Column<Guid>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    PackageStatusId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckPackage", x => x.DutyCheckPackageId);
                    table.ForeignKey(
                        name: "FK_DutyCheckPackage_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckPackage_SystemOption_PackageStatusId",
                        column: x => x.PackageStatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayPeriod",
                columns: table => new
                {
                    DayPeriodId = table.Column<Guid>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false),
                    ScheduleCycleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayPeriod", x => x.DayPeriodId);
                    table.ForeignKey(
                        name: "FK_DayPeriod_ScheduleCycle_ScheduleCycleId",
                        column: x => x.ScheduleCycleId,
                        principalTable: "ScheduleCycle",
                        principalColumn: "ScheduleCycleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(nullable: false),
                    EffectiveTime = table.Column<DateTime>(nullable: false),
                    ExpirationTime = table.Column<DateTime>(nullable: true),
                    ScheduleCycleId = table.Column<Guid>(nullable: true),
                    ScheduleName = table.Column<string>(nullable: true),
                    ScheduleTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedule_ScheduleCycle_ScheduleCycleId",
                        column: x => x.ScheduleCycleId,
                        principalTable: "ScheduleCycle",
                        principalColumn: "ScheduleCycleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_SystemOption_ScheduleTypeId",
                        column: x => x.ScheduleTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemplateCell",
                columns: table => new
                {
                    TemplateCellId = table.Column<Guid>(nullable: false),
                    Column = table.Column<int>(nullable: false),
                    ColumnSpan = table.Column<int>(nullable: false),
                    Row = table.Column<int>(nullable: false),
                    RowSpan = table.Column<int>(nullable: false),
                    TemplateLayoutId = table.Column<Guid>(nullable: true),
                    ViewCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateCell", x => x.TemplateCellId);
                    table.ForeignKey(
                        name: "FK_TemplateCell_TemplateLayout_TemplateLayoutId",
                        column: x => x.TemplateLayoutId,
                        principalTable: "TemplateLayout",
                        principalColumn: "TemplateLayoutId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    ResourcesActionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permission_ApplicationResource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "ApplicationResource",
                        principalColumn: "ApplicationResourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permission_ResourcesAction_ResourcesActionId",
                        column: x => x.ResourcesActionId,
                        principalTable: "ResourcesAction",
                        principalColumn: "ResourcesActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    ClassRow = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    DegreeOfEducationId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DutyCheckTypeId = table.Column<Guid>(nullable: true),
                    EnrolAddress = table.Column<string>(nullable: true),
                    EnrolTime = table.Column<DateTime>(nullable: true),
                    FamilyPhone = table.Column<string>(nullable: true),
                    MaritalStatusId = table.Column<Guid>(nullable: true),
                    NationId = table.Column<Guid>(nullable: true),
                    NativePlace = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    PartyTime = table.Column<DateTime>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PhotoId = table.Column<Guid>(nullable: true),
                    PhysiclalStatusId = table.Column<Guid>(nullable: true),
                    PoliticalLandscapeId = table.Column<Guid>(nullable: true),
                    PositionTypeId = table.Column<Guid>(nullable: true),
                    PostalZipCode = table.Column<string>(nullable: true),
                    RankTypeId = table.Column<Guid>(nullable: true),
                    ReignStatusId = table.Column<Guid>(nullable: true),
                    ReligiousBelief = table.Column<string>(nullable: true),
                    SexId = table.Column<Guid>(nullable: false),
                    StaffCode = table.Column<int>(nullable: false),
                    StaffGroupId = table.Column<Guid>(nullable: true),
                    StaffName = table.Column<string>(nullable: true),
                    Stature = table.Column<double>(nullable: true),
                    WorkingPropertyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staff_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_DegreeOfEducationId",
                        column: x => x.DegreeOfEducationId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_DutyCheckTypeId",
                        column: x => x.DutyCheckTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_NationId",
                        column: x => x.NationId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_UserPhoto_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "UserPhoto",
                        principalColumn: "UserPhotoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_PhysiclalStatusId",
                        column: x => x.PhysiclalStatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_PoliticalLandscapeId",
                        column: x => x.PoliticalLandscapeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_PositionTypeId",
                        column: x => x.PositionTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_RankTypeId",
                        column: x => x.RankTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_ReignStatusId",
                        column: x => x.ReignStatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_SexId",
                        column: x => x.SexId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_StaffGroup_StaffGroupId",
                        column: x => x.StaffGroupId,
                        principalTable: "StaffGroup",
                        principalColumn: "StaffGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_SystemOption_WorkingPropertyId",
                        column: x => x.WorkingPropertyId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(nullable: false),
                    AttachmentName = table.Column<string>(nullable: true),
                    AttachmentPath = table.Column<string>(nullable: true),
                    AttachmentType = table.Column<int>(nullable: false),
                    AttachmentVersion = table.Column<double>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachment_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControlResources",
                columns: table => new
                {
                    ControlResourcesId = table.Column<Guid>(nullable: false),
                    ResourceTypeId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlResources", x => x.ControlResourcesId);
                    table.ForeignKey(
                        name: "FK_ControlResources_SystemOption_ResourceTypeId",
                        column: x => x.ResourceTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlResources_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OnlineUser",
                columns: table => new
                {
                    OnLineUserId = table.Column<Guid>(nullable: false),
                    KeepAlived = table.Column<DateTime>(nullable: false),
                    LoginTerminalId = table.Column<Guid>(nullable: false),
                    LoginTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineUser", x => x.OnLineUserId);
                    table.ForeignKey(
                        name: "FK_OnlineUser_UserTerminal_LoginTerminalId",
                        column: x => x.LoginTerminalId,
                        principalTable: "UserTerminal",
                        principalColumn: "UserTerminalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnlineUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSettingMapping",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    UserSettingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettingMapping", x => new { x.UserId, x.UserSettingId });
                    table.ForeignKey(
                        name: "FK_UserSettingMapping_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSettingMapping_UserSetting_UserSettingId",
                        column: x => x.UserSettingId,
                        principalTable: "UserSetting",
                        principalColumn: "UserSettingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceGroup",
                columns: table => new
                {
                    DeviceGroupId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 128, nullable: true),
                    DeviceGroupName = table.Column<string>(nullable: true),
                    DeviceGroupTypeId = table.Column<Guid>(nullable: false),
                    DeviceListJson = table.Column<string>(nullable: true),
                    ModifiedByUserId = table.Column<Guid>(nullable: true),
                    Mondified = table.Column<DateTime>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceGroup", x => x.DeviceGroupId);
                    table.ForeignKey(
                        name: "FK_DeviceGroup_SystemOption_DeviceGroupTypeId",
                        column: x => x.DeviceGroupTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceGroup_User_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceGroup_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IPDeviceInfo",
                columns: table => new
                {
                    IPDeviceInfoId = table.Column<Guid>(nullable: false),
                    DeviceTypeId = table.Column<Guid>(nullable: false),
                    EndPointsJson = table.Column<string>(nullable: true),
                    IPDeviceCode = table.Column<int>(nullable: false),
                    IPDeviceName = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    SeriesNo = table.Column<string>(nullable: true),
                    StatusId = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPDeviceInfo", x => x.IPDeviceInfoId);
                    table.ForeignKey(
                        name: "FK_IPDeviceInfo_SystemOption_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPDeviceInfo_User_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IPDeviceInfo_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPDeviceInfo_SystemOption_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerInfo",
                columns: table => new
                {
                    ServerInfoId = table.Column<Guid>(nullable: false),
                    EndPointsJson = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    ServerName = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerInfo", x => x.ServerInfoId);
                    table.ForeignKey(
                        name: "FK_ServerInfo_User_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerInfo_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VideoRoundScene",
                columns: table => new
                {
                    VideoRoundSceneId = table.Column<Guid>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: true),
                    VideoRoundSceneFlagId = table.Column<Guid>(nullable: false),
                    VideoRoundSceneName = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoRoundScene", x => x.VideoRoundSceneId);
                    table.ForeignKey(
                        name: "FK_VideoRoundScene_User_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VideoRoundScene_SystemOption_VideoRoundSceneFlagId",
                        column: x => x.VideoRoundSceneFlagId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriod",
                columns: table => new
                {
                    TimePeriodId = table.Column<Guid>(nullable: false),
                    DayPeriodId = table.Column<Guid>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ExtraJson = table.Column<string>(nullable: true),
                    OrderNo = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriod", x => x.TimePeriodId);
                    table.ForeignKey(
                        name: "FK_TimePeriod_DayPeriod_DayPeriodId",
                        column: x => x.DayPeriodId,
                        principalTable: "DayPeriod",
                        principalColumn: "DayPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckPackageTimePlan",
                columns: table => new
                {
                    DutyCheckPackageTimePlanId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    RandomRate = table.Column<double>(nullable: false),
                    ScheduleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckPackageTimePlan", x => x.DutyCheckPackageTimePlanId);
                    table.ForeignKey(
                        name: "FK_DutyCheckPackageTimePlan_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckPackageTimePlan_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false),
                    RoleId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fingerprint",
                columns: table => new
                {
                    FingerprintId = table.Column<Guid>(nullable: false),
                    FigureNo = table.Column<int>(nullable: false),
                    FingerprintBuffer = table.Column<byte[]>(nullable: true),
                    FingerprintNo = table.Column<int>(nullable: false),
                    StaffId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fingerprint", x => x.FingerprintId);
                    table.ForeignKey(
                        name: "FK_Fingerprint_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyOnDuty",
                columns: table => new
                {
                    DailyOnDutyId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DutyDate = table.Column<DateTime>(nullable: false),
                    DutyOfficerTodayId = table.Column<Guid>(nullable: false),
                    InNumber = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: true),
                    StrengthNumber = table.Column<int>(nullable: false),
                    TomorrowAttendantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyOnDuty", x => x.DailyOnDutyId);
                    table.ForeignKey(
                        name: "FK_DailyOnDuty_Staff_DutyOfficerTodayId",
                        column: x => x.DutyOfficerTodayId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyOnDuty_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyOnDuty_SystemOption_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyOnDuty_Staff_TomorrowAttendantId",
                        column: x => x.TomorrowAttendantId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyGroupSchedule",
                columns: table => new
                {
                    DutyGroupScheduleId = table.Column<Guid>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsCancel = table.Column<bool>(nullable: false),
                    ListerId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    ScheduleId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TabulationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyGroupSchedule", x => x.DutyGroupScheduleId);
                    table.ForeignKey(
                        name: "FK_DutyGroupSchedule_Staff_ListerId",
                        column: x => x.ListerId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyGroupSchedule_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyGroupSchedule_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DutySchedule",
                columns: table => new
                {
                    DutyScheduleId = table.Column<Guid>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ListerId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    ScheduleId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TabulationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutySchedule", x => x.DutyScheduleId);
                    table.ForeignKey(
                        name: "FK_DutySchedule_Staff_ListerId",
                        column: x => x.ListerId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutySchedule_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutySchedule_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionsDutyCheckSchedule",
                columns: table => new
                {
                    InstitutionsDutyCheckScheduleId = table.Column<Guid>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    EntouragesJson = table.Column<string>(nullable: true),
                    InspectedOrganizationId = table.Column<Guid>(nullable: false),
                    InspectionKey = table.Column<string>(nullable: true),
                    InspectionTargetJson = table.Column<string>(nullable: true),
                    LeadId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionsDutyCheckSchedule", x => x.InstitutionsDutyCheckScheduleId);
                    table.ForeignKey(
                        name: "FK_InstitutionsDutyCheckSchedule_Organization_InspectedOrganizationId",
                        column: x => x.InspectedOrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstitutionsDutyCheckSchedule_Staff_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShiftHandoverLog",
                columns: table => new
                {
                    ShiftHandoverLogId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HandoverDate = table.Column<DateTime>(nullable: false),
                    HandoverTime = table.Column<DateTime>(nullable: false),
                    OffGoingId = table.Column<Guid>(nullable: false),
                    OnComingId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftHandoverLog", x => x.ShiftHandoverLogId);
                    table.ForeignKey(
                        name: "FK_ShiftHandoverLog_Staff_OffGoingId",
                        column: x => x.OffGoingId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftHandoverLog_Staff_OnComingId",
                        column: x => x.OnComingId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftHandoverLog_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftHandoverLog_SystemOption_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckAppraise",
                columns: table => new
                {
                    DutyCheckAppraiseId = table.Column<Guid>(nullable: false),
                    AppraiseICOId = table.Column<Guid>(nullable: true),
                    AppraiseTypeId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DutyCheckAppraiseName = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckAppraise", x => x.DutyCheckAppraiseId);
                    table.ForeignKey(
                        name: "FK_DutyCheckAppraise_Attachment_AppraiseICOId",
                        column: x => x.AppraiseICOId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckAppraise_SystemOption_AppraiseTypeId",
                        column: x => x.AppraiseTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckAppraise_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckMatter",
                columns: table => new
                {
                    DutyCheckMatterId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MatterICOId = table.Column<Guid>(nullable: true),
                    MatterName = table.Column<string>(nullable: true),
                    MatterScore = table.Column<int>(nullable: false),
                    OrderNo = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    VoiceFileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckMatter", x => x.DutyCheckMatterId);
                    table.ForeignKey(
                        name: "FK_DutyCheckMatter_Attachment_MatterICOId",
                        column: x => x.MatterICOId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckMatter_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckMatter_Attachment_VoiceFileId",
                        column: x => x.VoiceFileId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckOperationAttachment",
                columns: table => new
                {
                    DutyCheckOperationAttachmentId = table.Column<Guid>(nullable: false),
                    AttachmentId = table.Column<Guid>(nullable: false),
                    AttachmentTypeId = table.Column<Guid>(nullable: false),
                    DutyCheckOperationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckOperationAttachment", x => x.DutyCheckOperationAttachmentId);
                    table.ForeignKey(
                        name: "FK_DutyCheckOperationAttachment_Attachment_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DutyCheckOperationAttachment_SystemOption_AttachmentTypeId",
                        column: x => x.AttachmentTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckOperationAttachment_DutyCheckOperation_DutyCheckOperationId",
                        column: x => x.DutyCheckOperationId,
                        principalTable: "DutyCheckOperation",
                        principalColumn: "DutyCheckOperationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryDuty",
                columns: table => new
                {
                    TemporaryDutyId = table.Column<Guid>(nullable: false),
                    Bullets = table.Column<int>(nullable: false),
                    CommanderId = table.Column<Guid>(nullable: false),
                    Contact = table.Column<string>(nullable: true),
                    DutyProgramme = table.Column<string>(nullable: true),
                    DutyProgrammePictureId = table.Column<Guid>(nullable: false),
                    DutyTypeId = table.Column<Guid>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Guns = table.Column<int>(nullable: false),
                    HasBullet = table.Column<bool>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    Posts = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    TaskName = table.Column<string>(nullable: true),
                    Troops = table.Column<int>(nullable: false),
                    VehicleTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryDuty", x => x.TemporaryDutyId);
                    table.ForeignKey(
                        name: "FK_TemporaryDuty_Staff_CommanderId",
                        column: x => x.CommanderId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryDuty_Attachment_DutyProgrammePictureId",
                        column: x => x.DutyProgrammePictureId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemporaryDuty_SystemOption_DutyTypeId",
                        column: x => x.DutyTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryDuty_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryDuty_SystemOption_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlarmLog",
                columns: table => new
                {
                    AlarmLogId = table.Column<Guid>(nullable: false),
                    AlarmLevelId = table.Column<Guid>(nullable: false),
                    AlarmSourceId = table.Column<Guid>(nullable: false),
                    AlarmStatusId = table.Column<Guid>(nullable: true),
                    AlarmTypeId = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: true),
                    TimeCreated = table.Column<DateTime>(nullable: false),
                    UploadCount = table.Column<int>(nullable: false),
                    UploadStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmLog", x => x.AlarmLogId);
                    table.ForeignKey(
                        name: "FK_AlarmLog_SystemOption_AlarmLevelId",
                        column: x => x.AlarmLevelId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmLog_IPDeviceInfo_AlarmSourceId",
                        column: x => x.AlarmSourceId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmLog_SystemOption_AlarmStatusId",
                        column: x => x.AlarmStatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmLog_SystemOption_AlarmTypeId",
                        column: x => x.AlarmTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmLog_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmLog_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BulletboxLog",
                columns: table => new
                {
                    BulletboxLogId = table.Column<Guid>(nullable: false),
                    BulletboxSnapshotId = table.Column<Guid>(nullable: true),
                    ComfirmInfo = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FrontSnapshotId = table.Column<Guid>(nullable: true),
                    LockStatusId = table.Column<Guid>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<Guid>(nullable: true),
                    SentinelDeviceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletboxLog", x => x.BulletboxLogId);
                    table.ForeignKey(
                        name: "FK_BulletboxLog_Attachment_CartridgeBoxSnapshotId",
                        column: x => x.BulletboxSnapshotId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulletboxLog_Attachment_FrontSnapshotId",
                        column: x => x.FrontSnapshotId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulletboxLog_SystemOption_LockStatusId",
                        column: x => x.LockStatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BulletboxLog_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulletboxLog_IPDeviceInfo_SentinelDeviceId",
                        column: x => x.SentinelDeviceId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PunchLog",
                columns: table => new
                {
                    PunchLogId = table.Column<Guid>(nullable: false),
                    AppraiseTypeId = table.Column<Guid>(nullable: true),
                    BulletboxSnapshotId = table.Column<Guid>(nullable: true),
                    FrontSnapshotId = table.Column<Guid>(nullable: true),
                    LogResultId = table.Column<Guid>(nullable: true),
                    LogTime = table.Column<DateTime>(nullable: false),
                    PunchDeviceId = table.Column<Guid>(nullable: false),
                    PunchTypeId = table.Column<Guid>(nullable: true),
                    StaffId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PunchLog", x => x.PunchLogId);
                    table.ForeignKey(
                        name: "FK_PunchLog_SystemOption_AppraiseTypeId",
                        column: x => x.AppraiseTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunchLog_Attachment_CartridgeBoxSnapshotId",
                        column: x => x.BulletboxSnapshotId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunchLog_Attachment_FrontSnapshotId",
                        column: x => x.FrontSnapshotId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunchLog_SystemOption_LogResultId",
                        column: x => x.LogResultId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunchLog_IPDeviceInfo_PunchDeviceId",
                        column: x => x.PunchDeviceId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PunchLog_SystemOption_PunchTypeId",
                        column: x => x.PunchTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PunchLog_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlarmMainframe",
                columns: table => new
                {
                    AlarmMainframeId = table.Column<Guid>(nullable: false),
                    DeviceInfoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmMainframe", x => x.AlarmMainframeId);
                    table.ForeignKey(
                        name: "FK_AlarmMainframe_IPDeviceInfo_DeviceInfoId",
                        column: x => x.DeviceInfoId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceGroupIPDevice",
                columns: table => new
                {
                    DeviceGroupId = table.Column<Guid>(nullable: false),
                    IPDeviceInfoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceGroupIPDevice", x => new { x.DeviceGroupId, x.IPDeviceInfoId });
                    table.ForeignKey(
                        name: "FK_DeviceGroupIPDevice_DeviceGroup_DeviceGroupId",
                        column: x => x.DeviceGroupId,
                        principalTable: "DeviceGroup",
                        principalColumn: "DeviceGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceGroupIPDevice_IPDeviceInfo_IPDeviceInfoId",
                        column: x => x.IPDeviceInfoId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceStatusHistory",
                columns: table => new
                {
                    DeviceStatusHistoryId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DeviceInfoId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStatusHistory", x => x.DeviceStatusHistoryId);
                    table.ForeignKey(
                        name: "FK_DeviceStatusHistory_IPDeviceInfo_DeviceInfoId",
                        column: x => x.DeviceInfoId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceStatusHistory_SystemOption_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encoder",
                columns: table => new
                {
                    EncoderId = table.Column<Guid>(nullable: false),
                    DeviceInfoId = table.Column<Guid>(nullable: false),
                    EncoderTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encoder", x => x.EncoderId);
                    table.ForeignKey(
                        name: "FK_Encoder_IPDeviceInfo_DeviceInfoId",
                        column: x => x.DeviceInfoId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encoder_EncoderType_EncoderTypeId",
                        column: x => x.EncoderTypeId,
                        principalTable: "EncoderType",
                        principalColumn: "EncoderTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaceRecognition",
                columns: table => new
                {
                    FaceRecognitionId = table.Column<Guid>(nullable: false),
                    AageGroup = table.Column<short>(nullable: false),
                    AbsTime = table.Column<DateTime>(nullable: true),
                    BackgroundPicLen = table.Column<long>(nullable: false),
                    BackgroupBuffer = table.Column<byte[]>(nullable: true),
                    ByEyeGlass = table.Column<byte>(nullable: false),
                    DeviceInfoId = table.Column<Guid>(nullable: false),
                    FaceBuffer = table.Column<byte[]>(nullable: true),
                    FacePicLen = table.Column<long>(nullable: false),
                    FaceScore = table.Column<int>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceRecognition", x => x.FaceRecognitionId);
                    table.ForeignKey(
                        name: "FK_FaceRecognition_IPDeviceInfo_DeviceInfoId",
                        column: x => x.DeviceInfoId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicensePlateRecognition",
                columns: table => new
                {
                    LicensePlateRecognitionId = table.Column<Guid>(nullable: false),
                    AbsTime = table.Column<DateTime>(nullable: false),
                    BackgroupBuffer = table.Column<byte[]>(nullable: true),
                    Color = table.Column<short>(nullable: false),
                    DeviceInfoId = table.Column<Guid>(nullable: false),
                    License = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    PicLen = table.Column<long>(nullable: false),
                    PicPlateLen = table.Column<long>(nullable: false),
                    PlateType = table.Column<short>(nullable: false),
                    VechileNoBuffer = table.Column<byte[]>(nullable: true),
                    VehicleType = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlateRecognition", x => x.LicensePlateRecognitionId);
                    table.ForeignKey(
                        name: "FK_LicensePlateRecognition_IPDeviceInfo_DeviceInfoId",
                        column: x => x.DeviceInfoId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInfo",
                columns: table => new
                {
                    ServiceInfoId = table.Column<Guid>(nullable: false),
                    EndPointsJson = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    ServerInfoId = table.Column<Guid>(nullable: false),
                    ServiceName = table.Column<string>(maxLength: 32, nullable: true),
                    ServiceTypeId = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInfo", x => x.ServiceInfoId);
                    table.ForeignKey(
                        name: "FK_ServiceInfo_User_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInfo_ServerInfo_ServerInfoId",
                        column: x => x.ServerInfoId,
                        principalTable: "ServerInfo",
                        principalColumn: "ServerInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInfo_SystemOption_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    PlanId = table.Column<Guid>(nullable: false),
                    PlanName = table.Column<string>(nullable: true),
                    PlanTypeId = table.Column<Guid>(nullable: true),
                    RealVideoRoundSceneId = table.Column<Guid>(nullable: true),
                    TvVideoRoundSceneId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK_Plan_SystemOption_PlanTypeId",
                        column: x => x.PlanTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plan_VideoRoundScene_RealVideoRoundSceneId",
                        column: x => x.RealVideoRoundSceneId,
                        principalTable: "VideoRoundScene",
                        principalColumn: "VideoRoundSceneId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plan_VideoRoundScene_TvVideoRoundSceneId",
                        column: x => x.TvVideoRoundSceneId,
                        principalTable: "VideoRoundScene",
                        principalColumn: "VideoRoundSceneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VideoRoundSection",
                columns: table => new
                {
                    VideoRoundSectionId = table.Column<Guid>(nullable: false),
                    RoundInterval = table.Column<int>(nullable: false),
                    TemplateLayoutId = table.Column<Guid>(nullable: false),
                    VideoRoundSceneId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoRoundSection", x => x.VideoRoundSectionId);
                    table.ForeignKey(
                        name: "FK_VideoRoundSection_TemplateLayout_TemplateLayoutId",
                        column: x => x.TemplateLayoutId,
                        principalTable: "TemplateLayout",
                        principalColumn: "TemplateLayoutId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoRoundSection_VideoRoundScene_VideoRoundSceneId",
                        column: x => x.VideoRoundSceneId,
                        principalTable: "VideoRoundScene",
                        principalColumn: "VideoRoundSceneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyGroupScheduleDetail",
                columns: table => new
                {
                    DutyGroupScheduleDetailId = table.Column<Guid>(nullable: false),
                    CheckManId = table.Column<Guid>(nullable: false),
                    DutyGroupScheduleId = table.Column<Guid>(nullable: true),
                    OrderNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyGroupScheduleDetail", x => x.DutyGroupScheduleDetailId);
                    table.ForeignKey(
                        name: "FK_DutyGroupScheduleDetail_Staff_CheckManId",
                        column: x => x.CheckManId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DutyGroupScheduleDetail_DutyGroupSchedule_DutyGroupScheduleId",
                        column: x => x.DutyGroupScheduleId,
                        principalTable: "DutyGroupSchedule",
                        principalColumn: "DutyGroupScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyTeam",
                columns: table => new
                {
                    DutyGroupScheduleId = table.Column<Guid>(nullable: false),
                    StaffId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyTeam", x => new { x.DutyGroupScheduleId, x.StaffId });
                    table.ForeignKey(
                        name: "FK_EmergencyTeam_DutyGroupSchedule_DutyGroupScheduleId",
                        column: x => x.DutyGroupScheduleId,
                        principalTable: "DutyGroupSchedule",
                        principalColumn: "DutyGroupScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmergencyTeam_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservegroup",
                columns: table => new
                {
                    DutyGroupScheduleId = table.Column<Guid>(nullable: false),
                    StaffId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservegroup", x => new { x.DutyGroupScheduleId, x.StaffId });
                    table.ForeignKey(
                        name: "FK_Reservegroup_DutyGroupSchedule_DutyGroupScheduleId",
                        column: x => x.DutyGroupScheduleId,
                        principalTable: "DutyGroupSchedule",
                        principalColumn: "DutyGroupScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservegroup_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Materiel",
                columns: table => new
                {
                    MaterielId = table.Column<Guid>(nullable: false),
                    MaterielCode = table.Column<string>(nullable: true),
                    MaterielName = table.Column<string>(nullable: true),
                    TemporaryDutyId = table.Column<Guid>(nullable: true),
                    UnitSystemOptionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiel", x => x.MaterielId);
                    table.ForeignKey(
                        name: "FK_Materiel_TemporaryDuty_TemporaryDutyId",
                        column: x => x.TemporaryDutyId,
                        principalTable: "TemporaryDuty",
                        principalColumn: "TemporaryDutyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materiel_SystemOption_UnitSystemOptionId",
                        column: x => x.UnitSystemOptionId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlarmProcessed",
                columns: table => new
                {
                    AlarmProcessedId = table.Column<Guid>(nullable: false),
                    AlarmLogId = table.Column<Guid>(nullable: false),
                    Conclusion = table.Column<string>(nullable: true),
                    ProcessedByUserId = table.Column<Guid>(nullable: true),
                    TimeProcessed = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmProcessed", x => x.AlarmProcessedId);
                    table.ForeignKey(
                        name: "FK_AlarmProcessed_AlarmLog_AlarmLogId",
                        column: x => x.AlarmLogId,
                        principalTable: "AlarmLog",
                        principalColumn: "AlarmLogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmProcessed_User_ProcessedByUserId",
                        column: x => x.ProcessedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlarmPeripheral",
                columns: table => new
                {
                    AlarmPeripheralId = table.Column<Guid>(nullable: false),
                    AlarmChannel = table.Column<int>(nullable: false),
                    AlarmDeviceId = table.Column<Guid>(nullable: false),
                    AlarmMainframeId = table.Column<Guid>(nullable: true),
                    AlarmTypeId = table.Column<Guid>(nullable: false),
                    DefendArea = table.Column<int>(nullable: false),
                    EncoderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmPeripheral", x => x.AlarmPeripheralId);
                    table.ForeignKey(
                        name: "FK_AlarmPeripheral_IPDeviceInfo_AlarmDeviceId",
                        column: x => x.AlarmDeviceId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmPeripheral_AlarmMainframe_AlarmMainframeId",
                        column: x => x.AlarmMainframeId,
                        principalTable: "AlarmMainframe",
                        principalColumn: "AlarmMainframeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmPeripheral_SystemOption_AlarmTypeId",
                        column: x => x.AlarmTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmPeripheral_Encoder_EncoderId",
                        column: x => x.EncoderId,
                        principalTable: "Encoder",
                        principalColumn: "EncoderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceEventLog",
                columns: table => new
                {
                    ServiceEventLogId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EventSourceId = table.Column<Guid>(nullable: false),
                    EventTypeId = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEventLog", x => x.ServiceEventLogId);
                    table.ForeignKey(
                        name: "FK_ServiceEventLog_ServiceInfo_EventSourceId",
                        column: x => x.EventSourceId,
                        principalTable: "ServiceInfo",
                        principalColumn: "ServiceInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceEventLog_SystemOption_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Camera",
                columns: table => new
                {
                    CameraId = table.Column<Guid>(nullable: false),
                    CameraNo = table.Column<int>(nullable: false),
                    EncoderChannel = table.Column<int>(nullable: false),
                    EncoderId = table.Column<Guid>(nullable: false),
                    IPDeviceId = table.Column<Guid>(nullable: false),
                    SnapshotId = table.Column<Guid>(nullable: true),
                    VideoForwardId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camera", x => x.CameraId);
                    table.ForeignKey(
                        name: "FK_Camera_Encoder_EncoderId",
                        column: x => x.EncoderId,
                        principalTable: "Encoder",
                        principalColumn: "EncoderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Camera_IPDeviceInfo_IPDeviceId",
                        column: x => x.IPDeviceId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Camera_Attachment_SnapshotId",
                        column: x => x.SnapshotId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Camera_ServiceInfo_VideoForwardId",
                        column: x => x.VideoForwardId,
                        principalTable: "ServiceInfo",
                        principalColumn: "ServiceInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlarmSetting",
                columns: table => new
                {
                    AlarmSettingId = table.Column<Guid>(nullable: false),
                    AlarmLevelId = table.Column<Guid>(nullable: false),
                    AlarmSourceId = table.Column<Guid>(nullable: false),
                    AlarmTypeId = table.Column<Guid>(nullable: false),
                    BeforePlanId = table.Column<Guid>(nullable: true),
                    EmergencyPlanId = table.Column<Guid>(nullable: true),
                    ScheduleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmSetting", x => x.AlarmSettingId);
                    table.ForeignKey(
                        name: "FK_AlarmSetting_SystemOption_AlarmLevelId",
                        column: x => x.AlarmLevelId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmSetting_IPDeviceInfo_AlarmSourceId",
                        column: x => x.AlarmSourceId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmSetting_SystemOption_AlarmTypeId",
                        column: x => x.AlarmTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmSetting_Plan_BeforePlanId",
                        column: x => x.BeforePlanId,
                        principalTable: "Plan",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmSetting_Plan_EmergencyPlanId",
                        column: x => x.EmergencyPlanId,
                        principalTable: "Plan",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmSetting_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanAction",
                columns: table => new
                {
                    PlanActionId = table.Column<Guid>(nullable: false),
                    PlanDeviceId = table.Column<Guid>(nullable: false),
                    PlanId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanAction", x => x.PlanActionId);
                    table.ForeignKey(
                        name: "FK_PlanAction_IPDeviceInfo_PlanDeviceId",
                        column: x => x.PlanDeviceId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanAction_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimerTask",
                columns: table => new
                {
                    TimerTaskId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PlanId = table.Column<Guid>(nullable: true),
                    TaskScheduleId = table.Column<Guid>(nullable: false),
                    TimerTaskName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimerTask", x => x.TimerTaskId);
                    table.ForeignKey(
                        name: "FK_TimerTask_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimerTask_Schedule_TaskScheduleId",
                        column: x => x.TaskScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CruiseScanGroup",
                columns: table => new
                {
                    CruiseScanGroupId = table.Column<Guid>(nullable: false),
                    CameraId = table.Column<Guid>(nullable: false),
                    GroupIndex = table.Column<int>(nullable: false),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruiseScanGroup", x => x.CruiseScanGroupId);
                    table.ForeignKey(
                        name: "FK_CruiseScanGroup_Camera_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Camera",
                        principalColumn: "CameraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitorySite",
                columns: table => new
                {
                    MonitorySiteId = table.Column<Guid>(nullable: false),
                    CameraId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InstallAddress = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDutycheckSite = table.Column<bool>(nullable: false),
                    MonitorySiteName = table.Column<string>(maxLength: 64, nullable: true),
                    OrderNo = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    Phone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorySite", x => x.MonitorySiteId);
                    table.ForeignKey(
                        name: "FK_MonitorySite_Camera_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Camera",
                        principalColumn: "CameraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonitorySite_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PresetSite",
                columns: table => new
                {
                    PresetSiteId = table.Column<Guid>(nullable: false),
                    CameraId = table.Column<Guid>(nullable: false),
                    PresetSiteNo = table.Column<byte>(nullable: false),
                    PresetSizeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetSite", x => x.PresetSiteId);
                    table.ForeignKey(
                        name: "FK_PresetSite_Camera_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Camera",
                        principalColumn: "CameraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PredefinedAction",
                columns: table => new
                {
                    PredefinedActionId = table.Column<Guid>(nullable: false),
                    ActionArgument = table.Column<string>(nullable: true),
                    ActionId = table.Column<Guid>(nullable: false),
                    PlanActionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedAction", x => x.PredefinedActionId);
                    table.ForeignKey(
                        name: "FK_PredefinedAction_SystemOption_ActionId",
                        column: x => x.ActionId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PredefinedAction_PlanAction_PlanActionId",
                        column: x => x.PlanActionId,
                        principalTable: "PlanAction",
                        principalColumn: "PlanActionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckSiteSchedule",
                columns: table => new
                {
                    DutyCheckSiteScheduleId = table.Column<Guid>(nullable: false),
                    CheckDutySiteId = table.Column<Guid>(nullable: true),
                    CheckManId = table.Column<Guid>(nullable: true),
                    DutyCheckGroupId = table.Column<Guid>(nullable: true),
                    DutyGroupScheduleDetailId = table.Column<Guid>(nullable: true),
                    SiteOrganizationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckSiteSchedule", x => x.DutyCheckSiteScheduleId);
                    table.ForeignKey(
                        name: "FK_DutyCheckSiteSchedule_MonitorySite_CheckDutySiteId",
                        column: x => x.CheckDutySiteId,
                        principalTable: "MonitorySite",
                        principalColumn: "MonitorySiteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckSiteSchedule_Staff_CheckManId",
                        column: x => x.CheckManId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckSiteSchedule_DutyCheckGroup_DutyCheckGroupId",
                        column: x => x.DutyCheckGroupId,
                        principalTable: "DutyCheckGroup",
                        principalColumn: "DutyCheckGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckSiteSchedule_DutyGroupScheduleDetail_DutyGroupScheduleDetailId",
                        column: x => x.DutyGroupScheduleDetailId,
                        principalTable: "DutyGroupScheduleDetail",
                        principalColumn: "DutyGroupScheduleDetailId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckSiteSchedule_Organization_SiteOrganizationId",
                        column: x => x.SiteOrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fault",
                columns: table => new
                {
                    FaultId = table.Column<Guid>(nullable: false),
                    CheckDutySiteId = table.Column<Guid>(nullable: false),
                    CheckManId = table.Column<Guid>(nullable: true),
                    CircularTime = table.Column<DateTime>(nullable: false),
                    DutyCheckOperationId = table.Column<Guid>(nullable: true),
                    DutyOrganizationId = table.Column<Guid>(nullable: false),
                    FaultTypeId = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fault", x => x.FaultId);
                    table.ForeignKey(
                        name: "FK_Fault_MonitorySite_CheckDutySiteId",
                        column: x => x.CheckDutySiteId,
                        principalTable: "MonitorySite",
                        principalColumn: "MonitorySiteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fault_Staff_CheckManId",
                        column: x => x.CheckManId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fault_DutyCheckOperation_DutyCheckOperationId",
                        column: x => x.DutyCheckOperationId,
                        principalTable: "DutyCheckOperation",
                        principalColumn: "DutyCheckOperationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fault_Organization_DutyOrganizationId",
                        column: x => x.DutyOrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fault_SystemOption_FaultTypeId",
                        column: x => x.FaultTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CruiseScanGroupPresetSite",
                columns: table => new
                {
                    CruiseScanGroupId = table.Column<Guid>(nullable: false),
                    PresetSiteID = table.Column<Guid>(nullable: false),
                    ScanInterval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruiseScanGroupPresetSite", x => new { x.CruiseScanGroupId, x.PresetSiteID });
                    table.ForeignKey(
                        name: "FK_CruiseScanGroupPresetSite_CruiseScanGroup_CruiseScanGroupId",
                        column: x => x.CruiseScanGroupId,
                        principalTable: "CruiseScanGroup",
                        principalColumn: "CruiseScanGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CruiseScanGroupPresetSite_PresetSite_PresetSiteID",
                        column: x => x.PresetSiteID,
                        principalTable: "PresetSite",
                        principalColumn: "PresetSiteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VideoRoundMonitorySiteSetting",
                columns: table => new
                {
                    VideoRoundMonitorySiteSettingId = table.Column<Guid>(nullable: false),
                    Monitor = table.Column<int>(nullable: false),
                    MonitorySiteId = table.Column<Guid>(nullable: false),
                    PresetSiteId = table.Column<Guid>(nullable: true),
                    SubView = table.Column<int>(nullable: false),
                    VideoRoundSectionId = table.Column<Guid>(nullable: true),
                    VideoStream = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoRoundMonitorySiteSetting", x => x.VideoRoundMonitorySiteSettingId);
                    table.ForeignKey(
                        name: "FK_VideoRoundMonitorySiteSetting_MonitorySite_MonitorySiteId",
                        column: x => x.MonitorySiteId,
                        principalTable: "MonitorySite",
                        principalColumn: "MonitorySiteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoRoundMonitorySiteSetting_PresetSite_PresetSiteId",
                        column: x => x.PresetSiteId,
                        principalTable: "PresetSite",
                        principalColumn: "PresetSiteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VideoRoundMonitorySiteSetting_VideoRoundSection_VideoRoundSectionId",
                        column: x => x.VideoRoundSectionId,
                        principalTable: "VideoRoundSection",
                        principalColumn: "VideoRoundSectionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckLog",
                columns: table => new
                {
                    DutyCheckLogId = table.Column<Guid>(nullable: false),
                    DayPeriodId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DutyCheckOperationId = table.Column<Guid>(nullable: true),
                    DutyCheckSiteScheduleId = table.Column<Guid>(nullable: true),
                    DutyCheckStaffId = table.Column<Guid>(nullable: true),
                    DutycheckSiteId = table.Column<Guid>(nullable: true),
                    DutycheckSiteName = table.Column<string>(nullable: true),
                    MainAppriseId = table.Column<Guid>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    PlanDate = table.Column<DateTime>(nullable: true),
                    RecordTime = table.Column<DateTime>(nullable: true),
                    RecordTypeId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: true),
                    TimePeriodJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckLog", x => x.DutyCheckLogId);
                    table.ForeignKey(
                        name: "FK_DutyCheckLog_DayPeriod_DayPeriodId",
                        column: x => x.DayPeriodId,
                        principalTable: "DayPeriod",
                        principalColumn: "DayPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckLog_DutyCheckOperation_DutyCheckOperationId",
                        column: x => x.DutyCheckOperationId,
                        principalTable: "DutyCheckOperation",
                        principalColumn: "DutyCheckOperationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckLog_DutyCheckSiteSchedule_DutyCheckSiteScheduleId",
                        column: x => x.DutyCheckSiteScheduleId,
                        principalTable: "DutyCheckSiteSchedule",
                        principalColumn: "DutyCheckSiteScheduleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckLog_Staff_DutyCheckStaffId",
                        column: x => x.DutyCheckStaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckLog_SystemOption_MainAppriseId",
                        column: x => x.MainAppriseId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckLog_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckLog_SystemOption_RecordTypeId",
                        column: x => x.RecordTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckLog_SystemOption_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Circular",
                columns: table => new
                {
                    CircularId = table.Column<Guid>(nullable: false),
                    CircularStaffId = table.Column<Guid>(nullable: false),
                    CircularTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DutyCheckLogId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circular", x => x.CircularId);
                    table.ForeignKey(
                        name: "FK_Circular_Staff_CircularStaffId",
                        column: x => x.CircularStaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Circular_DutyCheckLog_DutyCheckLogId",
                        column: x => x.DutyCheckLogId,
                        principalTable: "DutyCheckLog",
                        principalColumn: "DutyCheckLogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckLogAppraise",
                columns: table => new
                {
                    DutyCheckLogAppraiseId = table.Column<Guid>(nullable: false),
                    DutyCheckAppraiseId = table.Column<Guid>(nullable: false),
                    DutyCheckLogId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckLogAppraise", x => x.DutyCheckLogAppraiseId);
                    table.ForeignKey(
                        name: "FK_DutyCheckLogAppraise_DutyCheckAppraise_DutyCheckAppraiseId",
                        column: x => x.DutyCheckAppraiseId,
                        principalTable: "DutyCheckAppraise",
                        principalColumn: "DutyCheckAppraiseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DutyCheckLogAppraise_DutyCheckLog_DutyCheckLogId",
                        column: x => x.DutyCheckLogId,
                        principalTable: "DutyCheckLog",
                        principalColumn: "DutyCheckLogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckLogDispose",
                columns: table => new
                {
                    DutyCheckLogDisposeId = table.Column<Guid>(nullable: false),
                    DisposeId = table.Column<Guid>(nullable: false),
                    DutyCheckLogId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckLogDispose", x => x.DutyCheckLogDisposeId);
                    table.ForeignKey(
                        name: "FK_DutyCheckLogDispose_SystemOption_DisposeId",
                        column: x => x.DisposeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DutyCheckLogDispose_DutyCheckLog_DutyCheckLogId",
                        column: x => x.DutyCheckLogId,
                        principalTable: "DutyCheckLog",
                        principalColumn: "DutyCheckLogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckPackageLog",
                columns: table => new
                {
                    DutyCheckLogId = table.Column<Guid>(nullable: false),
                    DutyCheckPackageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckPackageLog", x => new { x.DutyCheckLogId, x.DutyCheckPackageId });
                    table.ForeignKey(
                        name: "FK_DutyCheckPackageLog_DutyCheckLog_DutyCheckLogId",
                        column: x => x.DutyCheckLogId,
                        principalTable: "DutyCheckLog",
                        principalColumn: "DutyCheckLogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DutyCheckPackageLog_DutyCheckPackage_DutyCheckPackageId",
                        column: x => x.DutyCheckPackageId,
                        principalTable: "DutyCheckPackage",
                        principalColumn: "DutyCheckPackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<Guid>(nullable: false),
                    CircularId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FaultId = table.Column<Guid>(nullable: true),
                    FeedbackStaffId = table.Column<Guid>(nullable: false),
                    FeedbackTime = table.Column<DateTime>(nullable: false),
                    FeedbackType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedback_Circular_CircularId",
                        column: x => x.CircularId,
                        principalTable: "Circular",
                        principalColumn: "CircularId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedback_Fault_FaultId",
                        column: x => x.FaultId,
                        principalTable: "Fault",
                        principalColumn: "FaultId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedback_Staff_FeedbackStaffId",
                        column: x => x.FeedbackStaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sentinel",
                columns: table => new
                {
                    SentinelId = table.Column<Guid>(nullable: false),
                    AudioFileId = table.Column<Guid>(nullable: true),
                    BulletboxCameraId = table.Column<Guid>(nullable: true),
                    DeviceInfoId = table.Column<Guid>(nullable: false),
                    FrontCameraId = table.Column<Guid>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Phone = table.Column<int>(nullable: false),
                    SentinelSettingId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentinel", x => x.SentinelId);
                    table.ForeignKey(
                        name: "FK_Sentinel_Attachment_AudioFileId",
                        column: x => x.AudioFileId,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sentinel_IPDeviceInfo_DeviceInfoId",
                        column: x => x.DeviceInfoId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sentinel_SentinelSetting_SentinelSettingId",
                        column: x => x.SentinelSettingId,
                        principalTable: "SentinelSetting",
                        principalColumn: "SentinelSettingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DefenseDevice",
                columns: table => new
                {
                    DefenseDeviceId = table.Column<Guid>(nullable: false),
                    AlarmIn = table.Column<int>(nullable: false),
                    AlarmInNormalOpen = table.Column<bool>(nullable: false),
                    AlarmOut = table.Column<int>(nullable: false),
                    DefenseDirectionId = table.Column<Guid>(nullable: true),
                    DefenseNo = table.Column<int>(nullable: false),
                    DeviceInfoId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SentinelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefenseDevice", x => x.DefenseDeviceId);
                    table.ForeignKey(
                        name: "FK_DefenseDevice_SystemOption_DefenseDirectionId",
                        column: x => x.DefenseDirectionId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DefenseDevice_IPDeviceInfo_DeviceInfoId",
                        column: x => x.DeviceInfoId,
                        principalTable: "IPDeviceInfo",
                        principalColumn: "IPDeviceInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefenseDevice_Sentinel_SentinelId",
                        column: x => x.SentinelId,
                        principalTable: "Sentinel",
                        principalColumn: "SentinelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceChannelSetting",
                columns: table => new
                {
                    DeviceChannelSettingId = table.Column<Guid>(nullable: false),
                    ChannelNo = table.Column<int>(nullable: false),
                    ChannelTypeId = table.Column<Guid>(nullable: false),
                    SentinelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceChannelSetting", x => x.DeviceChannelSettingId);
                    table.ForeignKey(
                        name: "FK_DeviceChannelSetting_SystemOption_ChannelTypeId",
                        column: x => x.ChannelTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceChannelSetting_Sentinel_SentinelId",
                        column: x => x.SentinelId,
                        principalTable: "Sentinel",
                        principalColumn: "SentinelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SentinelFingerPrintMapping",
                columns: table => new
                {
                    SentinelFingerPrintMappingId = table.Column<Guid>(nullable: false),
                    FingerprintId = table.Column<Guid>(nullable: false),
                    SentinelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentinelFingerPrintMapping", x => x.SentinelFingerPrintMappingId);
                    table.ForeignKey(
                        name: "FK_SentinelFingerPrintMapping_Fingerprint_FingerprintId",
                        column: x => x.FingerprintId,
                        principalTable: "Fingerprint",
                        principalColumn: "FingerprintId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SentinelFingerPrintMapping_Sentinel_SentinelId",
                        column: x => x.SentinelId,
                        principalTable: "Sentinel",
                        principalColumn: "SentinelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SentinelVideo",
                columns: table => new
                {
                    SentinelVideoId = table.Column<Guid>(nullable: false),
                    CameraId = table.Column<Guid>(nullable: false),
                    OrderNo = table.Column<int>(nullable: false),
                    PlayByDevice = table.Column<bool>(nullable: false),
                    SentinelId = table.Column<Guid>(nullable: true),
                    VideoTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentinelVideo", x => x.SentinelVideoId);
                    table.ForeignKey(
                        name: "FK_SentinelVideo_Camera_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Camera",
                        principalColumn: "CameraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SentinelVideo_Sentinel_SentinelId",
                        column: x => x.SentinelId,
                        principalTable: "Sentinel",
                        principalColumn: "SentinelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SentinelVideo_SystemOption_VideoTypeId",
                        column: x => x.VideoTypeId,
                        principalTable: "SystemOption",
                        principalColumn: "SystemOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyCheckSchedule",
                columns: table => new
                {
                    DutyCheckScheduleId = table.Column<Guid>(nullable: false),
                    CheckTimePeriodId = table.Column<Guid>(nullable: true),
                    DeputyId = table.Column<Guid>(nullable: true),
                    DutyScheduleDetailId = table.Column<Guid>(nullable: true),
                    LeaderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyCheckSchedule", x => x.DutyCheckScheduleId);
                    table.ForeignKey(
                        name: "FK_DutyCheckSchedule_TimePeriod_CheckTimePeriodId",
                        column: x => x.CheckTimePeriodId,
                        principalTable: "TimePeriod",
                        principalColumn: "TimePeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckSchedule_Staff_DeputyId",
                        column: x => x.DeputyId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyCheckSchedule_Staff_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DutyScheduleDetail",
                columns: table => new
                {
                    DutyScheduleDetailId = table.Column<Guid>(nullable: false),
                    CadreScheduleId = table.Column<Guid>(nullable: false),
                    CheckDay = table.Column<DateTime>(nullable: false),
                    DutyScheduleId = table.Column<Guid>(nullable: true),
                    OfficerScheduleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyScheduleDetail", x => x.DutyScheduleDetailId);
                    table.ForeignKey(
                        name: "FK_DutyScheduleDetail_DutyCheckSchedule_CadreScheduleId",
                        column: x => x.CadreScheduleId,
                        principalTable: "DutyCheckSchedule",
                        principalColumn: "DutyCheckScheduleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyScheduleDetail_DutySchedule_DutyScheduleId",
                        column: x => x.DutyScheduleId,
                        principalTable: "DutySchedule",
                        principalColumn: "DutyScheduleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DutyScheduleDetail_DutyCheckSchedule_OfficerScheduleId",
                        column: x => x.OfficerScheduleId,
                        principalTable: "DutyCheckSchedule",
                        principalColumn: "DutyCheckScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmLog_AlarmLevelId",
                table: "AlarmLog",
                column: "AlarmLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmLog_AlarmSourceId",
                table: "AlarmLog",
                column: "AlarmSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmLog_AlarmStatusId",
                table: "AlarmLog",
                column: "AlarmStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmLog_AlarmTypeId",
                table: "AlarmLog",
                column: "AlarmTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmLog_ApplicationId",
                table: "AlarmLog",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmLog_OrganizationId",
                table: "AlarmLog",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmProcessed_AlarmLogId",
                table: "AlarmProcessed",
                column: "AlarmLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmProcessed_ProcessedByUserId",
                table: "AlarmProcessed",
                column: "ProcessedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmSetting_AlarmLevelId",
                table: "AlarmSetting",
                column: "AlarmLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmSetting_AlarmTypeId",
                table: "AlarmSetting",
                column: "AlarmTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmSetting_BeforePlanId",
                table: "AlarmSetting",
                column: "BeforePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmSetting_EmergencyPlanId",
                table: "AlarmSetting",
                column: "EmergencyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmSetting_ScheduleId",
                table: "AlarmSetting",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmSetting_AlarmSourceId_AlarmTypeId",
                table: "AlarmSetting",
                columns: new[] { "AlarmSourceId", "AlarmTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAlarmMapping_AlarmTypeId",
                table: "DeviceAlarmMapping",
                column: "AlarmTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAlarmMapping_DeviceTypeId",
                table: "DeviceAlarmMapping",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_PlanTypeId",
                table: "Plan",
                column: "PlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_RealVideoRoundSceneId",
                table: "Plan",
                column: "RealVideoRoundSceneId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_TvVideoRoundSceneId",
                table: "Plan",
                column: "TvVideoRoundSceneId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanAction_PlanDeviceId",
                table: "PlanAction",
                column: "PlanDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanAction_PlanId",
                table: "PlanAction",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PredefinedAction_ActionId",
                table: "PredefinedAction",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_PredefinedAction_PlanActionId",
                table: "PredefinedAction",
                column: "PlanActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEventLog_EventSourceId",
                table: "ServiceEventLog",
                column: "EventSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEventLog_EventTypeId",
                table: "ServiceEventLog",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimerTask_PlanId",
                table: "TimerTask",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TimerTask_TaskScheduleId",
                table: "TimerTask",
                column: "TaskScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_ApplicationName",
                table: "Application",
                column: "ApplicationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationResource_ApplicationId",
                table: "ApplicationResource",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationResource_ParentResourceId",
                table: "ApplicationResource",
                column: "ParentResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSetting_ApplicationId",
                table: "ApplicationSetting",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSetting_SettingKey",
                table: "ApplicationSetting",
                column: "SettingKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSystemOption_SystemOptionId",
                table: "ApplicationSystemOption",
                column: "SystemOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ModifiedById",
                table: "Attachment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ControlResources_ResourceTypeId",
                table: "ControlResources",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlResources_UserId",
                table: "ControlResources",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DayPeriod_ScheduleCycleId",
                table: "DayPeriod",
                column: "ScheduleCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_ApplicationId",
                table: "EventLog",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_EventLevelId",
                table: "EventLog",
                column: "EventLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_EventLogTypeId",
                table: "EventLog",
                column: "EventLogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_EventSourceId",
                table: "EventLog",
                column: "EventSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_OrganizationId",
                table: "EventLog",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Fingerprint_StaffId",
                table: "Fingerprint",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineUser_LoginTerminalId",
                table: "OnlineUser",
                column: "LoginTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineUser_UserId",
                table: "OnlineUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_CenterId",
                table: "Organization",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_InServiceTypeId",
                table: "Organization",
                column: "InServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationFullName",
                table: "Organization",
                column: "OrganizationFullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationTypeId",
                table: "Organization",
                column: "OrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_ParentOrganizationId",
                table: "Organization",
                column: "ParentOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ResourceId",
                table: "Permission",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ResourcesActionId",
                table: "Permission",
                column: "ResourcesActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesAction_ApplicationResourceId",
                table: "ResourcesAction",
                column: "ApplicationResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ApplicationId",
                table: "Role",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ControlResourcesTypeId",
                table: "Role",
                column: "ControlResourcesTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_OrganizationId",
                table: "Role",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleName",
                table: "Role",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId1",
                table: "RolePermission",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ScheduleCycleId",
                table: "Schedule",
                column: "ScheduleCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ScheduleName",
                table: "Schedule",
                column: "ScheduleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ScheduleTypeId",
                table: "Schedule",
                column: "ScheduleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleCycle_CycleTypeId",
                table: "ScheduleCycle",
                column: "CycleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ApplicationId",
                table: "Staff",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_DegreeOfEducationId",
                table: "Staff",
                column: "DegreeOfEducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_DutyCheckTypeId",
                table: "Staff",
                column: "DutyCheckTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_MaritalStatusId",
                table: "Staff",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_NationId",
                table: "Staff",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_OrganizationId",
                table: "Staff",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PhotoId",
                table: "Staff",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PhysiclalStatusId",
                table: "Staff",
                column: "PhysiclalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PoliticalLandscapeId",
                table: "Staff",
                column: "PoliticalLandscapeId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PositionTypeId",
                table: "Staff",
                column: "PositionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_RankTypeId",
                table: "Staff",
                column: "RankTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ReignStatusId",
                table: "Staff",
                column: "ReignStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_SexId",
                table: "Staff",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_StaffGroupId",
                table: "Staff",
                column: "StaffGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_StaffName",
                table: "Staff",
                column: "StaffName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_WorkingPropertyId",
                table: "Staff",
                column: "WorkingPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffGroup_ApplicationId",
                table: "StaffGroup",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffGroup_OrganizationId",
                table: "StaffGroup",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOption_ParentSystemOptionId",
                table: "SystemOption",
                column: "ParentSystemOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOption_SystemOptionCode",
                table: "SystemOption",
                column: "SystemOptionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimePeriod_DayPeriodId",
                table: "TimePeriod",
                column: "DayPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ApplicationId",
                table: "User",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_User_OrganizationId",
                table: "User",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettingMapping_UserSettingId",
                table: "UserSettingMapping",
                column: "UserSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTerminal_UserTerminalTypeId",
                table: "UserTerminal",
                column: "UserTerminalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletboxLog_CartridgeBoxSnapshotId",
                table: "BulletboxLog",
                column: "BulletboxSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletboxLog_FrontSnapshotId",
                table: "BulletboxLog",
                column: "FrontSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletboxLog_LockStatusId",
                table: "BulletboxLog",
                column: "LockStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletboxLog_ModifiedById",
                table: "BulletboxLog",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BulletboxLog_SentinelDeviceId",
                table: "BulletboxLog",
                column: "SentinelDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Circular_CircularStaffId",
                table: "Circular",
                column: "CircularStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Circular_DutyCheckLogId",
                table: "Circular",
                column: "DutyCheckLogId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyOnDuty_DutyOfficerTodayId",
                table: "DailyOnDuty",
                column: "DutyOfficerTodayId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyOnDuty_OrganizationId",
                table: "DailyOnDuty",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyOnDuty_StatusId",
                table: "DailyOnDuty",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyOnDuty_TomorrowAttendantId",
                table: "DailyOnDuty",
                column: "TomorrowAttendantId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckAppraise_AppraiseICOId",
                table: "DutyCheckAppraise",
                column: "AppraiseICOId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckAppraise_AppraiseTypeId",
                table: "DutyCheckAppraise",
                column: "AppraiseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckAppraise_OrganizationId",
                table: "DutyCheckAppraise",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLog_DayPeriodId",
                table: "DutyCheckLog",
                column: "DayPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLog_DutyCheckOperationId",
                table: "DutyCheckLog",
                column: "DutyCheckOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLog_DutyCheckSiteScheduleId",
                table: "DutyCheckLog",
                column: "DutyCheckSiteScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLog_DutyCheckStaffId",
                table: "DutyCheckLog",
                column: "DutyCheckStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLog_MainAppriseId",
                table: "DutyCheckLog",
                column: "MainAppriseId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLog_OrganizationId",
                table: "DutyCheckLog",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLog_RecordTypeId",
                table: "DutyCheckLog",
                column: "RecordTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLog_StatusId",
                table: "DutyCheckLog",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLogAppraise_DutyCheckAppraiseId",
                table: "DutyCheckLogAppraise",
                column: "DutyCheckAppraiseId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLogAppraise_DutyCheckLogId",
                table: "DutyCheckLogAppraise",
                column: "DutyCheckLogId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLogDispose_DisposeId",
                table: "DutyCheckLogDispose",
                column: "DisposeId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckLogDispose_DutyCheckLogId",
                table: "DutyCheckLogDispose",
                column: "DutyCheckLogId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckMatter_MatterICOId",
                table: "DutyCheckMatter",
                column: "MatterICOId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckMatter_OrganizationId",
                table: "DutyCheckMatter",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckMatter_VoiceFileId",
                table: "DutyCheckMatter",
                column: "VoiceFileId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckOperationAttachment_AttachmentId",
                table: "DutyCheckOperationAttachment",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckOperationAttachment_AttachmentTypeId",
                table: "DutyCheckOperationAttachment",
                column: "AttachmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckOperationAttachment_DutyCheckOperationId",
                table: "DutyCheckOperationAttachment",
                column: "DutyCheckOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckPackage_OrganizationId",
                table: "DutyCheckPackage",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckPackage_PackageStatusId",
                table: "DutyCheckPackage",
                column: "PackageStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckPackageLog_DutyCheckPackageId",
                table: "DutyCheckPackageLog",
                column: "DutyCheckPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckPackageTimePlan_OrganizationId",
                table: "DutyCheckPackageTimePlan",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckPackageTimePlan_ScheduleId",
                table: "DutyCheckPackageTimePlan",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckSchedule_CheckTimePeriodId",
                table: "DutyCheckSchedule",
                column: "CheckTimePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckSchedule_DeputyId",
                table: "DutyCheckSchedule",
                column: "DeputyId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckSchedule_DutyScheduleDetailId",
                table: "DutyCheckSchedule",
                column: "DutyScheduleDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckSchedule_LeaderId",
                table: "DutyCheckSchedule",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckSiteSchedule_CheckDutySiteId",
                table: "DutyCheckSiteSchedule",
                column: "CheckDutySiteId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckSiteSchedule_CheckManId",
                table: "DutyCheckSiteSchedule",
                column: "CheckManId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckSiteSchedule_DutyCheckGroupId",
                table: "DutyCheckSiteSchedule",
                column: "DutyCheckGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckSiteSchedule_DutyGroupScheduleDetailId",
                table: "DutyCheckSiteSchedule",
                column: "DutyGroupScheduleDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyCheckSiteSchedule_SiteOrganizationId",
                table: "DutyCheckSiteSchedule",
                column: "SiteOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyGroupSchedule_ListerId",
                table: "DutyGroupSchedule",
                column: "ListerId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyGroupSchedule_OrganizationId",
                table: "DutyGroupSchedule",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyGroupSchedule_ScheduleId",
                table: "DutyGroupSchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyGroupScheduleDetail_CheckManId",
                table: "DutyGroupScheduleDetail",
                column: "CheckManId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyGroupScheduleDetail_DutyGroupScheduleId",
                table: "DutyGroupScheduleDetail",
                column: "DutyGroupScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DutySchedule_ListerId",
                table: "DutySchedule",
                column: "ListerId");

            migrationBuilder.CreateIndex(
                name: "IX_DutySchedule_OrganizationId",
                table: "DutySchedule",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DutySchedule_ScheduleId",
                table: "DutySchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyScheduleDetail_CadreScheduleId",
                table: "DutyScheduleDetail",
                column: "CadreScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyScheduleDetail_DutyScheduleId",
                table: "DutyScheduleDetail",
                column: "DutyScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyScheduleDetail_OfficerScheduleId",
                table: "DutyScheduleDetail",
                column: "OfficerScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyTeam_StaffId",
                table: "EmergencyTeam",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Fault_CheckDutySiteId",
                table: "Fault",
                column: "CheckDutySiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Fault_CheckManId",
                table: "Fault",
                column: "CheckManId");

            migrationBuilder.CreateIndex(
                name: "IX_Fault_DutyCheckOperationId",
                table: "Fault",
                column: "DutyCheckOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Fault_DutyOrganizationId",
                table: "Fault",
                column: "DutyOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Fault_FaultTypeId",
                table: "Fault",
                column: "FaultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CircularId",
                table: "Feedback",
                column: "CircularId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_FaultId",
                table: "Feedback",
                column: "FaultId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_FeedbackStaffId",
                table: "Feedback",
                column: "FeedbackStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionsDutyCheckSchedule_InspectedOrganizationId",
                table: "InstitutionsDutyCheckSchedule",
                column: "InspectedOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionsDutyCheckSchedule_LeadId",
                table: "InstitutionsDutyCheckSchedule",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_PunchLog_AppraiseTypeId",
                table: "PunchLog",
                column: "AppraiseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PunchLog_CartridgeBoxSnapshotId",
                table: "PunchLog",
                column: "BulletboxSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_PunchLog_FrontSnapshotId",
                table: "PunchLog",
                column: "FrontSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_PunchLog_LogResultId",
                table: "PunchLog",
                column: "LogResultId");

            migrationBuilder.CreateIndex(
                name: "IX_PunchLog_PunchDeviceId",
                table: "PunchLog",
                column: "PunchDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PunchLog_PunchTypeId",
                table: "PunchLog",
                column: "PunchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PunchLog_StaffId",
                table: "PunchLog",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservegroup_StaffId",
                table: "Reservegroup",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandoverLog_OffGoingId",
                table: "ShiftHandoverLog",
                column: "OffGoingId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandoverLog_OnComingId",
                table: "ShiftHandoverLog",
                column: "OnComingId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandoverLog_OrganizationId",
                table: "ShiftHandoverLog",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandoverLog_StatusId",
                table: "ShiftHandoverLog",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDuty_CommanderId",
                table: "TemporaryDuty",
                column: "CommanderId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDuty_DutyProgrammePictureId",
                table: "TemporaryDuty",
                column: "DutyProgrammePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDuty_DutyTypeId",
                table: "TemporaryDuty",
                column: "DutyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDuty_OrganizationId",
                table: "TemporaryDuty",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDuty_VehicleTypeId",
                table: "TemporaryDuty",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmMainframe_DeviceInfoId",
                table: "AlarmMainframe",
                column: "DeviceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmPeripheral_AlarmDeviceId",
                table: "AlarmPeripheral",
                column: "AlarmDeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlarmPeripheral_AlarmMainframeId",
                table: "AlarmPeripheral",
                column: "AlarmMainframeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmPeripheral_AlarmTypeId",
                table: "AlarmPeripheral",
                column: "AlarmTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmPeripheral_EncoderId",
                table: "AlarmPeripheral",
                column: "EncoderId");

            migrationBuilder.CreateIndex(
                name: "IX_Camera_IPDeviceId",
                table: "Camera",
                column: "IPDeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Camera_SnapshotId",
                table: "Camera",
                column: "SnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_Camera_VideoForwardId",
                table: "Camera",
                column: "VideoForwardId");

            migrationBuilder.CreateIndex(
                name: "IX_Camera_EncoderId_EncoderChannel",
                table: "Camera",
                columns: new[] { "EncoderId", "EncoderChannel" });

            migrationBuilder.CreateIndex(
                name: "IX_CruiseScanGroup_CameraId_GroupName",
                table: "CruiseScanGroup",
                columns: new[] { "CameraId", "GroupName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CruiseScanGroupPresetSite_PresetSiteID",
                table: "CruiseScanGroupPresetSite",
                column: "PresetSiteID");

            migrationBuilder.CreateIndex(
                name: "IX_DefenseDevice_DefenseDirectionId",
                table: "DefenseDevice",
                column: "DefenseDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_DefenseDevice_DeviceInfoId",
                table: "DefenseDevice",
                column: "DeviceInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DefenseDevice_SentinelId",
                table: "DefenseDevice",
                column: "SentinelId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceChannelSetting_ChannelTypeId",
                table: "DeviceChannelSetting",
                column: "ChannelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceChannelSetting_SentinelId",
                table: "DeviceChannelSetting",
                column: "SentinelId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceChannelTypeMapping_ChannelTypeId",
                table: "DeviceChannelTypeMapping",
                column: "ChannelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceChannelTypeMapping_DeviceTypeId",
                table: "DeviceChannelTypeMapping",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceGroup_DeviceGroupTypeId",
                table: "DeviceGroup",
                column: "DeviceGroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceGroup_ModifiedByUserId",
                table: "DeviceGroup",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceGroup_OrganizationId_DeviceGroupName",
                table: "DeviceGroup",
                columns: new[] { "OrganizationId", "DeviceGroupName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceGroupIPDevice_IPDeviceInfoId",
                table: "DeviceGroupIPDevice",
                column: "IPDeviceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStatusHistory_DeviceInfoId",
                table: "DeviceStatusHistory",
                column: "DeviceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStatusHistory_StatusId",
                table: "DeviceStatusHistory",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Encoder_DeviceInfoId",
                table: "Encoder",
                column: "DeviceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Encoder_EncoderTypeId",
                table: "Encoder",
                column: "EncoderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IPDeviceInfo_DeviceTypeId",
                table: "IPDeviceInfo",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IPDeviceInfo_ModifiedByUserId",
                table: "IPDeviceInfo",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IPDeviceInfo_OrganizationId",
                table: "IPDeviceInfo",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_IPDeviceInfo_StatusId",
                table: "IPDeviceInfo",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Materiel_TemporaryDutyId",
                table: "Materiel",
                column: "TemporaryDutyId");

            migrationBuilder.CreateIndex(
                name: "IX_Materiel_UnitSystemOptionId",
                table: "Materiel",
                column: "UnitSystemOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitorySite_CameraId",
                table: "MonitorySite",
                column: "CameraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonitorySite_OrganizationId_MonitorySiteName",
                table: "MonitorySite",
                columns: new[] { "OrganizationId", "MonitorySiteName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PresetSite_CameraId_PresetSizeName",
                table: "PresetSite",
                columns: new[] { "CameraId", "PresetSizeName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sentinel_AudioFileId",
                table: "Sentinel",
                column: "AudioFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Sentinel_BulletboxCameraId",
                table: "Sentinel",
                column: "BulletboxCameraId");

            migrationBuilder.CreateIndex(
                name: "IX_Sentinel_DeviceInfoId",
                table: "Sentinel",
                column: "DeviceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sentinel_FrontCameraId",
                table: "Sentinel",
                column: "FrontCameraId");

            migrationBuilder.CreateIndex(
                name: "IX_Sentinel_SentinelSettingId",
                table: "Sentinel",
                column: "SentinelSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_SentinelFingerPrintMapping_FingerprintId",
                table: "SentinelFingerPrintMapping",
                column: "FingerprintId");

            migrationBuilder.CreateIndex(
                name: "IX_SentinelFingerPrintMapping_SentinelId",
                table: "SentinelFingerPrintMapping",
                column: "SentinelId");

            migrationBuilder.CreateIndex(
                name: "IX_SentinelVideo_CameraId",
                table: "SentinelVideo",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_SentinelVideo_SentinelId",
                table: "SentinelVideo",
                column: "SentinelId");

            migrationBuilder.CreateIndex(
                name: "IX_SentinelVideo_VideoTypeId",
                table: "SentinelVideo",
                column: "VideoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerInfo_ModifiedByUserId",
                table: "ServerInfo",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerInfo_OrganizationId_ServerName",
                table: "ServerInfo",
                columns: new[] { "OrganizationId", "ServerName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInfo_ModifiedByUserId",
                table: "ServiceInfo",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInfo_ServerInfoId",
                table: "ServiceInfo",
                column: "ServerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInfo_ServiceTypeId",
                table: "ServiceInfo",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCell_TemplateLayoutId",
                table: "TemplateCell",
                column: "TemplateLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateLayout_LayoutTypeId",
                table: "TemplateLayout",
                column: "LayoutTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateLayout_TemplateTypeId",
                table: "TemplateLayout",
                column: "TemplateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateLayout_TemplateLayoutName_TemplateTypeId",
                table: "TemplateLayout",
                columns: new[] { "TemplateLayoutName", "TemplateTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoRoundMonitorySiteSetting_MonitorySiteId",
                table: "VideoRoundMonitorySiteSetting",
                column: "MonitorySiteId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoRoundMonitorySiteSetting_PresetSiteId",
                table: "VideoRoundMonitorySiteSetting",
                column: "PresetSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoRoundMonitorySiteSetting_VideoRoundSectionId",
                table: "VideoRoundMonitorySiteSetting",
                column: "VideoRoundSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoRoundScene_ModifiedByUserId",
                table: "VideoRoundScene",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoRoundScene_VideoRoundSceneFlagId",
                table: "VideoRoundScene",
                column: "VideoRoundSceneFlagId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoRoundScene_VideoRoundSceneName",
                table: "VideoRoundScene",
                column: "VideoRoundSceneName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoRoundSection_TemplateLayoutId",
                table: "VideoRoundSection",
                column: "TemplateLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoRoundSection_VideoRoundSceneId",
                table: "VideoRoundSection",
                column: "VideoRoundSceneId");

            migrationBuilder.CreateIndex(
                name: "IX_FaceRecognition_DeviceInfoId",
                table: "FaceRecognition",
                column: "DeviceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlateRecognition_DeviceInfoId",
                table: "LicensePlateRecognition",
                column: "DeviceInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sentinel_SentinelVideo_BulletboxCameraId",
                table: "Sentinel",
                column: "BulletboxCameraId",
                principalTable: "SentinelVideo",
                principalColumn: "SentinelVideoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sentinel_SentinelVideo_FrontCameraId",
                table: "Sentinel",
                column: "FrontCameraId",
                principalTable: "SentinelVideo",
                principalColumn: "SentinelVideoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DutyCheckSchedule_DutyScheduleDetail_DutyScheduleDetailId",
                table: "DutyCheckSchedule",
                column: "DutyScheduleDetailId",
                principalTable: "DutyScheduleDetail",
                principalColumn: "DutyScheduleDetailId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_SystemOption_InServiceTypeId",
                table: "Organization");

            migrationBuilder.DropForeignKey(
                name: "FK_Organization_SystemOption_OrganizationTypeId",
                table: "Organization");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_SystemOption_ScheduleTypeId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleCycle_SystemOption_CycleTypeId",
                table: "ScheduleCycle");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_DegreeOfEducationId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_DutyCheckTypeId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_MaritalStatusId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_NationId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_PhysiclalStatusId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_PoliticalLandscapeId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_PositionTypeId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_RankTypeId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_ReignStatusId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_SexId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_SystemOption_WorkingPropertyId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_IPDeviceInfo_SystemOption_DeviceTypeId",
                table: "IPDeviceInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_IPDeviceInfo_SystemOption_StatusId",
                table: "IPDeviceInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_SentinelVideo_SystemOption_VideoTypeId",
                table: "SentinelVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceInfo_SystemOption_ServiceTypeId",
                table: "ServiceInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Camera_IPDeviceInfo_IPDeviceId",
                table: "Camera");

            migrationBuilder.DropForeignKey(
                name: "FK_Encoder_IPDeviceInfo_DeviceInfoId",
                table: "Encoder");

            migrationBuilder.DropForeignKey(
                name: "FK_Sentinel_IPDeviceInfo_DeviceInfoId",
                table: "Sentinel");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Application_ApplicationId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffGroup_Application_ApplicationId",
                table: "StaffGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Application_ApplicationId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Organization_OrganizationId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffGroup_Organization_OrganizationId",
                table: "StaffGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Organization_OrganizationId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_DutySchedule_Organization_OrganizationId",
                table: "DutySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerInfo_Organization_OrganizationId",
                table: "ServerInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_User_ModifiedById",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerInfo_User_ModifiedByUserId",
                table: "ServerInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceInfo_User_ModifiedByUserId",
                table: "ServiceInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_DutySchedule_Schedule_ScheduleId",
                table: "DutySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Camera_ServiceInfo_VideoForwardId",
                table: "Camera");

            migrationBuilder.DropForeignKey(
                name: "FK_DayPeriod_ScheduleCycle_ScheduleCycleId",
                table: "DayPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_DutyCheckSchedule_Staff_DeputyId",
                table: "DutyCheckSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_DutyCheckSchedule_Staff_LeaderId",
                table: "DutyCheckSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_DutySchedule_Staff_ListerId",
                table: "DutySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TimePeriod_DayPeriod_DayPeriodId",
                table: "TimePeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_Camera_Attachment_SnapshotId",
                table: "Camera");

            migrationBuilder.DropForeignKey(
                name: "FK_Sentinel_Attachment_AudioFileId",
                table: "Sentinel");

            migrationBuilder.DropForeignKey(
                name: "FK_DutyCheckSchedule_TimePeriod_CheckTimePeriodId",
                table: "DutyCheckSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_DutyCheckSchedule_DutyScheduleDetail_DutyScheduleDetailId",
                table: "DutyCheckSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Camera_Encoder_EncoderId",
                table: "Camera");

            migrationBuilder.DropForeignKey(
                name: "FK_SentinelVideo_Camera_CameraId",
                table: "SentinelVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_SentinelVideo_Sentinel_SentinelId",
                table: "SentinelVideo");

            migrationBuilder.DropTable(
                name: "AlarmProcessed");

            migrationBuilder.DropTable(
                name: "AlarmSetting");

            migrationBuilder.DropTable(
                name: "DeviceAlarmMapping");

            migrationBuilder.DropTable(
                name: "PredefinedAction");

            migrationBuilder.DropTable(
                name: "ServiceEventLog");

            migrationBuilder.DropTable(
                name: "TimerTask");

            migrationBuilder.DropTable(
                name: "ApplicationSetting");

            migrationBuilder.DropTable(
                name: "ApplicationSystemOption");

            migrationBuilder.DropTable(
                name: "ControlResources");

            migrationBuilder.DropTable(
                name: "EventLog");

            migrationBuilder.DropTable(
                name: "OnlineUser");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserSettingMapping");

            migrationBuilder.DropTable(
                name: "BulletboxLog");

            migrationBuilder.DropTable(
                name: "DailyOnDuty");

            migrationBuilder.DropTable(
                name: "DutyCheckLogAppraise");

            migrationBuilder.DropTable(
                name: "DutyCheckLogDispose");

            migrationBuilder.DropTable(
                name: "DutyCheckMatter");

            migrationBuilder.DropTable(
                name: "DutyCheckOperationAttachment");

            migrationBuilder.DropTable(
                name: "DutyCheckPackageLog");

            migrationBuilder.DropTable(
                name: "DutyCheckPackageTimePlan");

            migrationBuilder.DropTable(
                name: "EmergencyTeam");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "InstitutionsDutyCheckSchedule");

            migrationBuilder.DropTable(
                name: "PunchLog");

            migrationBuilder.DropTable(
                name: "Reservegroup");

            migrationBuilder.DropTable(
                name: "ShiftHandoverLog");

            migrationBuilder.DropTable(
                name: "AlarmPeripheral");

            migrationBuilder.DropTable(
                name: "CruiseScanGroupPresetSite");

            migrationBuilder.DropTable(
                name: "DefenseDevice");

            migrationBuilder.DropTable(
                name: "DeviceChannelSetting");

            migrationBuilder.DropTable(
                name: "DeviceChannelTypeMapping");

            migrationBuilder.DropTable(
                name: "DeviceGroupIPDevice");

            migrationBuilder.DropTable(
                name: "DeviceStatusHistory");

            migrationBuilder.DropTable(
                name: "Materiel");

            migrationBuilder.DropTable(
                name: "SentinelFingerPrintMapping");

            migrationBuilder.DropTable(
                name: "SentinelLayout");

            migrationBuilder.DropTable(
                name: "TemplateCell");

            migrationBuilder.DropTable(
                name: "VideoRoundMonitorySiteSetting");

            migrationBuilder.DropTable(
                name: "FaceRecognition");

            migrationBuilder.DropTable(
                name: "LicensePlateRecognition");

            migrationBuilder.DropTable(
                name: "AlarmLog");

            migrationBuilder.DropTable(
                name: "PlanAction");

            migrationBuilder.DropTable(
                name: "UserTerminal");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "UserSetting");

            migrationBuilder.DropTable(
                name: "DutyCheckAppraise");

            migrationBuilder.DropTable(
                name: "DutyCheckPackage");

            migrationBuilder.DropTable(
                name: "Circular");

            migrationBuilder.DropTable(
                name: "Fault");

            migrationBuilder.DropTable(
                name: "AlarmMainframe");

            migrationBuilder.DropTable(
                name: "CruiseScanGroup");

            migrationBuilder.DropTable(
                name: "DeviceGroup");

            migrationBuilder.DropTable(
                name: "TemporaryDuty");

            migrationBuilder.DropTable(
                name: "Fingerprint");

            migrationBuilder.DropTable(
                name: "PresetSite");

            migrationBuilder.DropTable(
                name: "VideoRoundSection");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "ResourcesAction");

            migrationBuilder.DropTable(
                name: "DutyCheckLog");

            migrationBuilder.DropTable(
                name: "TemplateLayout");

            migrationBuilder.DropTable(
                name: "VideoRoundScene");

            migrationBuilder.DropTable(
                name: "ApplicationResource");

            migrationBuilder.DropTable(
                name: "DutyCheckOperation");

            migrationBuilder.DropTable(
                name: "DutyCheckSiteSchedule");

            migrationBuilder.DropTable(
                name: "MonitorySite");

            migrationBuilder.DropTable(
                name: "DutyCheckGroup");

            migrationBuilder.DropTable(
                name: "DutyGroupScheduleDetail");

            migrationBuilder.DropTable(
                name: "DutyGroupSchedule");

            migrationBuilder.DropTable(
                name: "SystemOption");

            migrationBuilder.DropTable(
                name: "IPDeviceInfo");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "ApplicationCenter");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "ServiceInfo");

            migrationBuilder.DropTable(
                name: "ServerInfo");

            migrationBuilder.DropTable(
                name: "ScheduleCycle");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "UserPhoto");

            migrationBuilder.DropTable(
                name: "StaffGroup");

            migrationBuilder.DropTable(
                name: "DayPeriod");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "TimePeriod");

            migrationBuilder.DropTable(
                name: "DutyScheduleDetail");

            migrationBuilder.DropTable(
                name: "DutyCheckSchedule");

            migrationBuilder.DropTable(
                name: "DutySchedule");

            migrationBuilder.DropTable(
                name: "Encoder");

            migrationBuilder.DropTable(
                name: "EncoderType");

            migrationBuilder.DropTable(
                name: "Camera");

            migrationBuilder.DropTable(
                name: "Sentinel");

            migrationBuilder.DropTable(
                name: "SentinelVideo");

            migrationBuilder.DropTable(
                name: "SentinelSetting");
        }
    }
}
