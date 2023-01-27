using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class VigemBusVersionMismatchException : Exception
{
    public VigemBusVersionMismatchException()
    {
    }

    public VigemBusVersionMismatchException(string message)
        : base(message)
    {
    }

    public VigemBusVersionMismatchException(string format, params object[] args)
        : base(string.Format(format, args))
    {
    }

    public VigemBusVersionMismatchException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public VigemBusVersionMismatchException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
    {
    }

    protected VigemBusVersionMismatchException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}