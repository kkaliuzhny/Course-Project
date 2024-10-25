using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class Change_Delete_Options_And_Add_Index_For_Titlle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Unique_Title",
                table: "Templates",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unique_Title",
                table: "Templates");

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("70f81f3e-681e-4f2f-894e-b33a0a44cc0a"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("ca7dea0d-2414-4928-8800-cbc5fd78ef8a"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("de9eefbb-00ca-4e6c-b80e-0cb0b7d9ceb8"));

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
    }
}
