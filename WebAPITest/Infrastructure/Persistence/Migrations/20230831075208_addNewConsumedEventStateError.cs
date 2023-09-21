using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPITest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNewConsumedEventStateError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "Race");

            migrationBuilder.InsertData(
                table: "ConsumedEventState",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 5, "Error", "При обработке события произошла ошибка" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConsumedEventState",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "Race",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
