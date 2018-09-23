namespace Nefarius.ViGEm.Client
{
    internal interface IViGEmTarget
    {
        ushort VendorId { get; }

        ushort ProductId { get; }
    }
}