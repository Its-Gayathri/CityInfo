using System.Diagnostics;

namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = "gayathriu64@gmail.com";
        private string _mailFrom = "gayathri2112@gmail.com";

        public void SendMail(string subject, string Message)
        {
            //send mail- output to debug window
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with  CloudMailService");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"Message{Message}");
        }
    }
}
