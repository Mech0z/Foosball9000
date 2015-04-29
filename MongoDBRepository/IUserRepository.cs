using System.Collections.Generic;
using Models;

namespace MongoDBRepository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        void AddUser(User user);
    }
}