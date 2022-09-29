using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "application",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    sponsorId = table.Column<int>(type: "int", nullable: false),
                    approvalStatus = table.Column<sbyte>(type: "tinyint", nullable: false),
                    description = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    requestedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    responseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    isActive = table.Column<sbyte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.userId, x.sponsorId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "sponsorOrgs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrgName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrgDescription = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CatalogueID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sponsorOrgs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "catalogue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SponsorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogue", x => x.Id);
                    table.ForeignKey(
                        name: "catalogue_SponsorFK",
                        column: x => x.SponsorId,
                        principalTable: "sponsorOrgs",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SponsorID = table.Column<int>(type: "int", nullable: true),
                    Username = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserFName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserLName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserAddress = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserEmail = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPhoneNum = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPronouns = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPwd = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "users_sponsorOrgFK",
                        column: x => x.SponsorID,
                        principalTable: "sponsorOrgs",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    catalogueId = table.Column<int>(type: "int", nullable: false),
                    orderID = table.Column<int>(type: "int", nullable: true),
                    pointValue = table.Column<int>(type: "int", nullable: false),
                    orderQuantity = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    availibility = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, comment: "I dont know whether we want a simple bool (yes this item still has stock left) or the specific amount left a product", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "mediumtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "product_catalogueIdFK",
                        column: x => x.catalogueId,
                        principalTable: "catalogue",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "driverOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    SponsorID = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalPointVal = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driverOrders", x => x.Id);
                    table.ForeignKey(
                        name: "order_driverFK",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "order_sponsorFK",
                        column: x => x.SponsorID,
                        principalTable: "sponsorOrgs",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "pointTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sponsorId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    pointValue = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "mediumtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modificationDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pointTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "pointTransaction_sponsorIdFK",
                        column: x => x.sponsorId,
                        principalTable: "sponsorOrgs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "pointTransaction_userIdFK",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "catalogue_SponsorFK_idx",
                table: "catalogue",
                column: "SponsorId");

            migrationBuilder.CreateIndex(
                name: "order_driverFK",
                table: "driverOrders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "order_sponsorFK",
                table: "driverOrders",
                column: "SponsorID");

            migrationBuilder.CreateIndex(
                name: "pointTransaction_sponsorIdFK_idx",
                table: "pointTransaction",
                column: "sponsorId");

            migrationBuilder.CreateIndex(
                name: "pointTransaction_userIdFK_idx",
                table: "pointTransaction",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "product_catalogueIdFK_idx",
                table: "product",
                column: "catalogueId");

            migrationBuilder.CreateIndex(
                name: "users_sponsorOrgFK",
                table: "users",
                column: "SponsorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application");

            migrationBuilder.DropTable(
                name: "driverOrders");

            migrationBuilder.DropTable(
                name: "pointTransaction");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "catalogue");

            migrationBuilder.DropTable(
                name: "sponsorOrgs");
        }
    }
}
