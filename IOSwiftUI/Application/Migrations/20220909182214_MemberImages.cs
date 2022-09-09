using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    public partial class MemberImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MemberImages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MemberImages_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$NNBIRS6vFuTDj0+LPwjxfUrepth9wjh/IRI38fpPd6dddKHm4K8jU8DU0ovzDel+F9r0E8f9l8z+xUcsPPNiWjKZsO1Ew6fk+qlkeNXvCL9pOTfKv/AcN589NjKPAg8N");

            migrationBuilder.CreateIndex(
                name: "IX_MemberImages_CreateDate",
                table: "MemberImages",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_MemberImages_MemberID",
                table: "MemberImages",
                column: "MemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberImages");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Members");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$5m0ylN3bS94ONPbhf2wqwkh+nOS7HtVbO4Ip31fsffh0ecnUcFve95a1S4rDn63YT9brhvx7rOOyyrDChFw7AKVXkh4fXmvMCBLD3zNRMKovOiR97qokvFxo/9ZDgHMM");
        }
    }
}
