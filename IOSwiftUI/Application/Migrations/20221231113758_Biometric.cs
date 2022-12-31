using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    public partial class Biometric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberFaceIDs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    AuthenticationKey = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PairDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberFaceIDs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MemberFaceIDs_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$SOkE5oCTi5wWVy8kjHltpHUQkc825PSCKDF49eReWdcK/Iq9ZQP3buE3qjJGYkPta+id+IRdoaOUMq+8P0GlTclEqq/djAAUrjnfQj55LYeU9qsld6D5sV7H4MbYlCVK");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFaceIDs_MemberID",
                table: "MemberFaceIDs",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFaceIDs_PairDate",
                table: "MemberFaceIDs",
                column: "PairDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberFaceIDs");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$qce2k3WBRz3sWBY8efWGxMdFFPakirjhVsM7BGeQygXT74lbSTA5bsmD8EJxArHTGG5s++WgMBlUIhAKB0DxZ9kirRoQa3A+UTsH/USVd9vvCzPG4pL74/IVkK3ws/my");
        }
    }
}
