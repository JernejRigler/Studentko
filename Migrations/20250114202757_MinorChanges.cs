using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentko.Migrations
{
    /// <inheritdoc />
    public partial class MinorChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileAttachment",
                table: "Event");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileAttachment",
                table: "Event",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
