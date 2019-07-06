using System;
using System.Collections.Generic;

namespace b15_dbfirst_efcore.Models
{
    public partial class ChuDe
    {
        public ChuDe()
        {
            GopY = new HashSet<GopY>();
        }

        public int MaCd { get; set; }
        public string TenCd { get; set; }
        public string MaNv { get; set; }

        public NhanVien MaNvNavigation { get; set; }
        public ICollection<GopY> GopY { get; set; }
    }
}
