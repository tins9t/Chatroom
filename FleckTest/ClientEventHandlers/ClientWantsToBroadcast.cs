using System.Text.Json;
using Fleck;
using lib;

namespace FleckTest;

public class ClientWantsToBroadcastDto : BaseDto
{
    public string? messageContent { get; set; }
}

public class ClientWantsToBroadcast : BaseEventHandler<ClientWantsToBroadcastDto>
{
    public override Task Handle(ClientWantsToBroadcastDto dto, IWebSocketConnection socket)
    {
        foreach (var webSocketConnection in Connections.allSockets)
        {
            var broadcast = new ClientBroadcast()
            {
                broadcastValue = "broadcast: " + dto.messageContent
            };
            var messageToClient = JsonSerializer.Serialize(broadcast);
            webSocketConnection.Send(messageToClient);
        }

        return Task.CompletedTask;
    }
}

public class ClientBroadcast : BaseDto
{
    public string? broadcastValue { get; set; }
}
