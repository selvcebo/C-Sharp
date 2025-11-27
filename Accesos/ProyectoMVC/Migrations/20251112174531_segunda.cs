using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_TRABAJO.Migrations
{
    /// <inheritdoc />
    public partial class segunda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorreoElectronico",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroDocumento",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CorreoElectronico",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NumeroDocumento",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Clientes");
        }
    }
}
