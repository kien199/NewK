using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Client.Controllers
{
    public class SearchController : Controller
    {
        // GET: Client/Search
        public ActionResult Index(string search)
        {
            var data = search;
            //Lấy thể loại tin hiện lên header
            var cates = new CateDao().GetAllCate();
            ViewBag.cates = cates;

            ViewBag.searchString = search;

            var posts = new PostDao().SearchPost(search);
            ViewBag.posts = posts;

            return View();
        }
    }
}