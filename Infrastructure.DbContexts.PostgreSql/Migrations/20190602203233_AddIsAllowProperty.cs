using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.DbContexts.PostgreSql.Migrations
{
    public partial class AddIsAllowProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accessible_accounts_histories_accessible_accounts_AccountId",
                table: "accessible_accounts_histories");

            migrationBuilder.AddColumn<bool>(
                name: "IsAllow",
                table: "accessible_accounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_accessible_accounts_histories_accessible_accounts_AccountId",
                table: "accessible_accounts_histories",
                column: "AccountId",
                principalTable: "accessible_accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accessible_accounts_histories_accessible_accounts_AccountId",
                table: "accessible_accounts_histories");

            migrationBuilder.DropColumn(
                name: "IsAllow",
                table: "accessible_accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_accessible_accounts_histories_accessible_accounts_AccountId",
                table: "accessible_accounts_histories",
                column: "AccountId",
                principalTable: "accessible_accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
