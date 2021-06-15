using System;
using System.Collections.Generic;
using System.Text;

namespace MidasoftTest.Common.Responses
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }
    }
}
