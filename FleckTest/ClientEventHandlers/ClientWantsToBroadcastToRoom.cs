using System.Text.Json;
using Fleck;
using FleckTest.DataLayer;
using FleckTest.Models;
using lib;

namespace FleckTest;

public class ClientWantsToBroadcastToRoomDto : BaseDto
{
    public string message { get; set; }
    public int roomId { get; set; }
}

public class ClientWantsToBroadcastToRoom(ChatRepository chatRepository) : BaseEventHandler<ClientWantsToBroadcastToRoomDto>
{
    public override Task Handle(ClientWantsToBroadcastToRoomDto dto, IWebSocketConnection socket)
    {
        var insertedMessage = new Message
        {
            room = dto.roomId,
            message = dto.message,
            username = StateService.Connections[socket.ConnectionInfo.Id].Username
        };
        var message = new ServerBroadcastsMessageWithUsername()
        {
            message = insertedMessage
        };
        StateService.BroadcastToRoom(dto.roomId, JsonSerializer.Serialize(message));
        //chatRepository.SendMessage(insertedMessage);
        return Task.CompletedTask;
    }
}

public class ServerBroadcastsMessageWithUsername : BaseDto
{
    public Message? message { get; set; }
}