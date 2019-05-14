namespace SharedLogin.Infrastructure.Contexts.Migrations
{
	using System;
	using Microsoft.EntityFrameworkCore.Metadata;
	using Microsoft.EntityFrameworkCore.Migrations;

	public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SharedAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SharedAccountId = table.Column<int>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    LoginDateTime = table.Column<DateTime>(nullable: false),
                    EndLoginDateTime = table.Column<DateTime>(nullable: true),
                    SharedAccountId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessHistories_SharedAccounts_SharedAccountId",
                        column: x => x.SharedAccountId,
                        principalTable: "SharedAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessHistories_SharedAccounts_SharedAccountId1",
                        column: x => x.SharedAccountId1,
                        principalTable: "SharedAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistories_SharedAccountId",
                table: "AccessHistories",
                column: "SharedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistories_SharedAccountId1",
                table: "AccessHistories",
                column: "SharedAccountId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessHistories");

            migrationBuilder.DropTable(
                name: "SharedAccounts");
        }
    }
}
