using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using PatientUploadTest2.Models;

namespace PatientUploadTest2.Data.Migrations
{
    public partial class NewIdentityUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: Roles.HospitalClient);

            migrationBuilder.AddColumn<string>(
                name: "SigniturePath",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SigniturePath",
                table: "AspNetUsers");
        }
    }
}
