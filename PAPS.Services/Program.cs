using Microsoft.AspNetCore.Hosting;
using PAPS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAPS.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DutyGroupScheduleHelper.GetAbsoultedDutyCheckLog(new Model.DutyGroupSchedule());

            //DutyCheckPackageHelper.TestData();

            //DutyCheckPackageHelper.AddTestMonitorySite(100, "一中队");

            //InitUnitDutyCheckSchedule();

            var host = new WebHostBuilder().UseKestrel().UseStartup<Startup>().Build();



            host.Run();


        }


        private static void InitUnitDutyCheckSchedule()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                List<UnitDutyCheckScheduleDetail> list = new List<UnitDutyCheckScheduleDetail>();
                list.Add(new UnitDutyCheckScheduleDetail
                {
                    CheckManId = new Guid("5a23904c-4bb3-4caf-5212-9c58302721d5"),
                    Data = "2017-01-07",
                    TimePeriodId = new Guid("476f089d-cae0-0e10-4560-34f3ff659c9e"),
                    UnitDutyCheckScheduleDetailId = Guid.NewGuid()
                });
                //
                list.Add(new UnitDutyCheckScheduleDetail
                {
                    CheckManId = new Guid("9c1607e9-14a9-8ba1-caae-a211678390c3"),
                    Data = "2017-01-07",
                    TimePeriodId = new Guid("d2bb10b9-73a0-7117-cf5a-ab41f638c12b"),
                    UnitDutyCheckScheduleDetailId = Guid.NewGuid()
                });


                list.Add(new UnitDutyCheckScheduleDetail
                {
                    CheckManId = new Guid("e4cb9413-31d5-984b-6d53-063df03cb97b"),
                    Data = "2017-01-08",
                    TimePeriodId = new Guid("476f089d-cae0-0e10-4560-34f3ff659c9e"),
                    UnitDutyCheckScheduleDetailId = Guid.NewGuid()
                });
                //
                list.Add(new UnitDutyCheckScheduleDetail
                {
                    CheckManId = new Guid("9c1607e9-14a9-8ba1-caae-a211678390c3"),
                    Data = "2017-01-08",
                    TimePeriodId = new Guid("d2bb10b9-73a0-7117-cf5a-ab41f638c12b"),
                    UnitDutyCheckScheduleDetailId = Guid.NewGuid()
                });



                UnitDutyCheckSchedule model = new UnitDutyCheckSchedule
                {
                    EndDate = Convert.ToDateTime("2017-01-15"),
                    ListerId = new Guid("5a23904c-4bb3-4caf-5212-9c58302721d5"),
                    OrganizationId = new Guid("8098e65c-e366-404b-9d09-b8bcea3adca5"),
                    StartDate = Convert.ToDateTime("2017-01-09"),
                    ScheduleId = new Guid("9b94a7d4-9f34-a101-4b35-53db7ca37c2a"),
                    UnitDutyCheckScheduleId = Guid.NewGuid(),
                    UnitDutyCheckScheduleDetails = list
                };

                try
                {
                    db.UnitDutyCheckSchedule.Add(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }

            }
        }
    }
}
