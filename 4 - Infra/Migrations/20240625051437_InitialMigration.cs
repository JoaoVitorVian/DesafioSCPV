using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4___Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostosDeVacinacao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostosDeVacinacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacinas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Fabricante = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Lote = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostoDeVacinacaoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacinas_PostosDeVacinacao_PostoDeVacinacaoId",
                        column: x => x.PostoDeVacinacaoId,
                        principalTable: "PostosDeVacinacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacinas_PostoDeVacinacaoId",
                table: "Vacinas",
                column: "PostoDeVacinacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacinas");

            migrationBuilder.DropTable(
                name: "PostosDeVacinacao");
        }
    }
}
