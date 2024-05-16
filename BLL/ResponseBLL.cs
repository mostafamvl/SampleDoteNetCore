using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public class ResponseBLL
    {

        public int Status { get; set; }
        public string Message { get; set; }

        public ResponseBLL GetResponses(int status)
        {
            List<ResponseBLL> responseList = new List<ResponseBLL>()
            {
                new ResponseBLL
                {
                    Status = 1,
                    Message = "Request Is Added",
                },
                new ResponseBLL
                {
                    Status = 2,
                    Message = "MobileNumber was added before",
                },
                new ResponseBLL
                {
                    Status = 3,
                    Message = "An unexpected error has occurred. Please try again later.",
                }
        };
            return responseList.Where(x => x.Status == status).First();
        }

    }
}
