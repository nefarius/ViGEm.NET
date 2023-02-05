using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;
#pragma warning disable IDE1006

namespace Nefarius.ViGEm.Client;

using PVIGEM_CLIENT = IntPtr;
using PVIGEM_TARGET = IntPtr;
using PVIGEM_TARGET_ADD_RESULT = IntPtr;
using PVIGEM_USER_DATA = IntPtr;

[SuppressUnmanagedCodeSecurity]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public sealed partial class ViGEmClient
{
    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern PVIGEM_CLIENT vigem_alloc();

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern void vigem_free(
        PVIGEM_CLIENT vigem);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern VIGEM_ERROR vigem_connect(
        PVIGEM_CLIENT vigem);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern void vigem_disconnect(
        PVIGEM_CLIENT vigem);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern PVIGEM_TARGET vigem_target_x360_alloc();

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern PVIGEM_TARGET vigem_target_ds4_alloc();

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void vigem_target_free(
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern VIGEM_ERROR vigem_target_add(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern VIGEM_ERROR vigem_target_add_async(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target,
        PVIGEM_TARGET_ADD_RESULT result);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern VIGEM_ERROR vigem_target_remove(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern VIGEM_ERROR vigem_target_x360_register_notification(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target,
        PVIGEM_X360_NOTIFICATION notification);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern VIGEM_ERROR vigem_target_ds4_register_notification(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target,
        PVIGEM_DS4_NOTIFICATION notification);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void vigem_target_x360_unregister_notification(
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void vigem_target_ds4_unregister_notification(
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void vigem_target_set_vid(
        PVIGEM_TARGET target,
        ushort vid);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void vigem_target_set_pid(
        PVIGEM_TARGET target,
        ushort pid);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern ushort vigem_target_get_vid(
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern ushort vigem_target_get_pid(
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern VIGEM_ERROR vigem_target_x360_update(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target,
        XUSB_REPORT report);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern VIGEM_ERROR vigem_target_ds4_update(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target,
        DS4_REPORT report);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern VIGEM_ERROR vigem_target_ds4_update_ex(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target,
        DS4_REPORT_EX report);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern uint vigem_target_get_index(
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern VIGEM_TARGET_TYPE vigem_target_get_type(
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool vigem_target_is_attached(
        PVIGEM_TARGET target);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    private static extern VIGEM_ERROR vigem_target_x360_get_user_index(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target,
        out UInt32 index);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern VIGEM_ERROR vigem_target_ds4_await_output_report(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target,
        ref DS4_AWAIT_OUTPUT_BUFFER buffer);

    [DllImport("vigemclient.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
    internal static extern VIGEM_ERROR vigem_target_ds4_await_output_report_timeout(
        PVIGEM_CLIENT vigem,
        PVIGEM_TARGET target,
        UInt32 milliseconds,
        ref DS4_AWAIT_OUTPUT_BUFFER buffer);

    internal enum VIGEM_ERROR : UInt32
    {
        VIGEM_ERROR_NONE = 0x20000000,
        VIGEM_ERROR_BUS_NOT_FOUND = 0xE0000001,
        VIGEM_ERROR_NO_FREE_SLOT = 0xE0000002,
        VIGEM_ERROR_INVALID_TARGET = 0xE0000003,
        VIGEM_ERROR_REMOVAL_FAILED = 0xE0000004,
        VIGEM_ERROR_ALREADY_CONNECTED = 0xE0000005,
        VIGEM_ERROR_TARGET_UNINITIALIZED = 0xE0000006,
        VIGEM_ERROR_TARGET_NOT_PLUGGED_IN = 0xE0000007,
        VIGEM_ERROR_BUS_VERSION_MISMATCH = 0xE0000008,
        VIGEM_ERROR_BUS_ACCESS_FAILED = 0xE0000009,
        VIGEM_ERROR_CALLBACK_ALREADY_REGISTERED = 0xE0000010,
        VIGEM_ERROR_CALLBACK_NOT_FOUND = 0xE0000011,
        VIGEM_ERROR_BUS_ALREADY_CONNECTED = 0xE0000012,
        VIGEM_ERROR_BUS_INVALID_HANDLE = 0xE0000013,
        VIGEM_ERROR_XUSB_USERINDEX_OUT_OF_RANGE = 0xE0000014,
        VIGEM_ERROR_INVALID_PARAMETER = 0xE0000015,
        VIGEM_ERROR_NOT_SUPPORTED = 0xE0000016,
        VIGEM_ERROR_WINAPI = 0xE0000017,
        VIGEM_ERROR_TIMED_OUT = 0xE0000018,
        VIGEM_ERROR_IS_DISPOSING = 0xE0000019,
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct XUSB_REPORT
    {
        public ushort wButtons;
        public byte bLeftTrigger;
        public byte bRightTrigger;
        public short sThumbLX;
        public short sThumbLY;
        public short sThumbRX;
        public short sThumbRY;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DS4_REPORT
    {
        public byte bThumbLX;
        public byte bThumbLY;
        public byte bThumbRX;
        public byte bThumbRY;
        public ushort wButtons;
        public byte bSpecial;
        public byte bTriggerL;
        public byte bTriggerR;
    }

    /// <summary>
    ///     TODO: populate actual fields
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DS4_REPORT_EX
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 63)]
        public byte[] Report;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    internal struct DS4_AWAIT_OUTPUT_BUFFER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] Buffer;
    }

    internal enum VIGEM_TARGET_TYPE : UInt32
    {
        // 
        // Microsoft Xbox 360 Controller (wired)
        // 
        Xbox360Wired = 0,
        
        //
        // Sony DualShock 4 (wired)
        // 
        DualShock4Wired = 2
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DS4_LIGHTBAR_COLOR
    {
        public byte Red;
        public byte Green;
        public byte Blue;
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void PVIGEM_X360_NOTIFICATION(
        PVIGEM_CLIENT Client,
        PVIGEM_TARGET Target,
        byte LargeMotor,
        byte SmallMotor,
        byte LedNumber,
        PVIGEM_USER_DATA UserData);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void PVIGEM_DS4_NOTIFICATION(
        PVIGEM_CLIENT Client,
        PVIGEM_TARGET Target,
        byte LargeMotor,
        byte SmallMotor,
        DS4_LIGHTBAR_COLOR LightbarColor,
        PVIGEM_USER_DATA UserData);
}