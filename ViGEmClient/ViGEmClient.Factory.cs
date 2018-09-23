using Nefarius.ViGEm.Client.Targets;

namespace Nefarius.ViGEm.Client
{
    public partial class ViGEmClient
    {
        public IVirtualGamepad CreateXbox360Controller()
        {
            return new Xbox360Controller(this);
        }

        public IVirtualGamepad CreateXbox360Controller(ushort vendorId, ushort productId)
        {
            return new Xbox360Controller(this, vendorId, productId);
        }

        public IVirtualGamepad CreateDualShock4Controller()
        {
            return new DualShock4Controller(this);
        }

        public IVirtualGamepad CreateDualShock4Controller(ushort vendorId, ushort productId)
        {
            return new DualShock4Controller(this, vendorId, productId);
        }
    }
}
