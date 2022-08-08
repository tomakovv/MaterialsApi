using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaterialsApi.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Authors_AuthorId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialTypes_TypeId",
                table: "Materials");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Authors_AuthorId",
                table: "Materials",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialTypes_TypeId",
                table: "Materials",
                column: "TypeId",
                principalTable: "MaterialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Authors_AuthorId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialTypes_TypeId",
                table: "Materials");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Materials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Materials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Authors_AuthorId",
                table: "Materials",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialTypes_TypeId",
                table: "Materials",
                column: "TypeId",
                principalTable: "MaterialTypes",
                principalColumn: "Id");
        }
    }
}
