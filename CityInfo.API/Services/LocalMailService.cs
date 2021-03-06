using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public LocalMailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public void SendMail(string subject, string Message)
        {
            //send mail- output to debug window
            Debug.WriteLine($"Mail from {_configuration["mailSettings:mailFromAddress"]} to {_configuration["mailSettings:mailToAddress"]}, with  LocalMailService");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"Message{Message}");
        }
    }
}
