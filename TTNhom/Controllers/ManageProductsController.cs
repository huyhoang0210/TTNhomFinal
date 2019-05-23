using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTNChieuT5.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace TTNChieuT5.Controllers
{
    public class ManageProductsController : Controller
    {
        OnlShopDbContext db = new OnlShopDbContext();


        // GET: ManageProducts
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var lst = db.SanPhams.Where(n=>n.SoLuong > 0).ToList().OrderBy(n => n.TenSanPham).ToPagedList(pageNumber, pageSize);
            return View(lst);
        }

        public ActionResult OutOfStockProduct(int ? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var lst = db.SanPhams.Where(n=>n.SoLuong == 0).ToList().OrderBy(n => n.TenSanPham).ToPagedList(pageNumber, pageSize);
            return View(lst);
        }
        public ActionResult Insert()
        {
            //ViewBag.IDDanhMuc = db.DanhMucSanPhams.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Insert(SanPham sp, HttpPostedFileBase fileUpLoad)
        {

            if (fileUpLoad  == null)
            {
                ViewBag.ThongBao = "Bạn chưa chọn ảnh";
                return View();
            }
            if (ModelState.IsValid)
            {
                string _fileName = Path.GetFileName(fileUpLoad.FileName);
                string _path = Path.Combine(Server.MapPath("/Images"), _fileName);
                fileUpLoad.SaveAs(_path);

                sp.HinhAnh = fileUpLoad.FileName;

                db.SanPhams.Add(sp);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();

        }
        public ActionResult Update(int id)
        {
            var product = db.SanPhams.Find(id);
            if (product == null)
            {
                return View("_404");
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Update(SanPham sp, HttpPostedFileBase fileUpLoad)
        {
            var product = db.SanPhams.Find(sp.IDSanPham);
            if (product == null)
            {
                return RedirectToAction("_404");
            }
            if (fileUpLoad != null)
            {
                string _fileName = Path.GetFileName(fileUpLoad.FileName);
                string _path = Path.Combine(Server.MapPath("/Images"), _fileName);
                fileUpLoad.SaveAs(_path);

                product.HinhAnh = fileUpLoad.FileName;
            }
            product.SoLuong = sp.SoLuong;
            product.Gia = sp.Gia;
            product.MoTa = sp.MoTa;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult _404()
        {
            return View();
        }
    }
}