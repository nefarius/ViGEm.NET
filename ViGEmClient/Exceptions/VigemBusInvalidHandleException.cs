using System;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions
{
    [Serializable]
    public class VigemBusInvalidHandleException : Exception
    {
        public VigemBusInvalidHandleException()
            : base() { }

        public VigemBusInvalidHandleException(string message)
            : base(message) { }

        public VigemBusInvalidHandleException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public VigemBusInvalidHandleException(string message, Exception innerException)
            : base(message, innerException) { }

        public VigemBusInvalidHandleException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected VigemBusInvalidHandleException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}