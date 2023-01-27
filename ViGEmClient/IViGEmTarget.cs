using System.Diagnostics.CodeAnalysis;

namespace Nefarius.ViGEm.Client;

/// <summary>
///     Describes basic properties every emulated target shares.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
internal interface IViGEmTarget
{
	/// <summary>
	///     16-bit unsigned vendor identifier the device should report.
	/// </summary>
	ushort VendorId { get; }

	/// <summary>
	///     16-bit unsigned product identifier the device should report.
	/// </summary>
	ushort ProductId { get; }
}