﻿using System;
using System.Collections.Generic;

namespace b15_dbfirst_efcore.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            HangHoa = new HashSet<HangHoa>();
        }

        public string MaNcc { get; set; }
        public string TenCongTy { get; set; }
        public string Logo { get; set; }
        public string NguoiLienLac { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string MoTa { get; set; }

        public ICollection<HangHoa> HangHoa { get; set; }
    }
}
