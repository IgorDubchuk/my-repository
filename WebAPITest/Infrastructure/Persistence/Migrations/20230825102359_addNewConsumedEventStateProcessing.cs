using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPITest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNewConsumedEventStateProcessing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConsumedEventState",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 4, "Processing", "Событие обрабатывается" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConsumedEventState",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
