using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientUploadTest2.Data.Migrations
{
    public partial class AddEmployerInApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HospitalClient",
                table: "Report",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Employer",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalClient",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Employer",
                table: "AspNetUsers");
        }
    }
}
