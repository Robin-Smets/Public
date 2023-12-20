using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovelistBlazor.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlotUnitTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "PlotUnitTypes",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Free Hand Plot"},
                { 2, "Heros Journey" }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "PlotUnitTypes",
            keyColumn: "Id",
            keyValues: new object[] { 1, 2 });
        }
    }
}
