using System;

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
        }

        public virtual void Disconnect()
        {
            var error = ViGEmClient.vigem_target_remove(Client.NativeHandle, NativeHandle);
        }
    }
}
