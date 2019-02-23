//Legacy Functions

using System;

namespace Nefarius.ViGEm.Client.Targets.Xbox360
{
    [Obsolete]
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

        
    }
}