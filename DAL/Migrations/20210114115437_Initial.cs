using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lisas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lisas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PizzaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "POrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaTypeId = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PizzaTypeLisaAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaTypeId = table.Column<int>(nullable: false),
                    LisaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaTypeLisaAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaTypeLisaAssignments_Lisas_LisaId",
                        column: x => x.LisaId,
                        principalTable: "Lisas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaTypeLisaAssignments_PizzaTypes_PizzaTypeId",
                        column: x => x.PizzaTypeId,
                        principalTable: "PizzaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POrderLisaAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POrderId = table.Column<int>(nullable: false),
                    LisaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POrderLisaAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POrderLisaAssignments_Lisas_LisaId",
                        column: x => x.LisaId,
                        principalTable: "Lisas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POrderLisaAssignments_POrders_POrderId",
                        column: x => x.POrderId,
                        principalTable: "POrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POrderOrderAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    POrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POrderOrderAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POrderOrderAssignments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POrderOrderAssignments_POrders_POrderId",
                        column: x => x.POrderId,
                        principalTable: "POrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTypeLisaAssignments_LisaId",
                table: "PizzaTypeLisaAssignments",
                column: "LisaId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTypeLisaAssignments_PizzaTypeId",
                table: "PizzaTypeLisaAssignments",
                column: "PizzaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_POrderLisaAssignments_LisaId",
                table: "POrderLisaAssignments",
                column: "LisaId");

            migrationBuilder.CreateIndex(
                name: "IX_POrderLisaAssignments_POrderId",
                table: "POrderLisaAssignments",
                column: "POrderId");

            migrationBuilder.CreateIndex(
                name: "IX_POrderOrderAssignments_OrderId",
                table: "POrderOrderAssignments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_POrderOrderAssignments_POrderId",
                table: "POrderOrderAssignments",
                column: "POrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaTypeLisaAssignments");

            migrationBuilder.DropTable(
                name: "POrderLisaAssignments");

            migrationBuilder.DropTable(
                name: "POrderOrderAssignments");

            migrationBuilder.DropTable(
                name: "PizzaTypes");

            migrationBuilder.DropTable(
                name: "Lisas");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "POrders");
        }
    }
}
