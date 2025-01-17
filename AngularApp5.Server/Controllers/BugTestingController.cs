using AngularApp5.Server.Data;
using AngularApp5.Server.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AngularApp5.Server.Controllers
{
    //controller returning/creating errors used for testing

    [Route("api/[controller]")]
    [ApiController]
    public class BugTestingController : ControllerBase
    {
        private readonly StoreContext _context;

        public BugTestingController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            var item = _context.Products.Find(100);

            if(item == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var item = _context.Products.Find(100);

            var item2 = item.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFound(int id)
        {
            return BadRequest();
        }
    }
}
