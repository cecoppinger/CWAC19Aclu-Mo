using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CWAC19AcluMo.ViewModels
{
    public class SendNotificationViewModel
    {
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        [Required]
        public string StatusCode { get; set; }
    }
}
