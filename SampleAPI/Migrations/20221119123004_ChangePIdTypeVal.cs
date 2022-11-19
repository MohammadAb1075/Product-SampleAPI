using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleAPI.Migrations
{
    public partial class ChangePIdTypeVal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "71db0eab-febc-4edb-a51a-11126d96558b",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "d1bc4770-9e13-4d01-8569-e09ab6643103");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "d1bc4770-9e13-4d01-8569-e09ab6643103",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "71db0eab-febc-4edb-a51a-11126d96558b");
        }
    }
}
