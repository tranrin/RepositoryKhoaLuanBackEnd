using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    public class DanhGiaResponse
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int Sao { get; set; }
        public int SaoTrungBinh { get; set; }
        public int SoLuong { get; set; }
    }
}
