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
            //tang luot xem moi khi vao bai viet
            var view = new PostDao().IncViews(postslug, post.theloai_id.Value);
            //loc ra hot-news
            var cate_posts = new PostDao().GetAllPost(cateId);
            var hotNews = cate_posts.Where(x => x.noibat == true && x.id!= post.id).OrderByDescending(x => x.soluotxem).Take(6).ToList(); //100 25 20 15
            foreach (var hotNew in hotNews)
            {
                hotNew.theloaitin = new CateDao().GetSingleCate(hotNew.theloai_id.Value);
            }
            ViewBag.hotNews = hotNews;
            var relatedNews = cate_posts.Where(x => x.id != post.id).OrderByDescending(x => x.ngaytao).Take(6).ToList();

            ViewBag.relatedNews = relatedNews;

            var comments = new CommentDao().GetAllComment(post.id);
            ViewBag.comments = comments;

            return View();
        }
    }
}