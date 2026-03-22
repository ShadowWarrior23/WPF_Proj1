using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_Proj1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "daily_menu",
                columns: table => new
                {
                    Day = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Soup = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    dish_a = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    dish_b = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_daily_menu", x => x.Day);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    full_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    Balance = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    wants_soup = table.Column<bool>(type: "INTEGER", nullable: false),
                    dish_choice = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_daily_menu_Day",
                        column: x => x.Day,
                        principalTable: "daily_menu",
                        principalColumn: "Day",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_Day",
                table: "orders",
                column: "Day");

            migrationBuilder.CreateIndex(
                name: "IX_orders_user_id_Day",
                table: "orders",
                columns: new[] { "user_id", "Day" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "daily_menu");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
