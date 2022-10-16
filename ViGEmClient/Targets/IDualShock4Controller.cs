using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace Nefarius.ViGEm.Client.Targets
{
    public interface IDualShock4Controller : IVirtualGamepad
    {
        [UsedImplicitly]
        void SetButtonState(DualShock4Button button, bool pressed);

        [UsedImplicitly]
        void SetDPadDirection(DualShock4DPadDirection direction);

        [UsedImplicitly]
        void SetAxisValue(DualShock4Axis axis, byte value);

        [UsedImplicitly]
        void SetSliderValue(DualShock4Slider slider, byte value);

        [UsedImplicitly]
        [Obsolete("This event might not behave as expected and has been deprecated. Use AwaitRawOutputReport() instead.")]
        event DualShock4FeedbackReceivedEventHandler FeedbackReceived;

        [UsedImplicitly]
        void SetButtonsFull(ushort buttons);

        [UsedImplicitly]
        void SetSpecialButtonsFull(byte buttons);

        [UsedImplicitly]
        ref byte LeftTrigger { get; }
        [UsedImplicitly]
        ref byte RightTrigger { get; }
        [UsedImplicitly]
        ref byte LeftThumbX { get; }
        [UsedImplicitly]
        ref byte LeftThumbY { get; }
        [UsedImplicitly]
        ref byte RightThumbX { get; }
        [UsedImplicitly]
        ref byte RightThumbY { get; }

        /// <summary>
        ///     Submits the full input report to the device.
        /// </summary>
        /// <param name="buffer">The input report.</param>
        [UsedImplicitly]
        void SubmitRawReport(byte[] buffer);

        /// <summary>
        ///     Awaits until a pending output report is available and returns it as byte array.
        /// </summary>
        /// <returns>The output report buffer.</returns>
        [UsedImplicitly]
        IEnumerable<byte> AwaitRawOutputReport();

        /// <summary>
        ///     Awaits until a pending output report is available and returns it as byte array.
        /// </summary>
        /// <param name="timeout">Timeout to block in milliseconds.</param>
        /// <param name="timedOut">True if it timed out, false if data is available.</param>
        /// <returns>The output report buffer.</returns>
        [UsedImplicitly]
        IEnumerable<byte> AwaitRawOutputReport(int timeout, out bool timedOut);
    }
}