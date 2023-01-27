using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class VigemBusNotFoundException : Exception
{
    public VigemBusNotFoundException()
    {
    }

    public VigemBusNotFoundException(string message)
        : base(message)
    {
    }

    public VigemBusNotFoundException(string format, params object[] args)
        : base(string.Format(format, args))
    {
    }

    public VigemBusNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public VigemBusNotFoundException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
    {
    }

    protected VigemBusNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}