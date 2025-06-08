using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Socket server = new Socket(AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);

IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 2525);
server.Bind(serverEndPoint);

server.Listen(10);

Console.ForegroundColor = ConsoleColor.DarkGray;
Console.WriteLine("Сервер запущенно");
Console.ResetColor();

while (true)
{
    Socket client = server.Accept();

    var buffer = new byte[1024];
    int bytesRead = client.Receive(buffer);

    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

    Console.WriteLine($"О {(DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString())}:" +
        $"{(DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString())} " +
        $"від {client.RemoteEndPoint} " +
        $"отримано рядок: {message}");

    string responseMessage = $"Привіт, клієнт!";
    byte[] responseBytes = Encoding.UTF8.GetBytes(responseMessage);

    client.Send(responseBytes);

    client.Shutdown(SocketShutdown.Both);
    client.Close();
}