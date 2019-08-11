using AutoMapper;
using Marketplace.Api.Automapper;
using Marketplace.Data.Context;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Repositories;
using Marketplace.Model;
using Marketplace.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Marketplace.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
            services.AddTransient<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOfferRepository, OfferRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
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
            services.AddTransient<IDialogService, DialogService>();


            services.AddTransient<IFilterBooleanService, FilterBooleanService>();
            services.AddTransient<IFilterBooleanValueService, FilterBooleanValueService>();
            services.AddTransient<IFilterRangeService, FilterRangeService>();
            services.AddTransient<IFilterRangeValueService, FilterRangeValueService>();
            services.AddTransient<IFilterTextService, FilterTextService>();
            services.AddTransient<IFilterTextValueService, FilterTextValueService>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc();
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

            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
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
            IMapper mapper = AutoMapperConfiguration.Configure();
            services.AddSingleton(mapper);

        }
    }
}
