using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtandInterface2.Migrations
{
    public partial class ChangeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Roles_RoleId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_RoleId",
                table: "UserInfos");

            migrationBuilder.CreateTable(
                name: "RoleDtoUserInfoDto",
                columns: table => new
                {
                    RoleDtosId = table.Column<int>(type: "int", nullable: false),
                    UserInfoDtosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDtoUserInfoDto", x => new { x.RoleDtosId, x.UserInfoDtosId });
                    table.ForeignKey(
                        name: "FK_RoleDtoUserInfoDto_Roles_RoleDtosId",
                        column: x => x.RoleDtosId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleDtoUserInfoDto_UserInfos_UserInfoDtosId",
                        column: x => x.UserInfoDtosId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleDtoUserInfoDto_UserInfoDtosId",
                table: "RoleDtoUserInfoDto",
                column: "UserInfoDtosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleDtoUserInfoDto");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_RoleId",
                table: "UserInfos",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Roles_RoleId",
                table: "UserInfos",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
