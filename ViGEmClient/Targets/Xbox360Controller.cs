using Nefarius.ViGEm.Client.Exceptions;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections.Generic;

namespace Nefarius.ViGEm.Client.Targets
{
    /// <inheritdoc cref="ViGEmTarget" />
    /// <summary>
    ///     Represents an emulated wired Microsoft Xbox 360 Controller.
    /// </summary>
    internal class Xbox360Controller : ViGEmTarget, IVirtualGamepad
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
            Xbox360Axis.LeftTrigger,
            Xbox360Axis.RightTrigger,
            Xbox360Axis.LeftThumbX,
            Xbox360Axis.LeftThumbY,
            Xbox360Axis.RightThumbX,
            Xbox360Axis.RightThumbY
        };

        private readonly Xbox360Report _report = new Xbox360Report();

        private ViGEmClient.XUSB_REPORT _nativeReport = new ViGEmClient.XUSB_REPORT();

        private ViGEmClient.PVIGEM_X360_NOTIFICATION _notificationCallback;

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
            _notificationCallback = (client, target, largeMotor, smallMotor, number) => FeedbackReceived?.Invoke(this,
                new Xbox360FeedbackReceivedEventArgs(largeMotor, smallMotor, number));

            var error = ViGEmClient.vigem_target_x360_register_notification(Client.NativeHandle, NativeHandle,
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
            ViGEmClient.vigem_target_x360_unregister_notification(NativeHandle);

            base.Disconnect();
        }

        public int ButtonCount => ButtonMap.Count;

        public int AxisCount => AxisMap.Count;

        public void SetButtonState(int index, bool pressed)
        {
            if (pressed)
            {
                _nativeReport.wButtons |= (ushort)ButtonMap[index];
            }
            else
            {
                _nativeReport.wButtons &= (ushort)~ButtonMap[index];
            }

            SubmitNativeReport(_nativeReport);
        }

        private static short Scale(byte value, bool invert)
        {
            int intValue = (value - 0x80);
            if (intValue == -128) intValue = -127;

            var wtfValue = intValue * 258.00787401574803149606299212599f; // what the fuck?

            return (short)(invert ? -wtfValue : wtfValue);
        }

        public void SetAxisValue(int index, short value)
        {
            var axis = AxisMap[index];

            switch (axis)
            {
                case Xbox360Axis.LeftTrigger:
                    throw new NotImplementedException();
                    break;
                case Xbox360Axis.RightTrigger:
                    throw new NotImplementedException();
                    break;
                case Xbox360Axis.LeftThumbX:
                    _nativeReport.sThumbLX = value;
                    break;
                case Xbox360Axis.LeftThumbY:
                    _nativeReport.sThumbLY = value;
                    break;
                case Xbox360Axis.RightThumbX:
                    _nativeReport.sThumbRX = value;
                    break;
                case Xbox360Axis.RightThumbY:
                    _nativeReport.sThumbRY = value;
                    break;
            }

            SubmitNativeReport(_nativeReport);
        }

        private void SubmitNativeReport(ViGEmClient.XUSB_REPORT report)
        {
            var error = ViGEmClient.vigem_target_x360_update(Client.NativeHandle, NativeHandle, report);

            switch (error)
            {
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_BUS_NOT_FOUND:
                    throw new VigemBusNotFoundException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_INVALID_TARGET:
                    throw new VigemInvalidTargetException();
            }
        }

        public event Xbox360FeedbackReceivedEventHandler FeedbackReceived;
    }

    public delegate void Xbox360FeedbackReceivedEventHandler(object sender, Xbox360FeedbackReceivedEventArgs e);
}