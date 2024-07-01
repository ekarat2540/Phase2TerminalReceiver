using System.Net;
using System.Net.Sockets;

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
            string message = await streamReader.ReadLineAsync();
            if (message == "RECEIVER_REGISTERED")
            {
                Console.WriteLine("Receiver is registered. Waiting for Sender to connect...");
            }
            streamReader.Close();
            writer.Close();
            stream.Close();
            client.Close();
            TcpClient senderClient = new TcpClient();
            senderClient.Connect(serverIp, serverPort); // ใช้การเชื่อมต่อขาออกไปยังเซิร์ฟเวอร์
            NetworkStream senderStream = senderClient.GetStream();
            StreamReader reader = new StreamReader(senderStream);

            while ((message = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine($"Received: {message}");
            }

            // ปิดการเชื่อมต่อ
            reader.Close();
            senderStream.Close();
            senderClient.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
