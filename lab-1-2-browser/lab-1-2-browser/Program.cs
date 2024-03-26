using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Loopback, 8080); 
        listener.Start();

        Console.WriteLine("Waiting for a connection...");

        using (TcpClient client = listener.AcceptTcpClient())
        using (NetworkStream stream = client.GetStream())
        {
            byte[] responseBytes = Encoding.UTF8.GetBytes(
                "HTTP/1.1 200 OK\r\n" +
                "Date: Wed, 11 Feb 2009 11:20:59 GMT\r\n" +
                "Server: Apache\r\n" +
                "Last-Modified: Wed, 11 Feb 2021 11:20:59 GMT\r\n" +
                "Content-Type: text/html; charset=utf-8\r\n" +
                "Content-Length: 1234\r\n\r\n" +
                "<!DOCTYPE html>\r\n" +
                "<html>\r\n" +
                "<body>\r\n" +
                "<h1>My First Heading</h1>\r\n" +
                "<p>My first paragraph.</p>\r\n" +
                "</body>\r\n" +
                "</html>\r\n");

            stream.Write(responseBytes, 0, responseBytes.Length);
            Thread.Sleep(5000);
        }

        listener.Stop();
        Console.WriteLine("HTTP response sent. Open d in your browser.");
        Console.ReadKey();
    }
}