using Marketplace.Data.Configuration;
using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Context
{
    //public class ApplicationDbContextOptions
    //{
    //    public readonly DbContextOptions<ApplicationContext> Options;
    //    public readonly IDbContextSeed DbContextSeed;
    //    public readonly IEnumerable<IEntityTypeConfiguration> Configurations;

    //    public ApplicationDbContextOptions(DbContextOptions<ApplicationContext> options, IDbContextSeed dbContextSeed, IEnumerable<IEntityTypeConfiguration> configurations)
    //    {
    //        DbContextSeed = dbContextSeed;
    //        Options = options;
    //        Configurations = configurations;
    //    }
    //}

    public class ApplicationContext : IdentityDbContext<User, Role, int>
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        #region Identity
        //public DbSet<User> Users { get; set; }
        //public virtual DbSet<UserLogin> UserLogins { get; set; }
        //public virtual DbSet<UserClaim> UserClaims { get; set; }
        //public virtual DbSet<UserRole> UserRoles { get; set; }
        //public DbSet<Role> Roles { get; set; }
        #endregion

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<AccountInfo> AccountInfos { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<StatusLog> StatusLogs { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserConfiguration(modelBuilder.Entity<User>());
            new RoleConfiguration(modelBuilder.Entity<Role>());
            new UserProfileConfiguration(modelBuilder.Entity<UserProfile>());

            new AccountInfoConfiguration(modelBuilder.Entity<AccountInfo>());
            new BillingConfiguration(modelBuilder.Entity<Billing>());
            new DialogConfiguration(modelBuilder.Entity<Dialog>());
            new FeedbackConfiguration(modelBuilder.Entity<Feedback>());
            new GameConfiguration(modelBuilder.Entity<Game>());
            new MessageConfiguration(modelBuilder.Entity<Message>());
            new OfferConfiguration(modelBuilder.Entity<Offer>());
            new OrderConfiguration(modelBuilder.Entity<Order>());
            new ScreenshotConfiguration(modelBuilder.Entity<Screenshot>());
            new StatusLogConfiguration(modelBuilder.Entity<StatusLog>());
            new TransactionConfiguration(modelBuilder.Entity<Transaction>());
            new WithdrawConfiguration(modelBuilder.Entity<Withdraw>());

            new FilterBooleanConfiguration(modelBuilder.Entity<FilterBoolean>());
            new FilterBooleanValueConfiguration(modelBuilder.Entity<FilterBooleanValue>());
            new FilterRangeConfiguration(modelBuilder.Entity<FilterRange>());
            new FilterRangeValueConfiguration(modelBuilder.Entity<FilterRangeValue>());
            new FilterTextConfiguration(modelBuilder.Entity<FilterText>());
            new FilterTextValueConfiguration(modelBuilder.Entity<FilterTextValue>());
            
        }
    }

    
}
