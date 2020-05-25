using Baochi.Areas.Admin.Models;
using Baochi.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baochi.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                try
                {
                    var session = new UserLogin();
                    session = (UserLogin)Session[CommonConstants.USER_SESSION]; // lấy từ session
                    var name = new UserDao().GetName(session.userName);
                    ViewBag.session = name;

                    var users = new UserDao().GetPagedListUser(page, pageSize);
                    return View(users);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction("Login", "User");
        }
        public ActionResult Adding()
        {
            return View();
        }
        public ActionResult Editing()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult ActionLogin(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.email, Encryptor.MD5Hash(model.password));
                if(result == 1) //thành công
                {
                    var user = dao.GetByEmail(model.email);
                    
                    //Đặt giá trị cho Session
                    var userSession = new UserLogin();
                    userSession.userName = user.email;
                    userSession.userID = user.id;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Mật khâủ không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }
            return View("Login");
        }
        public ActionResult Register()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult ActionAdding()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var data = Request.Form;
                var userID = new UserDao().AddUser(data["name"], data["email"], Encryptor.MD5Hash(data["password"]));
                if(userID == -1) // bị trùng email
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                    return View("Adding");
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                var data = Request.Form;
                var userID = new UserDao().AddUser(data["name"], data["email"], Encryptor.MD5Hash(data["password"]));
                if (userID == -1) // bị trùng email
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                    return View("Register");
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
        }
        public ActionResult Logout()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                Session.Remove(CommonConstants.USER_SESSION);

                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("Index", "Home");
        }
        public JsonResult Searching()
        {
            try
            {
                var data = Request.Form;
                var users = new UserDao().SearchUser(data["search"]);
                return Json(new
                {
                    status = true,
                    data = users
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

    }
}