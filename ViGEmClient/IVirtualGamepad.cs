using JetBrains.Annotations;

namespace Nefarius.ViGEm.Client
{
    /// <summary>
    ///     Describes a generic set of properties and methods all emulated devices share.
    /// </summary>
    public interface IVirtualGamepad
    {
        /// <summary>
        ///     Returns the count of available digital buttons of this device.
        /// </summary>
        int ButtonCount { get; }

        /// <summary>
        ///     Returns the count of available axes of this device.
        /// </summary>
        int AxisCount { get; }

        /// <summary>
        ///     Returns the count of available sliders of this device.
        /// </summary>
        int SliderCount { get; }

        /// <summary>
        ///     Gets or sets whether every change to a pad property should get submitted to the driver automatically. Default is
        ///     true. Once set to false, <see cref="SubmitReport"/> has to be called explicitly to submit state changes.
        /// </summary>
        [UsedImplicitly]
        bool AutoSubmitReport { get; set; }

        /// <summary>
        ///     Connects (attaches) the virtual device to the system.
        /// </summary>
        [UsedImplicitly]
        void Connect();

        /// <summary>
        ///     Disconnects (removes) the virtual device from the system.
        /// </summary>
        [UsedImplicitly]
        void Disconnect();

        /// <summary>
        ///     Changes the state of a digital button identified by index.
        /// </summary>
        /// <remarks>Use <see cref="ButtonCount" /> to determine the upper limit of the index.</remarks>
        /// <param name="index">The index of the digital button.</param>
        /// <param name="pressed">True if pressed/down, false if released/up.</param>
        [UsedImplicitly]
        void SetButtonState(int index, bool pressed);

        /// <summary>
        ///     Changes the value of an axis identified by index.
        /// </summary>
        /// <remarks>Use <see cref="AxisCount" /> to determine the upper limit of the index.</remarks>
        /// <param name="index">The index of the axis.</param>
        /// <param name="value">
        ///     The 16-bit signed value of the axis where 0 represents centered. The value is expected to stay
        ///     between -32768 and 32767.
        /// </param>
        [UsedImplicitly]
        void SetAxisValue(int index, short value);

        /// <summary>
        ///     Changes the value of a slider identified by index.
        /// </summary>
        /// <remarks>Use <see cref="SliderCount" /> to determine the upper limit of the index.</remarks>
        /// <param name="index">The index of the slider.</param>
        /// <param name="value">
        ///     The 8-bit unsigned value of the slider. A value of 0 represents lowest (released) while 255
        ///     represents highest (engaged).
        /// </param>
        [UsedImplicitly]
        void SetSliderValue(int index, byte value);

        /// <summary>
        ///     Resets the internal report structure to its default values. 
        /// </summary>
        [UsedImplicitly]
        void ResetReport();

        /// <summary>
        ///     Submits the current report to the driver. Not necessary if <see cref="AutoSubmitReport"/> is true.
        /// </summary>
        [UsedImplicitly]
        void SubmitReport();
    }
}