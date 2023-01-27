namespace Nefarius.ViGEm.Client.Targets;

internal partial class DualShock4Controller
{
    public ref byte LeftTrigger => ref _nativeReport.bTriggerL;

    public ref byte RightTrigger => ref _nativeReport.bTriggerR;

    public ref byte LeftThumbX => ref _nativeReport.bThumbLX;

    public ref byte LeftThumbY => ref _nativeReport.bThumbLY;

    public ref byte RightThumbX => ref _nativeReport.bThumbRX;

    public ref byte RightThumbY => ref _nativeReport.bThumbRY;

    public void SetButtonsFull(ushort buttons)
    {
        _nativeReport.wButtons = buttons;
    }

    public void SetSpecialButtonsFull(byte buttons)
    {
        _nativeReport.bSpecial = buttons;
    }
}