using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeOfQuestionToValueFromEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("6a487fa0-b1dc-494a-8a81-0bf953d606ae"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("7251b1cb-f957-497b-866b-a52e995b3e95"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("c82fa25b-e77b-4d88-ab22-19bdae97257b"));

            migrationBuilder.RenameColumn(
                name: "QuestionType",
                table: "Questions",
                newName: "Type");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Questions",
                newName: "QuestionType");

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "TopicId", "Name" },
                values: new object[,]
                {
                    { new Guid("6a487fa0-b1dc-494a-8a81-0bf953d606ae"), "Other" },
                    { new Guid("7251b1cb-f957-497b-866b-a52e995b3e95"), "Education" },
                    { new Guid("c82fa25b-e77b-4d88-ab22-19bdae97257b"), "Quiz" }
                });
        }
    }
}
