using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaPestControlSystem.Migrations
{
    /// <inheritdoc />
    public partial class deletedFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Customers_CustomerId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CustomerId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Appointments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Schedule",
                table: "Appointments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Schedule",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId1",
                table: "Appointments",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Customers_CustomerId1",
                table: "Appointments",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
