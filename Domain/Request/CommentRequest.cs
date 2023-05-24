using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public class CommentRequest
    {
        public int? ParentId { get; set; }
        public int? CongThucId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}
