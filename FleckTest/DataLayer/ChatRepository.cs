using Dapper;
using FleckTest.Models;

namespace FleckTest.DataLayer;

public class ChatRepository
{
    public IEnumerable<Message> GetMessages(int room)
    {
        var sql = $@"
        SELECT username, message
        FROM chatroom
        WHERE room = @room;";
    
        using (var conn = DatabaseConnection.DataSource.OpenConnection())
        {
            return conn.Query<Message>(sql, new { room });
        }
    }

    public Message SendMessage(Message message)
    {
        var sql = $@"INSERT INTO chatroom(room, message, username) VALUES (@room, @message, @username);";
        using (var conn = DatabaseConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Message>(sql, new
            {
                room = message.room,
                message = message.message,
                username = message.username
            });
        }
    }
}