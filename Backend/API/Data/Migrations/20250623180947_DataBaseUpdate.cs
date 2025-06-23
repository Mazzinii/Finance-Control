using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonTransation.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataBaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transations_Users_UserModelUserId",
                table: "Transations");

            migrationBuilder.DropIndex(
                name: "IX_Transations_UserModelUserId",
                table: "Transations");

            migrationBuilder.DropColumn(
                name: "UserModelUserId",
                table: "Transations");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Transations",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transations_UserId",
                table: "Transations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transations_Users_UserId",
                table: "Transations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transations_Users_UserId",
                table: "Transations");

            migrationBuilder.DropIndex(
                name: "IX_Transations_UserId",
                table: "Transations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transations",
                newName: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "UserModelUserId",
                table: "Transations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transations_UserModelUserId",
                table: "Transations",
                column: "UserModelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transations_Users_UserModelUserId",
                table: "Transations",
                column: "UserModelUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
