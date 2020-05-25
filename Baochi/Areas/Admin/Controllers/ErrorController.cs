using Baochi.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Admin.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Admin/Error
        public ActionResult Index()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION]; // lấy từ session
                var name = new UserDao().GetName(session.userName);
                ViewBag.session = name;
            }

            return View();
        }
    }
}