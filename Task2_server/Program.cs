using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Socket server = new Socket(AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);

IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 2526);
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

    string responseMessage = "";

    if (message == "Date")
    {
        responseMessage = $"{(DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString())}." +
            $"{(DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString())}." +
            $"{(DateTime.Now.Year < 10 ? "0" + DateTime.Now.Year.ToString() : DateTime.Now.Year.ToString())}";
    }
    else if (message == "Time")
    {
        responseMessage = $"{(DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString())}:" +
           $"{(DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString())}:" +
           $"{(DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString())}";
    }
    else
    {
        responseMessage = "\"Date\" or \"Time\" only";
    }

    Console.WriteLine($"Клієнт {client.RemoteEndPoint} => {message}");
  
    byte[] responseBytes = Encoding.UTF8.GetBytes(responseMessage);
    client.Send(responseBytes);

    client.Shutdown(SocketShutdown.Both);
    client.Close();
}