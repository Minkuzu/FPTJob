using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTJob.Data.Migrations
{
    /// <inheritdoc />
    public partial class changetoApplicationJobId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListings_JobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "JobId",
                schema: "dbo",
                table: "JobApplications",
                newName: "ApplicationJobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_JobId",
                schema: "dbo",
                table: "JobApplications",
                newName: "IX_JobApplications_ApplicationJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListings_ApplicationJobId",
                schema: "dbo",
                table: "JobApplications",
                column: "ApplicationJobId",
                principalSchema: "dbo",
                principalTable: "JobListings",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListings_ApplicationJobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "ApplicationJobId",
                schema: "dbo",
                table: "JobApplications",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_ApplicationJobId",
                schema: "dbo",
                table: "JobApplications",
                newName: "IX_JobApplications_JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListings_JobId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobId",
                principalSchema: "dbo",
                principalTable: "JobListings",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
