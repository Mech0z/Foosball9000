using Models;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Builders;

namespace MongoDBRepository
{
    public class UserRepository : MongoBase<User>, IUserRepository
    {
        public UserRepository() : base("Users")
        {
        }

        public List<User> GetUsers()
        {
            var users = Collection.FindAll().ToList();

            foreach (User user in users)
            {
                if (user.Password == null || user.Password == "default")
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword("default", BCrypt.Net.BCrypt.GenerateSalt());
                    AddUser(user);
                }
            }

            return users;
        }

        public void AddUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword("default", BCrypt.Net.BCrypt.GenerateSalt());

            Collection.Save(user);
        }

        public string Login(User inputUser)
        {
            var user = Collection.Find(Query<User>.Where(x => x.Email == inputUser.Email)).SingleOrDefault();

            if (user != null)
            {
                if(BCrypt.Net.BCrypt.CheckPassword(inputUser.Password, user.Password))
                {
                    return user.Password;
                }
            }

            return string.Empty;
        }

        public string ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = Collection.Find(Query<User>.Where(x => x.Email == email)).SingleOrDefault();

            if (!BCrypt.Net.BCrypt.CheckPassword(oldPassword, user.Password))
            {
                return string.Empty;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, BCrypt.Net.BCrypt.GenerateSalt());

            Collection.Save(user);

            return user.Password;
        }

        public bool Validate(User inputUser)
        {
            var user = Collection.Find(Query<User>.Where(x => x.Email == inputUser.Email)).SingleOrDefault();

            if (user != null)
            {
                if (user.Password == inputUser.Password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}