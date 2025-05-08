using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImperialInventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class NewTestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Facilities_FacilityId1",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_FacilityId1",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "FacilityId1",
                table: "InventoryItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacilityId1",
                table: "InventoryItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_FacilityId1",
                table: "InventoryItems",
                column: "FacilityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Facilities_FacilityId1",
                table: "InventoryItems",
                column: "FacilityId1",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
