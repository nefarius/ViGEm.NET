using System;
using Nefarius.ViGEm.Client.Exceptions;

namespace Nefarius.ViGEm.Client
{
    using PVIGEM_TARGET = IntPtr;

    public abstract class ViGEmTarget : IDisposable
    {
        protected ViGEmTarget(ViGEmClient client)
        {
            Client = client;
        }

        protected ViGEmClient Client { get; }

        protected PVIGEM_TARGET NativeHandle { get; set; }

        public ushort VendorId { get; protected set; }

        public ushort ProductId { get; protected set; }

        public virtual void Connect()
        {
            if (VendorId > 0 && ProductId > 0)
            {
                ViGEmClient.vigem_target_set_vid(NativeHandle, VendorId);
                ViGEmClient.vigem_target_set_pid(NativeHandle, ProductId);
            }

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

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    Disconnect();

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                ViGEmClient.vigem_target_free(NativeHandle);

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~ViGEmTarget()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}