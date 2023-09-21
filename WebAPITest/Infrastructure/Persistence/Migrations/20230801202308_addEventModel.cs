using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPITest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEventModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumedEventState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedEventState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumedEventType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedEventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublishedEventType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedEventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumedEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    EventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecieveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    ProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumedEvent_ConsumedEventState_StateId",
                        column: x => x.StateId,
                        principalTable: "ConsumedEventState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumedEvent_ConsumedEventType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ConsumedEventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublishedEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishedEvent_PublishedEventType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PublishedEventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConsumedEventState",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Recieved", "Событие получено" },
                    { 2, "Processed", "Событие обработано" },
                    { 3, "ToRepeatProcess", "Событие должно быть обработано повторно" }
                });

            migrationBuilder.InsertData(
                table: "ConsumedEventType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "SeasonCalendarPublished", "Опубликован календарь сезона" },
                    { 2, "SeasonParticipantsPublished", "Опубликован состав команд-участников сезона" },
                    { 3, "DriverContractSigned", "Заключен контракт с гонщиком" },
                    { 4, "RaceFinished", "Гонка завершилась" }
                });

            migrationBuilder.InsertData(
                table: "PublishedEventType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AfterRaceDriverStandings", "Позиции в чемпионате мира по итогам гонки" },
                    { 2, "AfterRaceCunstructorStandings", "Позиции в кубке конструкторов по итогам гонки" },
                    { 3, "DriverChampionDetermined", "Определен чемпион мира" },
                    { 4, "ConstructorChampionDetermined", "Определен обладатель кубка конструкторов" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumedEvent_StateId",
                table: "ConsumedEvent",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumedEvent_TypeId",
                table: "ConsumedEvent",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishedEvent_TypeId",
                table: "PublishedEvent",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumedEvent");

            migrationBuilder.DropTable(
                name: "PublishedEvent");

            migrationBuilder.DropTable(
                name: "ConsumedEventState");

            migrationBuilder.DropTable(
                name: "ConsumedEventType");

            migrationBuilder.DropTable(
                name: "PublishedEventType");
        }
    }
}
