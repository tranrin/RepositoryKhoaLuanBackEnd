using Application.Core;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Application.CongThuc;
using Domain.Response;
using Domain.Request;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Cors;
using RecipeFoodApiProject.Controllers;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;

namespace RecipeFoodApiProject.CongThucController
{
 
    public class CongThucController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CongThucController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
      //  [EnableCors("AllowOrigin")]

        [HttpGet("CongThucGets/{search?}")]
        [AllowAnonymous]
        public async Task<IActionResult> CongThucGets(CancellationToken ct, string search)
        {
            try
            {
                var listResult = await Mediator.Send(new CongThucGets.Query { data = search }, ct);
                //return HandlerResult(listResult);
                return HandlerResult(listResult);
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }

        }

        [HttpGet("CongThucGetsByTop")]
        [AllowAnonymous]
        public async Task<IActionResult> CongThucGetsByTop(CancellationToken ct)
        {
            try
            {
                var listResult = await Mediator.Send(new CongThucGetsTop.Query(), ct);
                //return HandlerResult(listResult);
                return HandlerResult(listResult);
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }

        }
        [HttpPost("CongThucAdd")]
       [Authorize]
        public async Task<IActionResult> CongThucAdd(CancellationToken ct, [FromBody] CongThucRequest data)
        {
           
            //if (data.File != null)
            //{
            //    PostFile(new FileUploadRequest { File = data.File });
            //    data.ThongTinChung.AnhKemTheo = "images" + data.File.FileName;
            //    //data.ThongTinChung.AnhKemTheo = data.File.FileName;
            //}

           
            data.ThongTinChung.IDUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listResult = await Mediator.Send(new CongThucAdd.Query { data = data }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpDelete("CongThucDelete/{idCongThuc}")]
        [AllowAnonymous]
        public async Task<IActionResult> CongThucDelete( int idCongThuc, CancellationToken ct)
        {
            var listResult = await Mediator.Send(new CongThucDelete.Command { Request = idCongThuc }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpPut("CongThucUpdate")]
        [AllowAnonymous]
        public async Task<IActionResult> CongThucUpdate([FromBody] CongThucRequestUpdate data, CancellationToken ct)
        {

            //URL+ fileName  (images)
            //if (data.Te != null)
            //{
            //   // PostFile(new FileUploadRequest { File = data.File });
            //    data.ThongTinChung.AnhKemTheo = "images" + data.File.FileName;
            //    //data.ThongTinChung.AnhKemTheo = data.File.FileName;
            //}
            data.ThongTinChung.IDUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listResult = await Mediator.Send(new CongThucEdit.Command { data = data }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }
        [HttpGet("CongThucGet/{data}")]
        [AllowAnonymous]
        public async Task<IActionResult> CongThucGet( int data,CancellationToken ct)
        {
            try
            {
                var listResult = await Mediator.Send(new CongThucGet.Query { CongThucID = data }, ct);
                //return HandlerResult(listResult);
                return HandlerResult(listResult);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }


        }
        [HttpGet("CongThucGetsByCategory/{data}")]
        [AllowAnonymous]
        public async Task<IActionResult> CongThucGetsByCategory(int data, CancellationToken ct)
        {
            try
            {
                var listResult = await Mediator.Send(new CongThucGetsByCategory.Query { data = data }, ct);
                //return HandlerResult(listResult);
                return HandlerResult(listResult);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }
        [HttpGet("CongThucGetsNewest")]
        [AllowAnonymous]
        public async Task<IActionResult> CongThucGetsNewest(CancellationToken ct)
        {
            try
            {
                //Directory bien duong dan
                var listResult = await Mediator.Send(new CongThucNewGets.Query(), ct);
                //return HandlerResult(listResult);
                return HandlerResult(listResult);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }
        [HttpGet("CongThucGetByUserID")]
        [Authorize]
        public async Task<IActionResult> CongThucGetByUserID(CancellationToken ct)
        {
            var IDUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var listResult = await Mediator.Send(new CongThucGetsByUserID.Query { IDUser = IDUser }, ct);
            //return HandlerResult(listResult);
            return HandlerResult(listResult);
        }



        [HttpPost("PostFile")]
        // file anh se co hai loai la file http va file local
        public string PostFile([FromForm] FileUploadRequest model)
        {
            try
            {   //var checkFile = model.File
                  //  byte[] bytes = System.IO.File.ReadAllBytes(model.File);
                var saveFilePath = Path.Combine("wwwroot\\images", model.File.FileName);
                //var saveFilePath = _webHostEnvironment.WebRootPath + "\\FileUpload\\";
                if (!Directory.Exists("wwwroot\\images"))
                {
                    Directory.CreateDirectory("wwwroot\\images");    
                }
                    //Path.Combine("../RecipeFoodApiProject/FileUpload", model.File.FileName);
               // using (var stream = new FileStream(saveFilePath, FileMode.Create))\
                   using (FileStream fileStream = System.IO.File.Create(saveFilePath))
                {

                 
                     model.File.CopyTo(fileStream);
                      fileStream.Flush();
                    //  return Ok(saveFilePath);
                    return "images/" + model.File.FileName;
                }
               
            }
            catch(

            Exception e
            )
            {
                return e.Message;
            }
        }
        //public string PostFile([FromForm] FileUploadRequest model)
        //{
        //    try
        //    {
        //        var saveFilePath = Path.Combine("wwwroot\\images", model.File.FileName);
        //        //var saveFilePath = _webHostEnvironment.WebRootPath + "\\FileUpload\\";
        //        if (!Directory.Exists(saveFilePath))
        //        {
        //            Directory.CreateDirectory(saveFilePath);
        //        }
        //        //Path.Combine("../RecipeFoodApiProject/FileUpload", model.File.FileName);
        //        // using (var stream = new FileStream(saveFilePath, FileMode.Create))\
        //        using (FileStream fileStream = System.IO.File.Create(saveFilePath + model.File.FileName))
        //        {


        //            model.File.CopyToAsync(fileStream);
        //            fileStream.Flush();
        //            //  return Ok(saveFilePath);
        //            return saveFilePath + model.File.FileName;
        //        }

        //    }
        //    catch (

        //    Exception e
        //    )
        //    {
        //        return e.Message;
        //    }
        //}
    }
}
