using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Nefarius.ViGEm.Client.Exceptions;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using Nefarius.ViGEm.Client.Targets.Xbox360.Exceptions;

namespace Nefarius.ViGEm.Client.Targets
{
    /// <inheritdoc cref="ViGEmTarget" />
    /// <summary>
    ///     Represents an emulated wired Microsoft Xbox 360 Controller.
    /// </summary>
    internal partial class Xbox360Controller : ViGEmTarget, IXbox360Controller
    {
        private static readonly List<Xbox360Button> ButtonMap = new List<Xbox360Button>
        {
            Xbox360Button.Up,
            Xbox360Button.Down,
            Xbox360Button.Left,
            Xbox360Button.Right,
            Xbox360Button.Start,
            Xbox360Button.Back,
            Xbox360Button.LeftThumb,
            Xbox360Button.RightThumb,
            Xbox360Button.LeftShoulder,
            Xbox360Button.RightShoulder,
            Xbox360Button.Guide,
            Xbox360Button.A,
            Xbox360Button.B,
            Xbox360Button.X,
            Xbox360Button.Y
        };

        private static readonly List<Xbox360Axis> AxisMap = new List<Xbox360Axis>
        {
            Xbox360Axis.LeftThumbX,
            Xbox360Axis.LeftThumbY,
            Xbox360Axis.RightThumbX,
            Xbox360Axis.RightThumbY
        };

        private static readonly List<Xbox360Slider> SliderMap = new List<Xbox360Slider>
        {
            Xbox360Slider.LeftTrigger,
            Xbox360Slider.RightTrigger
        };

        private ViGEmClient.XUSB_REPORT _nativeReport;

        private ViGEmClient.PVIGEM_X360_NOTIFICATION _notificationCallback;

        private int _userIndex = -1;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Nefarius.ViGEm.Client.Targets.Xbox360Controller" /> class bound to a
        ///     <see cref="T:Nefarius.ViGEm.Client.ViGEmClient" />.
        /// </summary>
        /// <param name="client">The <see cref="T:Nefarius.ViGEm.Client.ViGEmClient" /> this device is attached to.</param>
        public Xbox360Controller(ViGEmClient client) : base(client)
        {
            NativeHandle = ViGEmClient.vigem_target_x360_alloc();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Nefarius.ViGEm.Client.Targets.Xbox360Controller" /> class bound to a
        ///     <see cref="T:Nefarius.ViGEm.Client.ViGEmClient" /> overriding the default Vendor and Product IDs with the provided
        ///     values.
        /// </summary>
        /// <param name="client">The <see cref="T:Nefarius.ViGEm.Client.ViGEmClient" /> this device is attached to.</param>
        /// <param name="vendorId">The Vendor ID to use.</param>
        /// <param name="productId">The Product ID to use.</param>
        public Xbox360Controller(ViGEmClient client, ushort vendorId, ushort productId) : this(client)
        {
            VendorId = vendorId;
            ProductId = productId;
        }

        public override void Connect()
        {
            base.Connect();

            //
            // Callback to event
            // 
            _notificationCallback = (client, target, largeMotor, smallMotor, number) =>
            {
                UserIndex = number;

                FeedbackReceived?.Invoke(this,
                    new Xbox360FeedbackReceivedEventArgs(largeMotor, smallMotor, number));
            };

            var error = ViGEmClient.vigem_target_x360_register_notification(Client.NativeHandle, NativeHandle,
                _notificationCallback);

            switch (error)
            {
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_NONE:
                    break;
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_BUS_NOT_FOUND:
                    throw new VigemBusNotFoundException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_INVALID_TARGET:
                    throw new VigemInvalidTargetException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_CALLBACK_ALREADY_REGISTERED:
                    throw new VigemCallbackAlreadyRegisteredException();
                default:
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public override void Disconnect()
        {
            ViGEmClient.vigem_target_x360_unregister_notification(NativeHandle);

            base.Disconnect();
        }

        public int ButtonCount => ButtonMap.Count;

        public int AxisCount => AxisMap.Count;

        public int SliderCount => SliderMap.Count;

        public void SetButtonState(int index, bool pressed)
        {
            SetButtonState(ButtonMap[index], pressed);
        }

        public void SetAxisValue(int index, short value)
        {
            SetAxisValue(AxisMap[index], value);
        }

        public void SetSliderValue(int index, byte value)
        {
            SetSliderValue(SliderMap[index], value);
        }

        public bool AutoSubmitReport { get; set; } = true;

        public void ResetReport()
        {
            _nativeReport = default(ViGEmClient.XUSB_REPORT);
        }

        public void SubmitReport()
        {
            SubmitNativeReport(_nativeReport);
        }

        /// <summary>
        ///     Gets invoked if vibration or LED states have changed.
        /// </summary>
        public event Xbox360FeedbackReceivedEventHandler FeedbackReceived;

        /// <summary>
        ///     Gets the assigned player index set by the XInput sub-system.
        /// </summary>
        public int UserIndex
        {
            get
            {
                if (_userIndex == -1)
                    throw new Xbox360UserIndexNotReportedException();

                return _userIndex;
            }
            private set => _userIndex = value;
        }

        /// <summary>
        ///     Submits a new (or modified) report to the emulation bus.
        /// </summary>
        /// <param name="report">The report to submit.</param>
        private void SubmitNativeReport(ViGEmClient.XUSB_REPORT report)
        {
            var error = ViGEmClient.vigem_target_x360_update(Client.NativeHandle, NativeHandle, report);

            switch (error)
            {
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_NONE:
                    break;
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_BUS_INVALID_HANDLE:
                    throw new VigemBusInvalidHandleException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_INVALID_TARGET:
                    throw new VigemInvalidTargetException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_BUS_NOT_FOUND:
                    throw new VigemBusNotFoundException();
                default:
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }

    public delegate void Xbox360FeedbackReceivedEventHandler(object sender, Xbox360FeedbackReceivedEventArgs e);
}