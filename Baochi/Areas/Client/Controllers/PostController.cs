using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Client.Controllers
{
    public class PostController : Controller 
    {
        public ActionResult Index(string cateslug, string postslug)
        {
            //Lấy thể loại tin hiện lên header
            var cates = new CateDao().GetAllCate();
            ViewBag.cates = cates;

            //Lấy thông tin cate được chọn
            int cateId = new CateDao().GetId(cateslug);
            baiviet post = new PostDao().GetSinglePost(postslug, cateId);
            ViewBag.post = post;

            string cateName = new CateDao().GetName(post.theloai_id.Value);
            ViewBag.cateName = cateName;
            string cateSlug = new CateDao().GetSlug(post.theloai_id.Value);
            ViewBag.cateSlug = cateSlug;

            var view = new PostDao().IncViews(postslug, post.theloai_id.Value);

            return View();
        }
    }
}