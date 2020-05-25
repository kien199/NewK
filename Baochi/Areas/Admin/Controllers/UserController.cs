using Baochi.Areas.Admin.Models;
using Baochi.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
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
        public ActionResult Editing(int id)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var session = new UserLogin();
                session = (UserLogin)Session[CommonConstants.USER_SESSION]; // lấy từ session
                var name = new UserDao().GetName(session.userName);
                ViewBag.session = name;

                var user = new UserDao().GetById(id);

                ViewBag.user = user;
                return View();
            }
            return RedirectToAction("Login", "User");
        }
        public ActionResult ActionEditing()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var data = Request.Form;
                new UserDao().EditUser(data["username"], data["phone"], data["gender"], data["birth"], Convert.ToInt32(data["role"]), Convert.ToInt32(data["id"]));
                return Redirect("Index");
            }
            return RedirectToAction("Login", "User");
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
            if (Session[CommonConstants.USER_SESSION] == null)
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDao();
                    var result = dao.Login(model.email, Encryptor.MD5Hash(model.password));
                    if (result == 1) //thành công
                    {
                        var user = dao.GetByEmail(model.email);

                        //Đặt giá trị cho Session
                        var userSession = new UserLogin();
                        userSession.userName = user.email;
                        userSession.userID = user.id;

                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result == 0)
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (Session[CommonConstants.USER_SESSION] == null)
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
            else
            {
                return RedirectToAction("Index", "Home");
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
            if (Session[CommonConstants.USER_SESSION] != null)
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
            else
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Deleting()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                try
                {
                    var session = new UserLogin();
                    session = (UserLogin)Session[CommonConstants.USER_SESSION]; // lấy từ session

                    var data = Request.Form;
                    if(session.userID != Convert.ToInt32(data["userId"]))
                    {
                        var check = new UserDao().DeleteUser(Convert.ToInt32(data["userId"]));
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
            else
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}