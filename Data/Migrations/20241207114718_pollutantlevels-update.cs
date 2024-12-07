using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AQIViewer.Data.Migrations
{
    /// <inheritdoc />
    public partial class pollutantlevelsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LevelRecomendation",
                table: "PollutantLevel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelRecomendation",
                table: "PollutantLevel");
        }
    }
}
