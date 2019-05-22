using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTNhom.Models;

namespace TTNhom.Controllers
{
    public class LoginAdminController : Controller
    {

        OnlShopDbContext db = new OnlShopDbContext();


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string tk, string mk)
        {
            admin ad = db.admins.Where(n => n.TaiKhoan == tk && n.MatKhau == mk).SingleOrDefault();
            if (ad == null)
            {
                ViewBag.ThongBao = "Sai tài khoản hặc mật khẩu";
                return View();
            }

            Session["AdminName"] = ad.HoTen;
            return RedirectToAction("Index", "AdminPage");
        }
        public ActionResult Logout()
        {
            Session["AdminName"] = null;
            return View("Login");
        }
    }
}