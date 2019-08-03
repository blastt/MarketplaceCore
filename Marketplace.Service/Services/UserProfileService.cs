﻿using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Services
{
    public interface IUserProfileService
    {
        IEnumerable<UserProfile> GetAllUserProfiles();
        IEnumerable<UserProfile> GetAllUserProfiles(Func<IQueryable<UserProfile>, IIncludableQueryable<UserProfile, object>> include);
        Task<List<UserProfile>> GetAllUserProfilesAsync();
        Task<List<UserProfile>> GetAllUserProfilesAsync(Func<IQueryable<UserProfile>, IIncludableQueryable<UserProfile, object>> include);

        UserProfile GetUserProfile(Expression<Func<UserProfile, bool>> where, Func<IQueryable<UserProfile>, IIncludableQueryable<UserProfile, object>> include);
        UserProfile GetUserProfileById(int id);
        UserProfile GetUserProfileByName(string name);

        Task<UserProfile> GetUserProfileAsync(Expression<Func<UserProfile, bool>> where, Func<IQueryable<UserProfile>, IIncludableQueryable<UserProfile, object>> include);
        Task<UserProfile> GetUserProfileByIdAsync(int id);
        Task<UserProfile> GetUserProfileByNameAsync(string name);

        void CreateUserProfile(UserProfile userProfile);
        void UpdateUserProfile(UserProfile userProfile);
        void RemoveUserProfile(UserProfile userProfile);
        void SaveUserProfile();
        Task SaveUserProfileAsync();
    }

    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository userProfilesRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserProfileService(IUserProfileRepository userProfilesRepository, IUnitOfWork unitOfWork)
        {
            this.userProfilesRepository = userProfilesRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IUserProfileService Members

        public IEnumerable<UserProfile> GetAllUserProfiles()
        {
            var userProfile = userProfilesRepository.GetAll();
            return userProfile;
        }

        public IEnumerable<UserProfile> GetAllUserProfiles(Func<IQueryable<UserProfile>, IIncludableQueryable<UserProfile, object>> include)
        {
            var userProfile = userProfilesRepository.GetAll(include);
            return userProfile;
        }

        public async Task<List<UserProfile>> GetAllUserProfilesAsync()
        {
            return await userProfilesRepository.GetAllAsync();
        }

        public async Task<List<UserProfile>> GetAllUserProfilesAsync(Func<IQueryable<UserProfile>, IIncludableQueryable<UserProfile, object>> include)
        {
            return await userProfilesRepository.GetAllAsync(include);
        }



        public UserProfile GetUserProfileById(int id)
        {
            var userProfile = userProfilesRepository.GetUserById(id);
            return userProfile;
        }

        public UserProfile GetUserProfile(Expression<Func<UserProfile, bool>> where, Func<IQueryable<UserProfile>, IIncludableQueryable<UserProfile, object>> include)
        {
            var userProfile = userProfilesRepository.Get(where, include);
            return userProfile;
        }

        public UserProfile GetUserProfileByName(string name)
        {
            var userProfile = userProfilesRepository.GetUserByName(name);
            return userProfile;
        }

        public async Task<UserProfile> GetUserProfileByIdAsync(int id)
        {
            return await userProfilesRepository.GetUserByIdAsync(id);
        }

        public async Task<UserProfile> GetUserProfileAsync(Expression<Func<UserProfile, bool>> where, Func<IQueryable<UserProfile>, IIncludableQueryable<UserProfile, object>> include)
        {
            return await userProfilesRepository.GetAsync(where, include);
        }

        public async Task<UserProfile> GetUserProfileByNameAsync(string name)
        {
            return await userProfilesRepository.GetUserByNameAsync(name);
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            userProfilesRepository.Update(userProfile);
        }






        public void CreateUserProfile(UserProfile userProfile)
        {
            userProfilesRepository.Add(userProfile);
        }

        public void RemoveUserProfile(UserProfile userProfile)
        {
            userProfilesRepository.Remove(userProfile);
        }

        public void SaveUserProfile()
        {
            unitOfWork.SaveChangesAsync();
        }

        public async Task SaveUserProfileAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        #endregion

    }
}
