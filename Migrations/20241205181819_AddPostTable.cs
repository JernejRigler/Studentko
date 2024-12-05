using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentko.Migrations
{
    /// <inheritdoc />
    public partial class AddPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numberSpaces = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isTrending = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "EventCategory",
                columns: table => new
                {
                    EventCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategory", x => x.EventCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fileAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isTrending = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "EventCategory");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
