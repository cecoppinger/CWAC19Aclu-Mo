using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWAC19AcluMo.Data;
using CWAC19AcluMo.ViewModels;
using CWAC19AcluMo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CWAC19AcluMo.Services;

namespace CWAC19Aclu_Mo.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly CWAC19AcluMoContext _context;
        private readonly IEmailService _emailService;
        public ComplaintsController(CWAC19AcluMoContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult View(int id)
        {
            var client = _context.Complaints
                .Include(c => c.Client)
                .Include(c => c.Status)
                .SingleOrDefault(c => c.ID == id);

            return View(client);
        }

        public IActionResult Index()
        {
            var complaints = _context.Complaints
                .Include(c => c.Client)
                .Include(c => c.Status)
                .ToList();
            return View(complaints);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult New()
        {
            AddComplaintViewModel viewModel = new AddComplaintViewModel(_context.ComplaintStatuses.ToList());
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult New(AddComplaintViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ClientProfile client = ClientExists(viewModel.Email);
                if(client == null)
                {
                    client = new ClientProfile(viewModel.Email);
                    _context.ClientProfiles.Add(client);
                }

                Complaint newComplaint = new Complaint
                {
                    ClientProfileID = client.ID,
                    ComplaintStatusID = viewModel.StatusCodeID,
                    ComplaintNo = viewModel.ComplaintNo
                };

                _context.Complaints.Add(newComplaint);
                _context.SaveChanges();

                return View("Success");
            }

            return View(viewModel);
        }

        public IActionResult Update(string email = null)
        {
            if (email != null)
            {
                ClientProfile client = _context.ClientProfiles
                    .Include(cp => cp.Complaint)
                    .ThenInclude(c => c.Status)
                    .SingleOrDefault(c => c.Email == email);

                if(client == null)
                {
                    return NotFound();
                }

                UpdateComplaintViewModel viewModel = new UpdateComplaintViewModel(client.Complaint, 
                    _context.ComplaintStatuses.ToList());

                viewModel.Email = client.Email;
                return View(viewModel);
            }
            else
            {
                return View(new UpdateComplaintViewModel());
            }
        }

        [HttpPost]
        public IActionResult Update(UpdateComplaintViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var newComplaint = _context.Complaints
                    .Include(c => c.Client)
                    .Include(c => c.Status)
                    .Where(c => c.ID == viewModel.ComplaintID)
                    .SingleOrDefault();

                newComplaint.ComplaintStatusID = viewModel.StatusCodeID;

                SendNotification(newComplaint.Client.Email, viewModel.StatusCodeID);

                _context.SaveChanges();
                return RedirectToAction(nameof(View), new { id = newComplaint.ClientProfileID });
            }

            return NotFound();
        }

        public IActionResult Edit(int id)
        {
            var complaint = _context.Complaints
                .Include(c => c.Status)
                .SingleOrDefault(c => c.ID == id);

            if(complaint == null)
            {
                return NotFound();
            }

            EditComplaintViewModel viewModel = 
                    new EditComplaintViewModel(_context.ComplaintStatuses.ToList());

            viewModel.ComplaintID = complaint.ID;
            viewModel.StatusID = complaint.ComplaintStatusID;
            viewModel.ComplaintNo = complaint.ComplaintNo;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditComplaintViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var complaint = _context.Complaints
                    .Include(c => c.Client)
                    .SingleOrDefault(c => c.ID == viewModel.ComplaintID);
                complaint.ComplaintStatusID = viewModel.StatusID;

                SendNotification(complaint.Client.Email, viewModel.StatusID);

                _context.SaveChanges();

                return RedirectToAction(nameof(View), new { id = complaint.ClientProfileID });
            }

            return NotFound();
        }


        private ClientProfile ClientExists(string email)
        {
            return _context.ClientProfiles.SingleOrDefault(u => u.Email == email);
        }

        private void SendNotification(string email, int statusID)
        {
            var status = _context.ComplaintStatuses.Find(statusID);

            EmailMessage emailMessage = new EmailMessage
            {
                Subject = "Your Complaint Status With The ACLU-MO",
                Content = String.Format("The status of your case is: {0}<br><br>{1}",
                    status.Status, status.Description)
            };

            EmailAddress emailAddress = new EmailAddress
            {
                Name = "",
                Address = email
            };

            emailMessage.ToAddresses.Add(emailAddress);
            emailMessage.FromAddresses.Add(emailAddress);

            _emailService.Send(emailMessage);
        }
    }
}
