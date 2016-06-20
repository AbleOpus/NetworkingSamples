using System;

namespace SyncSocketsConsole
{
    class Program
    {
        public const int Port = 3333;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided.");
                Console.ReadLine();
                return;
            }

            // Argument "/c" will start this program in client-mode.
            // Argument "/s" will start this program in server-mode.
            switch (args[0].ToLower())
            {
                case "/c": 
                    Client client=  new Client();
                    client.StartSendLoop();
                    break;

                case "/s":
                    // Start this app in client mode as well.
                    string fileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    System.Diagnostics.Process.Start(fileName, "/c");

                    Server server = new Server();
                    server.Start();
                    break;
            }
        }
    }
}
