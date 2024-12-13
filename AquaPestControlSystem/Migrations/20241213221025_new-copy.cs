using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaPestControlSystem.Migrations
{
    /// <inheritdoc />
    public partial class newcopy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProblemImage",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Appointments");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProblemImage",
                table: "Appointments",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
