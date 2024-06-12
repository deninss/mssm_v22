using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mssm.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDBv9o : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModelTypeTask");

            migrationBuilder.DropTable(
                name: "TypeTask");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskModelTypeTask",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    taskId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModelTypeTask", x => new { x.TypeId, x.taskId });
                    table.ForeignKey(
                        name: "FK_TaskModelTypeTask_Task_taskId",
                        column: x => x.taskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskModelTypeTask_TypeTask_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TypeTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModelTypeTask_taskId",
                table: "TaskModelTypeTask",
                column: "taskId");
        }
    }
}
