using System.Net;
using System.Net.Sockets;

namespace Trit.DemoConsole.A_Utf8;

public static class Demo
{
    public static Task Main()
    {
        using Socket listener = GetListener(IPAddress.Loopback, 1337);

        WriteLine($"Waiting for connection on {IPAddress.Loopback}:1337...");

        Socket socket = listener.Accept();
        socket.Receive(Array.Empty<byte>());

        // FEATURE: Utf8 Strings Literals
        socket.Send("Hello!"u8);
        socket.Send("Bye."u8);
        socket.Close();

        return Task.CompletedTask;
    }

    #region Not interesting

    private static Socket GetListener(IPAddress ipAddress, int port)
    {
        Socket listener = new(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

        listener.Bind(new IPEndPoint(ipAddress, port));
        listener.Listen(100);
        return listener;
    }

    #endregion
}