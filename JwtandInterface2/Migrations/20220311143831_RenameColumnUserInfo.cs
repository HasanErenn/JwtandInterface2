using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtandInterface2.Migrations
{
    public partial class RenameColumnUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Roles_AuthorizationId",
                table: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "AuthorizationId",
                table: "UserInfos",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfos_AuthorizationId",
                table: "UserInfos",
                newName: "IX_UserInfos_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Roles_RoleId",
                table: "UserInfos",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Roles_RoleId",
                table: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserInfos",
                newName: "AuthorizationId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfos_RoleId",
                table: "UserInfos",
                newName: "IX_UserInfos_AuthorizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Roles_AuthorizationId",
                table: "UserInfos",
                column: "AuthorizationId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
