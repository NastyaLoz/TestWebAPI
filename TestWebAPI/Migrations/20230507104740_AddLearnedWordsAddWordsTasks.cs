using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWebAPI.Migrations
{
    public partial class AddLearnedWordsAddWordsTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LearnedWords",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    Percent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnedWords", x => new { x.UserId, x.WordId });
                    table.ForeignKey(
                        name: "FK_LearnedWords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnedWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordsTasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    WordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordsTasks", x => new { x.TaskId, x.WordId });
                    table.ForeignKey(
                        name: "FK_WordsTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordsTasks_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearnedWords_WordId",
                table: "LearnedWords",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_WordsTasks_WordId",
                table: "WordsTasks",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearnedWords");

            migrationBuilder.DropTable(
                name: "WordsTasks");
        }
    }
}
