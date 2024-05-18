using ChatClient.Entities;
using ChatClient.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class Seeder
    {
        private readonly ChatDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public Seeder(ChatDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Users.Any())
                {
                    var user = GetUser();
                    _context.Users.AddRange(user);
                    _context.SaveChanges();
                }
                if (!_context.ChatRooms.Any())
                {
                    var chatRoom = GetChatRoom();
                    _context.ChatRooms.AddRange(chatRoom);
                    _context.SaveChanges();
                }
            }
        }

        public IEnumerable<User> GetUser()
        {
            var user = new List<User>()
            {
                new User()
                {
                    Name = "TestUser",
                    Email = "TestUserEmail",
                    PasswordHash = "AQAAAAEAACcQAAAAEKX2VjDgdcfxRBcVNgdv10Gset51cAd0Q+vx1fyGeSruY/gFxoOv0IfSW2S3K2POqw==", //TestUser
                    Role = UserRole.User
                },
                new User()
                {
                    Name = "TestAdmin",
                    Email = "TestAdminEmail",
                    PasswordHash = "AQAAAAEAACcQAAAAEJc5PeNND4MI+GB6fLBpiyT7u87Dr617VyFW36/pTEhugCRFc3b14eifTH2i/Fau4A==", //TestAdmin
                    Role = UserRole.Admin
                },
                new User()
                {
                    //Id = 777,
                    Name = "Zuzanna",
                    Email = "zuzmor@mail.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEPA7rSD2PBaz0meBWHqkRuidUTT4zT0AQdIStQKAbckj4aPXtE9TRrBQUH+zYPNmfg==", //Zuzanna
                    Role = UserRole.Zuzanna
                }
            };
            return user;
        }

        public IEnumerable<ChatRoom> GetChatRoom()
        {
            var chatRoom = new List<ChatRoom>()
            {
                new ChatRoom()
                {
                    Name = "TestRoomUnlocked",
                    PasswordHash = null,
                    Messages = null
                },
                new ChatRoom()
                {
                    Name = "TestRoomLocked",
                    PasswordHash = "AQAAAAEAACcQAAAAEKX2VjDgdcfxRBcVNgdv10Gset51cAd0Q+vx1fyGeSruY/gFxoOv0IfSW2S3K2POqw==", //TestUser
                    Messages = null
                }
            };
            return chatRoom;
        }
    }
}