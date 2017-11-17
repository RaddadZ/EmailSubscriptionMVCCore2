using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace etohum.signup.Services
{
    public interface IMailService
    {
        void SendMailToQueue(string subject, string message, string mailTo);
    }
}
