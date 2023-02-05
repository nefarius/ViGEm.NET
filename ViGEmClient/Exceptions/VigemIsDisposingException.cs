using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class VigemIsDisposingException : Exception
{
    public VigemIsDisposingException()
    {
    }

    public VigemIsDisposingException(string message)
        : base(message)
    {
    }

    public VigemIsDisposingException(string format, params object[] args)
        : base(string.Format(format, args))
    {
    }

    public VigemIsDisposingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public VigemIsDisposingException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
    {
    }

    protected VigemIsDisposingException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}