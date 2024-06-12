using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mssm.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDBv8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeTask_Task_taskId",
                table: "TypeTask");

            migrationBuilder.DropIndex(
                name: "IX_TypeTask_taskId",
                table: "TypeTask");

            migrationBuilder.DropColumn(
                name: "taskId",
                table: "TypeTask");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModelTypeTask");

            migrationBuilder.AddColumn<string>(
                name: "taskId",
                table: "TypeTask",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeTask_taskId",
                table: "TypeTask",
                column: "taskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTask_Task_taskId",
                table: "TypeTask",
                column: "taskId",
                principalTable: "Task",
                principalColumn: "Id");
        }
    }
}
