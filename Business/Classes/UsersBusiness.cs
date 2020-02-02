using Business.Infrastructures;
using Business.Interfaces;
using Entities;
using Entities.Infrastructures;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Business.Classes
{
    public class UsersBusiness : IUsersBusiness
    {
        protected readonly IUserRepository _rpUser;
        private readonly AppSetting _appSettings;
        public UsersBusiness(IOptions<AppSetting> options, IUserRepository userRepository)
        {
            _appSettings = options.Value;
            _rpUser = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            var user = _rpUser.Login(username, password);
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[] {
                        new Claim (ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user.WithoutPassword();
        }
        public List<User> GetAll()
        {
            return _rpUser.GetAll().ToList();
        }
        public User GetById(int id)
        {
            var user = _rpUser.GetById(id);
            return user;
        }
    }
}
