using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    public class CommentResponse
    {
        public int ID { get; set; }
        public int? ParentId { get; set; }
        public int? IDCongThuc { get; set; }
        public string Content { get; set; }
        public string IDUser { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
