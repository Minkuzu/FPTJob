using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTJob.Data.Migrations
{
    /// <inheritdoc />
    public partial class addAllTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employer_User_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobCategories",
                schema: "dbo",
                columns: table => new
                {
                    JobCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.JobCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "JobSeeker",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Skill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeeker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobListing",
                schema: "dbo",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobRequirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobListing", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_JobListing_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalSchema: "dbo",
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobListing_JobCategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalSchema: "dbo",
                        principalTable: "JobCategories",
                        principalColumn: "JobCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                schema: "dbo",
                columns: table => new
                {
                    JobApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    JobSeekerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobListingJobId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.JobApplicationId);
                    table.ForeignKey(
                        name: "FK_JobApplications_JobListing_JobListingJobId",
                        column: x => x.JobListingJobId,
                        principalSchema: "dbo",
                        principalTable: "JobListing",
                        principalColumn: "JobId");
                    table.ForeignKey(
                        name: "FK_JobApplications_JobSeeker_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalSchema: "dbo",
                        principalTable: "JobSeeker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobListingJobId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobListingJobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobSeekerId",
                schema: "dbo",
                table: "JobApplications",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobListing_EmployerId",
                schema: "dbo",
                table: "JobListing",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobListing_JobCategoryId",
                schema: "dbo",
                table: "JobListing",
                column: "JobCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "JobListing",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "JobSeeker",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Employer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "JobCategories",
                schema: "dbo");
        }
    }
}
