using System.Net;
using System.Net.Sockets;

class Receiver
{
    static async Task Main(string[] args)
    {
        try
        {
            TcpClient client = new TcpClient("192.168.1.100",5713);
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            StreamReader streamReader = new StreamReader(stream);
            writer.WriteLine("RECEIVER");
            writer.Flush();
            string ipAndPort = await streamReader.ReadLineAsync();
            var split = ipAndPort.Split(":");
            string ipSender = split[0];
            string port = split[1];
            streamReader.Close();
            writer.Close();
            stream.Close();
            client.Close();
            TcpClient client2 = new TcpClient(ipSender,int.Parse(port));
            NetworkStream stream2 = client2.GetStream();
            StreamReader reader = new StreamReader(stream2);
            string message;
            while((message = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine(message);
            }
            Console.WriteLine("CLOSE");
            client2.Close();
            stream2.Close();
            reader.Close();
        }catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
