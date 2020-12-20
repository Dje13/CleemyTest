using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CleemyDAL.Models
{
    public partial class CleemyContext : DbContext
    {
        public CleemyContext()
        {
        }

        public CleemyContext(DbContextOptions<CleemyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<LuccaUser> LuccaUsers { get; set; }
        public virtual DbSet<Nature> Natures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Cleemy;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("Expense");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.CommentExpense)
                    .IsUnicode(false)
                    .HasColumnName("commentExpense");

                entity.Property(e => e.CurrencyId).HasColumnName("currencyId");

                entity.Property(e => e.DateExpense)
                    .HasColumnType("datetime")
                    .HasColumnName("dateExpense");

                entity.Property(e => e.LuccaUserId).HasColumnName("luccaUserId");

                entity.Property(e => e.NatureId).HasColumnName("natureId");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Expense_curencyId");

                entity.HasOne(d => d.LuccaUser)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.LuccaUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Expense_luccaUserId");

                entity.HasOne(d => d.Nature)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.NatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Expense_natureId");
            });

            modelBuilder.Entity<LuccaUser>(entity =>
            {
                entity.ToTable("LuccaUser");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrencyId).HasColumnName("currencyId");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.LuccaUsers)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_LuccaUser_curencyId");
            });

            modelBuilder.Entity<Nature>(entity =>
            {
                entity.ToTable("Nature");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
