using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace Nefarius.ViGEm.Client.Targets;

[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
public interface IDualShock4Controller : IVirtualGamepad, IDisposable
{
    ref byte LeftTrigger { get; }

    ref byte RightTrigger { get; }

    ref byte LeftThumbX { get; }

    ref byte LeftThumbY { get; }

    ref byte RightThumbX { get; }

    ref byte RightThumbY { get; }

    void SetButtonState(DualShock4Button button, bool pressed);
    
    void SetDPadDirection(DualShock4DPadDirection direction);
    
    void SetAxisValue(DualShock4Axis axis, byte value);
    
    void SetSliderValue(DualShock4Slider slider, byte value);
    
    [Obsolete("This event might not behave as expected and has been deprecated. Use AwaitRawOutputReport() instead.")]
    event DualShock4FeedbackReceivedEventHandler FeedbackReceived;
    
    void SetButtonsFull(ushort buttons);
    
    void SetSpecialButtonsFull(byte buttons);

    /// <summary>
    ///     Submits the full input report to the device.
    /// </summary>
    /// <param name="buffer">The input report.</param>
    void SubmitRawReport(byte[] buffer);

    /// <summary>
    ///     Awaits until a pending output report is available and returns it as byte array.
    /// </summary>
    /// <returns>The output report buffer.</returns>
    IEnumerable<byte> AwaitRawOutputReport();

    /// <summary>
    ///     Awaits until a pending output report is available and returns it as byte array.
    /// </summary>
    /// <param name="timeout">Timeout to block in milliseconds.</param>
    /// <param name="timedOut">True if it timed out, false if data is available.</param>
    /// <returns>The output report buffer.</returns>
    IEnumerable<byte> AwaitRawOutputReport(int timeout, out bool timedOut);
}