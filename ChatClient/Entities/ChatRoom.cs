using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Entities
{
    public class ChatRoom
    {
        public int ChatRoomId { get; set; }
        public string Name { get; set; }
        public string? PasswordHash { get; set; }
        public List<Message?> Messages { get; set; }
    }
}