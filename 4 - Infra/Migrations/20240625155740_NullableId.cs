using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4___Infra.Migrations
{
    /// <inheritdoc />
    public partial class NullableId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PostoDeVacinacaoId",
                table: "Vacinas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PostoDeVacinacaoId",
                table: "Vacinas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
