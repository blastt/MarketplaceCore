﻿// <auto-generated />
using System;
using Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Marketplace.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190320202751_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Marketplace.Model.Models.AccountInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInformation");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("EmailPassword");

                    b.Property<string>("Login");

                    b.Property<int>("OrderId");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("AccountInfos");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Billing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Billings");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Dialog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanionId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CreatorId");

                    b.HasKey("Id");

                    b.HasIndex("CompanionId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Grade");

                    b.Property<int?>("OrderId");

                    b.Property<int>("UserFromId");

                    b.Property<int>("UserToId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserFromId");

                    b.HasIndex("UserToId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Rank");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("DialogId");

                    b.Property<bool>("FromViewed");

                    b.Property<string>("MessageBody")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("ReceiverDeleted");

                    b.Property<int>("ReceiverId");

                    b.Property<bool>("SenderDeleted");

                    b.Property<int>("SenderId");

                    b.Property<bool>("ToViewed");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountLogin")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedAccountDate");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("DateDeleted");

                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<int>("GameId");

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsBanned");

                    b.Property<string>("JobId");

                    b.Property<decimal?>("MiddlemanPrice")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<bool>("PersonalAccount");

                    b.Property<decimal>("Price");

                    b.Property<bool>("SellerPaysMiddleman");

                    b.Property<int>("State");

                    b.Property<string>("Url");

                    b.Property<int>("UserProfileId");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("AmmountSellerGet");

                    b.Property<decimal?>("Amount");

                    b.Property<bool>("BuyerChecked");

                    b.Property<bool>("BuyerFeedbacked");

                    b.Property<int?>("BuyerId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CurrentStatusId");

                    b.Property<string>("JobId");

                    b.Property<int?>("MiddlemanId");

                    b.Property<int?>("OfferId");

                    b.Property<bool>("SellerChecked");

                    b.Property<bool>("SellerFeedbacked");

                    b.Property<int>("SellerId");

                    b.Property<decimal>("Sum");

                    b.Property<decimal?>("WithdrawAmount");

                    b.Property<decimal?>("WithdrawAmountSellerGet");

                    b.Property<decimal?>("WithmiddlemanSum")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("CurrentStatusId");

                    b.HasIndex("MiddlemanId");

                    b.HasIndex("OfferId")
                        .IsUnique()
                        .HasFilter("[OfferId] IS NOT NULL");

                    b.HasIndex("SellerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Marketplace.Model.Models.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DuringName");

                    b.Property<string>("FinishedName");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Screenshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("OfferId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Screenshots");
                });

            modelBuilder.Entity("Marketplace.Model.Models.StatusLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("NewStatusId");

                    b.Property<int>("OldStatusId");

                    b.Property<int>("OrderId");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("NewStatusId");

                    b.HasIndex("OldStatusId");

                    b.HasIndex("OrderId");

                    b.ToTable("StatusLogs");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("OrderId");

                    b.Property<int>("ReceiverId");

                    b.Property<int>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Marketplace.Model.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("LockoutReason");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserProfileId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Marketplace.Model.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AllFeedbackCount");

                    b.Property<string>("Avatar32");

                    b.Property<string>("Avatar64");

                    b.Property<string>("Avatar96");

                    b.Property<decimal>("Balance");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Discription");

                    b.Property<bool>("IsOnline");

                    b.Property<int>("NegativeFeedbackCount");

                    b.Property<double?>("NegativeFeedbackProcent");

                    b.Property<int>("PositiveFeedbackCount");

                    b.Property<double?>("PositiveFeedbackProcent");

                    b.Property<int?>("Rating");

                    b.Property<int?>("SuccessOrderRate");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Marketplace.Model.Models.Withdraw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Details");

                    b.Property<bool>("IsCanceled");

                    b.Property<bool>("IsPaid");

                    b.Property<string>("PaywayName");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Withdraws");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Marketplace.Model.Models.AccountInfo", b =>
                {
                    b.HasOne("Marketplace.Model.Models.Order", "Order")
                        .WithMany("AccountInfos")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Marketplace.Model.Models.Billing", b =>
                {
                    b.HasOne("Marketplace.Model.Models.UserProfile", "User")
                        .WithMany("Billings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Marketplace.Model.Models.Dialog", b =>
                {
                    b.HasOne("Marketplace.Model.Models.UserProfile", "Companion")
                        .WithMany("DialogsAsСompanion")
                        .HasForeignKey("CompanionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "Creator")
                        .WithMany("DialogsAsCreator")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Marketplace.Model.Models.Feedback", b =>
                {
                    b.HasOne("Marketplace.Model.Models.Order", "Order")
                        .WithMany("Feedbacks")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "UserFrom")
                        .WithMany("FeedbacksToOthers")
                        .HasForeignKey("UserFromId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "UserTo")
                        .WithMany("FeedbacksMy")
                        .HasForeignKey("UserToId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Marketplace.Model.Models.Message", b =>
                {
                    b.HasOne("Marketplace.Model.Models.Dialog", "Dialog")
                        .WithMany("Messages")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "Receiver")
                        .WithMany("MessagesAsReceiver")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "Sender")
                        .WithMany("MessagesAsSender")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Marketplace.Model.Models.Offer", b =>
                {
                    b.HasOne("Marketplace.Model.Models.Game", "Game")
                        .WithMany("Offers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "UserProfile")
                        .WithMany("Offers")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Marketplace.Model.Models.Order", b =>
                {
                    b.HasOne("Marketplace.Model.Models.UserProfile", "Buyer")
                        .WithMany("OrdersAsBuyer")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Marketplace.Model.Models.OrderStatus", "CurrentStatus")
                        .WithMany("Orders")
                        .HasForeignKey("CurrentStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "Middleman")
                        .WithMany("OrdersAsMiddleman")
                        .HasForeignKey("MiddlemanId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Marketplace.Model.Models.Offer", "Offer")
                        .WithOne("Order")
                        .HasForeignKey("Marketplace.Model.Models.Order", "OfferId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "Seller")
                        .WithMany("OrdersAsSeller")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Marketplace.Model.Models.Screenshot", b =>
                {
                    b.HasOne("Marketplace.Model.Models.Offer", "Offer")
                        .WithMany("Screenshots")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Marketplace.Model.Models.StatusLog", b =>
                {
                    b.HasOne("Marketplace.Model.Models.OrderStatus", "NewStatus")
                        .WithMany("NewStatusLogs")
                        .HasForeignKey("NewStatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Marketplace.Model.Models.OrderStatus", "OldStatus")
                        .WithMany("OldStatusLogs")
                        .HasForeignKey("OldStatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Marketplace.Model.Models.Order", "Order")
                        .WithMany("StatusLogs")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Marketplace.Model.Models.Transaction", b =>
                {
                    b.HasOne("Marketplace.Model.Models.Order", "Order")
                        .WithMany("Transactions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "Receiver")
                        .WithMany("TransactionsAsReceiver")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Marketplace.Model.Models.UserProfile", "Sender")
                        .WithMany("TransactionsAsSender")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Marketplace.Model.Models.User", b =>
                {
                    b.HasOne("Marketplace.Model.Models.UserProfile", "UserProfile")
                        .WithOne("User")
                        .HasForeignKey("Marketplace.Model.Models.User", "UserProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Marketplace.Model.Models.Withdraw", b =>
                {
                    b.HasOne("Marketplace.Model.Models.UserProfile", "User")
                        .WithMany("Withdraws")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Marketplace.Model.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Marketplace.Model.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Marketplace.Model.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Marketplace.Model.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Marketplace.Model.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Marketplace.Model.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
