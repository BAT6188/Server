using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.Model;
using Surveillance.ViewModel;
using Surveillance.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resources.Services
{
    /// <summary>
    /// 视频场景预案轮巡
    /// </summary>
    [Route("Resources/[controller]")]
    public class VideoRoundSceneController : Controller
    {
        ILogger<VideoRoundSceneController> _logger;

        public VideoRoundSceneController(ILogger<VideoRoundSceneController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] VideoRoundScene sence)
        {
            if (sence == null)
                return BadRequest("VideoRoundScene object can not be null!");
            if (ExistsName(sence.VideoRoundSceneName, sence.VideoRoundSceneId))
                return BadRequest("VideoRoundScene name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    db.VideoRoundScene.Add(sence);
                    db.SaveChanges();
                    return CreatedAtAction("", sence);
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError("添加视频轮巡场景异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError("添加视频轮巡场景异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] VideoRoundScene sence)
        {
            if (sence == null)
                return BadRequest("VideoRoundScene object can not be null!");
            if (ExistsName(sence.VideoRoundSceneName, sence.VideoRoundSceneId))
                return BadRequest("VideoRoundScene name has been used!");
            using (var db = new AllInOneContext.AllInOneContext())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        //先删除，再更新
                        var dbSence = db.VideoRoundScene.Include(t => t.VideoRoundSections).ThenInclude(t => t.RoundMonitorySiteSettings)
                            .FirstOrDefault(t => t.VideoRoundSceneId.Equals(sence.VideoRoundSceneId));
                        if (dbSence.VideoRoundSections != null)
                        {
                            dbSence.VideoRoundSections.ForEach(t =>
                            db.Set<VideoRoundMonitorySiteSetting>().RemoveRange(t.RoundMonitorySiteSettings));
                            db.Set<VideoRoundSection>().RemoveRange(dbSence.VideoRoundSections);
                        }
                        db.SaveChanges();

                        dbSence.ModifiedBy = sence.ModifiedBy;
                        dbSence.Modified = DateTime.Now;
                        dbSence.VideoRoundSceneName = sence.VideoRoundSceneName;
                        dbSence.VideoRoundSections = sence.VideoRoundSections;
                        db.VideoRoundScene.Update(dbSence);

                        db.SaveChanges();

                        tran.Commit();
                        return NoContent();
                    }
                    catch (DbUpdateException dbEx)
                    {
                        tran.Rollback();
                        _logger.LogError("更新视频轮巡场景异常：Message:{0}\r\n,StackTrace:{1}", dbEx.Message, dbEx.StackTrace);
                        return BadRequest(new ApplicationException { ErrorCode = "DBUpdate", ErrorMessage = "数据保存异常:" + dbEx.Message });
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        _logger.LogError("更新视频轮巡场景异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                        return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                try
                {
                    VideoRoundScene deleteObj = db.VideoRoundScene.FirstOrDefault(t => t.VideoRoundSceneId.Equals(id));
                    if (deleteObj == null)
                        return NotFound();
                    db.VideoRoundScene.Remove(deleteObj);
                    db.SaveChanges();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError("删除轮巡场景异常：Message:{0}\r\n,StackTrace:{1}", ex.Message, ex.StackTrace);
                    return BadRequest(new ApplicationException { ErrorCode = "Unknown", ErrorMessage = ex.Message });
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                var groups = db.VideoRoundScene.Where(t => t.VideoRoundSceneId.Equals(id)).
                    Include(t => t.VideoRoundSections).ThenInclude(t => t.RoundMonitorySiteSettings).ThenInclude(t => t.MonitorySite);
                VideoRoundScene cg = groups.FirstOrDefault(t => t.VideoRoundSceneId.Equals(id));
                if (cg == null)
                    return NotFound();
                return new OkObjectResult(cg);
            }
        }

        /// <summary>
        /// 根据场景应用类型获取
        /// </summary>
        /// <param name="systemOptionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/VideoRoundScene/systemOptionId={systemOptionId}")]
        public IEnumerable<VideoRoundScene> GetByFlag(Guid systemOptionId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.VideoRoundScene.Include(t => t.VideoRoundSections).ThenInclude(p => p.RoundMonitorySiteSettings).ThenInclude(t=>t.MonitorySite)
                    .Where(t => t.VideoRoundSceneFlagId.Equals(systemOptionId)).ToList();
            }
        }

        [HttpGet]
        public IEnumerable<VideoRoundScene> GetAll()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.VideoRoundScene.Include(p=>p.VideoRoundSections).ToList();
            }
        }

        //private bool Exists(Guid deviceid)
        //{
        //    using (var db = new AllInOneContext.AllInOneContext())
        //    {
        //        return db.VideoRoundScene.Where(t => t.VideoRoundSceneId.Equals(deviceid)).Count() > 0;
        //    }
        //}

        /// <summary>
        /// 判断场景预案名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="senceId"></param>
        /// <returns></returns>
        public bool ExistsName(string name, Guid senceId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                return db.VideoRoundScene.Where(t => t.VideoRoundSceneName.Equals(name)
                    && !t.VideoRoundSceneId.Equals(senceId)).Count() > 0;
            }
        }

        /// <summary>
        /// 根据场景应用获取轮巡视图
        /// </summary>
        /// <param name="systemOptionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/VideoRoundScene/View/systemOptionId={systemOptionId}")]
        public IEnumerable<VideoRoundSceneView> GetViewByFlag(Guid systemOptionId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                List<VideoRoundSceneView> sceneViews = new List<VideoRoundSceneView>();
                //先获取轮巡场景
                var scenes = db.VideoRoundScene.Include(t => t.VideoRoundSections).ThenInclude(t => t.TemplateLayout).ThenInclude(t => t.Cells).
                   Include(t => t.VideoRoundSections).ThenInclude(t => t.RoundMonitorySiteSettings).ThenInclude(t => t.MonitorySite).ThenInclude(t => t.Camera).
                   Where(t => t.VideoRoundSceneFlagId.Equals(systemOptionId)).ToList();
                //获取摄像机信息
                var cameras = GetAllCameraView(db);

                scenes.ForEach(t =>
                {
                    //按照场景视图定义封装数据
                    List<VideoRoundSectionView> sectionViews = new List<VideoRoundSectionView>();
                    t.VideoRoundSections.ForEach(f =>
                    {
                        //场景片段的摄像机
                        var sectionCams = (from ms in (f.RoundMonitorySiteSettings ?? new List<VideoRoundMonitorySiteSetting>())
                                           join cam in cameras on ms.MonitorySite.CameraId equals cam.CameraId
                                           // select cam).ToList();
                                           select new
                                           {
                                               PlayInfo = new RealPlayParam()
                                               {
                                                   CameraView = cam,
                                                   StreamType = (VideoStream)ms.VideoStream,
                                               },
                                               MonitorView = new MonitorView()
                                               {
                                                   Monitor = ms.Monitor,
                                                   SubView = ms.SubView
                                               }
                                           }).ToList();
                        sectionViews.Add(new VideoRoundSectionView()
                        {
                            PlayInfoList = sectionCams.Select(a=>a.PlayInfo).ToList(),
                            Monitors = sectionCams.Select(a=>a.MonitorView).ToList(),
                            RoundInterval = f.RoundInterval,
                            TemplateLayout = f.TemplateLayout
                        });
                    });

                    sceneViews.Add(new VideoRoundSceneView()
                    {
                        VideoRoundSceneName = t.VideoRoundSceneName,
                        VideoRoundSections = sectionViews
                    });
                });
                return sceneViews;
            }
        }

        /// <summary>
        /// 获取默认的场景 ，暂时保留。。。。。。
        /// </summary>
        /// <param name="systemOptionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/VideoRoundScene/View/Default")]
        public VideoRoundSceneView GetDeaultRoundScene()
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                VideoRoundSceneView sceneView = null;
                //先获取轮巡场景
                var scene = db.VideoRoundScene.Include(t => t.VideoRoundSections).ThenInclude(t => t.TemplateLayout).ThenInclude(t=>t.Cells).
                   Include(t => t.VideoRoundSections).ThenInclude(t => t.RoundMonitorySiteSettings).ThenInclude(t => t.MonitorySite).ThenInclude(t => t.Camera).
                   FirstOrDefault();
                //获取摄像机信息
                var cameras = GetAllCameraView(db);


                //按照场景视图定义封装数据
                List<VideoRoundSectionView> sectionViews = new List<VideoRoundSectionView>();
                scene.VideoRoundSections.ForEach(f =>
                {
                        //场景片段的摄像机
                        var sectionCams = (from ms in (f.RoundMonitorySiteSettings ?? new List<VideoRoundMonitorySiteSetting>())
                                       join cam in cameras on ms.MonitorySite.Camera.CameraId equals cam.CameraId
                                       select new
                                       {
                                           PlayInfo = new RealPlayParam()
                                           {
                                               CameraView = cam,
                                               StreamType = (VideoStream)ms.VideoStream,
                                           },
                                           MonitorView = new MonitorView()
                                           {
                                               Monitor = ms.Monitor,
                                               SubView = ms.SubView
                                           }
                                       }).ToList();
                        //select cam).ToList();
                        sectionViews.Add(new VideoRoundSectionView()
                    {
                        PlayInfoList = sectionCams.Select(a=>a.PlayInfo).ToList(),
                        Monitors = sectionCams.Select(a=>a.MonitorView).ToList(),
                        RoundInterval = f.RoundInterval,
                        TemplateLayout = f.TemplateLayout
                    });
                });

                sceneView = new VideoRoundSceneView()
                {
                    VideoRoundSceneName = scene.VideoRoundSceneName,
                    VideoRoundSections = sectionViews
                };
                return sceneView;
            }
        }

        /// <summary>
        /// 获取默认的场景
        /// </summary>
        /// <param name="systemOptionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/Resources/VideoRoundScene/View/videoRoundSceneId={videoRoundSceneId}")]
        public VideoRoundSceneView GetVideoRoundSceneViewById(Guid videoRoundSceneId)
        {
            using (var db = new AllInOneContext.AllInOneContext())
            {
                VideoRoundSceneView sceneView = null;
                //先获取轮巡场景
                var scene = db.VideoRoundScene.Include(t => t.VideoRoundSections).ThenInclude(t => t.TemplateLayout).ThenInclude(t => t.Cells).
                   Include(t => t.VideoRoundSections).ThenInclude(t => t.RoundMonitorySiteSettings).ThenInclude(t => t.MonitorySite).ThenInclude(t => t.Camera).
                   FirstOrDefault(t => t.VideoRoundSceneId.Equals(videoRoundSceneId));
                //获取摄像机信息
                var cameras = GetAllCameraView(db);


                //按照场景视图定义封装数据
                List<VideoRoundSectionView> sectionViews = new List<VideoRoundSectionView>();
                scene.VideoRoundSections.ForEach(f =>
                {
                    //场景片段的摄像机
                    var sectionCams = (from ms in (f.RoundMonitorySiteSettings ?? new List<VideoRoundMonitorySiteSetting>())
                                       join cam in cameras on ms.MonitorySite.CameraId equals cam.CameraId
                                       select new
                                       {
                                           PlayInfo = new RealPlayParam()
                                           {
                                               CameraView = cam,
                                               StreamType = (VideoStream)ms.VideoStream,
                                           },
                                           MonitorView = new MonitorView()
                                           {
                                               Monitor = ms.Monitor,
                                               SubView = ms.SubView
                                           }
                                       }).ToList();
                    //select cam).ToList();
                    sectionViews.Add(new VideoRoundSectionView()
                    {
                        PlayInfoList = sectionCams.Select(a => a.PlayInfo).ToList(),
                        Monitors = sectionCams.Select(a => a.MonitorView).ToList(),
                        RoundInterval = f.RoundInterval,
                        TemplateLayout = f.TemplateLayout
                    });
                });

                sceneView = new VideoRoundSceneView()
                {
                    VideoRoundSceneName = scene.VideoRoundSceneName,
                    VideoRoundSections = sectionViews
                };
                return sceneView;
            }
        }

        private List<CameraView> GetAllCameraView(AllInOneContext.AllInOneContext db)
        {
            //获取摄像机信息
            var cameras = (from cam in db.Set<Camera>().Include(t => t.VideoForward).Include(t => t.IPDevice).ThenInclude(t=>t.DeviceType)
                           join encoder in db.Encoder.Include(t => t.DeviceInfo).Include(t => t.EncoderType).ToList() on cam.EncoderId equals encoder.EncoderId
                           select new CameraView()
                           {
                               CameraId = cam.CameraId,
                               IPDeviceId = cam.IPDeviceId,
                               EncoderChannel = cam.EncoderChannel,
                               DeviceType = cam.IPDevice.DeviceType.SystemOptionName,
                               CameraName = cam.IPDevice.IPDeviceName,
                               CameraNo = cam.CameraNo,
                               EncoderInfo = new EncoderInfo()
                               {
                                   EncoderType = cam.Encoder.EncoderType.EncoderCode,
                                   EndPoints = cam.Encoder.DeviceInfo.EndPoints,
                                   Password = cam.Encoder.DeviceInfo.Password,
                                   User = cam.Encoder.DeviceInfo.UserName
                               },
                               VideoForwardInfo = new Surveillance.ViewModel.ServiceInfo()
                               {
                                   EndPoints = cam.VideoForward.EndPoints,
                                   User = cam.VideoForward.Username,
                                   Password = cam.VideoForward.Password
                               }
                           }).ToList();
            return cameras;
        }

    }
}
