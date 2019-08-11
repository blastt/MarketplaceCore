using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Services
{
    public interface IDialogService
    {
        IEnumerable<Dialog> GetAllDialogs();
        Task<List<Dialog>> GetAllDialogsAsync();
        Dialog GetDialog(int id);
        Dialog GetDialog(Expression<Func<Dialog, bool>> where, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include);

        Task<Dialog> GetDialogAsync(int id);
        Task<Dialog> GetDialogAsync(Expression<Func<Dialog, bool>> where, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include);

        void CreateDialog(Dialog message);

        Dialog GetPrivateDialog(UserProfile user1, UserProfile user2);
        int GetOtherUserInDialog(int dialogId, int userId);
        IEnumerable<Dialog> GetUserDialogs(int userId, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include);
        IEnumerable<Dialog> GetUserDialogs(int userId, Expression<Func<Dialog, bool>> where, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include);
        Task<List<Dialog>> GetUserDialogsAsync(int userId);
        Task<List<Dialog>> GetUserDialogsAsync(int userId, Expression<Func<Dialog, bool>> where);
        Task<List<Dialog>> GetUserDialogsAsync(int userId, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include);
        Task<List<Dialog>> GetUserDialogsAsync(int userId, Expression<Func<Dialog, bool>> where, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include);
        int UnreadDialogsForUserCount(int userId);
        int UnreadMessagesInDialogCount(Dialog dialog);

        void SaveDialog();
        Task SaveDialogAsync();
    }

    public class DialogService : IDialogService
    {
        private readonly IDialogRepository dialogsRepository;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUnitOfWork unitOfWork;

        public DialogService(IDialogRepository dialogsRepository, IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
        {
            this.dialogsRepository = dialogsRepository;
            this.unitOfWork = unitOfWork;
            this.userProfileRepository = userProfileRepository;
        }

        #region IDialogService Members

        public IEnumerable<Dialog> GetAllDialogs()
        {
            var dialogs = dialogsRepository.GetAll();
            return dialogs;
        }

        public async Task<List<Dialog>> GetAllDialogsAsync()
        {
            return await dialogsRepository.GetAllAsync();
        }


        public Dialog GetDialog(int id)
        {
            var dialog = dialogsRepository.GetById(id);
            return dialog;
        }

        public Dialog GetDialog(Expression<Func<Dialog, bool>> where, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include)
        {
            var dialog = dialogsRepository.Get(where, include);
            return dialog;
        }

        public async Task<Dialog> GetDialogAsync(int id)
        {
            return await dialogsRepository.GetByIdAsync(id);
        }

        public async Task<Dialog> GetDialogAsync(Expression<Func<Dialog, bool>> where, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include)
        {
            return await dialogsRepository.GetAsync(where, include);
        }

        public void CreateDialog(Dialog dialog)
        {
            dialogsRepository.Add(dialog);
        }

        public Dialog GetPrivateDialog(UserProfile user1, UserProfile user2)
        {
            var user1Dialogs = user1.DialogsAsCreator.Concat(user1.DialogsAsСompanion);

            var user2Dialogs = user2.DialogsAsCreator.Concat(user2.DialogsAsСompanion);

            foreach (var d1 in user1Dialogs)
            {
                foreach (var d2 in user2Dialogs)
                {
                    if (d1.Equals(d2) && (d1.CreatorId == user1.Id || d1.CreatorId == user2.Id) && (d2.CreatorId == user1.Id || d2.CreatorId == user2.Id))
                    {
                        return d1;
                    }
                }
            }
            return null;
        }

        public int GetOtherUserInDialog(int dialogId, int userId)
        {
            int otherUserId = -1;

            var dialog = dialogsRepository.GetById(dialogId);
            if (dialog != null)
            {
                if (dialog.CompanionId == userId)
                {
                    otherUserId = dialog.CreatorId;
                }
                else if (dialog.CreatorId == userId)
                {
                    otherUserId = dialog.CompanionId;
                }
            }

            return otherUserId;
        }

        public int UnreadDialogsForUserCount(int userId)
        {
            int unreadDialogsCount = 0;
            var user = userProfileRepository.GetMany(u => u.Id == userId,
                include: source => source.Include(u => u.DialogsAsCreator).ThenInclude(d => d.Messages).Include(i => i.DialogsAsСompanion).ThenInclude(d => d.Messages)).SingleOrDefault();
            if (user != null)
            {
                var creatorDialogs = user.DialogsAsCreator;
                foreach (var dialog in creatorDialogs)
                {
                    if (dialog.Messages.Any(m => !m.ToViewed && m.ReceiverId == userId))
                    {
                        unreadDialogsCount++;
                    }
                }

                var companionDialogs = user.DialogsAsСompanion;
                foreach (var dialog in companionDialogs)
                {
                    if (dialog.Messages.Any(m => !m.ToViewed && m.ReceiverId == userId))
                    {
                        unreadDialogsCount++;
                    }
                }
            }
            return unreadDialogsCount;
        }

        public IEnumerable<Dialog> GetUserDialogs(int userId, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include)
        {
            var dialogs = dialogsRepository.GetMany(d => d.CompanionId == userId || d.CreatorId == userId, include);
            return dialogs;
        }

        public IEnumerable<Dialog> GetUserDialogs(int userId, Expression<Func<Dialog, bool>> where, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include)
        {
            var dialogs = dialogsRepository.GetMany(where, include).Where(d => d.CompanionId == userId || d.CreatorId == userId);
            return dialogs;
        }

        public async Task<List<Dialog>> GetUserDialogsAsync(int userId)
        {
            var dialogs = await dialogsRepository.GetManyAsync(d => d.CompanionId == userId || d.CreatorId == userId);
            return dialogs;
        }

        public async Task<List<Dialog>> GetUserDialogsAsync(int userId, Expression<Func<Dialog, bool>> where)
        {
            var dialogs = (await dialogsRepository.GetManyAsync(m => m.Id == userId)).Where(d => d.CompanionId == userId || d.CreatorId == userId).ToList();
            return dialogs;
        }

        public async Task<List<Dialog>> GetUserDialogsAsync(int userId, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include)
        {
            var dialogs = await dialogsRepository.GetManyAsync(d => d.CompanionId == userId || d.CreatorId == userId, include);
            return dialogs;
        }

        public async Task<List<Dialog>> GetUserDialogsAsync(int userId, Expression<Func<Dialog, bool>> where, Func<IQueryable<Dialog>, IIncludableQueryable<Dialog, object>> include)
        {
            var dialogs = (await dialogsRepository.GetManyAsync(where, include)).Where(d => d.CompanionId == userId || d.CreatorId == userId).ToList();
            return dialogs;
        }

        public int UnreadMessagesInDialogCount(Dialog dialog)
        {
            int messagesInDialogCount = 0;
            foreach (var m in dialog.Messages)
            {
                if (!m.ToViewed)
                {
                    messagesInDialogCount++;
                }
            }
            return messagesInDialogCount;
        }

        public void SaveDialog()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveDialogAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        #endregion

    }
}
