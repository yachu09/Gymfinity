using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularApp5.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUselessProperties2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
