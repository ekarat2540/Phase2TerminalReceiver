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
            string ipAndPort = await streamReader.ReadLineAsync();
            string split = ipAndPort.Split(":");
            string ipSender = split[0];
            int port = split[1];
            writer.Flush();
            streamReader.Close();
            stream.Close();
            writer.Close();
            client.Close();

            Console.WriteLine("TEST");
        }catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
