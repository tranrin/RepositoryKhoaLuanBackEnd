using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public class DanhGiaRequest
    {
        public int Sao { get; set; }
        public string UserID { get; set; }
        public int CongThucID { get; set; }
    }
}
