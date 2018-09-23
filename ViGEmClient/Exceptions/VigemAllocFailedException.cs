using System;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions
{
    public class VigemAllocFailedException : Exception
    {
        public VigemAllocFailedException()
        {
        }

        public VigemAllocFailedException(string message) : base(message)
        {
        }

        public VigemAllocFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VigemAllocFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}