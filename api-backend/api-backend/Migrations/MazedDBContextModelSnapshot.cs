﻿// <auto-generated />
using System;
using MazedDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace api_backend.Migrations
{
    [DbContext(typeof(MazedDBContext))]
    partial class MazedDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("MazedDB.Models.Application", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.Property<int>("SponsorId")
                        .HasColumnType("int")
                        .HasColumnName("sponsorId");

                    b.Property<sbyte>("ApprovalStatus")
                        .HasColumnType("tinyint")
                        .HasColumnName("approvalStatus");

                    b.Property<string>("Description")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("description")
                        .UseCollation("utf8_general_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Description"), "utf8");

                    b.Property<sbyte>("IsActive")
                        .HasColumnType("tinyint")
                        .HasColumnName("isActive");

                    b.Property<DateTime>("RequestedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("requestedDate");

                    b.Property<DateTime?>("ResponseDate")
                        .HasColumnType("datetime")
                        .HasColumnName("responseDate");

                    b.HasKey("UserId", "SponsorId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.ToTable("application", (string)null);
                });

            modelBuilder.Entity("MazedDB.Models.Catalogue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("SponsorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "SponsorId" }, "catalogue_SponsorFK_idx");

                    b.ToTable("catalogue", (string)null);
                });

            modelBuilder.Entity("MazedDB.Models.DriverOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("SponsorId")
                        .HasColumnType("int")
                        .HasColumnName("SponsorID");

                    b.Property<int>("TotalPointVal")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "order_driverFK");

                    b.HasIndex(new[] { "SponsorId" }, "order_sponsorFK");

                    b.ToTable("driverOrders", (string)null);
                });

            modelBuilder.Entity("MazedDB.Models.PointTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("modificationDate");

                    b.Property<int>("PointValue")
                        .HasColumnType("int")
                        .HasColumnName("pointValue");

                    b.Property<string>("Reason")
                        .HasColumnType("mediumtext")
                        .HasColumnName("reason");

                    b.Property<int>("SponsorId")
                        .HasColumnType("int")
                        .HasColumnName("sponsorId");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "SponsorId" }, "pointTransaction_sponsorIdFK_idx");

                    b.HasIndex(new[] { "UserId" }, "pointTransaction_userIdFK_idx");

                    b.ToTable("pointTransaction", (string)null);
                });

            modelBuilder.Entity("MazedDB.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Availibility")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("availibility")
                        .HasComment("I dont know whether we want a simple bool (yes this item still has stock left) or the specific amount left a product");

                    b.Property<int>("CatalogueId")
                        .HasColumnType("int")
                        .HasColumnName("catalogueId");

                    b.Property<string>("Description")
                        .HasColumnType("mediumtext")
                        .HasColumnName("description");

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob")
                        .HasColumnName("image");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderID");

                    b.Property<string>("OrderQuantity")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("orderQuantity");

                    b.Property<int>("PointValue")
                        .HasColumnType("int")
                        .HasColumnName("pointValue");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CatalogueId" }, "product_catalogueIdFK_idx");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("MazedDB.Models.SponsorOrg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CatalogueId")
                        .HasColumnType("int")
                        .HasColumnName("CatalogueID");

                    b.Property<string>("OrgDescription")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("OrgName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("sponsorOrgs", (string)null);
                });

            modelBuilder.Entity("MazedDB.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("SponsorId")
                        .HasColumnType("int")
                        .HasColumnName("SponsorID");

                    b.Property<string>("UserAddress")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("UserFname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("UserFName");

                    b.Property<string>("UserLname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("UserLName");

                    b.Property<string>("UserPhoneNum")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("UserPwd")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<bool>("blacklist")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "SponsorId" }, "users_sponsorOrgFK");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MazedDB.Models.Catalogue", b =>
                {
                    b.HasOne("MazedDB.Models.SponsorOrg", "Sponsor")
                        .WithMany("Catalogues")
                        .HasForeignKey("SponsorId")
                        .IsRequired()
                        .HasConstraintName("catalogue_SponsorFK");

                    b.Navigation("Sponsor");
                });

            modelBuilder.Entity("MazedDB.Models.DriverOrder", b =>
                {
                    b.HasOne("MazedDB.Models.SponsorOrg", "Sponsor")
                        .WithMany("DriverOrders")
                        .HasForeignKey("SponsorId")
                        .IsRequired()
                        .HasConstraintName("order_sponsorFK");

                    b.HasOne("MazedDB.Models.User", "User")
                        .WithMany("DriverOrders")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("order_driverFK");

                    b.Navigation("Sponsor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MazedDB.Models.PointTransaction", b =>
                {
                    b.HasOne("MazedDB.Models.SponsorOrg", "Sponsor")
                        .WithMany("PointTransactions")
                        .HasForeignKey("SponsorId")
                        .IsRequired()
                        .HasConstraintName("pointTransaction_sponsorIdFK");

                    b.HasOne("MazedDB.Models.User", "User")
                        .WithMany("PointTransactions")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("pointTransaction_userIdFK");

                    b.Navigation("Sponsor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MazedDB.Models.Product", b =>
                {
                    b.HasOne("MazedDB.Models.Catalogue", "Catalogue")
                        .WithMany("Products")
                        .HasForeignKey("CatalogueId")
                        .IsRequired()
                        .HasConstraintName("product_catalogueIdFK");

                    b.Navigation("Catalogue");
                });

            modelBuilder.Entity("MazedDB.Models.User", b =>
                {
                    b.HasOne("MazedDB.Models.SponsorOrg", "Sponsor")
                        .WithMany("Users")
                        .HasForeignKey("SponsorId")
                        .HasConstraintName("users_sponsorOrgFK");

                    b.Navigation("Sponsor");
                });

            modelBuilder.Entity("MazedDB.Models.Catalogue", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("MazedDB.Models.SponsorOrg", b =>
                {
                    b.Navigation("Catalogues");

                    b.Navigation("DriverOrders");

                    b.Navigation("PointTransactions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("MazedDB.Models.User", b =>
                {
                    b.Navigation("DriverOrders");

                    b.Navigation("PointTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
