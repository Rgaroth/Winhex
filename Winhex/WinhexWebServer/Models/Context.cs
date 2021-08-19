using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Winhex.WebServer.Models.Objects;

namespace WinhexWebServer.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();

            InitializeSettings();
        }

        private void InitializeSettings()
        {
            if (Settings.FirstOrDefault(x => x.ParameterName == "UsersKey") == null)
            {
                Settings.Add(new Settings() { ParameterName = "UsersKey", ParameterValue = "c4ee9d9c18734b0bd20f0c20f6dfd12d" });
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLog>()
                .HasMany(x => x.Logs)
                .WithOne(x => x.UserLog)
                .HasForeignKey(x => x.LogId);
            // использование Fluent API
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserLog> UserLog { get; set; }
        public DbSet<UserAction> UserAction { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}