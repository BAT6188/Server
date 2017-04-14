using System;

namespace PAPS.Model
{
    public class DutyCheckPackageLog
    {

        public Guid DutyCheckPackageId
        {
            get; set;
        }


        public virtual DutyCheckPackage DutyCheckPackage
        {
            get;set;
        }


        public Guid DutyCheckLogId
        {
            get;set;
        }

        public virtual DutyCheckLog DutyCheckLog
        {
            get; set;
        }

    }
}
