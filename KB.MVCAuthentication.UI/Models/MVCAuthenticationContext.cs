using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KB.MVCAuthentication.UI.Models
{
    public partial class MVCAuthenticationContext : DbContext
    {
        public MVCAuthenticationContext()
            : base("name=MVCAuthenticationContext")
        {
        }

        public virtual DbSet<SessionSecurityKey> SessionSecurityKeys { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionSecurityKey>()
                .Property(e => e.TripleDESKey)
                .IsUnicode(false);

            modelBuilder.Entity<SessionSecurityKey>()
                .Property(e => e.TripleDesIV)
                .IsUnicode(false);

            modelBuilder.Entity<UserLog>()
                .Property(e => e.Status)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserLogs)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
