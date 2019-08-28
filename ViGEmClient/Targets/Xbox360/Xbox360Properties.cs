using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Nefarius.ViGEm.Client.Targets.Xbox360
{
    /// <summary>
    ///     Describes a modifiable property of an <see cref="Xbox360Controller" /> object.
    /// </summary>
    public abstract class Xbox360Property : IComparable
    {
        [UsedImplicitly]
        protected Xbox360Property()
        {
        }

        protected Xbox360Property(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; }

        public int Id { get; }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((Xbox360Property) other).Id);
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Xbox360Property
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Xbox360Property;

            if (otherValue == null)
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        protected bool Equals(Xbox360Property other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    /// <summary>
    ///     Possible identifiers for digital (two-state) buttons on an <see cref="Xbox360Controller" /> surface.
    /// </summary>
    /// <remarks>
    ///     The directional pad button combinations are not validate and sent as received. The caller is responsible to
    ///     make sure that no opposing values get submitted (e.g. on a physical pad pressing both up and down at the same time
    ///     wouldn't be possible while a virtual pad would just pass them through).
    /// </remarks>
    public abstract class Xbox360Button : Xbox360Property
    {
        public static Xbox360Button Up = new UpButton();
        public static Xbox360Button Down = new DownButton();
        public static Xbox360Button Left = new LeftButton();
        public static Xbox360Button Right = new RightButton();
        public static Xbox360Button Start = new StartButton();
        public static Xbox360Button Back = new BackButton();
        public static Xbox360Button LeftThumb = new LeftThumbButton();
        public static Xbox360Button RightThumb = new RightThumbButton();
        public static Xbox360Button LeftShoulder = new LeftShoulderButton();
        public static Xbox360Button RightShoulder = new RightShoulderButton();
        public static Xbox360Button Guide = new GuideButton();
        public static Xbox360Button A = new AButton();
        public static Xbox360Button B = new BButton();
        public static Xbox360Button X = new XButton();
        public static Xbox360Button Y = new YButton();

        protected Xbox360Button(int id, string name, ushort value)
            : base(id, name)
        {
            Value = value;
        }

        [IgnoreDataMember]
        public ushort Value { get; }

        private class UpButton : Xbox360Button
        {
            public UpButton() : base(0, "Up", 0x0001)
            {
            }
        }

        private class DownButton : Xbox360Button
        {
            public DownButton() : base(1, "Down", 0x0002)
            {
            }
        }

        private class LeftButton : Xbox360Button
        {
            public LeftButton() : base(2, "Left", 0x0004)
            {
            }
        }

        private class RightButton : Xbox360Button
        {
            public RightButton() : base(3, "Right", 0x0008)
            {
            }
        }

        private class StartButton : Xbox360Button
        {
            public StartButton() : base(4, "Start", 0x0010)
            {
            }
        }

        private class BackButton : Xbox360Button
        {
            public BackButton() : base(5, "Back", 0x0020)
            {
            }
        }

        private class LeftThumbButton : Xbox360Button
        {
            public LeftThumbButton() : base(6, "LeftThumb", 0x0040)
            {
            }
        }

        private class RightThumbButton : Xbox360Button
        {
            public RightThumbButton() : base(7, "RightThumb", 0x0080)
            {
            }
        }

        private class LeftShoulderButton : Xbox360Button
        {
            public LeftShoulderButton() : base(8, "LeftShoulder", 0x0100)
            {
            }
        }

        private class RightShoulderButton : Xbox360Button
        {
            public RightShoulderButton() : base(9, "RightShoulder", 0x0200)
            {
            }
        }

        private class GuideButton : Xbox360Button
        {
            public GuideButton() : base(10, "Guide", 0x0400)
            {
            }
        }

        private class AButton : Xbox360Button
        {
            public AButton() : base(11, "A", 0x1000)
            {
            }
        }

        private class BButton : Xbox360Button
        {
            public BButton() : base(12, "B", 0x2000)
            {
            }
        }

        private class XButton : Xbox360Button
        {
            public XButton() : base(13, "X", 0x4000)
            {
            }
        }

        private class YButton : Xbox360Button
        {
            public YButton() : base(14, "Y", 0x8000)
            {
            }
        }
    }

    /// <summary>
    ///     Describes the axes of an <see cref="Xbox360Controller" /> object. The related valid value range is between -32768
    ///     and 32767 where 0 is the
    ///     centered position.
    /// </summary>
    public abstract class Xbox360Axis : Xbox360Property
    {
        public static Xbox360Axis LeftThumbX = new LeftThumbXAxis();
        public static Xbox360Axis LeftThumbY = new LeftThumbYAxis();
        public static Xbox360Axis RightThumbX = new RightThumbXAxis();
        public static Xbox360Axis RightThumbY = new RightThumbYAxis();

        protected Xbox360Axis(int id, string name)
            : base(id, name)
        {
        }

        private class LeftThumbXAxis : Xbox360Axis
        {
            public LeftThumbXAxis() : base(0, "LeftThumbX")
            {
            }
        }

        private class LeftThumbYAxis : Xbox360Axis
        {
            public LeftThumbYAxis() : base(1, "LeftThumbY")
            {
            }
        }

        private class RightThumbXAxis : Xbox360Axis
        {
            public RightThumbXAxis() : base(2, "RightThumbX")
            {
            }
        }

        private class RightThumbYAxis : Xbox360Axis
        {
            public RightThumbYAxis() : base(3, "RightThumbY")
            {
            }
        }
    }

    /// <summary>
    ///     Describes the sliders of an <see cref="Xbox360Controller" /> object. A slider typically has a value of 0 when in
    ///     its resting position and
    ///     can report a maximum of 255 when fully engaged (e.g. pressed down).
    /// </summary>
    public abstract class Xbox360Slider : Xbox360Property
    {
        public static Xbox360Slider LeftTrigger = new LeftTriggerSlider();
        public static Xbox360Slider RightTrigger = new RightTriggerSlider();

        protected Xbox360Slider(int id, string name)
            : base(id, name)
        {
        }

        private class LeftTriggerSlider : Xbox360Slider
        {
            public LeftTriggerSlider() : base(0, "LeftTrigger")
            {
            }
        }

        private class RightTriggerSlider : Xbox360Slider
        {
            public RightTriggerSlider() : base(1, "RightTrigger")
            {
            }
        }
    }
}