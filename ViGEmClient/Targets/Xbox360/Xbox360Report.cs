using System;

namespace Nefarius.ViGEm.Client.Targets.Xbox360
{
    [Obsolete]
    public class Xbox360Report
    {
        /// <summary>
        ///     Bitmask of the device digital buttons.
        ///     A set bit indicates that the corresponding button is pressed.
        /// </summary>
        public ushort Buttons { get; set; }

        /// <summary>
        ///     The current value of the left trigger analog control. The value is between 0 and 255.
        /// </summary>
        public byte LeftTrigger { get; set; }

        /// <summary>
        ///     The current value of the right trigger analog control. The value is between 0 and 255.
        /// </summary>
        public byte RightTrigger { get; set; }

        /// <summary>
        ///     Left thumbstick x-axis value.
        ///     Each of the thumbstick axis members is a signed value between -32768 and 32767 describing the position of the
        ///     thumbstick. A value of 0 is centered. Negative values signify down or to the left. Positive values signify up or to
        ///     the right.
        /// </summary>
        public short LeftThumbX { get; set; }

        /// <summary>
        ///     Left thumbstick y-axis value.
        ///     Each of the thumbstick axis members is a signed value between -32768 and 32767 describing the position of the
        ///     thumbstick. A value of 0 is centered. Negative values signify down or to the left. Positive values signify up or to
        ///     the right.
        /// </summary>
        public short LeftThumbY { get; set; }

        /// <summary>
        ///     Right thumbstick x-axis value.
        ///     Each of the thumbstick axis members is a signed value between -32768 and 32767 describing the position of the
        ///     thumbstick. A value of 0 is centered. Negative values signify down or to the left. Positive values signify up or to
        ///     the right.
        /// </summary>
        public short RightThumbX { get; set; }

        /// <summary>
        ///     Right thumbstick y-axis value.
        ///     Each of the thumbstick axis members is a signed value between -32768 and 32767 describing the position of the
        ///     thumbstick. A value of 0 is centered. Negative values signify down or to the left. Positive values signify up or to
        ///     the right.
        /// </summary>
        public short RightThumbY { get; set; }
    }
}