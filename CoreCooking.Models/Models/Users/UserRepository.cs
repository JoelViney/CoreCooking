using CoreCooking.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCooking.Models.Users
{

    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(string connectionString) : base(connectionString, "users")
        {

        }

        public UserRepository(string connectionString, string containerPath) : base(connectionString, containerPath)
        {

        }

        public async Task<LoginResult> Login(string name, string password)
        {
            bool success = true;
            string message = null;
            var list = await this.GetListAsync();

            User item;
            if (list.Count == 0)
            {
                User user = new User(name, password);
                await this.SaveAsync(user);
                item = user;
            }
            else
            {
                item = list.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            }

            if (item.Password != password)
            {
                success = false;
                message = "Invalid username or password";
                item = null;
            }

            return new LoginResult() { User = item, Success = success, Message = message };        
        }

        public async Task<User> FindByNameAsync(string name)
        {
            var list = await this.GetListAsync();

            var item = list.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            return item;
        }
    }
}
