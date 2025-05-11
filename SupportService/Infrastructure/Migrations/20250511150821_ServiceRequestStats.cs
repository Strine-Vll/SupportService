using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServiceRequestStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequestStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SatisfactionIndex = table.Column<double>(type: "float", nullable: false),
                    ReescalateAmount = table.Column<int>(type: "int", nullable: false),
                    ReactionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ResolutionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceRequestId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestStats_ServiceRequests_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ServiceRequestStats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestStats_ServiceRequestId",
                table: "ServiceRequestStats",
                column: "ServiceRequestId",
                unique: true,
                filter: "[ServiceRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestStats_UserId",
                table: "ServiceRequestStats",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequestStats");
        }
    }
}
