using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentko.Migrations
{
    /// <inheritdoc />
    public partial class AddFileAttachmentToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fileAttachment",
                table: "Post",
                newName: "FileAttachment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "Post",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileAttachment",
                table: "Post",
                newName: "fileAttachment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
