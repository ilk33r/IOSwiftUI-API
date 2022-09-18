using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    public partial class OTPUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ValidateDate",
                table: "OneTimeCodes",
                type: "DATETIME",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIME");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$qce2k3WBRz3sWBY8efWGxMdFFPakirjhVsM7BGeQygXT74lbSTA5bsmD8EJxArHTGG5s++WgMBlUIhAKB0DxZ9kirRoQa3A+UTsH/USVd9vvCzPG4pL74/IVkK3ws/my");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ValidateDate",
                table: "OneTimeCodes",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIME",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$AkOQUURPH3Ikqa+3Mby5gzH3jeg0vCxTPQrbJ6Kkk5qysT9c9Wnb3ehdIH5Y7mVe4ipcxUeU5rWNn0Nh+PqHXU2yrBFkhbhbtW12RHy4VYfITx9d7BYlQj5DBY8/SdiS");
        }
    }
}
