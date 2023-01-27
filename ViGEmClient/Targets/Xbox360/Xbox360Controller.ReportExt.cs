namespace Nefarius.ViGEm.Client.Targets;

internal partial class Xbox360Controller
{
    public ref byte LeftTrigger => ref _nativeReport.bLeftTrigger;

    public ref byte RightTrigger => ref _nativeReport.bRightTrigger;

    public ref short LeftThumbX => ref _nativeReport.sThumbLX;

    public ref short LeftThumbY => ref _nativeReport.sThumbLY;

    public ref short RightThumbX => ref _nativeReport.sThumbRX;

    public ref short RightThumbY => ref _nativeReport.sThumbRY;

    public ref ushort ButtonState => ref _nativeReport.wButtons;

    public void SetButtonsFull(ushort buttons)
    {
        _nativeReport.wButtons = buttons;
    }
}