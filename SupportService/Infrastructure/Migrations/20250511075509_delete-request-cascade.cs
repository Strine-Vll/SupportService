﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deleterequestcascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Groups_GroupId",
                table: "ServiceRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Groups_GroupId",
                table: "ServiceRequests",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Groups_GroupId",
                table: "ServiceRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Groups_GroupId",
                table: "ServiceRequests",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
