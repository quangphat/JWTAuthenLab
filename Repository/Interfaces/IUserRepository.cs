using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        User Login(string username, string password);
        List<User> GetAll();
        User GetById(int id);
    }
}
