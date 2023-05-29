using Application.CongThuc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.DanhGia;
using Domain.Request;
using System.Security.Claims;

namespace RecipeFoodApiProject.Controllers
{
    public class DanhGiaController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DanhGiaController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        [HttpPost("DanhGiaAdd")]
       [Authorize]
        public async Task<IActionResult> DanhGiaAdd(CancellationToken ct, [FromBody] DanhGiaRequest data)
        {
            data.UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listResult = await Mediator.Send(new DanhGiaAdd.Command { Request = data }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        //[HttpPost("DanhGiaEdit")]
        //[AllowAnonymous]
        //public async Task<IActionResult> DanhGiaEdit(CancellationToken ct, [FromBody] DanhGiaRequestEdit data)
        //{   

        //    var listResult = await Mediator.Send(new DanhGiaUpdate.Command { Request = data }, ct);
        //    //return HandlerResult(listResult);
        //    return HandlerResult(listResult);
        //}
        
        [HttpPost("DanhGiaByCongThucID/{CongThucID}")]
        [Authorize]
        public async Task<IActionResult> DanhGiaGet(CancellationToken ct, int CongThucID)
        {
            var UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listResult = await Mediator.Send(new DanhGiaGet.Query { CongThucID = CongThucID, UserId = UserID }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
    }
}
