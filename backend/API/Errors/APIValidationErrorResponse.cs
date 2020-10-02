using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace API.Errors
{
    public class APIValidationErrorResponse : APIResponse
    {
        public APIValidationErrorResponse() : base(StatusCodes.Status400BadRequest) { }

        public IEnumerable<string> Errors { get; set; }
    }
}