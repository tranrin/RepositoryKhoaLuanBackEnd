using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public class CongThucRequest
    {
       // public IFormFile? File { get; set; }
        public CongThucRequestAdd ThongTinChung { get; set; }
        public List<NguyenLieuRequest> NguyenLieu { get; set; }
        public List<BuocNauRequest> BuocNau { get; set;}
    }
}
