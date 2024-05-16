using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskService
{
    public class Worker : BackgroundService
    {
        readonly Request requestClass;
        readonly Response responseClass;
        readonly FileHandler fileHandler;
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger)
        {
            requestClass = new Request();
            responseClass = new Response();
            fileHandler = new FileHandler();
            _logger = logger;
        }
     
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string filePath = fileHandler.GetFileDirectory("Requests.csv");
                List<Request> requests = requestClass.ReadRequests(filePath);
                foreach (var request in requests)
                {
                    string body = requestClass.SerializeData(request);
                    IRestResponse response = requestClass.SubmitRequest(body);
                    responseClass.HandleResponse(response, request.MobileNumber);
                }
                fileHandler.MoveFiles("Done", "Requests.csv");
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000*60*5, stoppingToken);
            }
        }
    }
}
