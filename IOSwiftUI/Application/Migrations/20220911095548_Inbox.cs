using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    public partial class Inbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Members",
                type: "varchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "Members",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeviceManifacturer",
                table: "Members",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeviceModel",
                table: "Members",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OneTimeCode",
                table: "Members",
                type: "varchar(6)",
                maxLength: 6,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DirectMessages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InboxID = table.Column<int>(type: "int", nullable: false),
                    FromMemberID = table.Column<int>(type: "int", nullable: true),
                    ToMemberID = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MessageDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectMessages_Members_FromMemberID",
                        column: x => x.FromMemberID,
                        principalTable: "Members",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DirectMessages_Members_ToMemberID",
                        column: x => x.ToMemberID,
                        principalTable: "Members",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inbox",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FromMemberID = table.Column<int>(type: "int", nullable: true),
                    ToMemberID = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    UnreadMessageCount = table.Column<int>(type: "int", nullable: false),
                    LastMessageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inbox_DirectMessages_LastMessageID",
                        column: x => x.LastMessageID,
                        principalTable: "DirectMessages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Inbox_Members_FromMemberID",
                        column: x => x.FromMemberID,
                        principalTable: "Members",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Inbox_Members_ToMemberID",
                        column: x => x.ToMemberID,
                        principalTable: "Members",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$0c7NyCRjCViMZqrnHJi+zZGqTbYxsNxR4I/eT9G+xgxxVYB9CI66AGMYMFsGyij/N1HOBwzUiDqI00MD/JmcX+9tH6xcTWXiYU6dLexnFRmqt6wXIs2QSGwe+sAWFQ95");

            migrationBuilder.CreateIndex(
                name: "IX_Members_PhoneNumber",
                table: "Members",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_FromMemberID",
                table: "DirectMessages",
                column: "FromMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_InboxID",
                table: "DirectMessages",
                column: "InboxID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_MessageDate",
                table: "DirectMessages",
                column: "MessageDate");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_ToMemberID",
                table: "DirectMessages",
                column: "ToMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_FromMemberID",
                table: "Inbox",
                column: "FromMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_LastMessageID",
                table: "Inbox",
                column: "LastMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_ToMemberID",
                table: "Inbox",
                column: "ToMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_UpdateDate",
                table: "Inbox",
                column: "UpdateDate");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_Inbox_InboxID",
                table: "DirectMessages",
                column: "InboxID",
                principalTable: "Inbox",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_Inbox_InboxID",
                table: "DirectMessages");

            migrationBuilder.DropTable(
                name: "Inbox");

            migrationBuilder.DropTable(
                name: "DirectMessages");

            migrationBuilder.DropIndex(
                name: "IX_Members_PhoneNumber",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DeviceManifacturer",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DeviceModel",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "OneTimeCode",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Members",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldMaxLength: 16,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$Shh2fqbP8+LjYoxB6xEPecchSvGru3BlIHtN1xOgUsJX1S/3tsb+UgAom8Hx23deXS2vC7Sfgzf1GgWVv4d2VwNol0/XfistcLkcslw05ShPde7DILSxW8CoGlgqMFhg");
        }
    }
}
