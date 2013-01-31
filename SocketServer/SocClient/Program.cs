using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace POC.SocClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("localhost", 2099);
            try
            {
                Stream s = client.GetStream();
                StreamReader sr = new StreamReader(s);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true;
                Console.WriteLine(sr.ReadLine());
                while (true)
                {
                    Console.Write("Name:");
                    string message = Console.ReadLine();
                    sw.WriteLine(message);
                    if (message == "")
                    {
                        Console.WriteLine(sr.ReadLine());
                        break;
                    }
                }
                s.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                client.Close();
            }
        }
    }
}
