using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mssm.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDBv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeTask_Task_taskId",
                table: "TypeTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTask",
                table: "TypeTask");

            migrationBuilder.DropIndex(
                name: "IX_TypeTask_taskId",
                table: "TypeTask");

            migrationBuilder.DropColumn(
                name: "taskId",
                table: "TypeTask");

            migrationBuilder.RenameTable(
                name: "TypeTask",
                newName: "Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TypeId",
                table: "Task",
                column: "TypeId",
                unique: true,
                filter: "[TypeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Type_TypeId",
                table: "Task",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Type_TypeId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_TypeId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "TypeTask");

            migrationBuilder.AddColumn<string>(
                name: "taskId",
                table: "TypeTask",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTask",
                table: "TypeTask",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTask_taskId",
                table: "TypeTask",
                column: "taskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTask_Task_taskId",
                table: "TypeTask",
                column: "taskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
