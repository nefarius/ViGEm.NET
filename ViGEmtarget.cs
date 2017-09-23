using System;
using Nefarius.ViGEm.Client.Exceptions;

namespace Nefarius.ViGEm.Client
{
    using PVIGEM_TARGET = IntPtr;

    public abstract class ViGEmTarget
    {
        protected ViGEmClient Client { get; }

        protected PVIGEM_TARGET NativeHandle { get; set; }

        protected ViGEmTarget(ViGEmClient client)
        {
            Client = client;
        }

        ~ViGEmTarget()
        {
            ViGEmClient.vigem_target_free(NativeHandle);
        }

        public virtual void Connect()
        {
            var error = ViGEmClient.vigem_target_add(Client.NativeHandle, NativeHandle);

            switch (error)
            {
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_BUS_NOT_FOUND:
                    throw new VigemBusNotFoundException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_TARGET_UNINITIALIZED:
                    throw new VigemTargetUninitializedException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_ALREADY_CONNECTED:
                    throw new VigemAlreadyConnectedException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_NO_FREE_SLOT:
                    throw new VigemNoFreeSlotException();
            }
        }

        public virtual void Disconnect()
        {
            var error = ViGEmClient.vigem_target_remove(Client.NativeHandle, NativeHandle);

            switch (error)
            {
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_BUS_NOT_FOUND:
                    throw new VigemBusNotFoundException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_TARGET_UNINITIALIZED:
                    throw new VigemTargetUninitializedException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_TARGET_NOT_PLUGGED_IN:
                    throw new VigemTargetNotPluggedInException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_REMOVAL_FAILED:
                    throw new VigemRemovalFailedException();
            }
        }
    }
}
