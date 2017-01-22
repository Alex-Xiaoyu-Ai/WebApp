using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientUploadTest2.Data.Migrations
{
    public partial class InitialReport2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Report",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "Report",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "Report");
        }
    }
}
