using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class VigemNoFreeSlotException : Exception
{
    public VigemNoFreeSlotException()
    {
    }

    public VigemNoFreeSlotException(string message)
        : base(message)
    {
    }

    public VigemNoFreeSlotException(string format, params object[] args)
        : base(string.Format(format, args))
    {
    }

    public VigemNoFreeSlotException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public VigemNoFreeSlotException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
    {
    }

    protected VigemNoFreeSlotException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}