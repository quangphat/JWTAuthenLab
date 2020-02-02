using Entities;
using Entities.Infrastructures;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Classes
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Author", LastName = "Author", Username = "author", Password = "author" , Role = Roles.Author},
            new User {Id = 2, FirstName ="Phát", LastName ="Dinh", Username="quangphat",Password ="quangphat", Role = Roles.Admin},
            new User { Id = 3, FirstName = "reader", LastName = "reader", Username = "reader", Password = "reader" , Role = Roles.Reader}
        };

        public List<User> GetAll()
        {
            return _users;
        }
        public User GetById(int id)
        {
            return _users.Where(p => p.Id == id).SingleOrDefault();
        }
        public User Login(string username, string password)
        {
            var user = _users.Where(p => p.Username == username && p.Password == password).SingleOrDefault();
            return user;
        }
    }
}
