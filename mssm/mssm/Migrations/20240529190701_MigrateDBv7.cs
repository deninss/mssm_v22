using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mssm.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDBv7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TypeTask_TypeId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_TypeId",
                table: "Task");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Task_TypeId",
                table: "Task",
                column: "TypeId",
                unique: true,
                filter: "[TypeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TypeTask_TypeId",
                table: "Task",
                column: "TypeId",
                principalTable: "TypeTask",
                principalColumn: "Id");
        }
    }
}
