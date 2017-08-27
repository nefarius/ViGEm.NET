using System;
using System.Runtime.InteropServices;

namespace ViGEmClient
{
    internal partial class SetupApi
    {
        public static bool FindDevice(Guid target, ref string path, int instance = 0)
        {
            var detailDataBuffer = IntPtr.Zero;
            var deviceInfoSet = IntPtr.Zero;

            try
            {
                SP_DEVICE_INTERFACE_DATA deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA(),
                    da = new SP_DEVICE_INTERFACE_DATA();
                int bufferSize = 0, memberIndex = 0;

                deviceInfoSet = SetupDiGetClassDevs(ref target, IntPtr.Zero, IntPtr.Zero,
                    DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);

                deviceInterfaceData.cbSize = da.cbSize = Marshal.SizeOf(deviceInterfaceData);

                while (SetupDiEnumDeviceInterfaces(deviceInfoSet, IntPtr.Zero, ref target, memberIndex,
                    ref deviceInterfaceData))
                {
                    SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref deviceInterfaceData, IntPtr.Zero, 0,
                        ref bufferSize, ref da);
                    {
                        detailDataBuffer = Marshal.AllocHGlobal(bufferSize);

                        Marshal.WriteInt32(detailDataBuffer,
                            (IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8);

                        if (SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref deviceInterfaceData, detailDataBuffer,
                            bufferSize, ref bufferSize, ref da))
                        {
                            var pDevicePathName = detailDataBuffer + 4;

                            path = (Marshal.PtrToStringAuto(pDevicePathName) ?? "ERROR").ToUpper();
                            Marshal.FreeHGlobal(detailDataBuffer);

                            if (memberIndex == instance) return true;
                        }
                        else Marshal.FreeHGlobal(detailDataBuffer);
                    }

                    memberIndex++;
                }
            }
            finally
            {
                if (deviceInfoSet != IntPtr.Zero)
                {
                    SetupDiDestroyDeviceInfoList(deviceInfoSet);
                }
            }

            return false;
        }
    }
}
