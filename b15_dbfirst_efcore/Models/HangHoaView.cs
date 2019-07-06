using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace b15_dbfirst_efcore.Models
{
    public class HangHoaView
    {
        public int MaHh { get; set;}
        public string TenHh { get; set;}
        public string Hinh { get; set; }
        public string Loai { get; set; }
        public DateTime NgaySx { get; set; }
        public double GiaBan { get; set; }
        public int SoLuong { get; set; }
        public bool ConHang => SoLuong > 0;
    }
}
