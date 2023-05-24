using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Table_LuotThich
    {
        public int ID { get; set; }
        public int CongThucID {get;set;}
        public string UserID { get; set; }
        public DateTime ThoiGian { get; set; }
    }
}
