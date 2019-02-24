using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CWAC19AcluMo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CWAC19AcluMo.ViewModels
{
    public class UpdateComplaintViewModel
    {
        public string Email { get; set; }
        public int StatusCodeID { get; set; }

        public int ComplaintID {get; set; }
        public Complaint Complaint { get; set; }
        public List<SelectListItem> StatusCodes { get; set; }

        public UpdateComplaintViewModel() { }
        public UpdateComplaintViewModel(Complaint complaint, IEnumerable<ComplaintStatus> statusCodes)
        {
            StatusCodes = new List<SelectListItem>();
            ComplaintID = complaint.ID;
            Complaint = complaint;

            foreach (var statusCode in statusCodes)
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
