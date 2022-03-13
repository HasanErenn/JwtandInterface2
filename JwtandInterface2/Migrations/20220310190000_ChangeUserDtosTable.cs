using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtandInterface2.Migrations
{
    public partial class ChangeUserDtosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_Roles_AuthorizationId",
                table: "UserInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_Users_UserDtoId",
                table: "UserInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo");

            migrationBuilder.RenameTable(
                name: "UserInfo",
                newName: "UserInfos");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfo_UserDtoId",
                table: "UserInfos",
                newName: "IX_UserInfos_UserDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfo_AuthorizationId",
                table: "UserInfos",
                newName: "IX_UserInfos_AuthorizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Roles_AuthorizationId",
                table: "UserInfos",
                column: "AuthorizationId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Users_UserDtoId",
                table: "UserInfos",
                column: "UserDtoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Roles_AuthorizationId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Users_UserDtoId",
                table: "UserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                newName: "UserInfo");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfos_UserDtoId",
                table: "UserInfo",
                newName: "IX_UserInfo_UserDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfos_AuthorizationId",
                table: "UserInfo",
                newName: "IX_UserInfo_AuthorizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_Roles_AuthorizationId",
                table: "UserInfo",
                column: "AuthorizationId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_Users_UserDtoId",
                table: "UserInfo",
                column: "UserDtoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
