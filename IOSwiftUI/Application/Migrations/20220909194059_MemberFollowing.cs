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
            migrationBuilder.CreateTable(
                name: "MemberFollowings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FollowDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberFollowings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MemberFollowings_Members_MemberID",
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
                value: "$IOPSSWD$V1$10000$PfNx26e7O+9S260DpUIh6TqZPbJYS51BBa6qIJg45z0OCjdBJPVoVrBQy0SXV79nj1MbP+EAvqqHCaUym96yc1jHhZLt4eRNMycuhROzxf4jeID6MgHeipLxOa+xv6kp");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFollowings_FollowDate",
                table: "MemberFollowings",
                column: "FollowDate");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFollowings_MemberID",
                table: "MemberFollowings",
                column: "MemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberFollowings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$NNBIRS6vFuTDj0+LPwjxfUrepth9wjh/IRI38fpPd6dddKHm4K8jU8DU0ovzDel+F9r0E8f9l8z+xUcsPPNiWjKZsO1Ew6fk+qlkeNXvCL9pOTfKv/AcN589NjKPAg8N");
        }
    }
}
