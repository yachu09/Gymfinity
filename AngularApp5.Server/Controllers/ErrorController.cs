using AngularApp5.Server.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace AngularApp5.Server.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    //telling swagger to ignore this controller
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
