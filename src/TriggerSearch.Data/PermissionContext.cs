using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models;
using System.Threading.Tasks;
using TriggerSearch.Core;
using TriggerSearch.Core.Hooks;

namespace TriggerSearch.Data
{
    public class PermissionContext : HookDbContext
    {
        private string _sqlGetNow = "now()";
        public PermissionContext(DbContextOptions<PermissionContext> options, IHookFunction hookFunction) :base(options, hookFunction)
        { 
        }
        public DbSet<Group> Groups { set; get; }
        public DbSet<GroupRole> GroupRoles { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<ObjectShareUser> ObjectShareUsers { get; set; }
        public DbSet<Permit> Permits { get; set; }
        public DbSet<RolePermit> RolePermits { get; set; }     
        public DbSet<Role> Roles { get; set; }
        public DbSet<TypeObject> TypeObjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Group>()
                .Property(item => item.Created)
                .HasDefaultValueSql(_sqlGetNow);

            modelBuilder.Entity<GroupRole>()
               .Property(item => item.Created)
               .HasDefaultValueSql(_sqlGetNow);

            modelBuilder.Entity<GroupUser>()
               .Property(item => item.Created)
               .HasDefaultValueSql(_sqlGetNow);

            modelBuilder.Entity<ObjectShareUser>()
               .Property(item => item.Created)
               .HasDefaultValueSql(_sqlGetNow);

            modelBuilder.Entity<Permit>()
               .Property(item => item.Created)
               .HasDefaultValueSql(_sqlGetNow);

            modelBuilder.Entity<RolePermit>()
               .Property(item => item.Created)
               .HasDefaultValueSql(_sqlGetNow);

            modelBuilder.Entity<Role>()
               .Property(item => item.Created)
               .HasDefaultValueSql(_sqlGetNow);

            modelBuilder.Entity<TypeObject>()
               .Property(item => item.Created)
               .HasDefaultValueSql(_sqlGetNow);

            modelBuilder.Entity<User>()
               .Property(item => item.Created)
               .HasDefaultValueSql(_sqlGetNow);

            modelBuilder.Entity<UserRole>()
               .Property(item => item.Created)
               .HasDefaultValueSql(_sqlGetNow);
        }
    }
}
