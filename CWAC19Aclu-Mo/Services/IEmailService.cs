using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWAC19AcluMo.Services
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);

    }
}
