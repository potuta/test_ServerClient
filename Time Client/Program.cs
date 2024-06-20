using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Time_CLient
{
    class Program
    {
        private static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            Console.Title = "Client";
            LoopConnect();
            SendLoop();
        }

        private static void SendLoop()
        {
            while (true)
            {
                Console.Write("Enter a request: ");
                string request = Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(request);
                clientSocket.Send(buffer);

                byte[] receivedBuffer = new byte[1024];
                int received = clientSocket.Receive(receivedBuffer);
                byte[] data = new byte[received];
                Array.Copy(receivedBuffer, data, received);

                Console.WriteLine("Received: " + Encoding.ASCII.GetString(data));
            }
        }

        private static void LoopConnect()
        {
            int attempts = 0;
            while (!clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    clientSocket.Connect(IPAddress.Parse("112.204.108.171"), 6969);
                }
                catch (SocketException se)
                {
                    Console.Clear();
                    Console.WriteLine("Connection attempts: " + attempts.ToString() + "\n "+ se.Message);
                }
            }
            Console.Clear();
            Console.WriteLine("Connected");
        }
    }
}