<img src="assets/NSS-128x128.png" align="right" />

# ViGEm Client .NET SDK

.NET bindings for the `ViGEmClient` native library.

[![Build status](https://ci.appveyor.com/api/projects/status/pxjf36etx8ro901s?svg=true)](https://ci.appveyor.com/project/nefarius/vigem-net) ![Nuget](https://img.shields.io/nuget/dt/Nefarius.ViGEm.Client) [![Discord](https://img.shields.io/discord/346756263763378176.svg)](https://discord.nefarius.at/) [![GitHub followers](https://img.shields.io/github/followers/nefarius.svg?style=social&label=Follow)](https://github.com/nefarius) [![Mastodon Follow](https://img.shields.io/mastodon/follow/109321120351128938?domain=https%3A%2F%2Ffosstodon.org%2F&style=social)](https://fosstodon.org/@Nefarius)

---

## 🧟 THIS PROJECT HAS BEEN RETIRED 🧟

Users of this software are encouraged to [read the end-of-life statement](https://docs.nefarius.at/projects/ViGEm/End-of-Life/). So long, cheers 🖖

---

## About

.NET API to interact with features offered by [ViGEm Bus Driver](https://github.com/nefarius/ViGEmBus).

## Installation

This library can be consumed via [pre-built NuGet](https://www.nuget.org/packages/Nefarius.ViGEm.Client/):

```PowerShell
Install-Package Nefarius.ViGEm.Client
```

## Examples

### Get full output report of an emulated DS4

> Requires ViGEmBus v1.19.418 or newer.

```csharp
using Nefarius.ViGEm.Client;

// initializes the SDK instance
var client = new ViGEmClient();

// prepares a new DS4
var ds4 = client.CreateDualShock4Controller();

// brings the DS4 online
ds4.Connect();

// recommended: run this in its own thread
while (true)
    try
    {
        // blocks for 250ms to not burn CPU cycles if no report is available
        // an overload is available that blocks indefinitely until the device is disposed, your choice!
        var buffer = ds4.AwaitRawOutputReport(250, out var timedOut);

        if (timedOut)
        {
            Console.WriteLine("Timed out");
            continue;
        }

        // you got a new report, parse it and do whatever you need to do :)
        // here we simply hex-dump the contents
        Console.WriteLine($"[OUT] {string.Join(" ", buffer.Select(b => b.ToString("X2")))}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        Thread.Sleep(1000);
    }
```

## Contribute

### Bugs & Features

Found a bug and want it fixed? Open a detailed issue on the [GitHub issue tracker](../../issues)!

Have an idea for a new feature? Let's have a chat about your request on [our support channels](https://vigem.org/Community-Support/).

### Questions & Support

Please respect that the GitHub issue tracker isn't a helpdesk. We offer [support resources](https://vigem.org/Community-Support/), where you're welcome to check out and engage in discussions!

## 3rd party credits

This project uses [Fody Costura](https://github.com/Fody/Costura) to embed the native SDK DLLs into the resulting assembly ❤️
