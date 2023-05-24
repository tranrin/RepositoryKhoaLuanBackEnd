using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public class FileUploadRequest
    {
       // public string UploaderName { get; set; }
        public IFormFile? File { get; set; }
    }
}
