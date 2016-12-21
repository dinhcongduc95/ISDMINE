using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Serialization;
using MTBT.Web.Business;
using MTBT.Web.Common;
using MTBT.Web.Models;
using MTBT.Web.ViewModels;
using Webdiyer.WebControls.Mvc;

namespace MTBT.Web.Controllers
{
    public class EntryTestController : ControllerExt
    {
        private readonly MTBTWebDbContext _db;

        public EntryTestController()
        {
            if (_db == null)
            {
                _db = new MTBTWebDbContext();
            }
        }

        [AccessDeniedAuthorize(Roles = Constants.RoleNames.ADMINISTRATOR)]
        public ViewResult Index(string id, string psize = "20")
        {
            int pageSize = Convert.ToInt32(psize);
            int iId = Convert.ToInt32(id);
            var items = _db.EntryTestResults.OrderBy(m => m.Id).ToPagedList(iId, pageSize);

            return View(items);
        }

        public ActionResult UserTest(string id)
        {
            int idCourse = 0;
            if (string.IsNullOrEmpty(id) || !int.TryParse(id, out idCourse) ||
                (!string.IsNullOrEmpty(id) && int.Parse(id) <= 0))
                return RedirectToAction("Index", "Home", new {strMessage = Properties.Resources.ERR_PROCESSING});

            ViewBag.ServerIds = string.Empty;
            ViewBag.Message = string.Empty;
            ViewBag.CourseName = string.Empty;
            if (idCourse > 0)
            {

                var model = GetQuestionsWithCourseId(idCourse);
                ViewBag.CourseId = idCourse;
                if (model.questions.Any())
                {
                    ViewBag.ServerIds =
                        Utilities.ConvertIntListToString(model.questions.OrderBy(m => m.Id).Select(m => m.Id).ToList());
                }
                else
                {
                    ViewBag.Message = "Không có câu hỏi kiểm tra nào";
                    ViewBag.Code = "100";
                }
                var account = _db.Accounts.SingleOrDefault(m => m.LoginName.Equals(User.Identity.Name));
                if (account != null)
                {
                    ViewBag.UserId = account.ID;
                    var course = _db.Courses.SingleOrDefault(m => m.ID == idCourse);
                    if (_db.EntryTestResults.Any(m => m.CoureId == idCourse && m.UserId == account.ID && m.Mark > 50))
                    {
                        ViewBag.Message =
                            "Bạn đã hoàn thành bài kiểm tra và đã đạt đủ điểm vượt qua. Bạn không cần phải làm bài kiểm tra này nữa";
                        ViewBag.Code = "100";
                    }
                    if (course != null)
                    {
                        ViewBag.CourseName = course.Name;
                        ViewBag.TimeLimit = course.TimeLimit;
                    }
                }
                else
                {
                    ViewBag.Message = "Bạn cần đăng nhập để có thể kiểm tra";
                    ViewBag.Code = "100";
                }
                return View("UserTest", model);
            }
            return View();
        }

        private Questions GetQuestionsWithCourseId(long courseId)
        {
            try
            {
                if (courseId <= 0) return null;

                var course = _db.Courses.Find(courseId);
                if (course != null)
                {
                    string questionXml = _db.Courses.Find(courseId).ContentXml;
                    if (string.IsNullOrEmpty(questionXml))
                    {
                        ViewBag.Message = "Chưa có entry test question cho test này ";
                        Utilities.WriteToLog(
                            new Exception("Line 56 - GetData - Chưa có question test cho khoá học " + courseId));
                    }
                    using (
                        var reader =
                            new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(questionXml ?? string.Empty))))
                    {
                        var serializer = new XmlSerializer(typeof (Questions));
                        string strReader = reader.ReadToEnd();
                        var deserialized = serializer.Deserialize(new StringReader(strReader));
                        return (Questions) deserialized;
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.WriteToLog(ex);
            }
            return null;
        }

        public JsonResult SendTestResult(string strCourseId, string ids, string serverIds, string userId)
        {
            try
            {
                if (int.Parse(strCourseId) <= 0 || int.Parse(userId) <= 0 || !ids.Split(',').Any() || !serverIds.Any())
                    return
                        Json(
                            new
                            {
                                number = (int) Enumeric.ErrorsLog.ERR_UNKNOW,
                                mess = Properties.Resources.ERR_VALIDATE_PARAM
                            });

                long idCourse = long.Parse(strCourseId);
                var course = _db.Courses.SingleOrDefault(m => m.ID == idCourse);
                if (course == null)
                    return
                        Json(
                            new
                            {
                                number = (int) Enumeric.ErrorsLog.ERR_UNKNOW,
                                mess = Properties.Resources.ERR_PROCESSING
                            });

                var account = MemberManager.GetUserByUserId(userId);
                if (!User.Identity.Name.Equals(account.LoginName))
                    return
                        Json(
                            new
                            {
                                number = (int) Enumeric.ErrorsLog.ERR_UNKNOW,
                                mess = Properties.Resources.ERR_PROCESSING
                            });

                var idObjs = Utilities.ConvertStringToInts(serverIds);
                if (!idObjs.Any())
                    return
                        Json(new {number = (int) Enumeric.ErrorsLog.ERR_UNKNOW, mess = Properties.Resources.ERR_UNKNOW});

                if (string.IsNullOrEmpty(ids))
                    return Json(new {number = (int) Enumeric.ErrorsLog.SUS_PROCESS_OK, mess = "Điểm số đạt được: 0"});

                var clientIds = Utilities.ConvertToLisInt(ids.Split(',').ToList());
                var result = idObjs.Where(clientIds.Contains);
                var rs = result as int[] ?? result.ToArray();
                if (!rs.Any())
                    return Json(new {number = (int) Enumeric.ErrorsLog.SUS_PROCESS_OK, mess = "Điểm số đạt được: 0"});

                int mark = rs.Count()*10;
                if (mark >= course.PassedMark)
                {
                    _db.EntryTestResults.Add(new EntryTestResult
                    {
                        CoureId = course.ID,
                        Mark = mark,
                        UserId = account.ID,
                        IsPass = true
                    });
                }
                else
                {
                    _db.EntryTestResults.Add(new EntryTestResult
                    {
                        CoureId = course.ID,
                        Mark = mark,
                        UserId = account.ID,
                        IsPass = false
                    });
                }

                return Json(_db.SaveChanges() > 0
                    ? new
                    {
                        number = (int) Enumeric.ErrorsLog.SUS_PROCESS_OK,
                        mess = mark.ToString() + "/" + idObjs.Count*10
                    }
                    : new {number = (int) Enumeric.ErrorsLog.ERR_UNKNOW, mess = Properties.Resources.ERR_PROCESSING});


            }
            catch (Exception ex)
            {
                Utilities.WriteToLog(ex);
            }
            return Json(new {number = (int) Enumeric.ErrorsLog.ERR_UNKNOW, mess = Properties.Resources.ERR_UNKNOW});
        }

        public JsonResult AjaxDelete(FormCollection data)
        {
            var ids = Convert.ToString(data["Ids"]).Split(',');
            foreach (var id in ids)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var itemId = Convert.ToInt32(id);
                    var item = _db.EntryTestResults.Find(itemId);
                    if (item != null)
                    {
                        _db.EntryTestResults.Remove(item);
                    }
                }
            }
            _db.SaveChanges();
            return Json(new {Message = "Delete succeed!"});
        }
    }

}