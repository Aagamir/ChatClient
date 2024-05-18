using ChatClient.Entities;
using ChatClient.Enums;
using ChatClient.Exceptions;
using ChatClient.Models;
using ChatClient.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Commands
{
    public class RegisterUserCommand : IRequest<int>
    {
        public RegisterUserDto dto { get; set; }

        public RegisterUserCommand(RegisterUserDto dto)
        {
            this.dto = dto;
        }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IPasswordHasher<User> passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.dto.Name is null || request.dto.Email is null || request.dto.Password is null)
            {
                throw new ForbiddenException("User needs to have an Name, Email and Password, your request is missing one or more of those elements");
            }
            User user = new User();
            user.Name = request.dto.Name;
            user.Email = request.dto.Email;
            user.Role = UserRole.User;
            user.PasswordHash = _passwordHasher.HashPassword(user, request.dto.Password);
            await _userRepository.AddUser(user, cancellationToken);
            return user.Id;
        }
    }
}