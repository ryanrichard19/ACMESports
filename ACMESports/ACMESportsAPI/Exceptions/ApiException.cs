﻿using System.Runtime.Serialization;

namespace ACMESportsAPI.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception inner) : base(message, inner) { }
        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
