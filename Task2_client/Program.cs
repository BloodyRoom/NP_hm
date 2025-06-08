using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Socket client = new Socket(AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);

string ipServer = "127.0.0.1";

Console.Write("Вкажіть текст повідомлення: ");
string text = Console.ReadLine();
client.Connect(ipServer, 2526);

client.Send(Encoding.UTF8.GetBytes(text));

byte[] buffer = new byte[1024];
int bytesRead = client.Receive(buffer);
string responseMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
Console.WriteLine($"Відповідь: {responseMessage}");

client.Shutdown(SocketShutdown.Both);
client.Close();