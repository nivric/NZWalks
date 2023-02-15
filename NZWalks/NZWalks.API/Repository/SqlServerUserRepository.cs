using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Migrations;
using NZWalks.API.Models.Domain;
using System.Linq;

namespace NZWalks.API.Repository
{
    public class SqlServerUserRepository : IUserRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public SqlServerUserRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await nZWalksDbContext.Users.
                Include(usr=>usr.UserRoles).FirstOrDefaultAsync(user => user.UserName.Equals(username) && user.Password.Equals(password));
            user.Password = null;
            if (user == null)
            {
                return null;
            }

            foreach(var usr_role in user.UserRoles.ToList())
            {
                var role = await nZWalksDbContext.Roles.FirstOrDefaultAsync(role => role.Id == usr_role.RoleId);
                if (role != null)
                {
                    usr_role.Role = role;
                }
            }
           
            return user;
        }
    }
}
