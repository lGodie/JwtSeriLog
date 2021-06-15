using System;
using System.Collections.Generic;
using System.Text;

namespace MidasoftTest.Common.Requests
{
    public class FamilyGroupResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Relationship { get; set; }
        public short Age { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? younger { get; set; }

    }
}
