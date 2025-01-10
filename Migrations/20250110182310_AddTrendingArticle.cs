using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentko.Migrations
{
    /// <inheritdoc />
    public partial class AddTrendingArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isTrending",
                table: "Post");

            migrationBuilder.AddColumn<bool>(
                name: "isTrending",
                table: "Article",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isTrending",
                table: "Article");

            migrationBuilder.AddColumn<bool>(
                name: "isTrending",
                table: "Post",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
