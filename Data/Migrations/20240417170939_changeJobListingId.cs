using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTJob.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeJobListingId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobId",
                schema: "dbo",
                table: "JobListings",
                newName: "JobListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobListingId",
                schema: "dbo",
                table: "JobListings",
                newName: "JobId");
        }
    }
}
