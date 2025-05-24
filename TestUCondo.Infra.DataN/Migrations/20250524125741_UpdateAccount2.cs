using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestUCondo.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccount2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Accounts");
        }
    }
}
