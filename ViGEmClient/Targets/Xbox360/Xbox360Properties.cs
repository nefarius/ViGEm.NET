using System;

namespace Nefarius.ViGEm.Client.Targets.Xbox360
{
    /// <summary>
    ///     Possible identifiers for digital (two-state) buttons on an Xbox 360 gamepad surface. These can be combined as
    ///     flags.
    /// </summary>
    /// <remarks>
    ///     The directional pad button combinations are not validate and sent as received. The caller is responsible to
    ///     make sure that no opposing values get submitted (e.g. on a physical pad pressing both up and down at the same time
    ///     wouldn't be possible while a virtual pad would just pass them through).
    /// </remarks>
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

    /// <summary>
    ///     Describes the axes of an Xbox 360 pad. The related valid value range is between -32768 and 32767 where 0 is the
    ///     centered position.
    /// </summary>
    public enum Xbox360Axis
    {
        LeftThumbX,
        LeftThumbY,
        RightThumbX,
        RightThumbY
    }

    /// <summary>
    ///     Describes the sliders of an Xbox 360 pad. A slider typically has a value of 0 when in its resting position and
    ///     can report a maximum of 255 when fully engaged (e.g. pressed down).
    /// </summary>
    public enum Xbox360Slider
    {
        LeftTrigger,
        RightTrigger
    }
}