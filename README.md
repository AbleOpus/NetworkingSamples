# NetworkingSamples
Provides sample code for TCP socket programming. The samples are provided to demonstrate specific concepts and are not scalable solutions. __Do not ask me to add functionality to these projects, or make them more robust.__ The programs can connect remotely much like they can locally. Simple change the target IP address in the client (be it in a textbox or particular line of code), then open the appropriate port for the server. __Do not ask me to change the programs so they can connect remotely.__ And do not ask me how to connect them remotely. If you cannot connect them remotely, it is not a problem with the programs.

__Fundamental knowledge of C# and networking is required to work with sockets.__ Socket programming is easily one of the hardest and most frustrating things to do in computer programming. Trying to do socket programming without the fundamentals is like trying to sprint without knowing how to crawl. You will only fall on your face, and have seemingly eternal frustration.
If you need a simple yet robust TCP socket solution, take a look at my [TcpInteract](https://github.com/AbleOpus/TcpInteract) repository. I still recommend acquiring fundamental C# skills before using the library. For instance, there is no point wasting your time trying to implement such a library while having no understanding or generics and events.
## BasicAsyncClient
A WinForms application that demonstrates asynchronous connect and send using the “Begin” and “End” methods. Uses custom/manual serialization.
## BasicAsyncServer
A WinForms application that demonstrates asynchronous connect and send using the “Begin” and “End” methods. Uses custom/manual serialization.
## MultiClient
A console application demonstrating a client that is used to connect to a server that accepts multiple clients. Sends and receives simple strings.
## MultiServer
A console application demonstrating multiple client connections. It uses asynchronous accept and receive, and synchronous send. Sends and receives simple strings.
## SyncSocketsConsole
Demonstrates synchronous sockets in a very simple solution. Both the client and server are part of the same console application. The console application starts in client-mode when the “/c” command-line switch is provided to it. It starts in server-mode when the “/s” command-line switch is provided to it. But Brian, what is a command-line switch? __Something that should be understood 4 years in advance to any understanding of sockets…__
