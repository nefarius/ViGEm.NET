//Legacy Functions

using System;

namespace Nefarius.ViGEm.Client.Targets.Xbox360
{
    public static class Xbox360ReportExtensions
    {
        public static void SetButtons(this Xbox360Report report, params Xbox360Button[] buttons)
        {
            foreach (var button in buttons)
            {
                report.Buttons |= (ushort)button;
            }
        }

        public static void SetButtonsFull(this Xbox360Report report, Xbox360Button buttons)
        {
            report.Buttons = (ushort)buttons;
        }

        public static void SetButtonState(this Xbox360Report report, Xbox360Button button, bool state)
        {
            if (state)
            {
                report.Buttons |= (ushort)button;
            }
            else
            {
                report.Buttons &= (ushort)~button;
            }
        }

        public static void SetAxis(this Xbox360Report report, Xbox360Axis axis, short value)
        {
            switch (axis)
            {
                case Xbox360Axis.LeftTrigger:
                    report.LeftTrigger = (byte)value;
                    break;
                case Xbox360Axis.RightTrigger:
                    report.RightTrigger = (byte)value;
                    break;
                case Xbox360Axis.LeftThumbX:
                    report.LeftThumbX = value;
                    break;
                case Xbox360Axis.LeftThumbY:
                    report.LeftThumbY = value;
                    break;
                case Xbox360Axis.RightThumbX:
                    report.RightThumbX = value;
                    break;
                case Xbox360Axis.RightThumbY:
                    report.RightThumbY = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }
        }
    }
}