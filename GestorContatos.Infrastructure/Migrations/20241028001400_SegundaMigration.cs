using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorContatos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TesteMigration",
                table: "Contatos",
                type: "VARCHAR(100)",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TesteMigration",
                table: "Contatos");
        }
    }
}
