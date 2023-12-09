using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addMainModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverSeasonParticipation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<byte>(type: "tinyint", nullable: true),
                    Score = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverSeasonParticipation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverSeasonParticipation_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverSeasonParticipation_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverTeamContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTeamContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverTeamContract_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverTeamContract_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamSeasonParticipation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<byte>(type: "tinyint", nullable: true),
                    Score = table.Column<byte>(type: "tinyint", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamSeasonParticipation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamSeasonParticipation_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamSeasonParticipation_Team_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    NumberInSeason = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Race_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Race_Track_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Track",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverRaceParticipation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<byte>(type: "tinyint", nullable: true),
                    ScoreForRace = table.Column<byte>(type: "tinyint", nullable: true),
                    ScoreInSeasonAfterRace = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverRaceParticipation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverRaceParticipation_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverRaceParticipation_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverRaceParticipation_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamRaceParticipation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    ScoreForRace = table.Column<byte>(type: "tinyint", nullable: true),
                    ScoreInSeasonAfterRace = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRaceParticipation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamRaceParticipation_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamRaceParticipation_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverRaceParticipation_DriverId",
                table: "DriverRaceParticipation",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRaceParticipation_RaceId",
                table: "DriverRaceParticipation",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRaceParticipation_TeamId",
                table: "DriverRaceParticipation",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverSeasonParticipation_DriverId",
                table: "DriverSeasonParticipation",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverSeasonParticipation_SeasonId",
                table: "DriverSeasonParticipation",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverTeamContract_DriverId",
                table: "DriverTeamContract",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverTeamContract_TeamId",
                table: "DriverTeamContract",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_RaceId",
                table: "Race",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_SeasonId",
                table: "Race",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamRaceParticipation_RaceId",
                table: "TeamRaceParticipation",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamRaceParticipation_TeamId",
                table: "TeamRaceParticipation",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSeasonParticipation_DriverId",
                table: "TeamSeasonParticipation",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSeasonParticipation_SeasonId",
                table: "TeamSeasonParticipation",
                column: "SeasonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverRaceParticipation");

            migrationBuilder.DropTable(
                name: "DriverSeasonParticipation");

            migrationBuilder.DropTable(
                name: "DriverTeamContract");

            migrationBuilder.DropTable(
                name: "TeamRaceParticipation");

            migrationBuilder.DropTable(
                name: "TeamSeasonParticipation");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Track");
        }
    }
}
