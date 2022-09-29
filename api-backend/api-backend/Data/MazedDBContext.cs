using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MazedDB.Models;

namespace MazedDB.Data
{
    public partial class MazedDBContext : DbContext
    {
        public MazedDBContext()
        {
        }

        public MazedDBContext(DbContextOptions<MazedDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<AuditLogging> AuditLoggings { get; set; } = null!;
        public virtual DbSet<Catalogue> Catalogues { get; set; } = null!;
        public virtual DbSet<DriverOrder> DriverOrders { get; set; } = null!;
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; } = null!;
        public virtual DbSet<PointTransation> PointTransations { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<PwdChange> PwdChanges { get; set; } = null!;
        public virtual DbSet<SponsorOrg> SponsorOrgs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId, e.SponsorId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("application");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");

                entity.Property(e => e.ApprovalStatus).HasColumnName("approvalStatus");

                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RequestedDate).HasColumnName("requestedDate");

                entity.Property(e => e.ResponseDate).HasColumnName("responseDate");
            });

            modelBuilder.Entity<AuditLogging>(entity =>
            {
                entity.ToTable("auditLogging");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.AppReason).HasMaxLength(45);

                entity.Property(e => e.AppStatus).HasMaxLength(45);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(45);

                entity.Property(e => e.LoginSf)
                    .HasMaxLength(45)
                    .HasColumnName("LoginSF");

                entity.Property(e => e.PasswordReason).HasMaxLength(45);

                entity.Property(e => e.PointReason).HasMaxLength(45);

                entity.Property(e => e.SponsorId).HasMaxLength(45);

                entity.Property(e => e.Type).HasMaxLength(45);

                entity.Property(e => e.UserId).HasMaxLength(45);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.AuditLogging)
                    .HasForeignKey<AuditLogging>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SponsorIdFK_AuditLog");

                entity.HasOne(d => d.Id1)
                    .WithOne(p => p.AuditLogging)
                    .HasForeignKey<AuditLogging>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserIdFK_AuditLog");
            });

            modelBuilder.Entity<Catalogue>(entity =>
            {
                entity.ToTable("catalogue");

                entity.HasIndex(e => e.SponsorId, "catalogue_SponsorIdFK_idx");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.Catalogues)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("catalogue_SponsorIdFK");
            });

            modelBuilder.Entity<DriverOrder>(entity =>
            {
                entity.ToTable("driverOrders");

                entity.HasIndex(e => e.UserId, "order_driverFK");

                entity.HasIndex(e => e.SponsorId, "order_sponsorFK");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderStatus).HasMaxLength(30);

                entity.Property(e => e.SponsorId).HasColumnName("SponsorID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.DriverOrders)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_sponsorFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DriverOrders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_driverFK");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<PointTransation>(entity =>
            {
                entity.HasKey(e => e.PointId)
                    .HasName("PRIMARY");

                entity.ToTable("pointTransation");

                entity.HasIndex(e => e.UserId, "pointTransaction_DriverIdFK_idx");

                entity.HasIndex(e => e.SponsorId, "pointTransaction_SponsorIdFK_idx");

                entity.Property(e => e.PointId)
                    .ValueGeneratedNever()
                    .HasColumnName("pointId");

                entity.Property(e => e.ModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modDate");

                entity.Property(e => e.PointValue).HasColumnName("pointValue");

                entity.Property(e => e.Reason)
                    .HasMaxLength(45)
                    .HasColumnName("reason");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.PointTransations)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pointTransaction_SponsorIdFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PointTransations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pointTransaction_DriverIdFK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.CatalogueId, "product_catalogueIdFK_idx");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("productId");

                entity.Property(e => e.Availibility).HasColumnName("availibility");

                entity.Property(e => e.CatalogueId).HasColumnName("catalogueId");

                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasColumnType("blob")
                    .HasColumnName("image");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.OrderQuantity).HasColumnName("orderQuantity");

                entity.Property(e => e.PointValue).HasColumnName("pointValue");

                entity.HasOne(d => d.Catalogue)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatalogueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_catalogueIdFK");
            });

            modelBuilder.Entity<PwdChange>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("pwdChanges");

                entity.HasIndex(e => e.UserId, "UserIdFK_idx");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ChangedPwd).HasMaxLength(45);

                entity.Property(e => e.OgPwd).HasMaxLength(45);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PwdChanges)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserIdFK_PwdChanges");
            });

            modelBuilder.Entity<SponsorOrg>(entity =>
            {
                entity.ToTable("sponsorOrgs");

                entity.Property(e => e.CatalogueId).HasColumnName("CatalogueID");

                entity.Property(e => e.IsBlacklisted).HasColumnName("isBlacklisted");

                entity.Property(e => e.OrgDescription).HasMaxLength(30);

                entity.Property(e => e.OrgName).HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.IsBlacklisted).HasColumnName("isBlacklisted");

                entity.Property(e => e.SponsorId).HasColumnName("SponsorID");

                entity.Property(e => e.UserAddress).HasMaxLength(30);

                entity.Property(e => e.UserEmail).HasMaxLength(30);

                entity.Property(e => e.UserFname)
                    .HasMaxLength(30)
                    .HasColumnName("UserFName");

                entity.Property(e => e.UserLname)
                    .HasMaxLength(30)
                    .HasColumnName("UserLName");

                entity.Property(e => e.UserPhoneNum).HasMaxLength(30);

                entity.Property(e => e.UserPwd).HasMaxLength(30);

                entity.Property(e => e.UserType).HasMaxLength(30);

                entity.Property(e => e.Username).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
