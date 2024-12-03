using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AQIViewer.Data.Migrations
{
    /// <inheritdoc />
    public partial class modelupdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ReceiveAlerts",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Forecast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PredictedAQI = table.Column<int>(type: "int", nullable: false),
                    ForecastDate = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecast", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationPoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longtitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationPoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pollutant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Measure = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pollutant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollutionLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinPoint = table.Column<int>(type: "int", nullable: false),
                    MaxPoint = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollutionLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirQualityRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<TimeSpan>(type: "time", nullable: false),
                    AQI = table.Column<int>(type: "int", nullable: false),
                    LocationPointId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirQualityRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirQualityRecords_LocationPoint_LocationPointId",
                        column: x => x.LocationPointId,
                        principalTable: "LocationPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationPointId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLocation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLocation_LocationPoint_LocationPointId",
                        column: x => x.LocationPointId,
                        principalTable: "LocationPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollutantLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinConcentration = table.Column<double>(type: "float", nullable: false),
                    MaxConcentration = table.Column<double>(type: "float", nullable: false),
                    PollutionLevelId = table.Column<int>(type: "int", nullable: false),
                    PollutantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollutantLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollutantLevel_Pollutant_PollutantId",
                        column: x => x.PollutantId,
                        principalTable: "Pollutant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollutantLevel_PollutionLevel_PollutionLevelId",
                        column: x => x.PollutionLevelId,
                        principalTable: "PollutionLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAcknowleged = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    AQRId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alert_AirQualityRecords_AQRId",
                        column: x => x.AQRId,
                        principalTable: "AirQualityRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AQRForecast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AQRId = table.Column<int>(type: "int", nullable: false),
                    ForecastId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AQRForecast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AQRForecast_AirQualityRecords_AQRId",
                        column: x => x.AQRId,
                        principalTable: "AirQualityRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AQRForecast_Forecast_ForecastId",
                        column: x => x.ForecastId,
                        principalTable: "Forecast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollutionRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Concentration = table.Column<double>(type: "float", nullable: false),
                    PollutantId = table.Column<int>(type: "int", nullable: false),
                    AQRId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollutionRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollutionRecord_AirQualityRecords_AQRId",
                        column: x => x.AQRId,
                        principalTable: "AirQualityRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollutionRecord_Pollutant_PollutantId",
                        column: x => x.PollutantId,
                        principalTable: "Pollutant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAlert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AlertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAlert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAlert_Alert_AlertId",
                        column: x => x.AlertId,
                        principalTable: "Alert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAlert_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirQualityRecords_LocationPointId",
                table: "AirQualityRecords",
                column: "LocationPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Alert_AQRId",
                table: "Alert",
                column: "AQRId");

            migrationBuilder.CreateIndex(
                name: "IX_AQRForecast_AQRId",
                table: "AQRForecast",
                column: "AQRId");

            migrationBuilder.CreateIndex(
                name: "IX_AQRForecast_ForecastId",
                table: "AQRForecast",
                column: "ForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_PollutantLevel_PollutantId",
                table: "PollutantLevel",
                column: "PollutantId");

            migrationBuilder.CreateIndex(
                name: "IX_PollutantLevel_PollutionLevelId",
                table: "PollutantLevel",
                column: "PollutionLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_PollutionRecord_AQRId",
                table: "PollutionRecord",
                column: "AQRId");

            migrationBuilder.CreateIndex(
                name: "IX_PollutionRecord_PollutantId",
                table: "PollutionRecord",
                column: "PollutantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAlert_AlertId",
                table: "UserAlert",
                column: "AlertId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAlert_UserId",
                table: "UserAlert",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocation_LocationPointId",
                table: "UserLocation",
                column: "LocationPointId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocation_UserId",
                table: "UserLocation",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AQRForecast");

            migrationBuilder.DropTable(
                name: "PollutantLevel");

            migrationBuilder.DropTable(
                name: "PollutionRecord");

            migrationBuilder.DropTable(
                name: "UserAlert");

            migrationBuilder.DropTable(
                name: "UserLocation");

            migrationBuilder.DropTable(
                name: "Forecast");

            migrationBuilder.DropTable(
                name: "PollutionLevel");

            migrationBuilder.DropTable(
                name: "Pollutant");

            migrationBuilder.DropTable(
                name: "Alert");

            migrationBuilder.DropTable(
                name: "AirQualityRecords");

            migrationBuilder.DropTable(
                name: "LocationPoint");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReceiveAlerts",
                table: "AspNetUsers");
        }
    }
}
