using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTJob.Data.Migrations
{
    /// <inheritdoc />
    public partial class relationJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListings_JobListingJobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobListingJobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobListingJobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListings_JobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.AddColumn<int>(
                name: "JobListingJobId",
                schema: "dbo",
                table: "JobApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobListingJobId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobListingJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListings_JobListingJobId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobListingJobId",
                principalSchema: "dbo",
                principalTable: "JobListings",
                principalColumn: "JobId");
        }
    }
}
