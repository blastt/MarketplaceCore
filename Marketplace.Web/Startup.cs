using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Marketplace.Data.Context;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model.Models;
using Marketplace.Service.Identity;
using Marketplace.Service.Services;
using Marketplace.Web.Automapper;
using Marketplace.Web.Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }
        public IHostingEnvironment Environment { get; set; }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext();
            services.AddHangfire();
            services.AddTransient<IEmailSender, EmailService>();
            services.AddTransient<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOfferRepository, OfferRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IOrderStatusRepository, OrderStatusRepository>();
            services.AddTransient<IDialogRepository, DialogRepository>();
            services.AddTransient<IBillingRepository, BillingRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IStatusLogRepository, StatusLogRepository>();
            services.AddTransient<IBillingRepository, BillingRepository>();
            services.AddTransient<IAccountInfoRepository, AccountInfoRepository>();


            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            //services.AddTransient<IMessageService, MessageService>();
            //services.AddTransient<IOrderStatusService, OrderStatusService>();
            //services.AddTransient<IDialogService, DialogService>();
            //services.AddTransient<IBillingService, BillingService>();
            //services.AddTransient<ITransactionService, TransactionService>();
            //services.AddTransient<IStatusLogService, StatusLogService>();
            //services.AddTransient<IBillingService, BillingService>();
            //services.AddTransient<IUserService, UserService>();
            services.AddAutoMapper();
           

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddTransient<UserManager<User>>(provider => new UserManager<User>());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            #region Hangfire

            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            

            #endregion


            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    name: "MyAdmin",
                    areaName:"Admin",
                    template: "Admin/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                
            });
        }
    }

    public static class ConfigureContainerExtentions
    {
        public static void AddDbContext(this IServiceCollection serviceCollection, 
            string dataConnectionString = null, string authConnectionString = null)
        {
            serviceCollection.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(GetDataConnectionStringFromConfig()));

            serviceCollection.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddHangfire(this IServiceCollection serviceCollection,
            string dataConnectionString = null, string authConnectionString = null)
        {
            serviceCollection.AddHangfire(opts => opts.UseSqlServerStorage(GetDataConnectionStringFromConfig()));
        }

        private static string GetDataConnectionStringFromConfig()
        {
            return new DatabaseConfiguration().GetDataConnectionString();
        }

        private static string GetAuthConnectionStringFromConfig()
        {
            return new DatabaseConfiguration().GetAuthConnectionString();
        }
    }

    public static class ApplicationBuilderExtentions
    {
        public static void ConfigureHangfire(this IApplicationBuilder app,
            string dataConnectionString = null, string authConnectionString = null)
        {
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire");
        }

        private static string GetDataConnectionStringFromConfig()
        {
            return new DatabaseConfiguration().GetDataConnectionString();
        }

        private static string GetAuthConnectionStringFromConfig()
        {
            return new DatabaseConfiguration().GetAuthConnectionString();
        }
    }

    public static class AutoMapperExtentions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            AutoMapperConfiguration.Configure();
        }
    }


}
