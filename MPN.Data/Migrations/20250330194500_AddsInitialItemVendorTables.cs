using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPN.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsInitialItemVendorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(159)", maxLength: 159, nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 4095, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(15,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Items_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(41)", maxLength: 41, nullable: false),
                    ListID = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorPartNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 4095, nullable: true),
                    VendorCost = table.Column<decimal>(type: "decimal(15,5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPartNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorPartNumbers_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorPartNumbers_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name_ParentId",
                table: "Items",
                columns: new[] { "Name", "ParentId" },
                unique: true,
                filter: "[ParentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ParentId",
                table: "Items",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PartNumber",
                table: "Items",
                column: "PartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPartNumbers_ItemId_VendorId_PartNumber",
                table: "VendorPartNumbers",
                columns: new[] { "ItemId", "VendorId", "PartNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorPartNumbers_PartNumber",
                table: "VendorPartNumbers",
                column: "PartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPartNumbers_VendorId",
                table: "VendorPartNumbers",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_Name",
                table: "Vendors",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorPartNumbers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
