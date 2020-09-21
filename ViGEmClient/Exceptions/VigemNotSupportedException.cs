using System;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions
{
    [Serializable]
    public class VigemNotSupportedException : Exception
    {
        public VigemNotSupportedException()
        {
        }

        public VigemNotSupportedException(string message)
            : base(message)
        {
        }

        public VigemNotSupportedException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public VigemNotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public VigemNotSupportedException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected VigemNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}