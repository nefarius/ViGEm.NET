using Nefarius.ViGEm.Client.Exceptions;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace Nefarius.ViGEm.Client.Targets
{
    public class Xbox360Controller : ViGEmTarget
    {
        public Xbox360Controller(ViGEmClient client) : base(client)
        {
            NativeHandle = ViGEmClient.vigem_target_x360_alloc();
        }

        public void SendReport(Xbox360Report report)
        {
            var submit = new ViGEmClient.XUSB_REPORT()
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
        }

        public event Xbox360FeedbackReceivedEventHandler FeedbackReceived;

        //private void XusbNotification(ViGemUm.VigemTarget target, byte largeMotor, byte smallMotor, byte ledNumber)
        //{
        //    FeedbackReceived?.Invoke(this, new Xbox360FeedbackReceivedEventArgs(largeMotor, smallMotor, ledNumber));
        //}
    }

    public delegate void Xbox360FeedbackReceivedEventHandler(object sender, Xbox360FeedbackReceivedEventArgs e);
}
