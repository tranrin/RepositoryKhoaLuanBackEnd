using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Table_Comment
    {
        public int CongThucID { get; set; }
        public string Content { get; set; }
        public DateTime DateCreate { get; set; }
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string ScreenName { get; set; }
        public string Image { get; set; }
    }
}
