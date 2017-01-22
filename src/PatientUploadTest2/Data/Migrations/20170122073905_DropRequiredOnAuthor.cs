using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientUploadTest2.Data.Migrations
{
    public partial class DropRequiredOnAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Patient_patientid",
                table: "Report");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Report",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Patient_Patientid",
                table: "Report",
                column: "patientid",
                principalTable: "Patient",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "patientid",
                table: "Report",
                newName: "Patientid");

            migrationBuilder.RenameIndex(
                name: "IX_Report_patientid",
                table: "Report",
                newName: "IX_Report_Patientid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Patient_Patientid",
                table: "Report");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Report",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Patient_patientid",
                table: "Report",
                column: "Patientid",
                principalTable: "Patient",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "Patientid",
                table: "Report",
                newName: "patientid");

            migrationBuilder.RenameIndex(
                name: "IX_Report_Patientid",
                table: "Report",
                newName: "IX_Report_patientid");
        }
    }
}
