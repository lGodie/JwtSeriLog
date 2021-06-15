using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasoftTest.Data.DataContext
{
    public class FamilyGroup 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public Guid? GenreId { get; set; }
        public Guid? IdentyficationTypeId { get; set; }
        public string Relationship { get; set; }
        public short Age { get; set; }
        public DateTime? BirthDate { get; set; } 
        public bool? younger { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; } = DateTime.Now;
        public virtual Genre Genre { get; set; }
        public virtual IdentyficationType IdentyficationType { get; set; }

    }
}
