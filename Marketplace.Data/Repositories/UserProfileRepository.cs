using Marketplace.Data.Infrastructure;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Data.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IDbFactory dbFactory)
            : base(dbFactory) { }



        public UserProfile GetUserById(int userId)
        {
            return DbContext.UserProfiles.Find(userId);
        }

        public async Task<UserProfile> GetUserByIdAsync(int userId)
        {
            return await DbContext.UserProfiles.FindAsync(userId);
        }

        public UserProfile GetUserByName(string userName)
        {
            return DbContext.UserProfiles.Include(u => u.User).FirstOrDefault(u => u.User.UserName == userName);
        }

        public async Task<UserProfile> GetUserByNameAsync(string userName)
        {
            return await DbContext.UserProfiles.Include(u => u.User).FirstOrDefaultAsync(u => u.User.UserName == userName);
        }


    }

    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        UserProfile GetUserByName(string userName);
        UserProfile GetUserById(int userId);
        Task<UserProfile> GetUserByIdAsync(int userId);
        Task<UserProfile> GetUserByNameAsync(string userName);


    }
}
