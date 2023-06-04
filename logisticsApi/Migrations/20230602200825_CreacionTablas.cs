using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logisticsApi.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bodegas",
                columns: table => new
                {
                    BodegaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(200)", nullable: false),
                    Ciudad = table.Column<string>(type: "varchar(100)", nullable: false),
                    Pais = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bodegas", x => x.BodegaID);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(200)", nullable: false),
                    Ciudad = table.Column<string>(type: "varchar(100)", nullable: false),
                    Pais = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.ClienteID);
                });

            migrationBuilder.CreateTable(
                name: "puertos",
                columns: table => new
                {
                    PuertoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(200)", nullable: false),
                    Ciudad = table.Column<string>(type: "varchar(100)", nullable: false),
                    Pais = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_puertos", x => x.PuertoID);
                });

            migrationBuilder.CreateTable(
                name: "tiposProductos",
                columns: table => new
                {
                    TipoProductoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiposProductos", x => x.TipoProductoID);
                });

            migrationBuilder.CreateTable(
                name: "logisticaMaritima",
                columns: table => new
                {
                    LogisticaMaritimaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    TipoProductoID = table.Column<int>(type: "int", nullable: false),
                    CantidadProducto = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PuertoID = table.Column<int>(type: "int", nullable: false),
                    PrecioEnvio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NumeroFlota = table.Column<string>(type: "varchar(10)", nullable: false),
                    NumeroGuia = table.Column<string>(type: "varchar(10)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logisticaMaritima", x => x.LogisticaMaritimaID);
                    table.ForeignKey(
                        name: "FK_logisticaMaritima_clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "clientes",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_logisticaMaritima_puertos_PuertoID",
                        column: x => x.PuertoID,
                        principalTable: "puertos",
                        principalColumn: "PuertoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_logisticaMaritima_tiposProductos_TipoProductoID",
                        column: x => x.TipoProductoID,
                        principalTable: "tiposProductos",
                        principalColumn: "TipoProductoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "logisticaTerrestre",
                columns: table => new
                {
                    LogisticaMaritimaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    TipoProductoID = table.Column<int>(type: "int", nullable: false),
                    CantidadProducto = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BodegaID = table.Column<int>(type: "int", nullable: false),
                    PrecioEnvio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NumeroFlota = table.Column<string>(type: "varchar(10)", nullable: false),
                    NumeroGuia = table.Column<string>(type: "varchar(10)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logisticaTerrestre", x => x.LogisticaMaritimaID);
                    table.ForeignKey(
                        name: "FK_logisticaTerrestre_bodegas_BodegaID",
                        column: x => x.BodegaID,
                        principalTable: "bodegas",
                        principalColumn: "BodegaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_logisticaTerrestre_clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "clientes",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_logisticaTerrestre_tiposProductos_TipoProductoID",
                        column: x => x.TipoProductoID,
                        principalTable: "tiposProductos",
                        principalColumn: "TipoProductoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_logisticaMaritima_ClienteID",
                table: "logisticaMaritima",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_logisticaMaritima_PuertoID",
                table: "logisticaMaritima",
                column: "PuertoID");

            migrationBuilder.CreateIndex(
                name: "IX_logisticaMaritima_TipoProductoID",
                table: "logisticaMaritima",
                column: "TipoProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_logisticaTerrestre_BodegaID",
                table: "logisticaTerrestre",
                column: "BodegaID");

            migrationBuilder.CreateIndex(
                name: "IX_logisticaTerrestre_ClienteID",
                table: "logisticaTerrestre",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_logisticaTerrestre_TipoProductoID",
                table: "logisticaTerrestre",
                column: "TipoProductoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logisticaMaritima");

            migrationBuilder.DropTable(
                name: "logisticaTerrestre");

            migrationBuilder.DropTable(
                name: "puertos");

            migrationBuilder.DropTable(
                name: "bodegas");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "tiposProductos");
        }
    }
}
