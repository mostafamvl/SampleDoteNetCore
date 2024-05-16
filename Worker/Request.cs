using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static TaskService.Enums;

namespace TaskService
{
    public class Request
    {
        public int MobileNumber { get; set; }
        public DateTime RequestDate { get; set; }

        public List<Request> ReadRequests(string path)
        {
            if (File.Exists(path))
            {
                //Get the ANIs from CSV file 
                return FileHandler.GetANI(path, ',').ToList();
            }
            else
            {
                return null;
            }
        }

        public string SerializeData(Request request)
        {
            try
            {
                string serializedDocument = JsonConvert.SerializeObject(request);
                return serializedDocument;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return "An unexpected error has occured";
            }
        }

        public IRestResponse SubmitRequest(string body)
        {
            try
            {
                var client = new RestClient("https://localhost:44308/api/Request")
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error :" + ex);
                return null;
            }
        }


      
    }
}
