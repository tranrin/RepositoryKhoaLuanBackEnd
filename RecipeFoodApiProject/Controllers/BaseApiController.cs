
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MediatR;
using Application.Core;
using Microsoft.Extensions.DependencyInjection;


namespace RecipeFoodApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        private IWebHostEnvironment _hostingEnvironment;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public BaseApiController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        protected ActionResult HandlerResult<T>(Result<T> result)
        {
            var val = result.Value;
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
                // co the chinh sua tra ve ca value va issuccess   return Ok(result);
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);
        }
    }
}

