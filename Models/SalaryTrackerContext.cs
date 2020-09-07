using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Salary_tracker.Models
{
    public partial class SalaryTrackerContext : DbContext
    {
        public SalaryTrackerContext()
        {
        }

        public SalaryTrackerContext(DbContextOptions<SalaryTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Salary> Salary { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-T3DOI1D;Database=SalaryTracker;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Salary__85C600AFF86C6442");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.SalaryStatus)
                    .HasColumnName("salary_status")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Dept)
                    .HasColumnName("dept")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EmpName)
                    .HasColumnName("Emp_name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inv_transaction_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
