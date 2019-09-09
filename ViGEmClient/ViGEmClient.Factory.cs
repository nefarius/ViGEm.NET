using JetBrains.Annotations;
using Nefarius.ViGEm.Client.Targets;

namespace Nefarius.ViGEm.Client
{
    public partial class ViGEmClient
    {
        /// <summary>
        ///     Allocates a new virtual Xbox 360 Controller device on the bus.
        /// </summary>
        /// <returns>A new virtual Xbox 360 Controller device.</returns>
        [UsedImplicitly]
        public IXbox360Controller CreateXbox360Controller()
        {
            return new Xbox360Controller(this);
        }

        /// <summary>
        ///     Allocates a new virtual Xbox 360 Controller device on the bus with custom vendor and product identifiers.
        /// </summary>
        /// <param name="vendorId">16-bit unsigned vendor ID.</param>
        /// <param name="productId">16-bit unsigned product ID.</param>
        /// <returns>A new virtual Xbox 360 Controller device.</returns>
        [UsedImplicitly]
        public IXbox360Controller CreateXbox360Controller(ushort vendorId, ushort productId)
        {
            return new Xbox360Controller(this, vendorId, productId);
        }

        /// <summary>
        ///     Allocates a new virtual DualShock 4 device on the bus.
        /// </summary>
        /// <returns>A new virtual DualShock 4 Controller device.</returns>
        [UsedImplicitly]
        public IDualShock4Controller CreateDualShock4Controller()
        {
            return new DualShock4Controller(this);
        }

        /// <summary>
        ///     Allocates a new virtual DualShock 4 device on the bus.
        /// </summary>
        /// <param name="vendorId">16-bit unsigned vendor ID.</param>
        /// <param name="productId">16-bit unsigned product ID.</param>
        /// <returns>A new virtual DualShock 4 Controller device.</returns>
        [UsedImplicitly]
        public IDualShock4Controller CreateDualShock4Controller(ushort vendorId, ushort productId)
        {
            return new DualShock4Controller(this, vendorId, productId);
        }
    }
}
