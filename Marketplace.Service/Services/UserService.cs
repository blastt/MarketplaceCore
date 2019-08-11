using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        Task<List<User>> GetAllUsersAsync();
        //int GetUser(int id);
        Task<int> GetCurrentUserId(ClaimsPrincipal principal);
        User GetUser(Expression<Func<User, bool>> where, Func<IQueryable<User>, IIncludableQueryable<User, object>> include);

        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(Expression<Func<User, bool>> where, Func<IQueryable<User>, IIncludableQueryable<User, object>> include);

        Task<IdentityResult> CreateUserAsyncAndSave(User user, string password);

        void SaveDialog();
        Task SaveDialogAsync();
    }
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsyncAndSave(User user, string userPassword)
        {
            //userRepository.Add(user);
            IdentityResult result = await userManager.CreateAsync(user, userPassword);

            return result;
        }

        public async Task<int> GetCurrentUserId(ClaimsPrincipal principal)
        {

            User user = await userManager.GetUserAsync(principal);
            var userID = user.Id;
            return userID;
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(Expression<Func<User, bool>> where, Func<IQueryable<User>, IIncludableQueryable<User, object>> include)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(Expression<Func<User, bool>> where, Func<IQueryable<User>, IIncludableQueryable<User, object>> include)
        {
            throw new NotImplementedException();
        }

        public void SaveDialog()
        {
            throw new NotImplementedException();
        }

        public Task SaveDialogAsync()
        {
            throw new NotImplementedException();
        }
    }
}
