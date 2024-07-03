using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Receiver
{
    public static async Task Main(string[] args)
    {
        try
        {
            string serverIp = "192.168.1.102"; // IP ของ Server
            int serverPort = 5713;

            UdpClient udpClient = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

            string registerMessage = "RECEIVER";
            byte[] registerData = Encoding.UTF8.GetBytes(registerMessage);
            await udpClient.SendAsync(registerData, registerData.Length, serverEndPoint);

            UdpReceiveResult result = await udpClient.ReceiveAsync();
            string response = Encoding.UTF8.GetString(result.Buffer);

            if (response == "RECEIVER_REGISTERED")
            {
                Console.WriteLine("Receiver is registered. Waiting for messages...");
            }

            while (true)
            {
                UdpReceiveResult messageResult = await udpClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(messageResult.Buffer);
                Console.WriteLine($"Sender: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
