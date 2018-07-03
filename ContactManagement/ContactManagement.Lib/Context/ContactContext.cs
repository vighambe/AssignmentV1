using ContactManagement.Lib.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Lib.Context
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactContext(DbContextOptions options) : base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .ToTable("Contact");

            modelBuilder.Entity<Contact>()
                .Property(s => s.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(s => s.LastName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(s => s.Email)
                .HasMaxLength(120)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(s => s.PhoneNumber)
                .HasMaxLength(13);

            modelBuilder.Entity<Contact>()
                .Property(s => s.Status)
                .HasDefaultValue(ContactStatus.Active);

        }
    }
}
