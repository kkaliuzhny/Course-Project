using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseSizeOfTemplateDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("079a5af2-d856-4cf7-aa70-077804855c4c"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("6935e80d-8ee6-41a1-a1c0-6720342a613b"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("763615bb-4867-4b9a-b182-7a56168eaf5f"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Templates",
                type: "nvarchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "TopicId", "Name" },
                values: new object[,]
                {
                    { new Guid("1b801e97-1ebb-49e1-8468-2592399b7393"), "Education" },
                    { new Guid("2c2e0d1e-5997-4d56-ba23-9384933fd35d"), "Other" },
                    { new Guid("a2f672c7-6e45-498b-8016-49948b51d8fc"), "Quiz" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("1b801e97-1ebb-49e1-8468-2592399b7393"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("2c2e0d1e-5997-4d56-ba23-9384933fd35d"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("a2f672c7-6e45-498b-8016-49948b51d8fc"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Templates",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "TopicId", "Name" },
                values: new object[,]
                {
                    { new Guid("079a5af2-d856-4cf7-aa70-077804855c4c"), "Other" },
                    { new Guid("6935e80d-8ee6-41a1-a1c0-6720342a613b"), "Education" },
                    { new Guid("763615bb-4867-4b9a-b182-7a56168eaf5f"), "Quiz" }
                });
        }
    }
}
