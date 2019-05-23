using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTNChieuT5.Models;

namespace TTNChieuT5.Controllers
{
    public class AdminPageController : Controller
    {

        OnlShopDbContext db = new OnlShopDbContext();

        // GET: AdminPage
        public ActionResult Index()
        {
            ViewBag.totalOrder = db.HoaDons.Count().ToString();
            ViewBag.totalUser = db.users.Count().ToString();
            return View();
        }
    }
}