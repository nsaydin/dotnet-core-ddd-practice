using System;
using System.Collections.Generic;
using System.Net;

namespace Application.Dtos
{
    public class ApiError
    {
        public ApiError()
        {
            ErrorId = Guid.NewGuid();
        }

        public Guid ErrorId { get; }
        public HttpStatusCode StatusCode { set; get; }

        public List<string> Errors { get; set; } = new List<string>();
        public string PrivateMessage { get; set; }
    }
}