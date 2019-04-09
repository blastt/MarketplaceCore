using Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Marketplace.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ApplicationContext Init();
    }

    public abstract class ConfigurationBase
    {
        protected IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        }
    }

    public class DatabaseConfiguration : ConfigurationBase
    {
        private string DataConnectionKey = "DefaultConnection";
        private string AuthConnectionKey = "";

        public string GetDataConnectionString()
        {
            return GetConfiguration().GetConnectionString(DataConnectionKey);
        }

        public string GetAuthConnectionString()
        {
            return GetConfiguration().GetConnectionString(AuthConnectionKey);
        }
    }

    public class DbFactory : IDbFactory
    {
        private static string DataConnectionString => new DatabaseConfiguration().GetDataConnectionString();
        private ApplicationContext _db;
        public DbFactory(ApplicationContext db)
        {
            _db = db;
        }


        public ApplicationContext Init()
        {
            return _db ?? (_db = new ApplicationContext(new DbContextOptions<ApplicationContext>()));
        }

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }       

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }

                _disposed = true;
            }
        }
    }
}
