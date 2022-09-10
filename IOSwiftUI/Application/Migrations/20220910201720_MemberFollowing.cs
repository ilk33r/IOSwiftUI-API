using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    public partial class MemberFollowing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "BirthDate",
                table: "Members",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime");

            migrationBuilder.CreateTable(
                name: "MemberFollowings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FollowDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    FollowingMemberID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberFollowings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MemberFollowings_Members_FollowingMemberID",
                        column: x => x.FollowingMemberID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberFollowings_Members_MemberID",
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
                value: "$IOPSSWD$V1$10000$Shh2fqbP8+LjYoxB6xEPecchSvGru3BlIHtN1xOgUsJX1S/3tsb+UgAom8Hx23deXS2vC7Sfgzf1GgWVv4d2VwNol0/XfistcLkcslw05ShPde7DILSxW8CoGlgqMFhg");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFollowings_FollowDate",
                table: "MemberFollowings",
                column: "FollowDate");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFollowings_FollowingMemberID",
                table: "MemberFollowings",
                column: "FollowingMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFollowings_MemberID",
                table: "MemberFollowings",
                column: "MemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberFollowings");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "BirthDate",
                table: "Members",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "DATETIME");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$NNBIRS6vFuTDj0+LPwjxfUrepth9wjh/IRI38fpPd6dddKHm4K8jU8DU0ovzDel+F9r0E8f9l8z+xUcsPPNiWjKZsO1Ew6fk+qlkeNXvCL9pOTfKv/AcN589NjKPAg8N");
        }
    }
}
