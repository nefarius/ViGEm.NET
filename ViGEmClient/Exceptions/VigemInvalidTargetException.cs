using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class VigemInvalidTargetException : Exception
{
    public VigemInvalidTargetException()
    {
    }

    public VigemInvalidTargetException(string message)
        : base(message)
    {
    }

    public VigemInvalidTargetException(string format, params object[] args)
        : base(string.Format(format, args))
    {
    }

    public VigemInvalidTargetException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public VigemInvalidTargetException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
    {
    }

    protected VigemInvalidTargetException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}