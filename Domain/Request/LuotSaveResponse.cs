using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public class LuotSaveResponse
    {
        public int ID { get; set; }
        public int CongThucID { get; set; }
        public DateTime? ThoiGian { get;set; }
        public string TenCongThuc { get; set; }
        public int IDCategory { get; set; }
        public string AnhKemtheo { get; set; }
        public int SoSaoTrungBinh { get; set; }
        public int SoNguoiDanhGia { get; set; }
      
    }
}
