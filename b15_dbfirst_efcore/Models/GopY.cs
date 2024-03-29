﻿using System;
using System.Collections.Generic;

namespace b15_dbfirst_efcore.Models
{
    public partial class GopY
    {
        public string MaGy { get; set; }
        public int MaCd { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayGy { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public bool CanTraLoi { get; set; }
        public string TraLoi { get; set; }
        public DateTime? NgayTl { get; set; }

        public ChuDe MaCdNavigation { get; set; }
    }
}
