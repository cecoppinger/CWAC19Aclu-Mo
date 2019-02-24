using CWAC19AcluMo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWAC19AcluMo.ViewModels
{
    public class EditComplaintViewModel
    {
        public int StatusID { get; set; }
        public int ComplaintID { get; set; }
        public int ComplaintNo { get; set; }
        
        public List<SelectListItem> StatusCodes { get; set; }

        public EditComplaintViewModel() { }
        public EditComplaintViewModel(IEnumerable<ComplaintStatus> statuses)
        {
            StatusCodes = new List<SelectListItem>();
            foreach(ComplaintStatus status in statuses)
            {
                StatusCodes.Add(new SelectListItem
                {
                    Value = status.ID.ToString(),
                    Text = status.Status
                });
            }
        }
    }
}
