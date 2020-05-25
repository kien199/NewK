using Baochi.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Admin.Controllers
{
    public class CommentController : Controller
    {
        // GET: Admin/Comment
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                try
                {
                    var session = new UserLogin();
                    session = (UserLogin)Session[CommonConstants.USER_SESSION]; // lấy từ session
                    var name = new UserDao().GetName(session.userName);
                    ViewBag.session = name;

                    var commments = new CommentDao().GetPagedListComment(page, pageSize);
                    return View(commments);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        public JsonResult AddComment()
        {
            try
            {
                var data = Request.Form;
                var comment = new CommentDao().AddComment(data["content"], data["username"], Convert.ToInt32(data["postId"]));
                return Json(new
                {
                    status = true,
                    data = comment
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult Searching()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                try
                {
                    var data = Request.Form;
                    var comments = new CommentDao().SearchComment(data["search"]);

                    return Json(new
                    {
                        status = true,
                        data = comments
                    }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new
                    {
                        status = false
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Deleting()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                try
                {
                    var data = Request.Form;
                    var check = new CommentDao().DeleteComment(Convert.ToInt32(data["commentId"]));
                    if (check)
                    {
                        return Json(new
                        {
                            status = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            status = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return Json(new
                    {
                        status = false
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}