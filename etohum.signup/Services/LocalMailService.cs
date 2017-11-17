using Hangfire;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace etohum.signup.Services
{
    public class LocalMailService : IMailService
    {
        public string _mailFrom = "noreply@test";

        // In this method, the real smtp server and other settings should be written
        // here is just a simulation.
        public void Send(string subject, string message, string mailTo)
        {
            Debug.WriteLine($"Message from {_mailFrom} to {mailTo}, with LocalMailService.");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");

        }
        
        // using Hangfire's BackgroundJob class, we will send our 'send mail' method to queue
        // Hangfire will assign an async task for that, and it will work at bg
        public void SendMailToQueue(string subject, string message, string mailTo)
        {
            // Hangfire serilizes the method and parameters and save it to its database for later call.
            BackgroundJob.Enqueue(() => Send(subject, message, mailTo));
        }
    }
}
