using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductApp.Migrations
{
    public partial class MyApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(maxLength: 50, nullable: false),
                    CustomerMobile = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerAddress = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    GenderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    ProductPicturePath = table.Column<string>(nullable: true),
                    ProductByteImage = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(maxLength: 50, nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_District_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                    table.ForeignKey(
                        name: "FK_Brand_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Thana",
                columns: table => new
                {
                    ThanaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThanaName = table.Column<string>(maxLength: 50, nullable: true),
                    DistrictId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thana", x => x.ThanaId);
                    table.ForeignKey(
                        name: "FK_Thana_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryName = table.Column<string>(maxLength: 50, nullable: false),
                    BrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryId);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeAddress = table.Column<string>(maxLength: 50, nullable: true),
                    EmployeeMobileNo = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    ThanaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Thana_ThanaId",
                        column: x => x.ThanaId,
                        principalTable: "Thana",
                        principalColumn: "ThanaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCostInfo",
                columns: table => new
                {
                    ProductCostInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    ProductCategoryId = table.Column<int>(nullable: false),
                    ProductSize = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<double>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    TransportCost = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    DateOfOrder = table.Column<DateTime>(nullable: false),
                    Subtotal = table.Column<double>(nullable: false),
                    GrandTotal = table.Column<double>(nullable: false),
                    CurrentPayment = table.Column<double>(nullable: false),
                    DueAmount = table.Column<double>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCostInfo", x => x.ProductCostInfoId);
                    table.ForeignKey(
                        name: "FK_ProductCostInfo_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCostInfo_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCostInfo_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductCostInfo_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductSellInfo",
                columns: table => new
                {
                    ProductSellInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    ProductCategoryId = table.Column<int>(nullable: false),
                    ProductSize = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Subtotal = table.Column<double>(nullable: false),
                    DateOfSell = table.Column<DateTime>(nullable: false),
                    GrandTotal = table.Column<double>(nullable: false),
                    CurrentPayment = table.Column<double>(nullable: false),
                    DueAmount = table.Column<double>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSellInfo", x => x.ProductSellInfoId);
                    table.ForeignKey(
                        name: "FK_ProductSellInfo_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSellInfo_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSellInfo_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSellInfo_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductSellInfo_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_ProductId",
                table: "Brand",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_District_CountryId",
                table: "District",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CountryId",
                table: "Employee",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DistrictId",
                table: "Employee",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_GenderId",
                table: "Employee",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ThanaId",
                table: "Employee",
                column: "ThanaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_BrandId",
                table: "ProductCategory",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCostInfo_BrandId",
                table: "ProductCostInfo",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCostInfo_EmployeeId",
                table: "ProductCostInfo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCostInfo_ProductCategoryId",
                table: "ProductCostInfo",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCostInfo_ProductId",
                table: "ProductCostInfo",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSellInfo_BrandId",
                table: "ProductSellInfo",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSellInfo_CustomerId",
                table: "ProductSellInfo",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSellInfo_EmployeeId",
                table: "ProductSellInfo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSellInfo_ProductCategoryId",
                table: "ProductSellInfo",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSellInfo_ProductId",
                table: "ProductSellInfo",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Thana_DistrictId",
                table: "Thana",
                column: "DistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCostInfo");

            migrationBuilder.DropTable(
                name: "ProductSellInfo");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Thana");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
