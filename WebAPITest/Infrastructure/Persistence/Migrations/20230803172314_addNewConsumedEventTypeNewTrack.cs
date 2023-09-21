using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPITest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNewConsumedEventTypeNewTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConsumedEventType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 5, "NewTrack", "Новый автодром" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConsumedEventType",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
