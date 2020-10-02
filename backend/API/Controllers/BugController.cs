using API.Errors;
using Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BugController : BaseController
    {
        private readonly DatabaseContext _databaseContext;

        public BugController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundResult()
        {
            if (_databaseContext.Products.Find(56) == null)
                return NotFound(new APIResponse(StatusCodes.Status404NotFound));

            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _databaseContext.Products.Find(56);
            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetNotFoundRequest() => BadRequest(new APIResponse(StatusCodes.Status400BadRequest));

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id) => Ok();
    }
}