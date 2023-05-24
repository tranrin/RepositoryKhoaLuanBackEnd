using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public class UserEditRequest
    {
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ScreenName { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public IFormFile? TepDinhKem { get; set; }
        public string FacebookURL { get; set; }
        public string InstagramURL { get; set; }
        public string WebsiteURL { get; set; }
        public string TwitterURL { get; set; }

    }
}
