using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Net.Http;

namespace Database.Repositories
{
    /// <summary>
    /// Class repository works with databases
    /// </summary>
    internal class DatabaseRepository : DbContext
    {
        #region Properties
        /// <summary>
        /// Warriors
        /// </summary>
        public DbSet<CurrencyEntity> Currency
        { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Make configuring to database
        /// </summary>
        /// <param name="optionsBuilder">Options</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlite(@"Data Source=database.db");
        }
        #endregion
    }
}