using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTJob.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixJobApplication1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListing_JobListingJobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "JobListingJobId",
                schema: "dbo",
                table: "JobApplications",
                newName: "JobListingsJobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_JobListingJobId",
                schema: "dbo",
                table: "JobApplications",
                newName: "IX_JobApplications_JobListingsJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListing_JobListingsJobId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobListingsJobId",
                principalSchema: "dbo",
                principalTable: "JobListing",
                principalColumn: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListing_JobListingsJobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "JobListingsJobId",
                schema: "dbo",
                table: "JobApplications",
                newName: "JobListingJobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_JobListingsJobId",
                schema: "dbo",
                table: "JobApplications",
                newName: "IX_JobApplications_JobListingJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListing_JobListingJobId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobListingJobId",
                principalSchema: "dbo",
                principalTable: "JobListing",
                principalColumn: "JobId");
        }
    }
}
