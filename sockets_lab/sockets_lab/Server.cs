using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace sockets_lab
{
    internal class Server
    {
        public static void RunServer()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 6969;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Creating random number
                Random rnd = new Random();
                int value = 0;

                int time = 0;
                int result = 0;
                bool bst = false;

                // Enter the listening loop.
                while (true)
                {

                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        switch (data)
                        {
                            case "getnumber":
                                data = "Random number is: " + value;
                                break;
                            case "help":
                                data = "\n1. GetNumber - Get random number for this server session \n2. NewNumber - Generate new random number \n3. Help - Command list \n4. Who - Get information \n5. GetTime - Get the time set\n6. SetTime - Set the time for guessing\n7. Try - Try to find a number";
                                break;
                            case "newnumber":
                                data = "New random number is generated";
                                value = rnd.Next(0, 2000000);
                                break;
                            case "who":
                                data = "\nName: Arsenii Sahalianov \nGroup: K-28 \nOption: 20 ";
                                break;
                            case "try":
                                result = Algorithms.MonteCarlo(value, time);
                                if (result == value)
                                    data = "Success! The number has been successfully picked up!";
                                else
                                    data = "Fail! There wasn't enough time to pick it up, the closest number to the right one was: " + result;
                                break;
                            case "settime":
                                data = "Enter the time to try to guess: ";
                                bst = true;
                                break;
                            case "gettime":
                                data = "Current SetTime is: " + time;
                                break;
                            default:
                                if (bst == false)
                                    data = "Error try again";
                                else
                                {
                                    time = Int32.Parse(data);
                                    bst = false;
                                    data = "You set the time as: " + time;
                                }
                                break;
                        }


                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.Read();
        }
    }

}
