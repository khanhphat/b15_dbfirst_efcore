using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using b15_dbfirst_efcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b15_dbfirst_efcore.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyeStoreContext _context;
        public HangHoaController(MyeStoreContext context)
        {
            //khai bao biến để truyền vô.
            _context = context;
        }
        public IActionResult TimKiem(string TuKhoa = "", double GiaTu = 0, double GiaDen = 0)
        {
            //Lấy hàng hóa theo tiêu chí.
            var dsHangHoa = _context.HangHoa.AsQueryable();//chờ truy vấn. chưa có dữ liệu
            if (!string.IsNullOrEmpty(TuKhoa))
            {
                dsHangHoa = dsHangHoa.Where(h => h.TenHh.Contains(TuKhoa)).AsQueryable();
            }
            if (GiaTu > 0)
            {
                dsHangHoa = dsHangHoa.Where(h => h.DonGia >= GiaTu).AsQueryable();
            }
            if (GiaDen > 0)
            {
                dsHangHoa = dsHangHoa.Where(h => h.DonGia <= GiaDen).AsQueryable();
            }
            //1. Gởi dữ liệu dạng ViewBag
            ViewBag.ListHangHoa = dsHangHoa;
            //Khai báo lấy thêm thông tin của mã loại
            dsHangHoa = dsHangHoa.Include(hh => hh.MaLoaiNavigation);
            //2.Gởi dạng model
            return View(dsHangHoa);
        }

        public IActionResult DoanhSo()
        {
            //Thong ke doanh thu theo loai
            var data = _context.ChiTietHd
                //Gom nhóm theo hàng hóa,
                .GroupBy(n => n.MaHhNavigation)
                //sau khi gom key chính là HangHoa
                .Select(p => new
                {
                    HangHoa = p.Key.TenHh,
                    SoLuong = p.Sum(q => q.SoLuong),
                    DoanhThu = p.Sum(q => q.SoLuong * q.DonGia)
                });
                
                return Json(data);
        }

        public IActionResult DoanhSoTheoLoai()
        {
            //Thong ke doanh thu theo loai
            var data = _context.ChiTietHd
                //Gom nhóm theo hàng hóa,
                .GroupBy(n => n.MaHhNavigation.MaLoaiNavigation)
                //sau khi gom key chính là Loai
                .Select(p => new
                {
                    Loai = p.Key.TenLoai,
                    SoLuong = p.Sum(q => q.SoLuong),
                    DoanhThu = p.Sum(q => q.SoLuong * q.DonGia),
                    GiaTrungBinh = p.Sum(q=>q.SoLuong * q.DonGia )/ p.Sum(q =>q.SoLuong),
                    GiaThapNhat = p.Min(q=>q.DonGia)
                });

            return Json(data);
        }

        public IActionResult ThongKe()
        {
            //Thong ke doanh thu theo loai
            var data = _context.ChiTietHd
                //Gom nhóm theo Tháng / năm
                .GroupBy(n => new
                {
                    Thang = n.MaHdNavigation.NgayDat.Month,
                    Nam = n.MaHdNavigation.NgayDat.Year,
                    Loai = n.MaHhNavigation.MaLoaiNavigation.TenLoai
                }).Select(p => new
                {
                    //Lấy theo loại
                    p.Key.Loai,
                    NamThang =$"{p.Key.Thang}/{p.Key.Nam}" , 
                    DoanhThu = p.Sum(q => q.SoLuong * q.DonGia)
                    
                })
                //Sắp xếp tăng dần theo loại, giảm dần theo doanh thu
                .OrderBy(p => p.Loai)
                .ThenByDescending(p => p.DoanhThu);
                  
                //sau khi gom key chính là HangHoa
               

            return Json(data);
        }

        //Phân trang
        //
        int SoHangHoaMoiTrang = 5;
        public IActionResult PhanTrang(int pagebd = 1)
        {
            var dsHangHoa = _context.HangHoa
                .Skip((pagebd - 1) * SoHangHoaMoiTrang)
                .Take(SoHangHoaMoiTrang)
                //vis du ve cach thuc tao model
                .Select(p => new HangHoaView {
                      MaHh = p.MaHh,
                       TenHh = p.TenHh,
                      Loai = p.MaLoaiNavigation.TenLoai,
                      Hinh = p.Hinh,
                      GiaBan = p.DonGia.Value,
                      NgaySx = p.NgaySx,
                      SoLuong = new Random().Next()
                });
                
            return View(dsHangHoa);
        }
    }
}