using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleAPI.Migrations
{
    public partial class ChangePIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "d1bc4770-9e13-4d01-8569-e09ab6643103",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("a4be857c-1490-4803-afc8-f352f068a21e"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("a4be857c-1490-4803-afc8-f352f068a21e"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "d1bc4770-9e13-4d01-8569-e09ab6643103");
        }
    }
}
