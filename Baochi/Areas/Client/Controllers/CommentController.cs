using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Client.Controllers
{
    public class CommentController : Controller
    {
        // GET: Client/Comment
        //public ActionResult Index()
        //{
        //    return View();
        //}
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
    }
}