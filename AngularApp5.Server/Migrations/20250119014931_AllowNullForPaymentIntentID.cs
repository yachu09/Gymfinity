using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularApp5.Server.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullForPaymentIntentID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "PaymentIntentId",
            table: "Orders",
            type: "nvarchar(max)",
            nullable: true, // Zmiana na nullable
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: false); // Poprzednie ustawienie
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "PaymentIntentId",
            table: "Orders",
            type: "nvarchar(max)",
            nullable: false, // Powrót do NOT NULL
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);
        }
    }
}
