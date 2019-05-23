using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTNChieuT5.Models;


namespace TTNChieuT5.Controllers
{
    public class CartController : Controller
    {
        OnlShopDbContext db = new OnlShopDbContext();

        

        public List<CartItem> GetCartItem()
        {
            List<CartItem> List = Session["CartSession"] as List<CartItem>;
            if (List == null)
            {
                List<CartItem> ListItem = new List<CartItem>();
                Session["CartSession"] = ListItem;
                return ListItem;
            }

            return List;
        }

        public ActionResult ViewCart()
        {
            List<CartItem> lst = GetCartItem();
            if (lst.Count() == 0)
            {
                ViewBag.ThongBao = "Bạn chưa mua sản phẩm nào";
            }
            return View(lst);
        }

        public ActionResult AddToCartHomePage(int _IdSanPham ,string _url)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

            var product = db.SanPhams.SingleOrDefault(n => n.IDSanPham == _IdSanPham);
            if (product == null)
            {
                return RedirectToAction("_404","ManageProducts");
            }

            CartItem item = new CartItem(product.IDSanPham);
            
            List<CartItem> List = GetCartItem();
            CartItem newItem = List.SingleOrDefault(n => n.IdSanPham == item.IdSanPham);
            if (newItem == null)
            {
                List.Add(item);
            }
            newItem.SoLuong++;

            return Redirect(_url);
        }

        public ActionResult AddToCartDetailPage(int _IdSanPham,int _SoLuong, string _url)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

            var product = db.SanPhams.SingleOrDefault(n => n.IDSanPham == _IdSanPham);
            if (product == null)
            {
                return RedirectToAction("");
            }

            CartItem item = new CartItem(product.IDSanPham,_SoLuong);
            if (item.SoLuong > product.SoLuong)
            {
                ViewBag.ThongBao = "Số lượng sản phẩm không đủ !";
                return View("<script>alert('Số lượng không đủ')</script>");
            }

            List<CartItem> List = GetCartItem();
            CartItem newItem = List.SingleOrDefault(n => n.IdSanPham == item.IdSanPham);
            if (newItem == null)
            {
                List.Add(item);
                return Redirect(_url);
            }
            newItem.SoLuong+=item.SoLuong;

            return View(_url);
        }

        public ActionResult UpdateCart(int _IdItem)
        {
            var product = db.SanPhams.SingleOrDefault(n => n.IDSanPham == _IdItem);
            if (product == null)
            {
                return RedirectToAction("_404", "ManageProducts");
            }

            List<CartItem> List = GetCartItem();
            CartItem updateItem = List.SingleOrDefault(n => n.IdSanPham == _IdItem);
            if (updateItem == null)
            {
                return RedirectToAction("_404", "ManageProducts");
            }

            ViewBag.List = List;
            return View("ViewCart",updateItem);
        }

        public ActionResult RemoveItem(int _IdSanPham)
        {
            var checkProduct = db.SanPhams.Find(_IdSanPham);
            if (checkProduct == null)
            {
                return RedirectToAction("_404", "ManageProducts");
            }
            if (Session["CartSession"] == null)
            {

            }
            List<CartItem> List = GetCartItem();
            var removeItem = List.SingleOrDefault(n => n.IdSanPham == _IdSanPham);
            if (removeItem == null)
            {
                return RedirectToAction("_404", "ManageProducts");
            }
            List.RemoveAll(n => n.IdSanPham == _IdSanPham);
            return View("ViewCart");
        }

        public ActionResult ClearCart()
        {
            Session["CartSession"] = null;
            ViewBag.ThongBao = "Bạn chưa mua sản phẩm nào";
            return View("ViewCart");
        }

        public ActionResult PlaceOrder()
        {
            if (Session["CartSession"] == null)
            {
                return RedirectToAction("ViewCart");
            }

            //tạo mới đơn hàng
            HoaDon Order = new HoaDon();
            Order.NgayTao = DateTime.Now;
            db.HoaDons.Add(Order);
            db.SaveChanges();

            //tọa mới chi tiết đơn hàng
            List<CartItem> List = GetCartItem();
            foreach(var item in List)
            {
                ChiTietHoaDon detailOrder = new ChiTietHoaDon();
                detailOrder.IDHoaDon = Order.IDHoaDon;
                detailOrder.IDSanPham = item.IdSanPham;
                detailOrder.SoLuong = item.SoLuong;
                detailOrder.DonGia = item.DonGia;

                db.ChiTietHoaDons.Add(detailOrder);
            }
            db.SaveChanges();

            Session["CartSession"] = null;

            return RedirectToAction("ViewCart");

        }
    }
}