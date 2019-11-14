using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Nefarius.ViGEm.Client.Targets.DualShock4
{
    public abstract class DualShock4Property : IComparable
    {
        [UsedImplicitly]
        protected DualShock4Property()
        {
        }

        protected DualShock4Property(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; }

        public int Id { get; }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((DualShock4Property) other).Id);
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : DualShock4Property
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as DualShock4Property;

            if (otherValue == null)
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        protected bool Equals(DualShock4Property other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    public abstract class DualShock4Button : DualShock4Property
    {
        public static DualShock4Button ThumbRight = new ThumbRightButton();
        public static DualShock4Button ThumbLeft = new ThumbLeftButton();
        public static DualShock4Button Options = new OptionsButton();
        public static DualShock4Button Share = new ShareButton();
        public static DualShock4Button TriggerRight = new TriggerRightButton();
        public static DualShock4Button TriggerLeft = new TriggerLeftButton();
        public static DualShock4Button ShoulderRight = new ShoulderRightButton();
        public static DualShock4Button ShoulderLeft = new ShoulderLeftButton();
        public static DualShock4Button Triangle = new TriangleButton();
        public static DualShock4Button Circle = new CircleButton();
        public static DualShock4Button Cross = new CrossButton();
        public static DualShock4Button Square = new SquareButton();

        protected DualShock4Button(int id, string name, ushort value)
            : base(id, name)
        {
            Value = value;
        }

        [IgnoreDataMember] public ushort Value { get; protected set; }

        private class ThumbRightButton : DualShock4Button
        {
            public ThumbRightButton() : base(0, "ThumbRight", 1 << 15)
            {
            }
        }

        private class ThumbLeftButton : DualShock4Button
        {
            public ThumbLeftButton() : base(1, "ThumbLeft", 1 << 14)
            {
            }
        }

        private class OptionsButton : DualShock4Button
        {
            public OptionsButton() : base(2, "Options", 1 << 13)
            {
            }
        }

        private class ShareButton : DualShock4Button
        {
            public ShareButton() : base(3, "Share", 1 << 12)
            {
            }
        }

        private class TriggerRightButton : DualShock4Button
        {
            public TriggerRightButton() : base(4, "TriggerRight", 1 << 11)
            {
            }
        }

        private class TriggerLeftButton : DualShock4Button
        {
            public TriggerLeftButton() : base(5, "TriggerLeft", 1 << 10)
            {
            }
        }

        private class ShoulderRightButton : DualShock4Button
        {
            public ShoulderRightButton() : base(6, "ShoulderRight", 1 << 9)
            {
            }
        }

        private class ShoulderLeftButton : DualShock4Button
        {
            public ShoulderLeftButton() : base(7, "ShoulderLeft", 1 << 8)
            {
            }
        }

        private class TriangleButton : DualShock4Button
        {
            public TriangleButton() : base(8, "Triangle", 1 << 7)
            {
            }
        }

        private class CircleButton : DualShock4Button
        {
            public CircleButton() : base(9, "Circle", 1 << 6)
            {
            }
        }

        private class CrossButton : DualShock4Button
        {
            public CrossButton() : base(10, "Cross", 1 << 5)
            {
            }
        }

        private class SquareButton : DualShock4Button
        {
            public SquareButton() : base(11, "Square", 1 << 4)
            {
            }
        }
    }

    public abstract class DualShock4SpecialButton : DualShock4Button
    {
        public static DualShock4SpecialButton Ps = new PsButton();
        public static DualShock4SpecialButton Touchpad = new TouchpadButton();

        protected DualShock4SpecialButton(int id, string name, ushort value) : base(id, name, value)
        {
            Value = value;
        }

        private class PsButton : DualShock4SpecialButton
        {
            public PsButton() : base(0, "PS", 1 << 0)
            {
            }
        }

        private class TouchpadButton : DualShock4SpecialButton
        {
            public TouchpadButton() : base(1, "Touchpad", 1 << 1)
            {
            }
        }
    }

    public abstract class DualShock4DPadDirection : DualShock4Property
    {
        public static DualShock4DPadDirection None = new NoneDPadDirection();
        public static DualShock4DPadDirection Northwest = new NorthwestDPadDirection();
        public static DualShock4DPadDirection West = new WestDPadDirection();
        public static DualShock4DPadDirection Southwest = new SouthwestDPadDirection();
        public static DualShock4DPadDirection South = new SouthDPadDirection();
        public static DualShock4DPadDirection Southeast = new SoutheastDPadDirection();
        public static DualShock4DPadDirection East = new EastDPadDirection();
        public static DualShock4DPadDirection Northeast = new NortheastDPadDirection();
        public static DualShock4DPadDirection North = new NorthDPadDirection();

        protected DualShock4DPadDirection(int id, string name, ushort value)
            : base(id, name)
        {
            Value = value;
        }

        [IgnoreDataMember] public ushort Value { get; }

        private class NoneDPadDirection : DualShock4DPadDirection
        {
            public NoneDPadDirection() : base(0, "None", 0x8)
            {
            }
        }

        private class NorthwestDPadDirection : DualShock4DPadDirection
        {
            public NorthwestDPadDirection() : base(1, "Northwest", 0x7)
            {
            }
        }

        private class WestDPadDirection : DualShock4DPadDirection
        {
            public WestDPadDirection() : base(2, "West", 0x6)
            {
            }
        }

        private class SouthwestDPadDirection : DualShock4DPadDirection
        {
            public SouthwestDPadDirection() : base(3, "Southwest", 0x5)
            {
            }
        }

        private class SouthDPadDirection : DualShock4DPadDirection
        {
            public SouthDPadDirection() : base(4, "South", 0x4)
            {
            }
        }

        private class SoutheastDPadDirection : DualShock4DPadDirection
        {
            public SoutheastDPadDirection() : base(5, "Southeast", 0x3)
            {
            }
        }

        private class EastDPadDirection : DualShock4DPadDirection
        {
            public EastDPadDirection() : base(6, "East", 0x2)
            {
            }
        }

        private class NortheastDPadDirection : DualShock4DPadDirection
        {
            public NortheastDPadDirection() : base(7, "Northeast", 0x1)
            {
            }
        }

        private class NorthDPadDirection : DualShock4DPadDirection
        {
            public NorthDPadDirection() : base(8, "North", 0x0)
            {
            }
        }
    }

    public abstract class DualShock4Axis : DualShock4Property
    {
        public static DualShock4Axis LeftThumbX = new LeftThumbXAxis();
        public static DualShock4Axis LeftThumbY = new LeftThumbYAxis();
        public static DualShock4Axis RightThumbX = new RightThumbXAxis();
        public static DualShock4Axis RightThumbY = new RightThumbYAxis();

        protected DualShock4Axis(int id, string name)
            : base(id, name)
        {
        }

        private class LeftThumbXAxis : DualShock4Axis
        {
            public LeftThumbXAxis() : base(0, "LeftThumbX")
            {
            }
        }

        private class LeftThumbYAxis : DualShock4Axis
        {
            public LeftThumbYAxis() : base(1, "LeftThumbY")
            {
            }
        }

        private class RightThumbXAxis : DualShock4Axis
        {
            public RightThumbXAxis() : base(2, "RightThumbX")
            {
            }
        }

        private class RightThumbYAxis : DualShock4Axis
        {
            public RightThumbYAxis() : base(3, "RightThumbY")
            {
            }
        }
    }

    public abstract class DualShock4Slider : DualShock4Property
    {
        public static DualShock4Slider LeftTrigger = new LeftTriggerSlider();
        public static DualShock4Slider RightTrigger = new RightTriggerSlider();

        protected DualShock4Slider(int id, string name)
            : base(id, name)
        {
        }

        private class LeftTriggerSlider : DualShock4Slider
        {
            public LeftTriggerSlider() : base(0, "LeftTrigger")
            {
            }
        }

        private class RightTriggerSlider : DualShock4Slider
        {
            public RightTriggerSlider() : base(1, "RightTrigger")
            {
            }
        }
    }
}