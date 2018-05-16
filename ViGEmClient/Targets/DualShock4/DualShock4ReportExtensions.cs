using System;

namespace Nefarius.ViGEm.Client.Targets.DualShock4
{
    public static class DualShock4ReportExtensions
    {
        public static void SetButtons(this DualShock4Report report, params DualShock4Buttons[] buttons)
        {
            foreach (var button in buttons)
            {
                report.Buttons |= (ushort)button;
            }
        }

        public static void SetButtonState(this DualShock4Report report, DualShock4Buttons button, bool state)
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

        public static void SetDPad(this DualShock4Report report, DualShock4DPadValues value)
        {
            report.Buttons &= unchecked((ushort)~0xF);
            report.Buttons |= (ushort)value;
        }

        public static void SetSpecialButtons(this DualShock4Report report, params DualShock4SpecialButtons[] buttons)
        {
            foreach (var button in buttons)
            {
                report.SpecialButtons |= (byte)button;
            }
        }

        public static void SetSpecialButtonState(this DualShock4Report report, DualShock4SpecialButtons button, bool state)
        {
            if (state)
            {
                report.SpecialButtons |= (byte)button;
            }
            else
            {
                report.SpecialButtons &= (byte)~button;
            }
        }

        public static void SetAxis(this DualShock4Report report, DualShock4Axes axis, byte value)
        {
            switch (axis)
            {
                case DualShock4Axes.LeftTrigger:
                    report.LeftTrigger = value;
                    break;
                case DualShock4Axes.RightTrigger:
                    report.RightTrigger = value;
                    break;
                case DualShock4Axes.LeftThumbX:
                    report.LeftThumbX = value;
                    break;
                case DualShock4Axes.LeftThumbY:
                    report.LeftThumbY = value;
                    break;
                case DualShock4Axes.RightThumbX:
                    report.RightThumbX = value;
                    break;
                case DualShock4Axes.RightThumbY:
                    report.RightThumbY = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }
        }
    }
}