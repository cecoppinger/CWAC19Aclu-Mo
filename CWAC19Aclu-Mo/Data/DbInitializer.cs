using CWAC19AcluMo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWAC19AcluMo.Data
{
    public class DbInitializer
    {
        public static void Initialize(CWAC19AcluMoContext context)
        {
            context.Database.EnsureCreated();

            if(context.ComplaintStatuses.Count() == 0)
            {
                ComplaintStatus complaint1 = new ComplaintStatus
                {
                    StatusCode = "Received",
                    Description = "Your complaint was received"
                };

                ComplaintStatus complaint2 = new ComplaintStatus
                {
                    StatusCode = "Under Review",
                    Description = "Something somthing under review"
                };

                ComplaintStatus complaint3 = new ComplaintStatus
                {
                    StatusCode = "Fact-Finding",
                    Description = "Finding the facts, yo"
                };

                context.Add(complaint1);
                context.Add(complaint2);
                context.Add(complaint3);
                context.SaveChanges();
            }
        }
    }
}
