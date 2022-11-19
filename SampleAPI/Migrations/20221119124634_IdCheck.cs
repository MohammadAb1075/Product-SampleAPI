using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleAPI.Migrations
{
    public partial class IdCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "79bf11cc-bca1-419c-ae5a-401883a34cf9",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "71db0eab-febc-4edb-a51a-11126d96558b");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "71db0eab-febc-4edb-a51a-11126d96558b",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "79bf11cc-bca1-419c-ae5a-401883a34cf9");
        }
    }
}
