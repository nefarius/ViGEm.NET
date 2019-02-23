using System;

namespace Nefarius.ViGEm.Client.Targets.Xbox360
{
    [Flags]
    public enum Xbox360Button : ushort
    {
        Up = 0x0001,
        Down = 0x0002,
        Left = 0x0004,
        Right = 0x0008,
        Start = 0x0010,
        Back = 0x0020,
        LeftThumb = 0x0040,
        RightThumb = 0x0080,
        LeftShoulder = 0x0100,
        RightShoulder = 0x0200,
        Guide = 0x0400,
        A = 0x1000,
        B = 0x2000,
        X = 0x4000,
        Y = 0x8000
    }

    public enum Xbox360Axis
    {
        LeftTrigger,
        RightTrigger,
        LeftThumbX,
        LeftThumbY,
        RightThumbX,
        RightThumbY
    }

    public enum Xbox360Slider
    {
        LeftTrigger,
        RightTrigger
    }
}