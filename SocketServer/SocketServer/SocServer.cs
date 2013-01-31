using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace POC.SocketServer
{
    class SocServer
    {
        static TcpListener listener;
        const int Limit = 2;
        const int portno = 2099;
        static void Main(string[] args)
        {
            listener = new TcpListener(portno);
            listener.Start();
            Console.WriteLine("Listening to port {0}", portno);

            for (int i = 0; i < Limit; i++)
            {
                Thread t = new Thread(new ThreadStart(Service));
                t.Start();
            }
        }

        public static void Service()
        {
            while (true)
            {
                Socket soc = listener.AcceptSocket();
                try {
                    Stream s = new NetworkStream(soc);
                    StreamReader sr = new StreamReader(s);
                    StreamWriter sw = new StreamWriter(s);
                    sw.AutoFlush = true;
                    sw.WriteLine("Send Message: ");
                    while (true)
                    {
                        string msg = sr.ReadLine();
                        if (msg == "" || msg == null) break;
                        Console.WriteLine(msg);
                        sw.WriteLine("Message received");
                    }
                    s.Close();
                }
                catch(Exception e){
                    Console.WriteLine(e.Message);
                }
                soc.Close();
            }
            
        }
    }
}
