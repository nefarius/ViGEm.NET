using System;
using Nefarius.ViGEm.Client.Exceptions;

namespace Nefarius.ViGEm.Client
{
    using PVIGEM_CLIENT = IntPtr;

    public partial class ViGEmClient
    {
        public ViGEmClient()
        {
            NativeHandle = vigem_alloc();
            var error = vigem_connect(NativeHandle);

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

        internal PVIGEM_CLIENT NativeHandle { get; }

        ~ViGEmClient()
        {
            vigem_disconnect(NativeHandle);
            vigem_free(NativeHandle);
        }
    }
}