using Nefarius.ViGEm.Client.Exceptions;
using Nefarius.ViGEm.Client.Targets.DualShock4;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Nefarius.ViGEm.Client.Utilities;

namespace Nefarius.ViGEm.Client.Targets
{
    /// <inheritdoc cref="ViGEmTarget" />
    /// <summary>
    ///     Represents an emulated wired Sony DualShock 4 Controller.
    /// </summary>
    internal partial class DualShock4Controller : ViGEmTarget, IDualShock4Controller
    {
        private static readonly List<DualShock4Button> ButtonMap = new List<DualShock4Button>
        {
            DualShock4Button.ThumbRight,
            DualShock4Button.ThumbLeft,
            DualShock4Button.Options,
            DualShock4Button.Share,
            DualShock4Button.TriggerRight,
            DualShock4Button.TriggerLeft,
            DualShock4Button.ShoulderRight,
            DualShock4Button.ShoulderLeft,
            DualShock4Button.Triangle,
            DualShock4Button.Circle,
            DualShock4Button.Cross,
            DualShock4Button.Square,
            DualShock4SpecialButton.Ps,
            DualShock4SpecialButton.Touchpad
        };

        private static readonly List<DualShock4Axis> AxisMap = new List<DualShock4Axis>
        {
            DualShock4Axis.LeftThumbX,
            DualShock4Axis.LeftThumbY,
            DualShock4Axis.RightThumbX,
            DualShock4Axis.RightThumbY
        };

        private static readonly List<DualShock4Slider> SliderMap = new List<DualShock4Slider>
        {
            DualShock4Slider.LeftTrigger,
            DualShock4Slider.RightTrigger
        };

        private ViGEmClient.DS4_REPORT _nativeReport;

        private ViGEmClient.PVIGEM_DS4_NOTIFICATION _notificationCallback;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Nefarius.ViGEm.Client.Targets.DualShock4Controller" /> class bound
        ///     to a <see cref="T:Nefarius.ViGEm.Client.ViGEmClient" />.
        /// </summary>
        /// <param name="client">The <see cref="T:Nefarius.ViGEm.Client.ViGEmClient" /> this device is attached to.</param>
        public DualShock4Controller(ViGEmClient client) : base(client)
        {
            NativeHandle = ViGEmClient.vigem_target_ds4_alloc();

            ResetReport();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Nefarius.ViGEm.Client.Targets.DualShock4Controller" /> class bound
        ///     to a <see cref="T:Nefarius.ViGEm.Client.ViGEmClient" /> overriding the default Vendor and Product IDs with the
        ///     provided values.
        /// </summary>
        /// <param name="client">The <see cref="T:Nefarius.ViGEm.Client.ViGEmClient" /> this device is attached to.</param>
        /// <param name="vendorId">The Vendor ID to use.</param>
        /// <param name="productId">The Product ID to use.</param>
        public DualShock4Controller(ViGEmClient client, ushort vendorId, ushort productId) : this(client)
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
            _notificationCallback = (client, target, motor, smallMotor, color) => FeedbackReceived?.Invoke(this,
                new DualShock4FeedbackReceivedEventArgs(motor, smallMotor,
                    new LightbarColor(color.Red, color.Green, color.Blue)));

            var error = ViGEmClient.vigem_target_ds4_register_notification(Client.NativeHandle, NativeHandle,
                _notificationCallback);

            switch (error)
            {
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_BUS_NOT_FOUND:
                    throw new VigemBusNotFoundException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_INVALID_TARGET:
                    throw new VigemInvalidTargetException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_CALLBACK_ALREADY_REGISTERED:
                    throw new VigemCallbackAlreadyRegisteredException();
            }
        }

        public override void Disconnect()
        {
            ViGEmClient.vigem_target_ds4_unregister_notification(NativeHandle);

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
            SetAxisValue(AxisMap[index],
                (byte)MathUtil.ConvertRange(
                    short.MinValue,
                    short.MaxValue,
                    byte.MinValue,
                    byte.MaxValue,
                    value
                )
            );
        }

        public void SetSliderValue(int index, byte value)
        {
            SetSliderValue(SliderMap[index], value);
        }

        public bool AutoSubmitReport { get; set; } = true;

        public void ResetReport()
        {
            _nativeReport = default(ViGEmClient.DS4_REPORT);

            _nativeReport.wButtons &= unchecked((ushort)~0xF);
            _nativeReport.wButtons |= 0x08; // resting HAT switch position
            _nativeReport.bThumbLX = 0x80; // centered axis value
            _nativeReport.bThumbLY = 0x80; // centered axis value
            _nativeReport.bThumbRX = 0x80; // centered axis value
            _nativeReport.bThumbRY = 0x80; // centered axis value
        }

        public void SubmitReport()
        {
            SubmitNativeReport(_nativeReport);
        }

        private void SubmitNativeReport(ViGEmClient.DS4_REPORT report)
        {
            var error = ViGEmClient.vigem_target_ds4_update(Client.NativeHandle, NativeHandle, report);

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

        public event DualShock4FeedbackReceivedEventHandler FeedbackReceived;
    }

    public delegate void DualShock4FeedbackReceivedEventHandler(object sender, DualShock4FeedbackReceivedEventArgs e);
}