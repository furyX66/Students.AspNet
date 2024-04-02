using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Common.Migrations
{
    /// <inheritdoc />
    public partial class FOSmodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldsOfStudies",
                table: "FieldsOfStudies");

            migrationBuilder.RenameTable(
                name: "FieldsOfStudies",
                newName: "FieldOfStudies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldOfStudies",
                table: "FieldOfStudies",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldOfStudies",
                table: "FieldOfStudies");

            migrationBuilder.RenameTable(
                name: "FieldOfStudies",
                newName: "FieldsOfStudies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldsOfStudies",
                table: "FieldsOfStudies",
                column: "Id");
        }
    }
}
