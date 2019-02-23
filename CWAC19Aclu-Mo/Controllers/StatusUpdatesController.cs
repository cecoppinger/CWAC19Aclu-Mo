using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CWAC19AcluMo.Data;
using CWAC19AcluMo.ViewModels;
using CWAC19AcluMo.Services;

namespace CWAC19AcluMo.Controllers
{
    public class StatusUpdatesController : Controller
    {
        private readonly CWAC19AcluMoContext _context;
        private readonly IEmailService _emailService;

        public StatusUpdatesController(CWAC19AcluMoContext context, IEmailService email)
        {
            _context = context;
            _emailService = email;
        }

        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Send(SendNotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                EmailAddress newAddress = new EmailAddress
                {
                    Name = "",
                    Address = model.Email
                };

                EmailMessage newMessage = new EmailMessage();
                newMessage.ToAddresses.Add(newAddress);
                newMessage.FromAddresses.Add(newAddress);
                newMessage.Subject = "testing";
                newMessage.Content = "I hope this works";

                //send the notification
                _emailService.Send(newMessage);
                return View("Send");
            }

            return View(model);
        }
    }
}