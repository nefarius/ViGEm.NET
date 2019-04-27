using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace Nefarius.ViGEm.Client.Targets
{
    internal partial class Xbox360Controller
    {
        public void SetButtonState(Xbox360Button button, bool pressed)
        {
            if (pressed)
                _nativeReport.wButtons |= (ushort)button.Value;
            else
                _nativeReport.wButtons &= (ushort)~button.Value;

            if (AutoSubmitReport)
                SubmitNativeReport(_nativeReport);
        }

        public void SetAxisValue(Xbox360Axis axis, short value)
        {
            switch (axis.Name)
            {
                case "LeftThumbX":
                    _nativeReport.sThumbLX = value;
                    break;
                case "LeftThumbY":
                    _nativeReport.sThumbLY = value;
                    break;
                case "RightThumbX":
                    _nativeReport.sThumbRX = value;
                    break;
                case "RightThumbY":
                    _nativeReport.sThumbRY = value;
                    break;
            }

            if (AutoSubmitReport)
                SubmitNativeReport(_nativeReport);
        }

        public void SetSliderValue(Xbox360Slider slider, byte value)
        {
            switch (slider.Name)
            {
                case "LeftTrigger":
                    _nativeReport.bLeftTrigger = value;
                    break;
                case "RightTrigger":
                    _nativeReport.bRightTrigger = value;
                    break;
            }

            if (AutoSubmitReport)
                SubmitNativeReport(_nativeReport);
        }
    }
}