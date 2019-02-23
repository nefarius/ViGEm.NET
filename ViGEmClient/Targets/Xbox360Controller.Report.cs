using JetBrains.Annotations;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace Nefarius.ViGEm.Client.Targets
{
    internal partial class Xbox360Controller
    {
        public void SetButtonState(Xbox360Button button, bool pressed)
        {
            if (pressed)
                _nativeReport.wButtons |= (ushort)button;
            else
                _nativeReport.wButtons &= (ushort)~button;

            SubmitNativeReport(_nativeReport);
        }

        public void SetAxisValue(Xbox360Axis axis, short value)
        {
            switch (axis)
            {
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

        public void SetSliderValue(Xbox360Slider slider, byte value)
        {
            switch (slider)
            {
                case Xbox360Slider.LeftTrigger:
                    _nativeReport.bLeftTrigger = value;
                    break;
                case Xbox360Slider.RightTrigger:
                    _nativeReport.bRightTrigger = value;
                    break;
            }

            SubmitNativeReport(_nativeReport);
        }
    }
}
