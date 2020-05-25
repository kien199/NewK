using Baochi.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION]; // lấy từ session
                var name = new UserDao().GetName(session.userName);
                ViewBag.session = name;
            }

            var user_count = new UserDao().CountUser();
            var post_count = new PostDao().CountPost();
            var cate_count = new CateDao().CountCate();
            var comment_count = new CommentDao().CountComment();

            ViewBag.user_count = user_count;
            ViewBag.post_count = post_count;
            ViewBag.cate_count = cate_count;
            ViewBag.comment_count = comment_count;
            return View();
        }
    }
}