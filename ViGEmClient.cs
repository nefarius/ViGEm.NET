using System;

namespace Nefarius.ViGEm.Client
{
    using PVIGEM_CLIENT = IntPtr;
    using PVIGEM_TARGET = IntPtr;
    using PVIGEM_TARGET_ADD_RESULT = IntPtr;
    using PVIGEM_X360_NOTIFICATION = IntPtr;
    using PVIGEM_DS4_NOTIFICATION = IntPtr;

    public partial class ViGEmClient
    {
        internal PVIGEM_CLIENT NativeHandle { get; }

        public ViGEmClient()
        {
            NativeHandle = vigem_alloc();
            vigem_connect(NativeHandle);
        }

        ~ViGEmClient()
        {
            vigem_disconnect(NativeHandle);
            vigem_free(NativeHandle);
        }
    }
}
