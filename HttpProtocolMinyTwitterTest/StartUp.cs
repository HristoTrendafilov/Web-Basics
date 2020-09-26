using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpProtocol
{
    class StartUp
    {
        static async Task Main(string[] args)
        {
            var context = new HttpTestDbContext();
            context.Database.EnsureCreated();
            Console.WriteLine("database has been created!");

            Console.OutputEncoding = Encoding.UTF8;
            const string NewLine = "\r\n";

            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);
            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();

                using (
                    var stream = client.GetStream())
                {
                    var buffer = new byte[1000000];
                    var length = stream.Read(buffer, 0, buffer.Length);

                    var requestString = Encoding.UTF8.GetString(buffer, 0, length);

                    var typeOfRequest = requestString.Split(" ")[0];
                    
                    if (typeOfRequest == "POST")
                    {
                        var usernameRegex = Regex.Match(requestString, "(username)=[A-z0-9 ]*");
                        var username = usernameRegex.Value.Split("=")[1];

                        var commentRegex = Regex.Match(requestString, "(comment)=[A-z0-9+]*");
                        var comment = commentRegex.Value.Split("=")[1].Replace('+',' ');

                        var user = new User() { Username = username, Comment = comment };

                        context.Users.Add(user);
                        context.SaveChanges();
                    }

                    string html = $"<h1>Hello from Miny Twitter {DateTime.Now}</h1>" +
                        $"<form action=/tweet method=post>Username<input name=username />Comment<input name=comment />" +
                        $"<input type=submit /></form>";

                    foreach (var user in context.Users)
                    {
                        html += $"<center>{user.Username}:</center> <center>{user.Comment}</center><br>";
                    }

                    string response = "HTTP/1.1 200 OK" + NewLine +
                        "Server: MyServer 2020" + NewLine +
                        "Content-Type: text/html; charset=utf-8" + NewLine +
                        "Content-Lenght: " + html.Length + NewLine +
                        NewLine +
                        html + NewLine;

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes);

                    Console.WriteLine(new string('=', 70));
                }
            }
        }
    }
}
