using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    /// <inheritdoc />
    public partial class ImageMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DirectSale",
                table: "MemberImages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "MemberImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "MemberImages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "MemberImages",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PriceCurrency",
                table: "MemberImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SaleAmount",
                table: "MemberImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "MemberImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "ID", "Action", "CssClass", "MenuOrder", "Name", "ParentEntityID", "RequiredRole" },
                values: new object[] { 30, "actionGenerateBOPage", "fa-file-code", 30, "Generate BO Page", null, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$T+hL4A4cOEQlL8mDuSsL7ykYA0YbSTCbn1AUM19oOOz8GfssRkNFrjaANgiutBQIXdgRVse5rVN4uceambAd99nklvmMS/7iJCV9ry9/wiVj8OB55qyDGx6lOU5moQTT");

            migrationBuilder.CreateIndex(
                name: "IX_MemberImages_IsDraft",
                table: "MemberImages",
                column: "IsDraft");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MemberImages_IsDraft",
                table: "MemberImages");

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 30);

            migrationBuilder.DropColumn(
                name: "DirectSale",
                table: "MemberImages");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "MemberImages");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "MemberImages");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MemberImages");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "MemberImages");

            migrationBuilder.DropColumn(
                name: "SaleAmount",
                table: "MemberImages");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "MemberImages");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "Password",
                value: "$IOPSSWD$V1$10000$iF2eGK9pta12h/NXjo05ASI63DcTchHow0KFST2fw/hOsIYpylKOsBIgqvFLRX7ZEmSNfQg5Ymgc7SgQFHrbQQM1uhC4ymDRC0/kJJXY4MzrKHqLT4I9wSLh3e1WEaBk");
        }
    }
}
