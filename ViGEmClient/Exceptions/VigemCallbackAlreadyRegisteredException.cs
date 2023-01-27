using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class VigemCallbackAlreadyRegisteredException : Exception
{
    public VigemCallbackAlreadyRegisteredException()
    {
    }

    public VigemCallbackAlreadyRegisteredException(string message)
        : base(message)
    {
    }

    public VigemCallbackAlreadyRegisteredException(string format, params object[] args)
        : base(string.Format(format, args))
    {
    }

    public VigemCallbackAlreadyRegisteredException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public VigemCallbackAlreadyRegisteredException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
    {
    }

    protected VigemCallbackAlreadyRegisteredException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}