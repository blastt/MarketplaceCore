using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data.Configuration
{
    internal class UserProfileConfiguration
    {

        public UserProfileConfiguration(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");
            builder.HasKey(a => a.Id);
            builder.HasOne(o => o.User).WithOne(o => o.UserProfile).HasForeignKey<User>(u => u.UserProfileId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.FeedbacksMy).WithOne(m => m.UserTo).HasForeignKey(m => m.UserToId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.FeedbacksToOthers).WithOne(m => m.UserFrom).HasForeignKey(m => m.UserFromId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.MessagesAsReceiver).WithOne(m => m.Receiver).HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.MessagesAsSender).WithOne(m => m.Sender).HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.OrdersAsSeller).WithOne(u => u.Seller).HasForeignKey(m => m.SellerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.OrdersAsBuyer).WithOne(u => u.Buyer).HasForeignKey(m => m.BuyerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.OrdersAsMiddleman).WithOne(u => u.Middleman).HasForeignKey(m => m.MiddlemanId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.DialogsAsCreator).WithOne(m => m.Creator).HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.DialogsAsСompanion).WithOne(m => m.Companion).HasForeignKey(m => m.CompanionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
