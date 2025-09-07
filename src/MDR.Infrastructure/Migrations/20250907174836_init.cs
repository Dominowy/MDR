using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mas2Device",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TempAlarmThreshold = table.Column<double>(type: "double precision", nullable: false),
                    HumidityAlarmThreshold = table.Column<double>(type: "double precision", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mas2Device", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mouse2",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AlarmThreshold = table.Column<double>(type: "double precision", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouse2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mouse2B",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AlarmThreshold = table.Column<double>(type: "double precision", nullable: false),
                    CableLength = table.Column<double>(type: "double precision", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouse2B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MouseCombo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AlarmThreshold = table.Column<double>(type: "double precision", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouseCombo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mas2Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    Humidity = table.Column<double>(type: "double precision", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mas2Data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mas2Data_Mas2Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Mas2Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mouse2Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Voltage = table.Column<double>(type: "double precision", nullable: false),
                    Resistance = table.Column<double>(type: "double precision", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouse2Data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mouse2Data_Mouse2_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Mouse2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mouse2BData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Voltage = table.Column<double>(type: "double precision", nullable: false),
                    Resistance = table.Column<double>(type: "double precision", nullable: false),
                    LeakLocationPercent = table.Column<double>(type: "double precision", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouse2BData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mouse2BData_Mouse2B_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Mouse2B",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MouseComboData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Voltage = table.Column<double>(type: "double precision", nullable: false),
                    Resistance = table.Column<double>(type: "double precision", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouseComboData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MouseComboData_MouseCombo_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "MouseCombo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reflectogram",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeriesNumber = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    MouseComboDataId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reflectogram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reflectogram_MouseComboData_MouseComboDataId",
                        column: x => x.MouseComboDataId,
                        principalTable: "MouseComboData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mas2Data_DeviceId",
                table: "Mas2Data",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouse2BData_DeviceId",
                table: "Mouse2BData",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouse2Data_DeviceId",
                table: "Mouse2Data",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_MouseComboData_DeviceId",
                table: "MouseComboData",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reflectogram_MouseComboDataId",
                table: "Reflectogram",
                column: "MouseComboDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mas2Data");

            migrationBuilder.DropTable(
                name: "Mouse2BData");

            migrationBuilder.DropTable(
                name: "Mouse2Data");

            migrationBuilder.DropTable(
                name: "Reflectogram");

            migrationBuilder.DropTable(
                name: "Mas2Device");

            migrationBuilder.DropTable(
                name: "Mouse2B");

            migrationBuilder.DropTable(
                name: "Mouse2");

            migrationBuilder.DropTable(
                name: "MouseComboData");

            migrationBuilder.DropTable(
                name: "MouseCombo");
        }
    }
}
