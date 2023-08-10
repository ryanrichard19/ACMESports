using System.Runtime.Serialization;

namespace ACMESportsAPI.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() { }
        public ForbiddenAccessException(string message) : base(message) { }
        public ForbiddenAccessException(string message, Exception inner) : base(message, inner) { }
        protected ForbiddenAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}