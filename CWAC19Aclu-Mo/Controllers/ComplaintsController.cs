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
            return View(_context.ClientProfiles
                .Include(c => c.Complaints)
                .ThenInclude(c => c.Status)
                .SingleOrDefault(c => c.ID == id));
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
                    .Include(cp => cp.Complaints)
                    .ThenInclude(c => c.Status)
                    .SingleOrDefault(c => c.Email == email);

                if(client == null)
                {
                    return NotFound();
                }

                UpdateComplaintViewModel viewModel = new UpdateComplaintViewModel(client.Complaints,
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
                var client = _context.ClientProfiles
                    .Include(c => c.Complaints)
                    .ThenInclude(c => c.Status)
                    .SingleOrDefault(c => c.Email == viewModel.Email);

                client.Complaints = viewModel.Complaints;

                _context.SaveChanges();
                return RedirectToAction(nameof(View), client.ID);
            }

            return NotFound();
        }


        private ClientProfile ClientExists(string email)
        {
            return _context.ClientProfiles.SingleOrDefault(u => u.Email == email);
        }

        private IEnumerable<Complaint> GetComplaints(ClientProfile client)
        {
            var complaints = _context.ClientProfiles.Include(c => c.Complaints).SingleOrDefault(c => c.ID == client.ID);
            return complaints.Complaints;
        }
    }
}
