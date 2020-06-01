using Baochi.Areas.Client.Controllers;
using Baochi.Common;
using Model.Dao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        // GET: Admin/Article
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            try
            {
                if (Session[CommonConstants.USER_SESSION] != null)
                {
                    var session = new UserLogin();
                    session = (UserLogin)Session[CommonConstants.USER_SESSION]; // lấy từ session
                    var name = new UserDao().GetName(session.userName);
                    ViewBag.session = name;
                }

                var posts = new PostDao().GetPagedListPost(page, pageSize);
                return View(posts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult Searching()
        {
            try
            {
                var data = Request.Form;
                var posts = new PostDao().SearchPost(data["search"]);

                return Json(new
                {
                    status = true,
                    data = posts
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult Deleting()
        {
            try
            {
                var data = Request.Form;
                var check = new PostDao().DeletePost(Convert.ToInt32(data["postId"]));
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
        public ActionResult Adding()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION]; // lấy từ session
                var name = new UserDao().GetName(session.userName);
                ViewBag.session = name;
            }

            var cates = new CateDao().GetAllCate();
            ViewBag.cates = cates;
            return View();
        }
        public JsonResult ActionAdding()
        {
            //nếu không chọn ảnh cho bài viết thì để ảnh mặc định là http://localhost:44333/Asset_Admin/images/default-new-image.png
            try
            {
                var data = Request.Form;
                //Hàm sinh url cho ảnh tài từ file lên
                string url = "http://localhost:44333/Asset_Admin/images/" + GenImageUrl(Request.Files);
                string noidung = System.Uri.UnescapeDataString(data["content"]);
                var post_id = new PostDao().AddPost(data["title"], data["slug"], data["desc"], noidung, url, Convert.ToInt32(data["cateId"]));

                if (post_id != -1)
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
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);

            }
        }
        public string RandomNumber() // Random số có cùng độ dài
        {
            Random random = new Random();

            int length = 10;
            const string chars = "0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string GenImageUrl(HttpFileCollectionBase files)
        {
            string url = "";

            for (int i = 0; i < files.Count; i++)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };

                string filename = Path.GetFileNameWithoutExtension(files[i].FileName);
                string ext = Path.GetExtension(files[i].FileName);

                if (allowedExtensions.Contains(ext))
                {
                    string number = RandomNumber();
                    string GUID = Path.Combine(Guid.NewGuid().ToString().Replace("-", "_") + "_" + number);

                    url = files[i].FileName.Replace(filename, GUID);
                }
                else
                {
                    url = "";
                }
            }
            for (int i = 0; i < files.Count; i++)
            {
                files[i].SaveAs(Path.Combine(Server.MapPath("~/Asset_Admin/images"), url));
            }
            return url;
        }
        public ActionResult Editing(int id)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION]; // lấy từ session
                var name = new UserDao().GetName(session.userName);
                ViewBag.session = name;
            }

            var cates = new CateDao().GetAllCate();
            ViewBag.cates = cates;

            var article = new PostDao().GetSingleArticle(id);
            ViewBag.article = article;

            return View();
        }
        public JsonResult ActionEditing()
        {
            //nếu không chọn ảnh cho bài viết thì để ảnh mặc định là http://localhost:44333/Asset_Admin/images/default-new-image.png
            try
            {
                var data = Request.Form;
                //Hàm sinh url cho ảnh tài từ file lên
                string url = "http://localhost:44333/Asset_Admin/images/" + GenImageUrl(Request.Files);
                string noidung = System.Uri.UnescapeDataString(data["content"]);
                var post_id = new PostDao().EditPost(data["title"], data["slug"], data["desc"], noidung, url, Convert.ToInt32(data["cateId"]), Convert.ToInt32(data["id"]));

                if (post_id != -1)
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
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult ChangeStatus()
        {
            //nếu không chọn ảnh cho bài viết thì để ảnh mặc định là http://localhost:44333/Asset_Admin/images/default-new-image.png
            try
            {
                var data = Request.Form;
                var check = new PostDao().ChangeStatus(data["loai"], data["tinhtrang"], data["postID"]);

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
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Crawl()
        {
            //Xóa dữ liệu trong db
            new PostDao().DeleteAllPost();
            //Chạy dòng này khi muốn đưa crawl dữ liệu vào db
            new CrawlerController().Crawler("https://gamek.vn/");
            return RedirectToAction("Index", "Article");
        }
    }
}