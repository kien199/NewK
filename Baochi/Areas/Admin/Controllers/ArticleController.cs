using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Admin/Article
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Adding()
        {
            return View();
        }
        public ActionResult Editing()
        {
            return View();
        }
    }
}