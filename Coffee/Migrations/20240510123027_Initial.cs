using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Coffee.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Photo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Category_pkey", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    IdDish = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Photo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Dishes_pkey", x => x.IdDish);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    IdPost = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Posts_pkey", x => x.IdPost);
                });

            migrationBuilder.CreateTable(
                name: "StatusesOrder",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("StatusesOrder_pkey", x => x.IdStatus);
                });

            migrationBuilder.CreateTable(
                name: "DishCategory",
                columns: table => new
                {
                    IdList = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdCategory = table.Column<int>(type: "integer", nullable: false),
                    IdDish = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DishCategory_pkey", x => x.IdList);
                    table.ForeignKey(
                        name: "DishCategory_IdCategory_fkey",
                        column: x => x.IdCategory,
                        principalTable: "Category",
                        principalColumn: "IdCategory");
                    table.ForeignKey(
                        name: "DishCategory_IdDish_fkey",
                        column: x => x.IdDish,
                        principalTable: "Dishes",
                        principalColumn: "IdDish");
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    IdPromotion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdDish = table.Column<int>(type: "integer", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DiscountPercent = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Promotions_pkey", x => x.IdPromotion);
                    table.ForeignKey(
                        name: "Promotions_IdDish_fkey",
                        column: x => x.IdDish,
                        principalTable: "Dishes",
                        principalColumn: "IdDish");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    FName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    SName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    IdPost = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Users_pkey", x => x.IdUser);
                    table.ForeignKey(
                        name: "Users_IdPost_fkey",
                        column: x => x.IdPost,
                        principalTable: "Posts",
                        principalColumn: "IdPost");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    IdOrder = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullPrice = table.Column<float>(type: "real", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Orders_pkey", x => x.IdOrder);
                    table.ForeignKey(
                        name: "Orders_IdStatus_fkey",
                        column: x => x.IdStatus,
                        principalTable: "StatusesOrder",
                        principalColumn: "IdStatus");
                });

            migrationBuilder.CreateTable(
                name: "Cookings",
                columns: table => new
                {
                    IdCooking = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    IdOrder = table.Column<int>(type: "integer", nullable: false),
                    DateAdmission = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Cookings_pkey", x => x.IdCooking);
                    table.ForeignKey(
                        name: "Cookings_IdOrder_fkey",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "IdOrder");
                    table.ForeignKey(
                        name: "Cookings_IdUser_fkey",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "OrderDish",
                columns: table => new
                {
                    IdList = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdDish = table.Column<int>(type: "integer", nullable: false),
                    IdOrder = table.Column<int>(type: "integer", nullable: false),
                    CountDishes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderDish_pkey", x => x.IdList);
                    table.ForeignKey(
                        name: "OrderDish_IdDish_fkey",
                        column: x => x.IdDish,
                        principalTable: "Dishes",
                        principalColumn: "IdDish");
                    table.ForeignKey(
                        name: "OrderDish_IdOrder_fkey",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "IdOrder");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cookings_IdOrder",
                table: "Cookings",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Cookings_IdUser",
                table: "Cookings",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_DishCategory_IdCategory",
                table: "DishCategory",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_DishCategory_IdDish",
                table: "DishCategory",
                column: "IdDish");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDish_IdDish",
                table: "OrderDish",
                column: "IdDish");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDish_IdOrder",
                table: "OrderDish",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdStatus",
                table: "Orders",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_IdDish",
                table: "Promotions",
                column: "IdDish");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdPost",
                table: "Users",
                column: "IdPost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cookings");

            migrationBuilder.DropTable(
                name: "DishCategory");

            migrationBuilder.DropTable(
                name: "OrderDish");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "StatusesOrder");
        }
    }
}
