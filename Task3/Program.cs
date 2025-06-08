using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Socket client = new Socket(AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);


Console.Write("Вкажіть ІР-адресу: ");
string ip = Console.ReadLine();

Console.Write("Вкажіть мінімальний порт: ");
int minPort = int.Parse(Console.ReadLine() ?? "0");

Console.Write("Вкажіть максимальний порт: ");
int maxPort = int.Parse(Console.ReadLine() ?? "0");

if (minPort > maxPort)
{
    int tmp = minPort;
    minPort = maxPort;
    maxPort = tmp;
}

for (int currentPort = minPort; currentPort <= maxPort; currentPort++)
{
    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    try
    {
        client.Connect(ip, currentPort);
        Console.WriteLine($"{currentPort} відкритий");
    }
    catch
    {
        Console.WriteLine($"{currentPort} закритий");
    }
}