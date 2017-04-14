using AlarmAndPlan.Model;
using HttpClientEx;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace TaskServer
{
    [Route("Task/[controller]")]
    /// <summary>
    /// 报警联动
    /// </summary>
    public class PlanActionController : Controller
    {
        ILogger<PlanActionController> _logger;

        public PlanActionController(ILogger<PlanActionController> logger)
        {
            _logger = logger;
        }

        ///// <summary>
        ///// 执行手动或报警触发的预案
        ///// </summary>
        ///// <param name="planId"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("~/Task/PlanAction/Start/planId={planId}")]
        //public IActionResult ManualStart(Guid planId)
        //{
        //    try
        //    {
        //        _logger.LogInformation("启动预案{0}Begin......", planId);
        //        string url = string.Format("{0}/Plan/plan/{1}", GlobalSetting.AppServerBaseUrl, planId);
        //        Plan plan = HttpClientHelper.GetOne<Plan>(url);
        //        if (plan != null)
        //        {
        //            TimePeriod tp = new TimePeriod()
        //            {
        //                StartTime = DateTime.Now,
        //                EndTime = DateTime.MaxValue
        //            };
        //            PlanTaskWrapper task = new PlanTaskWrapper()
        //            {
        //                Plan = plan,
        //                RunTimePeriod = tp,
        //                TaskId = Guid.NewGuid(),
        //            };
        //            PlanExecutor executor = new PlanExecutor(task);
        //            PlanTaskScheduler.Instance.AddPlanExecutorToCache(executor);
        //            executor.Start();
        //            _logger.LogInformation("启动预案{0}End......", planId);
        //            return Ok();
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("启动预案异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
        //        return BadRequest(ex.Message);
        //    }
        //}

        /// <summary>
        /// 执行手动或报警触发的预案
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/Task/PlanAction/Start")]
        public IActionResult ManualStart([FromBody]Plan plan)
        {
            try
            {
                if (plan != null)
                {
                    _logger.LogInformation("启动预案{0}Begin......", plan.PlanId);
                    //if (PlanTaskScheduler.Instance.PlanIsRunning(plan.PlanId))  //待定，运行的任务是否要再启动
                    //{
                        //_logger.LogInformation("预案{0}已启动，取消执行......", plan.PlanId);
                        //return Ok("预案已启动");
                    //}
                    TimePeriod tp = new TimePeriod()
                    {
                        StartTime = DateTime.Now,
                        EndTime = DateTime.MaxValue
                    };
                    PlanTaskWrapper task = new PlanTaskWrapper()
                    {
                        Plan = plan,
                        RunTimePeriod = tp,
                        TaskId = Guid.NewGuid(),
                    };
                    PlanExecutor executor = new PlanExecutor(task);
                    PlanTaskScheduler.Instance.AddPlanExecutorToCache(executor);
                    executor.Start();
                    _logger.LogInformation("启动预案{0}End......", plan.PlanId);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("启动预案异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 停止手动或报警触发的预案
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("~/Task/PlanAction/Stop/planId={planId}")]
        public IActionResult ManualStop(Guid planId)
        {
            //从启动的预案列表查找
            _logger.LogInformation("停止预案{0}", planId);
            PlanTaskScheduler.Instance.RemovePlanExecutorFromCache(planId);
            return Ok();
        }

        [HttpPost]
        public IActionResult Add([FromBody]TimerTask task)
        {
            try
            {
                _logger.LogInformation("接收到任务....");
                PlanTaskScheduler.Instance.AddPlanTask(task);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("新增定时任务异常：Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            _logger.LogInformation("删除预案{0}",id);
            PlanTaskScheduler.Instance.RemovePlanTask(id);
            return Ok();
        }

        [HttpDelete]
        [Route("~/Task/PlanAction/Reset")]
        public IActionResult Reset()
        {
            _logger.LogInformation("复位报警预案 begin");
            PlanTaskScheduler.Instance.Reset();
            _logger.LogInformation("复位报警预案 end");
            return Ok();
        }

    }
}
