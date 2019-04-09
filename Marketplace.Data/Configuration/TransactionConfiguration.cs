using Marketplace.Data.Infrastructure;
using Marketplace.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Marketplace.Data.Configuration
{
    internal class TransactionConfiguration
    {

        public TransactionConfiguration(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(a => a.Id);
            builder.HasOne(t => t.Order).WithMany(o => o.Transactions).HasForeignKey(t => t.OrderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Receiver).WithMany(o => o.TransactionsAsReceiver).HasForeignKey(t => t.ReceiverId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Sender).WithMany(o => o.TransactionsAsSender).HasForeignKey(t => t.SenderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
