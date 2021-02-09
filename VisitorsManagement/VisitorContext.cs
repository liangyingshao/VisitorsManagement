using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorsManagement.Models;

namespace VisitorsManagement
{
    public class VisitorContext:DbContext
    {
        public VisitorContext(DbContextOptions<VisitorContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
