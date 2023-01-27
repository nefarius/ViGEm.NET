using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Nefarius.ViGEm.Client.Targets.DualShock4;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public abstract class DualShock4Property : IComparable
{
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
        return Id.CompareTo(((DualShock4Property)other).Id);
    }

    public override string ToString()
    {
        return Name;
    }

    public static IEnumerable<T> GetAll<T>() where T : DualShock4Property
    {
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public |
                                                 BindingFlags.Static |
                                                 BindingFlags.DeclaredOnly);

        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public override bool Equals(object obj)
    {
        DualShock4Property otherValue = obj as DualShock4Property;

        if (otherValue == null)
        {
            return false;
        }

        bool typeMatches = GetType() == obj.GetType();
        bool valueMatches = Id.Equals(otherValue.Id);

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
    public static readonly DualShock4Button ThumbRight = new ThumbRightButton();
    public static readonly DualShock4Button ThumbLeft = new ThumbLeftButton();
    public static readonly DualShock4Button Options = new OptionsButton();
    public static readonly DualShock4Button Share = new ShareButton();
    public static readonly DualShock4Button TriggerRight = new TriggerRightButton();
    public static readonly DualShock4Button TriggerLeft = new TriggerLeftButton();
    public static readonly DualShock4Button ShoulderRight = new ShoulderRightButton();
    public static readonly DualShock4Button ShoulderLeft = new ShoulderLeftButton();
    public static readonly DualShock4Button Triangle = new TriangleButton();
    public static readonly DualShock4Button Circle = new CircleButton();
    public static readonly DualShock4Button Cross = new CrossButton();
    public static readonly DualShock4Button Square = new SquareButton();

    protected DualShock4Button(int id, string name, ushort value)
        : base(id, name)
    {
        Value = value;
    }

    [IgnoreDataMember]
    public ushort Value { get; protected init; }

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
    public static readonly DualShock4SpecialButton Ps = new PsButton();
    public static readonly DualShock4SpecialButton Touchpad = new TouchpadButton();

    private DualShock4SpecialButton(int id, string name, ushort value) : base(id, name, value)
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
    public static readonly DualShock4DPadDirection None = new NoneDPadDirection();
    public static readonly DualShock4DPadDirection Northwest = new NorthwestDPadDirection();
    public static readonly DualShock4DPadDirection West = new WestDPadDirection();
    public static readonly DualShock4DPadDirection Southwest = new SouthwestDPadDirection();
    public static readonly DualShock4DPadDirection South = new SouthDPadDirection();
    public static readonly DualShock4DPadDirection Southeast = new SoutheastDPadDirection();
    public static readonly DualShock4DPadDirection East = new EastDPadDirection();
    public static readonly DualShock4DPadDirection Northeast = new NortheastDPadDirection();
    public static readonly DualShock4DPadDirection North = new NorthDPadDirection();

    private DualShock4DPadDirection(int id, string name, ushort value)
        : base(id, name)
    {
        Value = value;
    }

    [IgnoreDataMember]
    public ushort Value { get; }

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
    public static readonly DualShock4Axis LeftThumbX = new LeftThumbXAxis();
    public static readonly DualShock4Axis LeftThumbY = new LeftThumbYAxis();
    public static readonly DualShock4Axis RightThumbX = new RightThumbXAxis();
    public static readonly DualShock4Axis RightThumbY = new RightThumbYAxis();

    private DualShock4Axis(int id, string name)
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
    public static readonly DualShock4Slider LeftTrigger = new LeftTriggerSlider();
    public static readonly DualShock4Slider RightTrigger = new RightTriggerSlider();

    private DualShock4Slider(int id, string name)
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