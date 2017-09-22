using Nefarius.ViGEm.Client.Exceptions;
using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace Nefarius.ViGEm.Client.Targets
{
    public class DualShock4Controller : ViGEmTarget
    {
        public DualShock4Controller(ViGEmClient client) : base(client)
        {
        }

        public event DualShock4FeedbackReceivedEventHandler FeedbackReceived;

        // private void Ds4Notification(ViGemUm.VigemTarget target, byte largeMotor, byte smallMotor,
        //     ViGemUm.Ds4LightbarColor lightbarColor)
        // {
        //     var color = new LightbarColor(lightbarColor.Red, lightbarColor.Green, lightbarColor.Blue);
        //     FeedbackReceived?.Invoke(this, new DualShock4FeedbackReceivedEventArgs(largeMotor, smallMotor, color));
        // }
    }

    public delegate void DualShock4FeedbackReceivedEventHandler(object sender, DualShock4FeedbackReceivedEventArgs e);
}
