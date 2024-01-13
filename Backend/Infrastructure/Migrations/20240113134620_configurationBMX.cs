using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class configurationBMX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigurationsBMX",
                columns: table => new
                {
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameConfiguration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HandlebarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HandlebarCuffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HandlebarCapId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ForkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GallowsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HeadsetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RotorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaddleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaddleStemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaddleClampId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WheelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TireId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RimId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpokesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HubsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChainsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FrontBrakesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RearBrakesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssemblyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PedalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PedalArmsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CrankSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PegsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationsBMX", x => x.ConfigurationId);
                    table.ForeignKey(
                        name: "FK_ConfigurationsBMX_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationsBMX_UserId",
                table: "ConfigurationsBMX",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationsBMX");
        }
    }
}
