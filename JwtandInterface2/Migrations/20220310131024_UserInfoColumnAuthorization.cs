using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtandInterface2.Migrations
{
    public partial class UserInfoColumnAuthorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authorizations_UserInfos_UserInfoId",
                table: "Authorizations");

            migrationBuilder.DropIndex(
                name: "IX_Authorizations_UserInfoId",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Authorizations");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_AuthorizationId",
                table: "UserInfos",
                column: "AuthorizationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Authorizations_AuthorizationId",
                table: "UserInfos",
                column: "AuthorizationId",
                principalTable: "Authorizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Authorizations_AuthorizationId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_AuthorizationId",
                table: "UserInfos");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Authorizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_UserInfoId",
                table: "Authorizations",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authorizations_UserInfos_UserInfoId",
                table: "Authorizations",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
