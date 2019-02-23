using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWAC19AcluMo.Models
{
    public class ClientProfile
    {
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public bool Notify { get; set; } = true;
        public int ID { get; set; }

        public IList<Complaint> Complaints { get; set; }

        public ClientProfile() { }
        public ClientProfile(string email)
        {
            Email = email;
        }
    }
}
