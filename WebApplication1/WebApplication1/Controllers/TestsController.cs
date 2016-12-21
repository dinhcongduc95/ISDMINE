using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using Microsoft.AspNet.Identity;
using MTBT.Web.Common;
using MTBT.Web.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TestsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TestsController()
        {
            if (_db == null)
            {
                _db = new ApplicationDbContext();
            }
        }


        public ViewResult Index(string id, string psize = "20")
        {
            var pageSize = Convert.ToInt32(psize);
            var iId = Convert.ToInt32(id);
            var items = _db.TestResults.OrderBy(m => m.TestResultId).ToList();

            return View(items);
        }

        public ActionResult UserTest(int id)
        {

            ViewBag.ServerIds = string.Empty;
            ViewBag.Message = string.Empty;
            ViewBag.CourseName = string.Empty;
            if (id > 0)
            {
                var model = GetQuestionsWithTestId(id);
                ViewBag.TestId = id;
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
                var userId = User.Identity.GetUserId();
                var account = _db.Users.Find(userId);
                if (account != null)
                {
                    ViewBag.UserId = account.Id;
                    var test = _db.Tests.SingleOrDefault(m => m.TestId == id);

                    if (test != null)
                    {
                        ViewBag.CourseName = test.Name;
                        ViewBag.TimeLimit = test.TimeLimit;
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

        private Questions GetQuestionsWithTestId(long testId)
        {
            if (testId <= 0) return null;

            var test = _db.Tests.Find(testId);
            if (test != null)
            {
                var questionXml = test.XmlContent;

                using (
                    var reader =
                        new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(questionXml ?? string.Empty))))
                {
                    var serializer = new XmlSerializer(typeof(Questions));
                    var strReader = reader.ReadToEnd();
                    var deserialized = serializer.Deserialize(new StringReader(strReader));
                    return (Questions) deserialized;
                }
            }

            return null;
        }

        public JsonResult SendTestResult(string testId, string ids, string serverIds)
        {
            var test = _db.Tests.Find(int.Parse(testId));
            var userId = User.Identity.GetUserId();
            var user = _db.Users.Find(userId);

            var idObjs = Utilities.ConvertStringToInts(serverIds);
            if (!idObjs.Any())
                return
                    Json(new {number = (int) Enumeric.ErrorsLog.ERR_UNKNOW, mess = "Lỗi"});

            if (string.IsNullOrEmpty(ids))
                return Json(new {number = (int) Enumeric.ErrorsLog.SUS_PROCESS_OK, mess = "Điểm số đạt được: 0"});

            var clientIds = Utilities.ConvertToLisInt(ids.Split(',').ToList());
            var result = idObjs.Where(clientIds.Contains);
            var rs = result as int[] ?? result.ToArray();
            if (!rs.Any())
                return Json(new {number = (int) Enumeric.ErrorsLog.SUS_PROCESS_OK, mess = "Điểm số đạt được: 0"});

            var mark = rs.Count()*10;
            if (mark >= test.PassMark)
            {
                _db.TestResults.Add(new TestResult
                {
                    User = user,
                    Mark = mark,
                    Test = test,
                    Passed = true
                });
            }
            else
            {
                _db.TestResults.Add(new TestResult
                {
                    User = user,
                    Mark = mark,
                    Test = test,
                    Passed = true
                });
            }

            return Json(_db.SaveChanges() > 0
                ? new
                {
                    number = (int) Enumeric.ErrorsLog.SUS_PROCESS_OK,
                    mess = mark + "/" + idObjs.Count*10
                }
                : new {number = (int) Enumeric.ErrorsLog.ERR_UNKNOW, mess = "Lỗi"});
        }

        public JsonResult AjaxDelete(FormCollection data)
        {
            var ids = Convert.ToString(data["Ids"]).Split(',');
            foreach (var id in ids)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var itemId = Convert.ToInt32(id);
                    var item = _db.TestResults.Find(itemId);
                    if (item != null)
                    {
                        _db.TestResults.Remove(item);
                    }
                }
            }
            _db.SaveChanges();
            return Json(new {Message = "Delete succeed!"});
        }
    }
}