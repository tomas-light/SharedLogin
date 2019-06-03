using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.DbContexts.Sql.Migrations
{
    public partial class AddIsAllowProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessibleAccountsHistories_AccessibleAccounts_AccountId",
                table: "AccessibleAccountsHistories");

            migrationBuilder.AddColumn<bool>(
                name: "IsAllow",
                table: "AccessibleAccounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibleAccountsHistories_AccessibleAccounts_AccountId",
                table: "AccessibleAccountsHistories",
                column: "AccountId",
                principalTable: "AccessibleAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessibleAccountsHistories_AccessibleAccounts_AccountId",
                table: "AccessibleAccountsHistories");

            migrationBuilder.DropColumn(
                name: "IsAllow",
                table: "AccessibleAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessibleAccountsHistories_AccessibleAccounts_AccountId",
                table: "AccessibleAccountsHistories",
                column: "AccountId",
                principalTable: "AccessibleAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
