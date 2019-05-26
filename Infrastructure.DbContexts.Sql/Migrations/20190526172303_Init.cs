using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.DbContexts.Sql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessibleAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    AccessibleAccountId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibleAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessibleAccountsHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    AccessibleAccountName = table.Column<string>(nullable: true),
                    LoginDateTime = table.Column<DateTime>(nullable: false),
                    LogoutDateTime = table.Column<DateTime>(nullable: true),
                    AccountId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibleAccountsHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessibleAccountsHistories_AccessibleAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AccessibleAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessibleAccountsHistories_AccessibleAccounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "AccessibleAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessibleAccountsHistories_AccountId",
                table: "AccessibleAccountsHistories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibleAccountsHistories_AccountId1",
                table: "AccessibleAccountsHistories",
                column: "AccountId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessibleAccountsHistories");

            migrationBuilder.DropTable(
                name: "AccessibleAccounts");
        }
    }
}
