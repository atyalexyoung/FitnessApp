using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExerciseAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyPart",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseTags",
                table: "Exercises");

            migrationBuilder.CreateTable(
                name: "ExerciseBodyPart",
                columns: table => new
                {
                    ExerciseId = table.Column<string>(type: "text", nullable: false),
                    BodyPart = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseBodyPart", x => new { x.ExerciseId, x.BodyPart });
                    table.ForeignKey(
                        name: "FK_ExerciseBodyPart_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTag",
                columns: table => new
                {
                    ExerciseId = table.Column<string>(type: "text", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTag", x => new { x.ExerciseId, x.Tag });
                    table.ForeignKey(
                        name: "FK_ExerciseTag_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseBodyPart");

            migrationBuilder.DropTable(
                name: "ExerciseTag");

            migrationBuilder.AddColumn<int[]>(
                name: "BodyPart",
                table: "Exercises",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<int[]>(
                name: "ExerciseTags",
                table: "Exercises",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }
    }
}
