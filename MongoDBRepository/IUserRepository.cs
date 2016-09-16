using System.Collections.Generic;
using Models;

namespace MongoDBRepository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        void AddUser(User user);
        string Login(User inputUser);
        bool Validate(User inputUser);
        string ChangePassword(string email, string hashedPassword, string newPassword);
        bool ValidateAndHasRole(User inputUser, string role);
    }
}