using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Exceptions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class VigemTargetNotPluggedInException : Exception
{
    public VigemTargetNotPluggedInException()
    {
    }

    public VigemTargetNotPluggedInException(string message)
        : base(message)
    {
    }

    public VigemTargetNotPluggedInException(string format, params object[] args)
        : base(string.Format(format, args))
    {
    }

    public VigemTargetNotPluggedInException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public VigemTargetNotPluggedInException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
    {
    }

    protected VigemTargetNotPluggedInException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}