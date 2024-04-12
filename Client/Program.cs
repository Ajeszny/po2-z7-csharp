using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ.Sockets;
using NetMQ;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (RequestSocket client = new RequestSocket())
            {
                try
                {
                    client.Connect("tcp://127.0.0.1:1337");
                    Console.WriteLine("Enter your message: ");
                    String msg = Console.ReadLine();
                    client.SendFrame(msg);
                    Console.WriteLine(client.ReceiveFrameString());
                    long zalupa = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    client.SendFrame(zalupa.ToString());
                    String tstamp = client.ReceiveFrameString();
                    Console.WriteLine("Recieved: {0}", tstamp);
                    client.Close();
                } catch (Exception e)
                {
                    Console.WriteLine("Server not running!" + e.Message);
                }
            }
            while (true)
            {

            }
        }
    }
}
