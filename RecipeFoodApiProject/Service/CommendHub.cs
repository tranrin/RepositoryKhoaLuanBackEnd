using Application.Comment;
using Application.Core;
using Application.LuotThich;
using Dapper;
using Domain;
using Domain.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RecipeProject.Interface;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ServiceModel.Channels;
using Domain.Response;
using Application.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;

namespace RecipeFoodApiProject.Hubs
{



    [Authorize]
    public class CommentHub : Hub
    {

        private readonly IMediator _mediator;
     
        public CommentHub(IMediator mediator, IConfiguration configuration)
        {
            // _configuration = configuration;
            _mediator = mediator;
        }
       
        public override async Task OnConnectedAsync()
        {
            //string userName = Context.User.Identity.Name;
            //string connectionId = Context.ConnectionId;

            //await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            //var identity = (ClaimsIdentity)Context.User.Identity;
            //var tmp = identity.FindFirst(ClaimTypes.NameIdentifier);

            // Lưu user ID vào biến dữ liệu kết nối (Connection Metadata)
            //Context.Items["UserId"] = userId;

            await base.OnConnectedAsync();
        }


        public async Task SendOffersToUser([FromBody]CommentRequest data)
        {
            //CommentResponse
            try
            {
         
                var idUser = (Context.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                //// var accessToken = Context.GetHttpContext().Request.Headers["Authorization"].ToString();
                ////var userId = Context.Items["UserId"]?.ToString();

                //    //Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ////data.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             //   var listResult = await Mediator.Send(new LuotThichGets.Query { userID = idUser }, ct);
                var dataReturn = await _mediator.Send(new CommentAdd.Query
                {

                    Request = new CommentRequest
                    {
                        Content = data.Content,
                        UserId = idUser,
                        CongThucId = data.CongThucId,
                        ParentId = data.ParentId,
                    }
                });
                await Clients.All.SendAsync("ReceiveMessage", dataReturn);
            }
            catch (Exception ex)
            {
                Clients.All.SendAsync("ReceiveMessage", ex.Message);
      
            }
            //await Clients.All.SendComment(postId,parentId,message);
            // Lưu comment vào database
            //var comment = new Comment { PostId = postId, ParentId = parentId, Name = name, Message = message };
            //_dbContext.Comments.Add(comment);
            //await _dbContext.SaveChangesAsync();

            // Xác định group tương ứng với comment cha (nếu có)
            //var parentGroup = parentId.HasValue ? $"Post-{postId}-{parentId}" : $"Post-{postId}";

            //// Gửi comment đến tất cả các client đang kết nối với group tương ứng với comment cha (nếu có)
            //await Clients.Group(parentGroup).SendAsync("ReceiveComment", comment);

        }

    }

    }

