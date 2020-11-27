using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographiApp.Data.Migrations
{
    public partial class AddThumbnailToPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailHref",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailHref",
                table: "Photos");
        }
    }
}
