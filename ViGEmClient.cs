using System;

namespace Nefarius.ViGEm.Client
{
    using PVIGEM_CLIENT = IntPtr;

    public partial class ViGEmClient
    {
        public ViGEmClient()
        {
            NativeHandle = vigem_alloc();
            vigem_connect(NativeHandle);
        }

        internal PVIGEM_CLIENT NativeHandle { get; }

        ~ViGEmClient()
        {
            vigem_disconnect(NativeHandle);
            vigem_free(NativeHandle);
        }
    }
}