using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IUsersBusiness
    {
        User Authenticate(string username, string password);
        List<User> GetAll();
        User GetById(int id);
    }
}
