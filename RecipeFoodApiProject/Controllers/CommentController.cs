using Application.LuotThich;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using Microsoft.EntityFrameworkCore;

using Swashbuckle.AspNetCore.SwaggerGen;
using Domain.Request;
using System.Collections.Generic;
using System;
using Application.CongThuc;
using RecipeFoodApiProject.Hubs;
using RecipeProject.Interface;
using System.Security.Claims;

namespace RecipeFoodApiProject.Controllers
{
    public class CommentController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private IHubContext<CommentHub, ICommentHub> _CommentHub;
        public CommentController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            //IHubContext<CommentHub, ICommentHub> hubContext
            //, IHubContext<CommentHub, ICommentHub> hubContext
            _webHostEnvironment = hostingEnvironment;
           // _CommentHub = hubContext;
        }
        [HttpGet("GetsCommentByCongThucID/{CongThucID}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetsCommentByCongThucID(CancellationToken ct, int CongThucID)
        {
            var listResult = await Mediator.Send(new CommentGetsByIDCongThuc.Query { CongThucID = CongThucID }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        //[HttpPost]
        //[Route("SendComment")]
        //public async Task<IActionResult> SendComment(CancellationToken ct, [FromForm] CommentRequest data)

        //{
        //    data.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var retMessage = string.Empty;
        //    try
        //    {
        //        await _CommentHub.Clients.All.SendOffersToUser(data);
        //        await Mediator.Send(new CommentAdd.Command { Request = data }, ct);
             
              
        //        retMessage = "success";
        //    }
        //    catch(Exception e)
        //    {
        //        retMessage = e.ToString();

        //    }
        //    return Ok(retMessage);
        //}
    }
}
