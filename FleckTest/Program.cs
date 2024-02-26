using System.Reflection;
using System.Text.Json;
using Fleck;
using FleckTest;
using FleckTest.DataLayer;
using lib;

/*var server = new WebSocketServer("ws://0.0.0.0:8181");

var wsConnections = new List<IWebSocketConnection>();
server.Start(ws =>
{
    ws.OnOpen = () =>
    {
        wsConnections.Add(ws);
    };
    ws.OnMessage = message =>
    {
        foreach (var webSocketConnection in wsConnections)
        {
            webSocketConnection.Send(message);
        }
    };
});

WebApplication.CreateBuilder(args).Build().Run();*/


public static class Startup
{
    public static void Main(string[] args)
    {
       Statup(args);
       Console.ReadLine();
    }

    public static void Statup(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());
        builder.Services.AddSingleton<ChatRepository>();

        var app = builder.Build();
        
        var server = new WebSocketServer("ws://0.0.0.0:8181");
        server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                StateService.AddConnection(socket);
                Console.WriteLine("Open!");
                Connections.allSockets.Add(socket);
            };
            socket.OnClose = () =>
            {
                Console.WriteLine("Close!");
                Connections.allSockets.Remove(socket);
            };
            socket.OnMessage = async message =>
            {
                Console.WriteLine();
                Console.WriteLine(message);
                Console.WriteLine();
                try
                {
                    await app.InvokeClientEventHandler(clientEventHandlers, socket, message);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.InnerException);
                    Console.WriteLine(e.StackTrace);
                    // Write exception here
                }
            };
        });
    }
}




