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
        event DualShock4FeedbackReceivedEventHandler FeedbackReceived;
    }
}