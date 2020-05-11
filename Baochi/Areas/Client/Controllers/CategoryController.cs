using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Client.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Client/Category
        public ActionResult Index(string slug)
        {
            //Lấy thể loại tin hiện lên header
            var cates = new CateDao().GetAllCate();
            ViewBag.cates = cates;

            //Lấy thông tin cate được chọn
            var theloai_id = new CateDao().GetId(slug);
            var cateName = new CateDao().GetName(theloai_id);
            List<baiviet> posts = new List<baiviet>();
            if (theloai_id != -1)
            {
                posts = new PostDao().GetAllPost(theloai_id);
            }
            ViewBag.posts = posts;
            ViewBag.cateName = cateName;
            ViewBag.cateSlug = slug;
            return View();
        }

    }
}