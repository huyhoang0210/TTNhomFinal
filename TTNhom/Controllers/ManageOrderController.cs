using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using TTNChieuT5.Models;


namespace TTNChieuT5.Controllers
{
    public class ManageOrderController : Controller
    {

        OnlShopDbContext db = new OnlShopDbContext();

        // GET: ManageOrder
        public ActionResult Index(int ? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var lst = db.HoaDons.ToList().OrderByDescending(n => n.NgayTao).ToPagedList(pageNumber, pageSize);
            return View(lst);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.CTDH = db.ChiTietHoaDons.Where(n => n.IDHoaDon == id).ToList().OrderBy(n => n.SanPham.TenSanPham);
            var order = db.HoaDons.Find(id);
            return View(order);
        }

    }
}