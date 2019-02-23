using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CWAC19AcluMo.Models;

namespace CWAC19AcluMo.Data
{
    public class CWAC19AcluMoContext : DbContext
    {
        public CWAC19AcluMoContext (DbContextOptions<CWAC19AcluMoContext> options)
            : base(options)
        {
        }

        public DbSet<CWAC19AcluMo.Models.ClientProfile> ClientProfiles { get; set; }
        public DbSet<CWAC19AcluMo.Models.ComplaintStatus> ComplaintStatuses { get; set; }
        public DbSet<CWAC19AcluMo.Models.Complaint> Complaints { get; set; }
    }
}
