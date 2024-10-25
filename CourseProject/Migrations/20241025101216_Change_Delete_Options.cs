using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class Change_Delete_Options : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_AspNetUsers_TemplateAuthorId",
                table: "Templates");
            migrationBuilder.AlterColumn<string>(
                name: "TemplateAuthorId",
                table: "Templates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
            migrationBuilder.AddForeignKey(
                name: "FK_Templates_AspNetUsers_TemplateAuthorId",
                table: "Templates",
                column: "TemplateAuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_AspNetUsers_TemplateAuthorId",
                table: "Templates");

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("21442d40-7b5b-4c69-98d0-71833b27dbbd"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("b5499745-ff73-4e61-9eb2-3d8ee74fc64b"));

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "TopicId",
                keyValue: new Guid("e2db96fa-6513-4c94-a49e-63ad799edd2f"));

            migrationBuilder.AlterColumn<string>(
                name: "TemplateAuthorId",
                table: "Templates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "TopicId", "Name" },
                values: new object[,]
                {
                    { new Guid("b3cfd727-10a1-4144-bbe7-f8367d0d146e"), "Quiz" },
                    { new Guid("dbce75b6-7ba8-4b33-9d61-a615905f823d"), "Other" },
                    { new Guid("e416fb48-0522-4724-98df-0972042d422e"), "Education" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_AspNetUsers_TemplateAuthorId",
                table: "Templates",
                column: "TemplateAuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
