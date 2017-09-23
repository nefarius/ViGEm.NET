using Nefarius.ViGEm.Client.Exceptions;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace Nefarius.ViGEm.Client.Targets
{
    public class Xbox360Controller : ViGEmTarget
    {
        private ViGEmClient.PVIGEM_X360_NOTIFICATION _notificationCallback;

        public Xbox360Controller(ViGEmClient client) : base(client)
        {
            NativeHandle = ViGEmClient.vigem_target_x360_alloc();
        }

        public Xbox360Controller(ViGEmClient client, ushort vendorId, ushort productId) : this(client)
        {
            VendorId = vendorId;
            ProductId = productId;
        }

        public void SendReport(Xbox360Report report)
        {
            var submit = new ViGEmClient.XUSB_REPORT
            {
                wButtons = report.Buttons,
                bLeftTrigger = report.LeftTrigger,
                bRightTrigger = report.RightTrigger,
                sThumbLX = report.LeftThumbX,
                sThumbLY = report.LeftThumbY,
                sThumbRX = report.RightThumbX,
                sThumbRY = report.RightThumbY
            };

            var error = ViGEmClient.vigem_target_x360_update(Client.NativeHandle, NativeHandle, submit);

            switch (error)
            {
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_BUS_NOT_FOUND:
                    throw new VigemBusNotFoundException();
                case ViGEmClient.VIGEM_ERROR.VIGEM_ERROR_INVALID_TARGET:
                    throw new VigemInvalidTargetException();
            }
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

        public event Xbox360FeedbackReceivedEventHandler FeedbackReceived;
    }

    public delegate void Xbox360FeedbackReceivedEventHandler(object sender, Xbox360FeedbackReceivedEventArgs e);
}