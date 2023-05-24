using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public class CongThucRequestAdd
    {
        public string TenCongThuc { get; set; }
        public string MoTa { get; set; }
        public int? ThoiGianNau { get; set; }
        public int? ThoiGianChuanBi { get; set; }
        public string IDUser { get; set; }
        public int? IDCategory { get; set; }
        public string AnhKemTheo { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? DoKho { get; set; }
    }
}
