using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTJob.Data.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobListing_Employer_EmployerId",
                schema: "dbo",
                table: "JobListing");

            migrationBuilder.AlterColumn<string>(
                name: "EmployerId",
                schema: "dbo",
                table: "JobListing",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_JobListing_Employer_EmployerId",
                schema: "dbo",
                table: "JobListing",
                column: "EmployerId",
                principalSchema: "dbo",
                principalTable: "Employer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobListing_Employer_EmployerId",
                schema: "dbo",
                table: "JobListing");

            migrationBuilder.AlterColumn<string>(
                name: "EmployerId",
                schema: "dbo",
                table: "JobListing",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobListing_Employer_EmployerId",
                schema: "dbo",
                table: "JobListing",
                column: "EmployerId",
                principalSchema: "dbo",
                principalTable: "Employer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
