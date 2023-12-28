using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KRCars1298.Migrations
{
    /// <inheritdoc />
    public partial class Add_VehicleType_to_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VehicleTypeId",
                table: "Models",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Models_VehicleTypeId",
                table: "Models",
                column: "VehicleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_VehicleTypes_VehicleTypeId",
                table: "Models",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_VehicleTypes_VehicleTypeId",
                table: "Models");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropIndex(
                name: "IX_Models_VehicleTypeId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                table: "Models");
        }
    }
}
