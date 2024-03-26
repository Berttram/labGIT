using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lab_1_2_server
{
    public class ClientHandler
    {
        public TcpClient clientSocket;
        public void RunClient()
        {
            StreamReader readerSStream = new StreamReader(clientSocket.GetStream());
            NetworkStream writerStream = clientSocket.GetStream();

            string returnData = readerSStream.ReadLine();
            string name = returnData;

            Console.WriteLine("Welcome, " + name);

            while (true)
            {
                returnData = readerSStream.ReadLine();

                if(returnData.IndexOf("QUIT") > -1)
                {
                    Console.WriteLine("Goodbye, " + name);
                    break;
                }

                Console.WriteLine(name + ": " + returnData);
                returnData += "\r\n";

                byte[] dataWrite = Encoding.ASCII.GetBytes(returnData);
                writerStream.Write(dataWrite, 0, dataWrite.Length);
            }
            clientSocket.Close();
        }
    }
}
