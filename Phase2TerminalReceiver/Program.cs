using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;

class Receiver
{
    static async Task Main(string[] args)
    {
        try
        {
            string serverIp = "192.168.1.100"; // IP ของ Server
            int serverPort = 5713;
            TcpClient client = new TcpClient("192.168.1.100",5713);
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            StreamReader streamReader = new StreamReader(stream);
            writer.WriteLine("RECEIVER");
            writer.Flush();
            string message;
            while ((message = await streamReader.ReadLineAsync()) != null)
            {
                Console.WriteLine($"Sender: {message}");
            }
            writer.Close();
            streamReader.Close();
            stream.Close();
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
