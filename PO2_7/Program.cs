using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using NetMQ.Sockets;
using NetMQ;

namespace PO2_7
{
    internal class Program
    {
        static void ServeClient(ResponseSocket serv)
        {
            string msg = serv.ReceiveFrameString();
            serv.SendFrame("Message acknowledged, proceeding to read the timestamp!");
            string tstamp = serv.ReceiveFrameString();
            Console.WriteLine("Message: " + msg + "\nTimestamp: " + tstamp);
            long zalupa =  DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            serv.SendFrame(zalupa.ToString());
        }
       
        static void Main(string[] args)
        {
            try
            {
                using (ResponseSocket serv = new ResponseSocket())
                {
                    serv.Bind("tcp://*:1337");

                    while (true)
                    {
                        ServeClient(serv);
                    }
                }

            } catch (Exception e)
            {

            }
        }
    }
}
