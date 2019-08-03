using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Marketplace.Data.Context;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model.Models;
using Marketplace.Service.Identity;
using Marketplace.Service.Services;
using Marketplace.Web.Automapper;
using Marketplace.Web.Hangfire;
using Marketplace.Web.Hangfire.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = false;
            });

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

            services.AddTransient<IFilterBooleanRepository, FilterBooleanRepository>();
            services.AddTransient<IFilterBooleanValueRepository, FilterBooleanValueRepository>();
            services.AddTransient<IFilterRangeRepository, FilterRangeRepository>();
            services.AddTransient<IFilterRangeValueRepository, FilterRangeValueRepository>();
            services.AddTransient<IFilterTextRepository, FilterTextRepository>();
            services.AddTransient<IFilterTextValueRepository, FilterTextValueRepository>();
            //services.AddTransient<IFilterRepository, FilterRepository>();


            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<IMessageService, MessageService>();
            //services.AddTransient<IFilterItemService, FilterItemService>();
            //services.AddTransient<IFilterService, FilterService>();
            //services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IDialogService, DialogService>();
            //services.AddTransient<IBillingService, BillingService>();
            //services.AddTransient<ITransactionService, TransactionService>();
            //services.AddTransient<IStatusLogService, StatusLogService>();
            //services.AddTransient<IBillingService, BillingService>();
            //services.AddTransient<IUserService, UserService>();
            services.AddScoped<IConfirmOrderJob, ConfirmOrderJob>();
            services.AddScoped<IDeactivateOfferJob, DeactivateOfferJob>();
            services.AddScoped<ILeaveFeedbackJob, LeaveFeedbackJob>();
            services.AddScoped<IOrderCloseJob, OrderCloseJob>();
            services.AddScoped<ISendEmailAccountDataJob, SendEmailAccountDataJob>();
            services.AddScoped<ISendEmailChangeStatusJob, SendEmailChangeStatusJob>();

            services.AddTransient<IFilterBooleanService, FilterBooleanService>();
            services.AddTransient<IFilterBooleanValueService, FilterBooleanValueService>();
            services.AddTransient<IFilterRangeService, FilterRangeService>();
            services.AddTransient<IFilterRangeValueService, FilterRangeValueService>();
            services.AddTransient<IFilterTextService, FilterTextService>();
            services.AddTransient<IFilterTextValueService, FilterTextValueService>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddViewLocalization(
            LanguageViewLocationExpanderFormat.Suffix,
            opts => { opts.ResourcesPath = "Resources"; });
            services.Configure<RequestLocalizationOptions>(
        opts =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
                new CultureInfo("ua")
            };

            opts.DefaultRequestCulture = new RequestCulture("en");
            // Formatting numbers, dates, etc.
            opts.SupportedCultures = supportedCultures;
            // UI strings that we have localized.
            opts.SupportedUICultures = supportedCultures;
        });

            services.AddHangfire(configuration =>
        {
            var options = new SqlServerStorageOptions
            {
                PrepareSchemaIfNecessary = true,
                QueuePollInterval = TimeSpan.FromMinutes(5)
            };
            configuration.UseSqlServerStorage(ApplicationBuilderExtentions.GetDataConnectionStringFromConfig(), options);
        });

            // Add the processing server as IHostedService



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
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

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
                    areaName: "Admin",
                    template: "Admin/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: "MyUser",
                    areaName: "User",
                    template: "User/{controller=Home}/{action=Index}/{id?}");

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


        public static string GetDataConnectionStringFromConfig()
        {
            return new DatabaseConfiguration().GetDataConnectionString();
        }

        public static string GetAuthConnectionStringFromConfig()
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
