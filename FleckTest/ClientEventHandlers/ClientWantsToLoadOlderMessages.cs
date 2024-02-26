using System.Text.Json;
using Fleck;
using FleckTest.DataLayer;
using FleckTest.Models;
using lib;

namespace FleckTest;

public class ClientWantsToLoadOlderMessagesDto : BaseDto
{
    public int roomId { get; set; }
}

public class ClientWantsToLoadOlderMessages(ChatRepository chatRepository)
    : BaseEventHandler<ClientWantsToLoadOlderMessagesDto>
{
    public override Task Handle(ClientWantsToLoadOlderMessagesDto dto, IWebSocketConnection socket)
    {
        var oldMessages = chatRepository.GetMessages(dto.roomId);
        socket.Send(JsonSerializer.Serialize(new ServerSendsOlderMessagesToClient
        {
            roomId = dto.roomId,
            messages = oldMessages
        }));
        return Task.CompletedTask;
    }
}

public class ServerSendsOlderMessagesToClient : BaseDto
{
    public int roomId { get; set; }
    public IEnumerable<Message>? messages { get; set; }
}