using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtandInterface2.Migrations
{
    public partial class ChangeAuthorizationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Authorizations_AuthorizationId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_UserDtos_UserDtoId",
                table: "UserInfos");

            migrationBuilder.DropTable(
                name: "Authorizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDtos",
                table: "UserDtos");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                newName: "UserInfo");

            migrationBuilder.RenameTable(
                name: "UserDtos",
                newName: "Users");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_Roles_AuthorizationId",
                table: "UserInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_Users_UserDtoId",
                table: "UserInfo");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserDtos");

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
                name: "PK_UserDtos",
                table: "UserDtos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Authorizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorizations", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Authorizations_AuthorizationId",
                table: "UserInfos",
                column: "AuthorizationId",
                principalTable: "Authorizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_UserDtos_UserDtoId",
                table: "UserInfos",
                column: "UserDtoId",
                principalTable: "UserDtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
