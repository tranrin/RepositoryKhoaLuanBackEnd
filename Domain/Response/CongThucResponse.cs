using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    public class CongThucResponse
    {
        public string TenCongThuc { get; set; }
        public int ID { get; set; }
        public DateTime NgayTao { get; set; }
        public string AnhKemTheo { get; set; }
        public int SoSaoTrungBinh { get; set; }
        public int SaoTrungBinh { get; set; }
        public int TongSoLuong { get; set; }
        


    }
}
