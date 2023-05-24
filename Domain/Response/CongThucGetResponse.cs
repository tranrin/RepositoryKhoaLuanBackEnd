using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    public class CongThucGetResponse
    {
        public Table_CongThuc ThongTinChung { get; set; }
        public List<Table_NguyenLieu> NguyenLieu { get; set; }
        public List<Table_BuocNau> BuocNau { get; set; }
    }
}
