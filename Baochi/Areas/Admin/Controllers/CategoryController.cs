using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            try
            {
                var cates = new CateDao().GetPagedListCate(page, pageSize);
                return View(cates);
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
                var cates = new CateDao().SearchCate(data["search"]);
                return Json(new
                {
                    status = true,
                    data = cates
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
        public JsonResult Deleting()
        {
            try
            {
                var data = Request.Form;
                var check = new CateDao().DeleteCate(Convert.ToInt32(data["cateId"]));
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
            return View();
        }
        public ActionResult ActionAdding()
        {
            var data = Request.Form;
            new CateDao().AddCate(data["name"], data["slug"]);
            return Redirect("Index");
        }
        public ActionResult Editing(int id)
        {
            var cate = new CateDao().GetSingleCate(id);

            ViewBag.cate = cate;
            return View();
        }
        public ActionResult ActionEditing()
        {
            var data = Request.Form;
            new CateDao().EditCate(data["name"], data["slug"], Convert.ToInt32(data["id"]));
            return Redirect("Index");
        }

    }
}