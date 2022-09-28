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
        public virtual DbSet<Catalogue> Catalogues { get; set; } = null!;
        public virtual DbSet<DriverOrder> DriverOrders { get; set; } = null!;
        public virtual DbSet<PointTransaction> PointTransactions { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<SponsorOrg> SponsorOrgs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;


        //configures our models with our db so knows what model represents what
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SponsorId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("application");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");

                entity.Property(e => e.ApprovalStatus).HasColumnName("approvalStatus");

                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasColumnName("description")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RequestedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("requestedDate");

                entity.Property(e => e.ResponseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("responseDate");
            });

            modelBuilder.Entity<Catalogue>(entity =>
            {
                entity.ToTable("catalogue");

                entity.HasIndex(e => e.SponsorId, "catalogue_SponsorFK_idx");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.Catalogues)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("catalogue_SponsorFK");
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

            modelBuilder.Entity<PointTransaction>(entity =>
            {
                entity.ToTable("pointTransaction");

                entity.HasIndex(e => e.SponsorId, "pointTransaction_sponsorIdFK_idx");

                entity.HasIndex(e => e.UserId, "pointTransaction_userIdFK_idx");

                entity.Property(e => e.ModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modificationDate");

                entity.Property(e => e.PointValue).HasColumnName("pointValue");

                entity.Property(e => e.Reason)
                    .HasColumnType("mediumtext")
                    .HasColumnName("reason");

                entity.Property(e => e.SponsorId).HasColumnName("sponsorId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.PointTransactions)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pointTransaction_sponsorIdFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PointTransactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pointTransaction_userIdFK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.CatalogueId, "product_catalogueIdFK_idx");

                entity.Property(e => e.Availibility)
                    .HasMaxLength(45)
                    .HasColumnName("availibility")
                    .HasComment("I dont know whether we want a simple bool (yes this item still has stock left) or the specific amount left a product");

                entity.Property(e => e.CatalogueId).HasColumnName("catalogueId");

                entity.Property(e => e.Description)
                    .HasColumnType("mediumtext")
                    .HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.OrderQuantity)
                    .HasMaxLength(45)
                    .HasColumnName("orderQuantity");

                entity.Property(e => e.PointValue).HasColumnName("pointValue");

                entity.HasOne(d => d.Catalogue)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatalogueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_catalogueIdFK");
            });

            modelBuilder.Entity<SponsorOrg>(entity =>
            {
                entity.ToTable("sponsorOrgs");

                entity.Property(e => e.CatalogueId).HasColumnName("CatalogueID");

                entity.Property(e => e.OrgDescription).HasMaxLength(30);

                entity.Property(e => e.OrgName).HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.SponsorId, "users_sponsorOrgFK");

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

                //entity.Property(e => e.UserPronouns).HasMaxLength(30);

                entity.Property(e => e.UserPwd).HasMaxLength(30);

                entity.Property(e => e.UserType).HasMaxLength(30);

                entity.Property(e => e.Username).HasMaxLength(30);

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SponsorId)
                    .HasConstraintName("users_sponsorOrgFK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
