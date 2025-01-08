using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentko.Migrations
{
    /// <inheritdoc />
    public partial class AddArticleCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleCategoryID",
                table: "Article",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArticleCategoryID",
                table: "Article",
                column: "ArticleCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_ArticleCategory_ArticleCategoryID",
                table: "Article",
                column: "ArticleCategoryID",
                principalTable: "ArticleCategory",
                principalColumn: "ArticleCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_ArticleCategory_ArticleCategoryID",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_ArticleCategoryID",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "ArticleCategoryID",
                table: "Article");
        }
    }
}
