using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SyncSocketsConsole
{
    class Server
    {
        private Socket serverSocket, clientSocket;
        private byte[] buffer;

        public void Start()
        {
            // Create server socket and listen on any local interface.
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3333));
            serverSocket.Listen(0);
            // Followed by periods to indicate work.
            Console.WriteLine("Listening for connections on port " + Program.Port + "...");
            clientSocket = serverSocket.Accept();
            Console.WriteLine("Client Connected");
            Console.WriteLine("Waiting for data...");
            buffer = new byte[clientSocket.ReceiveBufferSize];
            ReceiveData();
            Console.WriteLine("Server loop ended. Press enter to exit.");
            Console.ReadLine();
            serverSocket.Close();
        }

        private void ReceiveData()
        {
            while (clientSocket.Connected)
            {
                int received = clientSocket.Receive(buffer);

                if (received == 0) // Assume the client has disconnected.
                {
                    Console.WriteLine("Client Disconnected");
                    break;
                }

                // Shrink the buffer so we don't get null chars in the text.
                Array.Resize(ref buffer, received);
                string receivedMsg = Encoding.ASCII.GetString(buffer);
                // Reset the buffer.
                Array.Resize(ref buffer, clientSocket.ReceiveBufferSize);
                Console.WriteLine("Message received: " + receivedMsg);
            }

            // Assume the client has disconnected and start listening again for connections.
            Console.WriteLine("\nListening again...");
            clientSocket = serverSocket.Accept();
            Console.WriteLine("Client Connected");
            Console.WriteLine("Waiting for data...");
            ReceiveData();
        }
    }
}
