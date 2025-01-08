using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Studentko.Migrations
{
    /// <inheritdoc />
    public partial class AddArticleCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCategory",
                columns: table => new
                {
                    ArticleCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategory", x => x.ArticleCategoryID);
                });

            migrationBuilder.InsertData(
                table: "ArticleCategory",
                columns: new[] { "ArticleCategoryID", "category" },
                values: new object[,]
                {
                    { 1, "Obvestila" },
                    { 2, "Predmetnik" },
                    { 3, "Pomoč učenju" },
                    { 4, "Šport" },
                    { 5, "Zaposlitvene možnosti" },
                    { 6, "Seminarji" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCategory");
        }
    }
}
