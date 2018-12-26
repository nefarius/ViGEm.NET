namespace Nefarius.ViGEm.Client
{
    public interface IVirtualGamepad
    {
        void Connect();

        void Disconnect();

        int ButtonCount { get; }

        int AxisCount { get; }

        void SetButtonState(int index, bool pressed);

        void SetAxisValue(int index, short value);

        void SetSliderValue(int index, byte value);
    }
}