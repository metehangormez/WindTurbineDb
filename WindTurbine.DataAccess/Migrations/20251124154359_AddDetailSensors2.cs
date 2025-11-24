using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WindTurbine.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailSensors2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BladeVibration",
                table: "Turbines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GearboxOilTemp",
                table: "Turbines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GearboxVibration",
                table: "Turbines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GeneratorRPM",
                table: "Turbines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GeneratorTemp",
                table: "Turbines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HubTemperature",
                table: "Turbines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MainBearingTemp",
                table: "Turbines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PitchAngle",
                table: "Turbines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TransformerTemp",
                table: "Turbines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BladeVibration",
                table: "Turbines");

            migrationBuilder.DropColumn(
                name: "GearboxOilTemp",
                table: "Turbines");

            migrationBuilder.DropColumn(
                name: "GearboxVibration",
                table: "Turbines");

            migrationBuilder.DropColumn(
                name: "GeneratorRPM",
                table: "Turbines");

            migrationBuilder.DropColumn(
                name: "GeneratorTemp",
                table: "Turbines");

            migrationBuilder.DropColumn(
                name: "HubTemperature",
                table: "Turbines");

            migrationBuilder.DropColumn(
                name: "MainBearingTemp",
                table: "Turbines");

            migrationBuilder.DropColumn(
                name: "PitchAngle",
                table: "Turbines");

            migrationBuilder.DropColumn(
                name: "TransformerTemp",
                table: "Turbines");
        }
    }
}
