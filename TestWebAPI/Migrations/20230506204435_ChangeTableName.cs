using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebAPI.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tapes_TypeId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tapes",
                table: "Tapes");

            migrationBuilder.RenameTable(
                name: "Tapes",
                newName: "Types");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Types_TypeId",
                table: "Tasks",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Types_TypeId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Tapes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tapes",
                table: "Tapes",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tapes_TypeId",
                table: "Tasks",
                column: "TypeId",
                principalTable: "Tapes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
