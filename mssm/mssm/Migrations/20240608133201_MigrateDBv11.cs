using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mssm.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDBv11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Task");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expansion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_FileId",
                table: "Task",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_File_FileId",
                table: "Task",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_File_FileId",
                table: "Task");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropIndex(
                name: "IX_Task_FileId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Task");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Task",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
