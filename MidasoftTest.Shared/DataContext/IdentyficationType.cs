using System;

namespace MidasoftTest.Data.DataContext
{
    public class IdentyficationType
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; } = DateTime.Now;
    }
}