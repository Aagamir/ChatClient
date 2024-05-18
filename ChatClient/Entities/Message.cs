using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; }
        public string UserName { get; set; }
        public int RoomId { get; set; }
        public DateTime Created { get; set; }
    }
}