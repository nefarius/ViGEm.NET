using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class VigemBusAccessFailedException : Exception
{
    public VigemBusAccessFailedException()
    {
    }

    public VigemBusAccessFailedException(string message)
        : base(message)
    {
    }

    public VigemBusAccessFailedException(string format, params object[] args)
        : base(string.Format(format, args))
    {
    }

    public VigemBusAccessFailedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public VigemBusAccessFailedException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
    {
    }

    protected VigemBusAccessFailedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}