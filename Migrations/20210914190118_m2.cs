using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Operations_.Net_Core___Movies_Site.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Poster",
                table: "Movies",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(2500)",
                oldMaxLength: 2500);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Poster",
                table: "Movies",
                type: "varbinary(2500)",
                maxLength: 2500,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }
    }
}
