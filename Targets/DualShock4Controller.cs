using Nefarius.ViGEm.Client.Exceptions;
using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace Nefarius.ViGEm.Client.Targets
{
    public class DualShock4Controller : ViGEmTarget
    {
        private ViGEmClient.PVIGEM_DS4_NOTIFICATION _notificationCallback;

        public DualShock4Controller(ViGEmClient client) : base(client)
        {
            NativeHandle = ViGEmClient.vigem_target_ds4_alloc();
        }

        public DualShock4Controller(ViGEmClient client, ushort vendorId, ushort productId) : this(client)
        {
            VendorId = vendorId;
            ProductId = productId;
        }

        public void SendReport(DualShock4Report report)
        {
            var submit = new ViGEmClient.DS4_REPORT
            {
                wButtons = report.Buttons,
                bSpecial = report.SpecialButtons,
                bThumbLX = report.LeftThumbX,
                bThumbLY = report.LeftThumbY,
                bThumbRX = report.RightThumbX,
                bThumbRY = report.RightThumbY,
                bTriggerL = report.LeftTrigger,
                bTriggerR = report.RightTrigger
            };

            var error = ViGEmClient.vigem_target_ds4_update(Client.NativeHandle, NativeHandle, submit);

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

        public event DualShock4FeedbackReceivedEventHandler FeedbackReceived;
    }

    public delegate void DualShock4FeedbackReceivedEventHandler(object sender, DualShock4FeedbackReceivedEventArgs e);
}