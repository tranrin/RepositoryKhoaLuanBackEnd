using Application.CongThuc;
using Domain.Request;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.LuotThich;
using Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;

namespace RecipeFoodApiProject.Controllers
{
    public class CollectionController : BaseApiController
    {   
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CollectionController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
     //   [EnableCors("AllowOrigin")]
        [HttpGet("GetCollectionByUserID")]
        [Authorize]
        public async Task<IActionResult> CollectionGetsByUserID(CancellationToken ct)
        {
            var idUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listResult = await Mediator.Send(new LuotThichGets.Query { userID = idUser }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpPost("AddCollection")]
       [Authorize]
        public async Task<IActionResult> AddCollection(CancellationToken ct, [FromBody] CollectionRequest data)
        {
            data.NguoiDungID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listResult = await Mediator.Send(new LuotThichAdd.Command { Request = data }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpDelete("DeleteCollection/{id}")]
       [AllowAnonymous]
        public async Task<IActionResult> DeleteCollection(CancellationToken ct, int id)
        {
            var listResult = await Mediator.Send(new LuotThichDelete.Command { Request = id }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
    }
}
