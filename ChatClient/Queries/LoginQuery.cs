using ChatClient.Entities;
using ChatClient.Exceptions;
using ChatClient.Models;
using ChatClient.Models.Dto;
using ChatClient.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Queries
{
    public class LoginQuery : IRequest<string>
    {
        public LoginDto Dto { get; set; }

        public LoginQuery(LoginDto dto)
        {
            Dto = dto;
        }

        public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
        {
            private readonly IPasswordHasher<User> _passwordHasher;
            private readonly AuthenticationSettings _authenticationSettings;
            private readonly IUserRepository _userRepository;

            public LoginQueryHandler(IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IUserRepository userRepository)
            {
                _passwordHasher = passwordHasher;
                _authenticationSettings = authenticationSettings;
                _userRepository = userRepository;
            }

            public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByEmail(request.Dto.Email, cancellationToken);
                if (user is null)
                {
                    throw new BadRequestException("Invalid username or password");
                }
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Dto.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    throw new BadRequestException("Invalid username or password");
                }
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                    _authenticationSettings.JwtIssuer,
                    claims,
                    expires: DateTime.Now.AddDays(15),
                    signingCredentials: cred);
                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);
            }
        }
    }
}