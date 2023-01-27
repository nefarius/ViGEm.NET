using System.Diagnostics.CodeAnalysis;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace Nefarius.ViGEm.Client.Targets;

/// <summary>
///     Describes Xbox 360 pad-specific methods and properties.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
[SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
public interface IXbox360Controller : IVirtualGamepad
{
	/// <summary>
	///     Gets the assigned player index set by the XInput sub-system.
	/// </summary>
	int UserIndex { get; }

	ref byte LeftTrigger { get; }

	ref byte RightTrigger { get; }

	ref short LeftThumbX { get; }

	ref short LeftThumbY { get; }

	ref short RightThumbX { get; }

	ref short RightThumbY { get; }

	ref ushort ButtonState { get; }

	/// <summary>
	///     Changes the state of a digital button identified by <see cref="Xbox360Button" />.
	/// </summary>
	/// <param name="button">The <see cref="Xbox360Button" /> to change.</param>
	/// <param name="pressed">True if pressed/down, false if released/up.</param>
	void SetButtonState(Xbox360Button button, bool pressed);

	/// <summary>
	///     Changes the value of an axis identified by <see cref="Xbox360Axis" />.
	/// </summary>
	/// <param name="axis">The <see cref="Xbox360Axis" /> to change.</param>
	/// <param name="value">
	///     The 16-bit signed value of the axis where 0 represents centered. The value is expected to stay
	///     between -32768 and 32767.
	/// </param>
	void SetAxisValue(Xbox360Axis axis, short value);

	/// <summary>
	///     Changes the value of a slider identified by <see cref="Xbox360Slider" />.
	/// </summary>
	/// <param name="slider">The <see cref="Xbox360Slider" /> to change.</param>
	/// <param name="value">
	///     The 8-bit unsigned value of the slider. A value of 0 represents lowest (released) while 255
	///     represents highest (engaged).
	/// </param>
	void SetSliderValue(Xbox360Slider slider, byte value);

	/// <summary>
	///     Fires when LED index change or vibration requests were sent to this device.
	/// </summary>
	event Xbox360FeedbackReceivedEventHandler FeedbackReceived;

	void SetButtonsFull(ushort buttons);
}