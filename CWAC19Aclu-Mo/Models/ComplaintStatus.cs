using CWAC19AcluMo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWAC19AcluMo.Models
{
    public class ComplaintStatus
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }

        public ComplaintStatus() { }
        public ComplaintStatus(string name, string description)
        {
            Status = name;
            Description = description;
        }
    }
}
