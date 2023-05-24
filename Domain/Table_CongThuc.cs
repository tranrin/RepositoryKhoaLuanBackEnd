using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Table_CongThuc
    {
        public int ID { get; set; }
        public string TenCongThuc { get; set; }
        public string MoTa { get; set; }
        public int ThoiGianNau { get; set; }
        public int ThoiGianChuanBi { get; set; }
        public string IDUser { get; set; }
        public TheLoai IDCategory { get; set; }
        public DoKho DoKho { get; set; }
        public string NameCategory { get; set; }
        public string AnhKemTheo { get; set; }
        public DateTime NgayTao { get; set; }
        public string NameDoKho { get; set; }
        public int TongSoLuong { get; set; }
        public int SaoTrungBinh { get; set; }
    }
}
