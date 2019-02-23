using CWAC19AcluMo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWAC19AcluMo.ViewModels
{
    public class AddComplaintViewModel
    {
        public string Email { get; set; }
        public int StatusCodeID { get; set; }
        public int ComplaintNo { get; set; }

        public List<SelectListItem> StatusCodes { get; set; }

        public AddComplaintViewModel() { }
        public AddComplaintViewModel(IEnumerable<ComplaintStatus> statusCodes)
        {
            StatusCodes = new List<SelectListItem>();

            foreach(var statusCode in statusCodes)
            {
                StatusCodes.Add(new SelectListItem
                {
                    Value = statusCode.ID.ToString(),
                    Text = statusCode.StatusCode
                });

            }
        }
    }
}
