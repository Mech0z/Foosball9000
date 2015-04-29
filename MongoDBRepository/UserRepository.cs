using Models;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBRepository
{
    public class UserRepository : MongoBase<User>, IUserRepository
    {
        public UserRepository() : base("Users")
        {
        }

        public List<User> GetUsers()
        {
            return Collection.FindAll().ToList();
        }

        public void AddUser(User user)
        {
            Collection.Save(user);
        }
    }
}