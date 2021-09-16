using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Operations_.Net_Core___Movies_Site.Migrations
{
    public partial class EditMoviesGenresTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Double",
                table: "Movies");

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Movies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "Double",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
