using Microsoft.EntityFrameworkCore;
using q5id.platform.email.dal.Entities;
using Microsoft.Data.SqlClient;

namespace q5id.platform.email.dal
{
	public class EmailContext : DbContext
	{

        public EmailContext(DbContextOptions<EmailContext> options) : base(options) { }

        public DbSet<EmailEntity> Emails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailEntity>(entity =>
            {
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(255);

            });
        }
    }
}

