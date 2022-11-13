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
        public virtual DbSet<DriverOrder> DriverOrders { get; set; } = null!;
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; } = null!;
        public virtual DbSet<LoginAttempt> LoginAttempts { get; set; } = null!;
        public virtual DbSet<PointTransaction> PointTransactions { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<PwdChange> PwdChanges { get; set; } = null!;
        public virtual DbSet<SponsQueryParam> SponsQueryParams { get; set; } = null!;
        public virtual DbSet<SponsorOrg> SponsorOrgs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserToSponsor> UserToSponsors { get; set; } = null!;
        public virtual DbSet<DriverCart> DriverCarts { get; set; } = null!;

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

                entity.Property(e => e.ApplicantName)
                    .HasMaxLength(45)
                    .HasColumnName("applicantName");

                entity.Property(e => e.ApprovalStatus).HasColumnName("approvalStatus");

                entity.Property(e => e.DecisionReason).HasColumnName("decisionReason");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RequestedDate).HasColumnName("requestedDate");

                entity.Property(e => e.ResponseDate).HasColumnName("responseDate");

                entity.Property(e => e.SponsorName)
                    .HasMaxLength(45)
                    .HasColumnName("sponsorName");
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

            modelBuilder.Entity<DriverCart>(entity =>
            {
                entity.ToTable("driverCart");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");

                entity.Property(e => e.PointValue).HasColumnName("pointValue");

                entity.Property(e => e.CartTotal).HasColumnName("cartTotal");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<LoginAttempt>(entity =>
            {
                entity.ToTable("loginAttempts");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.AttemptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("attemptedDate");

                entity.Property(e => e.IsLoginSuccessful).HasColumnName("isLoginSuccessful");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<PointTransaction>(entity =>
            {
                entity.HasKey(e => e.PointId)
                    .HasName("PRIMARY");

                entity.ToTable("pointTransaction");

                entity.Property(e => e.PointId).HasColumnName("pointId");

                entity.Property(e => e.IsSpecialTransaction).HasColumnName("isSpecialTransaction");

                entity.Property(e => e.ModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modDate");

                entity.Property(e => e.PointValue).HasColumnName("pointValue");

                entity.Property(e => e.Reason)
                    .HasMaxLength(45)
                    .HasColumnName("reason");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasColumnType("blob")
                    .HasColumnName("image");

                entity.Property(e => e.IsBlacklisted).HasColumnName("isBlacklisted");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.OrderQuantity).HasColumnName("orderQuantity");

                entity.Property(e => e.PointValue).HasColumnName("pointValue");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");
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

            modelBuilder.Entity<SponsQueryParam>(entity =>
            {
                entity.Property(e => e.Attributes)
                    .HasMaxLength(45)
                    .HasColumnName("attributes");

                entity.Property(e => e.Entities)
                    .HasMaxLength(45)
                    .HasColumnName("entities");

                entity.Property(e => e.Limit).HasColumnName("limit");

                entity.Property(e => e.MediaType)
                    .HasMaxLength(45)
                    .HasColumnName("mediaType");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorID");
            });

            modelBuilder.Entity<SponsorOrg>(entity =>
            {
                entity.ToTable("sponsorOrgs");

                entity.Property(e => e.CatalogueId).HasColumnName("CatalogueID");

                entity.Property(e => e.DollarToPoint)
                    .HasColumnName("dollarToPoint")
                    .HasDefaultValueSql("'0.01'");

                entity.Property(e => e.IsBlacklisted).HasColumnName("isBlacklisted");

                entity.Property(e => e.OrgName).HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.IsBlacklisted).HasColumnName("isBlacklisted");

                entity.Property(e => e.IssueNotifications)
                    .HasColumnName("issueNotifications")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.ModBy)
                    .HasMaxLength(45)
                    .HasColumnName("modBy");

                entity.Property(e => e.ModDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modDate");

                entity.Property(e => e.OrderNotifications)
                    .HasColumnName("orderNotifications")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.PointNotifications)
                    .HasColumnName("pointNotifications")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.SponsorCount).HasColumnName("sponsorCount");

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

            modelBuilder.Entity<UserToSponsor>(entity =>
            {
                entity.ToTable("userToSponsors");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");

                entity.Property(e => e.SponsorTotal).HasColumnName("sponsorTotal");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.UserPoints).HasColumnName("userPoints");

                entity.Property(e => e.UserType)
                    .HasMaxLength(45)
                    .HasColumnName("userType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
