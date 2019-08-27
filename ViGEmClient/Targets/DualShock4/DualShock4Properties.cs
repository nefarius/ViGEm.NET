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
            return Id.CompareTo(((DualShock4.DualShock4Property)other).Id);
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : DualShock4.DualShock4Property
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as DualShock4.DualShock4Property;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }
    }

    public abstract class DualShock4Button : DualShock4Property
    {
        public static DualShock4.DualShock4Button ThumbRight = new DualShock4.DualShock4Button.ThumbRightButton();
        public static DualShock4.DualShock4Button ThumbLeft = new DualShock4.DualShock4Button.ThumbLeftButton();
        public static DualShock4.DualShock4Button Options = new DualShock4.DualShock4Button.OptionsButton();
        public static DualShock4.DualShock4Button Share = new DualShock4.DualShock4Button.ShareButton();
        public static DualShock4.DualShock4Button TriggerRight = new DualShock4.DualShock4Button.TriggerRightButton();
        public static DualShock4.DualShock4Button TriggerLeft = new DualShock4.DualShock4Button.TriggerLeftButton();
        public static DualShock4.DualShock4Button ShoulderRight = new DualShock4.DualShock4Button.ShoulderRightButton();
        public static DualShock4.DualShock4Button ShoulderLeft = new DualShock4.DualShock4Button.ShoulderLeftButton();
        public static DualShock4.DualShock4Button Triangle = new DualShock4.DualShock4Button.TriangleButton();
        public static DualShock4.DualShock4Button Circle = new DualShock4.DualShock4Button.CircleButton();
        public static DualShock4.DualShock4Button Cross = new DualShock4.DualShock4Button.CrossButton();
        public static DualShock4.DualShock4Button Square = new DualShock4.DualShock4Button.SquareButton();
        public static DualShock4.DualShock4Button Ps = new DualShock4.DualShock4Button.PsButton();
        public static DualShock4.DualShock4Button Touchpad = new DualShock4.DualShock4Button.TouchpadButton();

        protected DualShock4Button(int id, string name, ushort value)
            : base(id, name)
        {
            Value = value;
        }

        [IgnoreDataMember]
        public ushort Value { get; }

        private class ThumbRightButton : DualShock4.DualShock4Button
        {
            public ThumbRightButton() : base(0, "ThumbRight", 1 << 15)
            {
            }
        }

        private class ThumbLeftButton : DualShock4.DualShock4Button
        {
            public ThumbLeftButton() : base(1, "ThumbLeft", 1 << 14)
            {
            }
        }

        private class OptionsButton : DualShock4.DualShock4Button
        {
            public OptionsButton() : base(2, "Options", 1 << 13)
            {
            }
        }

        private class ShareButton : DualShock4.DualShock4Button
        {
            public ShareButton() : base(3, "Share", 1 << 12)
            {
            }
        }

        private class TriggerRightButton : DualShock4.DualShock4Button
        {
            public TriggerRightButton() : base(4, "TriggerRight", 1 << 11)
            {
            }
        }

        private class TriggerLeftButton : DualShock4.DualShock4Button
        {
            public TriggerLeftButton() : base(5, "TriggerLeft", 1 << 10)
            {
            }
        }

        private class ShoulderRightButton : DualShock4.DualShock4Button
        {
            public ShoulderRightButton() : base(6, "ShoulderRight", 1 << 9)
            {
            }
        }

        private class ShoulderLeftButton : DualShock4.DualShock4Button
        {
            public ShoulderLeftButton() : base(7, "ShoulderLeft", 1 << 8)
            {
            }
        }

        private class TriangleButton : DualShock4.DualShock4Button
        {
            public TriangleButton() : base(8, "Triangle", 1 << 7)
            {
            }
        }

        private class CircleButton : DualShock4.DualShock4Button
        {
            public CircleButton() : base(9, "Circle", 1 << 6)
            {
            }
        }

        private class CrossButton : DualShock4.DualShock4Button
        {
            public CrossButton() : base(10, "Cross", 1 << 5)
            {
            }
        }

        private class SquareButton : DualShock4.DualShock4Button
        {
            public SquareButton() : base(11, "Square", 1 << 4)
            {
            }
        }

        private class PsButton : DualShock4.DualShock4Button
        {
            public PsButton() : base(12, "PS", 1 << 0)
            {
            }
        }

        private class TouchpadButton : DualShock4.DualShock4Button
        {
            public TouchpadButton() : base(13, "Touchpad", 1 << 1)
            {
            }
        }
    }
}
