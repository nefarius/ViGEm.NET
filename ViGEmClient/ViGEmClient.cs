using System;
using System.Diagnostics.CodeAnalysis;

using Nefarius.ViGEm.Client.Exceptions;

namespace Nefarius.ViGEm.Client;

using PVIGEM_CLIENT = IntPtr;

/// <summary>
///     Represents a managed gateway to a compatible emulation bus.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed partial class ViGEmClient : IDisposable
{
    [SuppressMessage("ReSharper", "SwitchStatementMissingSomeEnumCasesNoDefault")]
    public ViGEmClient()
    {
        NativeHandle = vigem_alloc();

        if (NativeHandle == IntPtr.Zero)
        {
            throw new VigemAllocFailedException();
        }

        VIGEM_ERROR error = vigem_connect(NativeHandle);

        switch (error)
        {
            case VIGEM_ERROR.VIGEM_ERROR_ALREADY_CONNECTED:
                throw new VigemAlreadyConnectedException();
            case VIGEM_ERROR.VIGEM_ERROR_BUS_NOT_FOUND:
                throw new VigemBusNotFoundException();
            case VIGEM_ERROR.VIGEM_ERROR_BUS_ACCESS_FAILED:
                throw new VigemBusAccessFailedException();
            case VIGEM_ERROR.VIGEM_ERROR_BUS_VERSION_MISMATCH:
                throw new VigemBusVersionMismatchException();
        }
    }

    /// <summary>
    ///     Gets the <see cref="PVIGEM_CLIENT" /> identifying the bus connection.
    /// </summary>
    internal PVIGEM_CLIENT NativeHandle { get; }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
    
    private void ReleaseUnmanagedResources()
    {
        vigem_disconnect(NativeHandle);
        vigem_free(NativeHandle);
    }

    ~ViGEmClient()
    {
        ReleaseUnmanagedResources();
    }
}