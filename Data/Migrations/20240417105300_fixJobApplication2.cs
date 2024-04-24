using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTJob.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixJobApplication2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListing_JobListingsJobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobListing_Employer_EmployerId",
                schema: "dbo",
                table: "JobListing");

            migrationBuilder.DropForeignKey(
                name: "FK_JobListing_JobCategories_JobCategoryId",
                schema: "dbo",
                table: "JobListing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobListing",
                schema: "dbo",
                table: "JobListing");

            migrationBuilder.RenameTable(
                name: "JobListing",
                schema: "dbo",
                newName: "JobListings",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_JobListing_JobCategoryId",
                schema: "dbo",
                table: "JobListings",
                newName: "IX_JobListings_JobCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_JobListing_EmployerId",
                schema: "dbo",
                table: "JobListings",
                newName: "IX_JobListings_EmployerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobListings",
                schema: "dbo",
                table: "JobListings",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListings_JobListingsJobId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobListingsJobId",
                principalSchema: "dbo",
                principalTable: "JobListings",
                principalColumn: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobListings_Employer_EmployerId",
                schema: "dbo",
                table: "JobListings",
                column: "EmployerId",
                principalSchema: "dbo",
                principalTable: "Employer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobListings_JobCategories_JobCategoryId",
                schema: "dbo",
                table: "JobListings",
                column: "JobCategoryId",
                principalSchema: "dbo",
                principalTable: "JobCategories",
                principalColumn: "JobCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListings_JobListingsJobId",
                schema: "dbo",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobListings_Employer_EmployerId",
                schema: "dbo",
                table: "JobListings");

            migrationBuilder.DropForeignKey(
                name: "FK_JobListings_JobCategories_JobCategoryId",
                schema: "dbo",
                table: "JobListings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobListings",
                schema: "dbo",
                table: "JobListings");

            migrationBuilder.RenameTable(
                name: "JobListings",
                schema: "dbo",
                newName: "JobListing",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_JobListings_JobCategoryId",
                schema: "dbo",
                table: "JobListing",
                newName: "IX_JobListing_JobCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_JobListings_EmployerId",
                schema: "dbo",
                table: "JobListing",
                newName: "IX_JobListing_EmployerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobListing",
                schema: "dbo",
                table: "JobListing",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListing_JobListingsJobId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobListingsJobId",
                principalSchema: "dbo",
                principalTable: "JobListing",
                principalColumn: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobListing_Employer_EmployerId",
                schema: "dbo",
                table: "JobListing",
                column: "EmployerId",
                principalSchema: "dbo",
                principalTable: "Employer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobListing_JobCategories_JobCategoryId",
                schema: "dbo",
                table: "JobListing",
                column: "JobCategoryId",
                principalSchema: "dbo",
                principalTable: "JobCategories",
                principalColumn: "JobCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
