using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using NguyenTheHoang_2020603205.Models;
namespace NguyenTheHoang_2020603205.Controllers
{
    public class NVienController : Controller
    {
        // GET: NVien'
        Model1 db = new Model1();
        public ActionResult Index()
        {
            var query = db.NhanViens.Select(p => p);
            return View(query);
        }
        [HttpGet]
        public ActionResult ChiTiet(string id)
        {
            var query = db.NhanViens.Where(m => m.Manv == id).First();
            return View(query);
        }
        [HttpGet]
        public ActionResult Them()
        {
            ViewData["Phong"] = new SelectList(db.Phongs,"Maphong","Tenphong");
            return View();
        }
        [HttpPost]
        public ActionResult Them(FormCollection f,NhanVien nv) {
            var ma = f["Manv"];
            var ten = f["Hoten"];
            var phong = f["Phong"];
            var luong = f["Luong"];
            if(String.IsNullOrEmpty(ma))
            {
                ViewData["Loi1"] = "Mã nhân viên không được để trống";
            } 
            else if(String.IsNullOrEmpty(ten))
            {
                ViewData["Loi2"] = "Tên không được để trống";
            }
            else if(String.IsNullOrEmpty(luong))
            {
                ViewData["Loi3"] = "Lương không được để trống";
            }
            else
            {
                nv.Manv = ma;
                nv.Hoten= ten;
                nv.Maphong = phong;
                nv.Luong=Convert.ToDouble(luong);
                db.NhanViens.Add(nv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Them();
        }
        [HttpGet]
        public ActionResult Xoa(string id)
        {
            var query = db.NhanViens.First(m => m.Manv == id);
            return View(query); 
        }
        [HttpPost]
        public ActionResult Xoa(string id,FormCollection Data)
        {
            var query=db.NhanViens.Where(m=>m.Manv==id).First();
            db.NhanViens.Remove(query);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Sua(string id)
        {
            var query=db.NhanViens.First(m=>m.Manv==id);
            ViewData["Phong"] = new SelectList(db.Phongs,"Maphong","Tenphong");
            return View(query);
        }
        [HttpPost]
        public ActionResult Sua(string id, FormCollection f)
        {
            var nv = db.NhanViens.First(m => m.Manv == id);
            var ten = f["Hoten"];
            var phong = f["Phong"];
            var luong = f["Luong"];
            if (String.IsNullOrEmpty(ten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            else
            {
                nv.Hoten = ten;
                nv.Maphong = phong;
                nv.Luong = Convert.ToDouble(luong);
                UpdateModel(nv);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return this.Sua(id);

        }

        }
}