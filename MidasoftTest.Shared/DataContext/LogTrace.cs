using System;

namespace MidasoftTest.Data.DataContext
{
    public class LogTrace
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}