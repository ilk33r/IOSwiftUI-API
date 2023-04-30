using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    public partial class MemberVerified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MRZFullString",
                table: "Members",
                type: "varchar(128)",
                maxLength: 128,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$iF2eGK9pta12h/NXjo05ASI63DcTchHow0KFST2fw/hOsIYpylKOsBIgqvFLRX7ZEmSNfQg5Ymgc7SgQFHrbQQM1uhC4ymDRC0/kJJXY4MzrKHqLT4I9wSLh3e1WEaBk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MRZFullString",
                table: "Members");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$SOkE5oCTi5wWVy8kjHltpHUQkc825PSCKDF49eReWdcK/Iq9ZQP3buE3qjJGYkPta+id+IRdoaOUMq+8P0GlTclEqq/djAAUrjnfQj55LYeU9qsld6D5sV7H4MbYlCVK");
        }
    }
}
