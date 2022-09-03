using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<string>(type: "varchar(48)", maxLength: 48, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientSecret = table.Column<string>(type: "varchar(48)", maxLength: 48, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnabled = table.Column<int>(type: "int", nullable: false),
                    RequestCount = table.Column<long>(type: "bigint", nullable: false),
                    MaxRequestCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConfigKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConfigIntValue = table.Column<int>(type: "int", nullable: true),
                    ConfigStringValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileType = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Scale = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CssClass = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MenuOrder = table.Column<int>(type: "int", nullable: false),
                    RequiredRole = table.Column<int>(type: "int", nullable: false),
                    ParentEntityID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MessageCreateDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    MessageStartDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    MessageEndDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    UserToken = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TokenDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PushNotifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientID = table.Column<int>(type: "int", nullable: true),
                    AppBuildNumber = table.Column<int>(type: "int", nullable: false),
                    AppBundleId = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppVersion = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BadgeCount = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceToken = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceType = table.Column<int>(type: "int", nullable: false),
                    LastUpdateTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PushNotifications_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PushNotificationMessages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientID = table.Column<int>(type: "int", nullable: true),
                    DeviceType = table.Column<int>(type: "int", nullable: false),
                    NotificationCategory = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NotificationData = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NotificationDate = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    NotificationMessage = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NotificationTitle = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCompleted = table.Column<int>(type: "int", nullable: false),
                    PushNotificationDeviceIDID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PushNotificationMessages_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PushNotificationMessages_PushNotifications_PushNotificationD~",
                        column: x => x.PushNotificationDeviceIDID,
                        principalTable: "PushNotifications",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PushNotificationDeliveredMessages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PushNotificationID = table.Column<int>(type: "int", nullable: true),
                    PushNotificationMessageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationDeliveredMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PushNotificationDeliveredMessages_PushNotificationMessages_P~",
                        column: x => x.PushNotificationMessageID,
                        principalTable: "PushNotificationMessages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PushNotificationDeliveredMessages_PushNotifications_PushNoti~",
                        column: x => x.PushNotificationID,
                        principalTable: "PushNotifications",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "ID", "ConfigIntValue", "ConfigKey", "ConfigStringValue" },
                values: new object[,]
                {
                    { 1, 0, "IsMaintenanceModeOn", null },
                    { 2, 0, "ForgotPasswordEmailTitle", "IOBootstrapt Reset Password" },
                    { 3, 0, "ForgotPasswordEmailHtmlBody", "<p>This email has been sent upon your 'Change My Password' request. If you don't have such request please ignore this email.\nPlease click to the link to  <a href=\"{0}\">Change your password.</a></p>\n<br />\n<p>Best Regards, IOBootstrapt</p>" },
                    { 4, 0, "ForgotPasswordEmailTextBody", "This email has been sent upon your 'Change My Password' request. If you don't have such request please ignore this email.\nPlease click to the link to  {0}\n\nBest Regards, IOBootstrapt" },
                    { 5, 0, "EMailFromName", "IOBootstrapt Support" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "ID", "Action", "CssClass", "MenuOrder", "Name", "ParentEntityID", "RequiredRole" },
                values: new object[,]
                {
                    { 1, "actionClients", "fa-cloud", 1, "Clients", null, 1 },
                    { 2, "clientsList", "fa-circle-o", 2, "List Clients", 1, 1 },
                    { 3, "clientsAdd", "fa-circle-o", 3, "Add Client", 1, 1 },
                    { 6, "actionUsers", "fa-users", 6, "Users", null, 1 },
                    { 7, "usersList", "fa-circle-o", 7, "List Users", 6, 1 },
                    { 8, "usersAdd", "fa-circle-o", 8, "Add User", 6, 1 },
                    { 11, "actionConfiguration", "fa-wrench", 11, "Configurations", null, 0 },
                    { 12, "configurationsList", "fa-circle-o", 12, "Edit Configurations", 11, 0 },
                    { 13, "configurationsAdd", "fa-circle-o", 13, "Add Configuration", 11, 0 },
                    { 14, "resetCache", "fa-circle-o", 14, "Reset Cache", 11, 0 },
                    { 15, "actionMenuEditor", "fa-list", 15, "Menu Editor", null, 0 },
                    { 16, "menuEditorList", "fa-circle-o", 16, "List Menu Items", 15, 0 },
                    { 17, "menuEditorAdd", "fa-circle-o", 17, "Add Menu Item", 15, 0 },
                    { 18, "actionMessages", "fa-envelope", 18, "Messages", null, 0 },
                    { 19, "messagesList", "fa-circle-o", 19, "List Messages", 18, 0 },
                    { 20, "messagesAdd", "fa-circle-o", 20, "Add Message", 18, 0 },
                    { 21, "actionPushNotification", "fa-comment-alt", 21, "Push Notifications", null, 2 },
                    { 22, "pushNotificationList", "fa-circle-o", 22, "List Messages", 21, 2 },
                    { 23, "pushNotificationSend", "fa-circle-o", 23, "Send", 21, 2 },
                    { 24, "actionResource", "fa-address-book", 24, "Resources", null, 0 },
                    { 25, "resourcesList", "fa-circle-o", 25, "Edit Resources", 24, 0 },
                    { 26, "resourceAdd", "fa-circle-o", 26, "Add Resource", 24, 0 },
                    { 27, "actionImages", "fa-image", 27, "Images", null, 1 },
                    { 28, "imagesEdit", "fa-circle-o", 28, "Edit Images", 27, 1 },
                    { 29, "imageAdd", "fa-circle-o", 29, "Add Image", 27, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Password", "TokenDate", "UserName", "UserRole", "UserToken" },
                values: new object[] { 1, "$IOPSSWD$V1$10000$ZgNuJbQUP1x45jSv12s+Z2jMz3WMMo7cibs8kaLi01TC60uUxa3OMSGIxTWrVw+m0tlxGoAMslGZxh7dS4DMuVbPzI+ljOzXJ+zUj/wntrmbTqo4NSiDjCxdZCFgrQPH", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "root", 0, "" });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientId",
                table: "Clients",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ConfigKey",
                table: "Configurations",
                column: "ConfigKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentEntityID_MenuOrder_RequiredRole",
                table: "Menu",
                columns: new[] { "ParentEntityID", "MenuOrder", "RequiredRole" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageCreateDate_MessageEndDate_MessageStartDate",
                table: "Messages",
                columns: new[] { "MessageCreateDate", "MessageEndDate", "MessageStartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationDeliveredMessages_PushNotificationID",
                table: "PushNotificationDeliveredMessages",
                column: "PushNotificationID");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationDeliveredMessages_PushNotificationMessageID",
                table: "PushNotificationDeliveredMessages",
                column: "PushNotificationMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationMessages_ClientID",
                table: "PushNotificationMessages",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationMessages_NotificationDate_DeviceType_IsCompl~",
                table: "PushNotificationMessages",
                columns: new[] { "NotificationDate", "DeviceType", "IsCompleted" });

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationMessages_PushNotificationDeviceIDID",
                table: "PushNotificationMessages",
                column: "PushNotificationDeviceIDID");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotifications_ClientID",
                table: "PushNotifications",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotifications_DeviceId_DeviceType_LastUpdateTime",
                table: "PushNotifications",
                columns: new[] { "DeviceId", "DeviceType", "LastUpdateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PushNotificationDeliveredMessages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PushNotificationMessages");

            migrationBuilder.DropTable(
                name: "PushNotifications");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
