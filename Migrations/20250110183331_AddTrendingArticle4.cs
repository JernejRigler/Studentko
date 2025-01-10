using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentko.Migrations
{
    /// <inheritdoc />
    public partial class AddTrendingArticle4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isTrending",
                table: "Article",
                newName: "IsTrending");

            migrationBuilder.AlterColumn<bool>(
                name: "IsTrending",
                table: "Article",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTrending",
                table: "Article",
                newName: "isTrending");

            migrationBuilder.AlterColumn<bool>(
                name: "isTrending",
                table: "Article",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
