using BLL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RequestController : ControllerBase
    {
        private readonly ILogger<RequestController> _logger;
        RequestBLL requestBLL;
        ResponseBLL responseBLL;

        public RequestController(ILogger<RequestController> logger)
        {
            _logger = logger;
            requestBLL = new RequestBLL();
            responseBLL = new ResponseBLL();
        }

        [HttpPost]
        public ContentResult HandleRequest([FromBody] Request request)
        {
            string content;
            if (requestBLL.IsDuplicated(x => x.MobileNumber == request.MobileNumber))
            {
                responseBLL = responseBLL.GetResponses(2);
                content = JsonConvert.SerializeObject(responseBLL);
                return new ContentResult() { Content = content, StatusCode = 409 }; //Conflict
            }
            else
            {
                if (requestBLL.Add(request))
                {
                    responseBLL = responseBLL.GetResponses(1);
                    content = JsonConvert.SerializeObject(responseBLL);
                    return new ContentResult() { Content = content, StatusCode = 200 }; //OK
                }
                else
                {
                    responseBLL = responseBLL.GetResponses(3);
                    content = JsonConvert.SerializeObject(responseBLL);
                    return new ContentResult() { Content = content, StatusCode = 400 }; //BadRequest
                }
            }
        }
        [HttpGet]
        public object GetRequest(DateTime? dtFrom, DateTime? dtTo)
        {
            var requests = JsonConvert.SerializeObject(requestBLL.GetAllByExpression(x => x.RequestDate >= dtFrom && x.RequestDate <= dtTo || (dtFrom == null || dtTo == null)).ToList());
            return requests;
        }
    }
}
