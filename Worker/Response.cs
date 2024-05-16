using RestSharp;
using System;
using static TaskService.Enums;

namespace TaskService
{
    public class Response
    {
        readonly FileHandler fileHandler;
        public Response()
        {
            fileHandler = new FileHandler();
        }
        public void HandleResponse(IRestResponse response, int mobileNumber)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                WriteToRequestFile((char)StatusResponse.Duplicated, mobileNumber);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                WriteToRequestFile((char)StatusResponse.Failed, mobileNumber);
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                WriteToRequestFile((char)StatusResponse.Failed, mobileNumber);
            }
            Console.WriteLine(response.Content);
        }
        public bool WriteToRequestFile(char responseType, int mobileNumber)
        {
            string fileName = ChooseFile(responseType);
            return fileHandler.AppendToFile(fileName, mobileNumber.ToString());
        }
        public string ChooseFile(char responseType)
        {
            if (responseType == (char)StatusResponse.Duplicated)
            {
                return "Duplicate.txt";
            }
            else if (responseType == (char)StatusResponse.Failed)
            {
                return "Failed.txt";
            }
            return null;
        }
    }
}
