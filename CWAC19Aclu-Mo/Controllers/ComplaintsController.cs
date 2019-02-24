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
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult New(AddComplaintViewModel viewModel)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        ClientProfile newClient = new ClientProfile(viewModel.Email);


        //    }
        //}

    }
}