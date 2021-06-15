using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasoftTest.Data.DataContext
{
   public class MidasoftContext : IdentityDbContext<Users>
    {
        public MidasoftContext(DbContextOptions<MidasoftContext> options)
            : base(options)
        {
        }

        public  DbSet<Genre> Genres { get; set; }
        public  DbSet<IdentyficationType> IdentyficationTypes { get; set; }
        public  DbSet<FamilyGroup> FamilyGroups { get; set; }
    }
}
