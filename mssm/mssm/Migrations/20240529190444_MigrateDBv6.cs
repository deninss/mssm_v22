using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mssm.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDBv6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Type_TypeId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "TypeTask");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTask",
                table: "TypeTask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TypeTask_TypeId",
                table: "Task",
                column: "TypeId",
                principalTable: "TypeTask",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TypeTask_TypeId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTask",
                table: "TypeTask");

            migrationBuilder.RenameTable(
                name: "TypeTask",
                newName: "Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Type_TypeId",
                table: "Task",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id");
        }
    }
}
