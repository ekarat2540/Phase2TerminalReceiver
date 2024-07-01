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
            writer.WriteLine("RECEIVER");
            writer.Flush();
            stream.Close();
            writer.Close();
            client.Close();
        }catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
