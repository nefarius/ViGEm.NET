using System;

namespace Nefarius.ViGEm.Client.Targets.DualShock4
{
    [Flags]
    public enum DualShock4Buttons : ushort
    {
        ThumbRight = 1 << 15,
        ThumbLeft = 1 << 14,
        Options = 1 << 13,
        Share = 1 << 12,
        TriggerRight = 1 << 11,
        TriggerLeft = 1 << 10,
        ShoulderRight = 1 << 9,
        ShoulderLeft = 1 << 8,
        Triangle = 1 << 7,
        Circle = 1 << 6,
        Cross = 1 << 5,
        Square = 1 << 4
    }

    [Flags]
    public enum DualShock4SpecialButtons : byte
    {
        Ps = 1 << 0,
        Touchpad = 1 << 1
    }

    public enum DualShock4Axes
    {
        LeftTrigger,
        RightTrigger,
        LeftThumbX,
        LeftThumbY,
        RightThumbX,
        RightThumbY
    }

    public enum DualShock4DPadValues
    {
        None = 0x8,
        Northwest = 0x7,
        West = 0x6,
        Southwest = 0x5,
        South = 0x4,
        Southeast = 0x3,
        East = 0x2,
        Northeast = 0x1,
        North = 0x0
    }

    public class DualShock4Report
    {
        public DualShock4Report()
        {
            Buttons &= unchecked((ushort)~0xF);
            Buttons |= 0x08;
            LeftThumbX = 0x80;
            LeftThumbY = 0x80;
            RightThumbX = 0x80;
            RightThumbY = 0x80;
        }

        public ushort Buttons { get; set; }

        public byte SpecialButtons { get; set; }

        public byte LeftTrigger { get; set; }

        public byte RightTrigger { get; set; }

        public byte LeftThumbX { get; set; }

        public byte LeftThumbY { get; set; }

        public byte RightThumbX { get; set; }

        public byte RightThumbY { get; set; }
    }
}
