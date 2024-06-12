using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mssm.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDB10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Subtasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Subtasks",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
