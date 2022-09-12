using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    public partial class OTP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneTimeCode",
                table: "Members");

            migrationBuilder.CreateTable(
                name: "OneTimeCodes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PhoneNumber = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OneTimeCode = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTimeOffset>(type: "DATETIME", nullable: false),
                    ValidateDate = table.Column<DateTimeOffset>(type: "DATETIME", nullable: false),
                    IsValidated = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneTimeCodes", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$AkOQUURPH3Ikqa+3Mby5gzH3jeg0vCxTPQrbJ6Kkk5qysT9c9Wnb3ehdIH5Y7mVe4ipcxUeU5rWNn0Nh+PqHXU2yrBFkhbhbtW12RHy4VYfITx9d7BYlQj5DBY8/SdiS");

            migrationBuilder.CreateIndex(
                name: "IX_OneTimeCodes_PhoneNumber",
                table: "OneTimeCodes",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OneTimeCodes");

            migrationBuilder.AddColumn<string>(
                name: "OneTimeCode",
                table: "Members",
                type: "varchar(6)",
                maxLength: 6,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$0c7NyCRjCViMZqrnHJi+zZGqTbYxsNxR4I/eT9G+xgxxVYB9CI66AGMYMFsGyij/N1HOBwzUiDqI00MD/JmcX+9tH6xcTWXiYU6dLexnFRmqt6wXIs2QSGwe+sAWFQ95");
        }
    }
}
