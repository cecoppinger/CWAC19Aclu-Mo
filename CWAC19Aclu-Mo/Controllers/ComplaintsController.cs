using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWAC19AcluMo.Data;
using CWAC19AcluMo.ViewModels;
using CWAC19AcluMo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CWAC19Aclu_Mo.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly CWAC19AcluMoContext _context;
        public ComplaintsController(CWAC19AcluMoContext context)
        {
            _context = context;
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

                return RedirectToAction("New");
            }

            return View(viewModel);
        }

        private ClientProfile ClientExists(string email)
        {
            return _context.ClientProfiles.SingleOrDefault(u => u.Email == email);
        }

    }
}