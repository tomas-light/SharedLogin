using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.DbContexts.Migrations
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
                    AccessibleAccountId = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true)
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
                    AccessibleAccountName = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    LoginDateTime = table.Column<DateTime>(nullable: false),
                    LogoutDateTime = table.Column<DateTime>(nullable: true),
                    AccountstringId = table.Column<int>(name: "Account<string>Id", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibleAccountsHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessibleAccountsHistories_AccessibleAccounts_Account<string>Id",
                        column: x => x.AccountstringId,
                        principalTable: "AccessibleAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessibleAccountsHistories_AccessibleAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AccessibleAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessibleAccountsHistories_Account<string>Id",
                table: "AccessibleAccountsHistories",
                column: "Account<string>Id");

            migrationBuilder.CreateIndex(
                name: "IX_AccessibleAccountsHistories_AccountId",
                table: "AccessibleAccountsHistories",
                column: "AccountId");
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
