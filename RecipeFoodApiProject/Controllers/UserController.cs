using Application.CongThuc;
using Domain.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.AspNetCore.Hosting;
using Domain;
using Application.User;
using Google.Apis.Auth;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using Application.Core;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Google.Apis.Auth.OAuth2.Responses;
using Domain.Response;
using Newtonsoft.Json.Linq;

namespace RecipeFoodApiProject.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(IWebHostEnvironment hostingEnvironment, IConfiguration config) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
            _config = config;
        }
     
        [HttpPost("EditProfile")]
        [Authorize]
        public async Task<IActionResult> EditProfile(CancellationToken ct, [FromBody] UserEditRequest data)
        {

            // PostFile(new FileUploadRequest { File = data.TepDinhKem });
            data.Image = data.Image.Replace("C:\\fakepath\\", "");
            //data.Image = data.Image.Replace("C:\\fakepath\\", "");
            data.Email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listResult = await Mediator.Send(new UserEdit.Command { infor = data }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
 
        [HttpPost("GetProfileUser")]
       [Authorize]
        public async Task<IActionResult> GetUser(CancellationToken ct)
        {
            var Email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listResult = await Mediator.Send(new UserGet.Query { IdUser = Email }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }

        [HttpPost("userLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyGoogleIdToken(CancellationToken ct, [FromBody] UserLoginRequest data)
        {
            try
            {
                // Verify the ID token.
                var payload = await GoogleJsonWebSignature.ValidateAsync(data.IdToken);

                // Use the payload information as needed.
                string email = payload.Email;
                string name = payload.Name;
                string Picture = payload.Picture;
                // check on database get idUser
                var listResult = await Mediator.Send(new UserAdd.Command { infor = new Table_User { Email = email, Image = Picture, ScreenName = name } }, ct );
                // Return a success response.
                //return HandlerResult(Result<string>.Success(GenerateToken(email)));
                if (listResult.IsSuccess)
                {

                    return HandlerResult(Result<TokenResponseLogin>.Success(new TokenResponseLogin { Token = GenerateToken(email), ImageUser = listResult.Value.Image })) ;
                  //  return Ok( new TokenResponseLogin { Token = GenerateToken(email), ImageUser = listResult.Value.Image });

                }
                else
                {
                    return BadRequest(listResult);
                }
              
            }
            catch (InvalidJwtException e)
            {
                // The token is invalid.
                return BadRequest("Invalid ID token." + e.Message);
            }
        }
       [NonAction]
        private string GenerateToken(string IdUser )
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,IdUser),

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        [NonAction]
        public string PostFile([FromForm] FileUploadRequest model)
        {
            try
            {
                var saveFilePath = Path.Combine("wwwroot\\images", model.File.FileName);
                //var saveFilePath = _webHostEnvironment.WebRootPath + "\\FileUpload\\";
                if (!Directory.Exists(saveFilePath))
                {
                    Directory.CreateDirectory(saveFilePath);
                }
                //Path.Combine("../RecipeFoodApiProject/FileUpload", model.File.FileName);
                // using (var stream = new FileStream(saveFilePath, FileMode.Create))\
                using (FileStream fileStream = System.IO.File.Create(saveFilePath + model.File.FileName))
                {


                    model.File.CopyToAsync(fileStream);
                    fileStream.Flush();
                    //  return Ok(saveFilePath);
                    return saveFilePath + model.File.FileName;
                }

            }
            catch (

            Exception e
            )
            {
                return e.Message;
            }
        }
    }
}
