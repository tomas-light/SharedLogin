using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.DbContexts.PostgreSql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accessible_accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: true),
                    AccessibleAccountId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accessible_accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "accessible_accounts_histories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    AccessibleAccountName = table.Column<string>(nullable: true),
                    LoginDateTime = table.Column<DateTime>(nullable: false),
                    LogoutDateTime = table.Column<DateTime>(nullable: true),
                    AccountId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accessible_accounts_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_accessible_accounts_histories_accessible_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accessible_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_accessible_accounts_histories_accessible_accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "accessible_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accessible_accounts_histories_AccountId",
                table: "accessible_accounts_histories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_accessible_accounts_histories_AccountId1",
                table: "accessible_accounts_histories",
                column: "AccountId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accessible_accounts_histories");

            migrationBuilder.DropTable(
                name: "accessible_accounts");
        }
    }
}
