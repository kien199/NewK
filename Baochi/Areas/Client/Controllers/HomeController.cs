using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        // GET: Client/Home
        public ActionResult Index()
        {
            //Chạy dòng này khi muốn đưa crawl dữ liệu vào db
            //new CrawlerController().Crawler("https://gamek.vn/");

            //Lấy thể loại tin hiện lên header
            var cates = new CateDao().GetAllCate();
            ViewBag.cates = cates;
            //Lọc bài viết hiện ra
            List<baiviet> posts = new List<baiviet>();
            List<baiviet> all_posts = new List<baiviet>();
            foreach (var cate in cates)
            {
                var cate_posts = new PostDao().GetAllPost(cate.id);
                posts.AddRange(cate_posts.Where(x => x.noibat == true).OrderByDescending(x => x.ngaytao).Take(3).ToList());
                all_posts.AddRange(cate_posts);
            }
            ViewBag.posts = posts;
            var hotNews = all_posts.Where(x => x.noibat == true).OrderByDescending(x => x.soluotxem).Take(4).ToList(); //100 25 20 15
            foreach(var hotNew in hotNews)
            {
                hotNew.theloaitin = new CateDao().GetSingleCate(hotNew.theloai_id.Value);
            }
            ViewBag.hotNews = hotNews;
            return View();
        }
    }
}