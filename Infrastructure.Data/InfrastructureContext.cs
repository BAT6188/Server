using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class InfrastructureContext: DbContext
    {

        /// <summary>
        /// 应用集合
        /// </summary>
        public DbSet<Application> ApplicationSet
        {
            get; set;
        }

        /// <summary>
        /// 应用中心集合
        /// </summary>
        public DbSet<ApplicationCenter> ApplicationCenterSet
        {
            get; set;
        }

        /// <summary>
        /// 系统资源集合
        /// </summary>
        public DbSet<ApplicationResource> ApplicationResourceSet
        {
            get; set;
        }

        /// <summary>
        /// 应用设置集合
        /// </summary>
        public DbSet<ApplicationSetting> ApplicationSettingsSet
        {
            get; set;
        }

        /// <summary>
        /// 附件集合
        /// </summary>
        public DbSet<Attachment> AttachmentSet
        {
            get; set;
        }

        /// <summary>
        /// 事件日志集合
        /// </summary>
        public DbSet<EventLog> EventLogSet
        {
            get; set;
        }

        /// <summary>
        /// 指纹集合
        /// </summary>
        public DbSet<Fingerprint> FingerprintSet
        {
            get; set;
        }

        /// <summary>
        /// 在线用户集合
        /// </summary>
        public DbSet<OnlineUser> OnlineUserSet
        {
            get; set;
        }

        /// <summary>
        /// 组织机构集合
        /// </summary>
        public DbSet<Organization> OrganizationSet
        {
            get; set;
        }

        /// <summary>
        /// 权限范围集合
        /// </summary>
        public DbSet<Permission> PermissionSet
        {
            get; set;
        }

        /// <summary>
        /// 操作权限集合
        /// </summary>
        public DbSet<ResourcesAction> ResourcesActionSet
        {
            get; set;
        }

        /// <summary>
        /// 角色集合
        /// </summary>
        public DbSet<Role> RoleSet
        {
            get; set;
        }

        /// <summary>
        /// 任务排程集合
        /// </summary>
        public DbSet<Schedule> ScheduleSet
        {
            get; set;
        }

        /// <summary>
        /// 人员集合
        /// </summary>
        public DbSet<Staff> StaffSet
        {
            get;set;
        }

        /// <summary>
        /// 人员组集合
        /// </summary>
        public DbSet<StaffGroup> StaffGroupSet
        {
            get; set;
        }

        /// <summary>
        /// 系统选项集合
        /// </summary>
        public DbSet<SystemOption> SystemOptionsSet
        {
            get; set;
        }

        /// <summary>
        /// 用户集合
        /// </summary>
        public DbSet<User> UserSet
        {
            get; set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=C:\\Sqlite\\paps.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Application
            modelBuilder.Entity<Application>().HasKey(p => p.ApplicationId);
            modelBuilder.Entity<Application>().HasMany(p => p.ApplicationSettings);
            //ApplicationCenter
            modelBuilder.Entity<ApplicationCenter>().HasKey(p => p.ApplicationCenterId);
            //modelBuilder.Entity<ApplicationCenter>().HasOne(p => p.InterconnectedApplicationCenter);
            modelBuilder.Entity<ApplicationCenter>().HasMany(p => p.EndPoints);
            //ApplicationResource
            modelBuilder.Entity<ApplicationResource>().HasKey(p => p.ApplicationResourceId);
            modelBuilder.Entity<ApplicationResource>().HasOne(p => p.Application);
            modelBuilder.Entity<ApplicationResource>().HasOne(p => p.ParentResource);
            modelBuilder.Entity<ApplicationResource>().HasMany(p => p.Actions);
            //ApplicationSetting
            modelBuilder.Entity<ApplicationSetting>().HasKey(p => p.ApplicationSettingId);
            //Attachment
            modelBuilder.Entity<Attachment>().HasKey(p => p.AttachmentId);
            //ControlResources
            modelBuilder.Entity<ControlResources>().HasKey(p => p.ControlResourcesId);
            //EndPointInfo
            //modelBuilder.Entity<EndPointInfo>().HasKey(p => p.EndPointInfoId);
            //EventLog
            modelBuilder.Entity<EventLog>().HasKey(p => p.EventLogId);
            modelBuilder.Entity<EventLog>().HasOne(p => p.EventLevel);
            modelBuilder.Entity<EventLog>().HasOne(p => p.EventSource);
            modelBuilder.Entity<EventLog>().HasOne(p => p.EventLogType);
            modelBuilder.Entity<EventLog>().HasOne(p => p.Organization);
            modelBuilder.Entity<EventLog>().HasOne(p => p.Application);
            //Fingerprint
            modelBuilder.Entity<Fingerprint>().HasKey(p => p.FingerprintId);
            //OnlineUser
            modelBuilder.Entity<OnlineUser>().HasKey(p => p.OnLineUserId);
            modelBuilder.Entity<OnlineUser>().HasOne(p => p.User);
            modelBuilder.Entity<OnlineUser>().HasOne(p => p.LoginTerminal);
            //Organization
            modelBuilder.Entity<Organization>().HasKey(p => p.OrganizationId);
            modelBuilder.Entity<Organization>().HasOne(p => p.Center);
            modelBuilder.Entity<Organization>().HasOne(p => p.OrganizationType);
            modelBuilder.Entity<Organization>().HasOne(p => p.ParentOrganization);
            //Permission
            modelBuilder.Entity<Permission>().HasKey(p => p.PermissionId);
            modelBuilder.Entity<Permission>().HasOne(p => p.Resource);
            modelBuilder.Entity<Permission>().HasOne(p => p.ResourcesAction);
            //ResourcesAction
            modelBuilder.Entity<ResourcesAction>().HasKey(p => p.ResourcesActionId);
            //Role
            modelBuilder.Entity<Role>().HasKey(p => p.RoleId);
            modelBuilder.Entity<Role>().HasMany(p => p.RolePermissions);
            modelBuilder.Entity<Role>().HasOne(p => p.ControlResourcesType);
            modelBuilder.Entity<Role>().HasOne(p => p.Organization);
            modelBuilder.Entity<Role>().HasOne(p => p.Application);
            //Schedule
            modelBuilder.Entity<Schedule>().HasKey(p => p.ScheduleId);
            modelBuilder.Entity<Schedule>().HasOne(p => p.ScheduleType);
            modelBuilder.Entity<Schedule>().HasOne(p => p.ScheduleCycle);
            //Staff
            modelBuilder.Entity<Staff>().HasKey(p => p.StaffId);
            modelBuilder.Entity<Staff>().HasOne(p => p.Photo);
            modelBuilder.Entity<Staff>().HasOne(p => p.Organization);
            modelBuilder.Entity<Staff>().HasOne(p => p.PositionType);
            modelBuilder.Entity<Staff>().HasOne(p => p.RankType);
            modelBuilder.Entity<Staff>().HasMany(p => p.Fingerprints);
            modelBuilder.Entity<Staff>().HasOne(p => p.Application);
            //StaffGroup
            modelBuilder.Entity<StaffGroup>().HasKey(p => p.StaffGroupId);
            modelBuilder.Entity<StaffGroup>().HasMany(p => p.Staffs);
            modelBuilder.Entity<StaffGroup>().HasOne(p => p.Organization);
            modelBuilder.Entity<StaffGroup>().HasOne(p => p.Application);
            //SystemOption
            modelBuilder.Entity<SystemOption>().HasKey(p => p.SystemOptionId);
            modelBuilder.Entity<SystemOption>().HasMany(p => p.ApplicationSystemOptions);
            //modelBuilder.Entity<SystemOption>().HasOne(p => p.ParentSystemOption);
            //User
            modelBuilder.Entity<User>().HasKey(p => p.UserId);
            modelBuilder.Entity<User>().HasOne(p => p.Organization);
            modelBuilder.Entity<User>().HasMany(p => p.UserManyToRole);
            modelBuilder.Entity<User>().HasMany(p => p.ControlResources);
            modelBuilder.Entity<User>().HasOne(p => p.Application);
            //UserPhoto
            modelBuilder.Entity<UserPhoto>().HasKey(p => p.UserPhotoId);
        }
    }
}
