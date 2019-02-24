using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWAC19AcluMo.Data;
using CWAC19AcluMo.ViewModels;
using CWAC19AcluMo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CWAC19Aclu_Mo.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly CWAC19AcluMoContext _context;
        public ComplaintsController(CWAC19AcluMoContext context)
        {
            _context = context;
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

                _context.SaveChanges();
                return RedirectToAction(nameof(View), newComplaint.ClientProfileID);
            }

            return NotFound();
        }


        private ClientProfile ClientExists(string email)
        {
            return _context.ClientProfiles.SingleOrDefault(u => u.Email == email);
        }
    }
}
