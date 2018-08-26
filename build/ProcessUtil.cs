using System;
using System.Diagnostics;

public static class ProcessUtil
{
    public static void StartWithArguments(string file, string args)
    {
        var proc = new Process
        {
            StartInfo =
            {
                FileName = file,
                Arguments = args,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            }
        };

        proc.OutputDataReceived += (sender, eventArgs) => Console.WriteLine($"++ {eventArgs.Data}");
        proc.ErrorDataReceived += (sender, eventArgs) => Console.WriteLine($"!! {eventArgs.Data}");

        proc.Start();
        proc.WaitForExit();
    }
}