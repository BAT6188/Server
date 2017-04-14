using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    /// <summary>
    /// 人员
    /// </summary>
    public class Staff
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid StaffId
        {
            get; set;
        }

        /// <summary>
        /// 人员名称
        /// </summary>
        public string StaffName
        {
            get; set;
        }

        /// <summary>
        /// 性别
        /// </summary>
        public Guid SexId
        {
            get; set;
        }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual SystemOption Sex
        {
            get;set;
        }

        /// <summary>
        /// 人员编号
        /// </summary>
        public int StaffCode
        {
            get; set;
        }

        /// <summary>
        /// 照片
        /// </summary>
        public Guid? PhotoId
        {
            get; set;
        }

        /// <summary>
        /// 照片
        /// </summary>
        public virtual UserPhoto Photo
        {
            get;set;
        }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public Guid OrganizationId
        {
            get; set;
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        public virtual Organization Organization
        {
            get;set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;set;
        }

        /// <summary>
        /// 职务类型Id
        /// </summary>
        public Guid? PositionTypeId
        {
            get;set;
        }

        /// <summary>
        /// 职务类型
        /// </summary>
        public virtual SystemOption PositionType
        {
            get;set;
        }


        /// <summary>
        /// 等级类型Id
        /// </summary>
        public Guid? RankTypeId
        {
            get;set;
        }

        /// <summary>
        /// 等级类型
        /// </summary>
        public virtual SystemOption RankType
        {
            get;set;
        }

        /// <summary>
        /// 执勤类型ID
        /// </summary>
        public Guid? DutyCheckTypeId
        {
            get;set;
        }


        /// <summary>
        /// 执勤类型
        /// </summary>
        public virtual SystemOption DutyCheckType
        {
            get;set;
        }

        /// <summary>
        /// 指纹集合
        /// </summary>
        public List<Fingerprint> Fingerprints
        {
            get; set;
        }

        /// <summary>
        /// 应用Id
        /// </summary>
        public Guid ApplicationId
        {
            get; set;
        }

        /// <summary>
        /// 应用
        /// </summary>
        public virtual Application Application
        {
            get; set;
        }

        /// <summary>
        /// 文化程度ID
        /// </summary>
        public Guid? DegreeOfEducationId
        {
            get;set;
        }

        /// <summary>
        /// 文化程度
        /// </summary>
        public virtual SystemOption DegreeOfEducation
        {
            get;set;
        }


        /// <summary>
        /// 身体状况ID
        /// </summary>
        public Guid? PhysiclalStatusId
        {
            get;set;
        }


        /// <summary>
        /// 身体状况
        /// </summary>
        public virtual SystemOption PhysiclalStatus
        {
            get;set;
        }


        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace
        {
            get;set;
        }

        private double? _stature;
        /// <summary>
        /// 身高（CM）
        /// </summary>
        public double? Stature
        {
            get { return _stature; }
            set
            {
                if (value == null)
                    _stature = 0;
                else
                {
                    _stature =value;
                }
            }
        }

        private DateTime? _enrolTime;
        /// <summary>
        /// 入伍时间
        /// </summary>
        public DateTime? EnrolTime
        {
            get { return _enrolTime; }
            set
            {
                if (value == null)
                    _enrolTime = DateTime.MinValue;
                else
                {
                    _enrolTime = value;
                }
            }
        }


        /// <summary>
        /// 个人手机
        /// </summary>
        public string Phone
        {
            get;set;
        }


        /// <summary>
        /// 宗教信仰
        /// </summary>
        public string ReligiousBelief
        {
            get;set;
        }


        /// <summary>
        /// 工作性质ID
        /// </summary>
        public Guid? WorkingPropertyId
        {
            get;set;
        }

        /// <summary>
        /// 工作性质
        /// </summary>
        public virtual SystemOption WorkingProperty
        {
            get;set;
        }


        /// <summary>
        /// 民族ID
        /// </summary>
        public Guid? NationId
        {
            get;set;
        }


        /// <summary>
        /// 民族
        /// </summary>
        public virtual SystemOption Nation
        {
            get;set;
        }


        /// <summary>
        /// 政治面貌ID
        /// </summary>
        public Guid? PoliticalLandscapeId
        {
            get;set;
        }


        /// <summary>
        /// 政治面貌
        /// </summary>
        public virtual SystemOption PoliticalLandscape
        {
            get;set;
        }

        private DateTime? _dateOfBirth;
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set {
                if (value == null)
                    _dateOfBirth = DateTime.MinValue;
                else
                {
                    _dateOfBirth = value ;
                }
            }
        }


        /// <summary>
        /// 婚姻状态ID
        /// </summary>
        public Guid? MaritalStatusId
        {
            get;set;
        }


        /// <summary>
        /// 婚姻状态
        /// </summary>
        public virtual SystemOption MaritalStatus
        {
            get;set;
        }



        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalZipCode
        {
            get;set;
        }

        private DateTime? _partyTime;
        /// <summary>
        /// 入党（团）时间
        /// </summary>
        public DateTime? PartyTime
        {
            get { return _partyTime; }
            set
            {
                if (value == null)
                    _partyTime = DateTime.MinValue;
                else
                {
                    _partyTime =value;
                }
            }
        }

        /// <summary>
        /// 入伍所在地
        /// </summary>
        public string EnrolAddress
        {
            get;set;
        }

        /// <summary>
        /// 家庭联系电话
        /// </summary>
        public string FamilyPhone
        {
            get;set;
        }


        /// <summary>
        /// 所属班排
        /// </summary>
        public string ClassRow
        {
            get;set;
        }

        /// <summary>
        /// 在位情况ID
        /// </summary>
        public Guid? ReignStatusId
        {
            get;set;
        }


        /// <summary>
        /// 在位情况
        /// </summary>
        public virtual SystemOption ReignStatus
        {
            get;set;
        }
    }
}