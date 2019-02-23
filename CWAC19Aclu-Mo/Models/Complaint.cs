using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWAC19AcluMo.Models
{
    public class Complaint
    { 
        public int ClientProfileID { get; set; }    
        public ClientProfile Client { get; set; }

        public int ComplaintStatusID { get; set; }
        public ComplaintStatus Status { get; set; }

        public int ID { get; set; }
        public int ComplaintNo { get; set; }
    }
}
