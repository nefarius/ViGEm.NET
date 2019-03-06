using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

    public abstract class Xbox360Property : IComparable
    {
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

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }
    }

    public abstract class Xbox360ButtonProp : Xbox360Property
    {
        public static Xbox360ButtonProp Up = new UpButton();
        public static Xbox360ButtonProp Down = new DownButton();
        public static Xbox360ButtonProp Left = new LeftButton();
        public static Xbox360ButtonProp Right = new RightButton();
        public static Xbox360ButtonProp Start = new StartButton();
        public static Xbox360ButtonProp Back = new BackButton();
        public static Xbox360ButtonProp LeftThumb = new LeftThumbButton();
        public static Xbox360ButtonProp RightThumb = new RightThumbButton();
        public static Xbox360ButtonProp LeftShoulder = new LeftShoulderButton();
        public static Xbox360ButtonProp RightShoulder = new RightShoulderButton();
        public static Xbox360ButtonProp Guide = new GuideButton();
        public static Xbox360ButtonProp A = new AButton();
        public static Xbox360ButtonProp B = new BButton();
        public static Xbox360ButtonProp X = new XButton();
        public static Xbox360ButtonProp Y = new YButton();

        protected Xbox360ButtonProp(int id, string name, ushort value)
            : base(id, name)
        {
            Value = value;
        }

        public ushort Value { get; }

        private class UpButton : Xbox360ButtonProp
        {
            public UpButton() : base(0, "Up", 0x0001)
            {
            }
        }

        private class DownButton : Xbox360ButtonProp
        {
            public DownButton() : base(1, "Down", 0x0002)
            {
            }
        }

        private class LeftButton : Xbox360ButtonProp
        {
            public LeftButton() : base(2, "Left", 0x0004)
            {
            }
        }

        private class RightButton : Xbox360ButtonProp
        {
            public RightButton() : base(3, "Right", 0x0008)
            {
            }
        }

        private class StartButton : Xbox360ButtonProp
        {
            public StartButton() : base(4, "Start", 0x0010)
            {
            }
        }

        private class BackButton : Xbox360ButtonProp
        {
            public BackButton() : base(5, "Back", 0x0020)
            {
            }
        }

        private class LeftThumbButton : Xbox360ButtonProp
        {
            public LeftThumbButton() : base(6, "LeftThumb", 0x0040)
            {
            }
        }

        private class RightThumbButton : Xbox360ButtonProp
        {
            public RightThumbButton() : base(7, "RightThumb", 0x0080)
            {
            }
        }

        private class LeftShoulderButton : Xbox360ButtonProp
        {
            public LeftShoulderButton() : base(8, "LeftShoulder", 0x0100)
            {
            }
        }

        private class RightShoulderButton : Xbox360ButtonProp
        {
            public RightShoulderButton() : base(9, "RightShoulder", 0x0200)
            {
            }
        }

        private class GuideButton : Xbox360ButtonProp
        {
            public GuideButton() : base(10, "Guide", 0x0400)
            {
            }
        }

        private class AButton : Xbox360ButtonProp
        {
            public AButton() : base(11, "A", 0x1000)
            {
            }
        }

        private class BButton : Xbox360ButtonProp
        {
            public BButton() : base(12, "B", 0x2000)
            {
            }
        }

        private class XButton : Xbox360ButtonProp
        {
            public XButton() : base(13, "X", 0x4000)
            {
            }
        }

        private class YButton : Xbox360ButtonProp
        {
            public YButton() : base(14, "Y", 0x8000)
            {
            }
        }
    }

    public abstract class Xbox360AxisProp : Xbox360Property
    {
        public static Xbox360AxisProp LeftThumbX = new LeftThumbXAxis();
        public static Xbox360AxisProp LeftThumbY = new LeftThumbYAxis();
        public static Xbox360AxisProp RightThumbX = new RightThumbYAxis();
        public static Xbox360AxisProp RightThumbY = new RightThumbYAxis();

        protected Xbox360AxisProp(int id, string name)
            : base(id, name)
        {
        }

        private class LeftThumbXAxis : Xbox360AxisProp
        {
            public LeftThumbXAxis() : base(0, "LeftThumbX")
            {
            }
        }

        private class LeftThumbYAxis : Xbox360AxisProp
        {
            public LeftThumbYAxis() : base(1, "LeftThumbY")
            {
            }
        }

        private class RightThumbXAxis : Xbox360AxisProp
        {
            public RightThumbXAxis() : base(2, "RightThumbX")
            {
            }
        }

        private class RightThumbYAxis : Xbox360AxisProp
        {
            public RightThumbYAxis() : base(3, "RightThumbY")
            {
            }
        }
    }

    public abstract class Xbox360SliderProp : Xbox360Property
    {
        public static Xbox360SliderProp LeftThumbX = new LeftTriggerSlider();
        public static Xbox360SliderProp LeftThumbY = new RightTriggerSlider();

        protected Xbox360SliderProp(int id, string name)
            : base(id, name)
        {
        }

        private class LeftTriggerSlider : Xbox360SliderProp
        {
            public LeftTriggerSlider() : base(0, "LeftTrigger")
            {
            }
        }

        private class RightTriggerSlider : Xbox360SliderProp
        {
            public RightTriggerSlider() : base(1, "RightTrigger")
            {
            }
        }
    }
}