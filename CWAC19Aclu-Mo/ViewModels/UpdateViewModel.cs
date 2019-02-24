using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CWAC19AcluMo.Models;

namespace CWAC19AcluMo.ViewModels
{
    public class UpdateViewModel
    {
        public string Email { get; set; }

        public IList<Complaint> complaints { get; set; }
    } 
}
