using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class StaticUserRepository : IUserRepository
    {
        List<User> users = new List<User>
        {
              new User 
              {
                  Id = Guid.NewGuid(), FirstName = "Nived", LastName = "Richard", Email = "nived501@gmail.com",
              Password = "notverystrong", UserName = "nivric",// Roles= new List<string> {"admin","reader"}
              },

              new User 
              {
                  Id = Guid.NewGuid(), FirstName = "Anjali", LastName = "Richard", Email = "anjali@gmail.com",
              Password = "notverylong", UserName = "anjali",// Roles= new List<string> {"reader"}
              }
        };

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = users.Find(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(password));
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
