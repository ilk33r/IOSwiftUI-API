﻿// <auto-generated />
using System;
using IOSwiftUI.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IOSwiftUI.Application.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.IOBackOfficeMessageEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("MessageCreateDate")
                        .HasColumnType("datetime");

                    b.Property<DateTimeOffset>("MessageEndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTimeOffset>("MessageStartDate")
                        .HasColumnType("datetime");

                    b.HasKey("ID");

                    b.HasIndex("MessageCreateDate", "MessageEndDate", "MessageStartDate");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.IOClientsEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClientDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("ClientId")
                        .HasMaxLength(48)
                        .HasColumnType("varchar(48)");

                    b.Property<string>("ClientSecret")
                        .HasMaxLength(48)
                        .HasColumnType("varchar(48)");

                    b.Property<int>("IsEnabled")
                        .HasColumnType("int");

                    b.Property<long>("MaxRequestCount")
                        .HasColumnType("bigint");

                    b.Property<long>("RequestCount")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.IOConfigurationEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ConfigIntValue")
                        .HasColumnType("int");

                    b.Property<string>("ConfigKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ConfigStringValue")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.HasIndex("ConfigKey")
                        .IsUnique();

                    b.ToTable("Configurations");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ConfigIntValue = 0,
                            ConfigKey = "IsMaintenanceModeOn"
                        },
                        new
                        {
                            ID = 2,
                            ConfigIntValue = 0,
                            ConfigKey = "ForgotPasswordEmailTitle",
                            ConfigStringValue = "IOBootstrapt Reset Password"
                        },
                        new
                        {
                            ID = 3,
                            ConfigIntValue = 0,
                            ConfigKey = "ForgotPasswordEmailHtmlBody",
                            ConfigStringValue = "<p>This email has been sent upon your 'Change My Password' request. If you don't have such request please ignore this email.\nPlease click to the link to  <a href=\"{0}\">Change your password.</a></p>\n<br />\n<p>Best Regards, IOBootstrapt</p>"
                        },
                        new
                        {
                            ID = 4,
                            ConfigIntValue = 0,
                            ConfigKey = "ForgotPasswordEmailTextBody",
                            ConfigStringValue = "This email has been sent upon your 'Change My Password' request. If you don't have such request please ignore this email.\nPlease click to the link to  {0}\n\nBest Regards, IOBootstrapt"
                        },
                        new
                        {
                            ID = 5,
                            ConfigIntValue = 0,
                            ConfigKey = "EMailFromName",
                            ConfigStringValue = "IOBootstrapt Support"
                        });
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.IOImagesEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("FileType")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<int?>("Scale")
                        .HasColumnType("int");

                    b.Property<int?>("Width")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.IOMenuEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CssClass")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("MenuOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("ParentEntityID")
                        .HasColumnType("int");

                    b.Property<int>("RequiredRole")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ParentEntityID", "MenuOrder", "RequiredRole");

                    b.ToTable("Menu");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Action = "actionClients",
                            CssClass = "fa-cloud",
                            MenuOrder = 1,
                            Name = "Clients",
                            RequiredRole = 1
                        },
                        new
                        {
                            ID = 2,
                            Action = "clientsList",
                            CssClass = "fa-circle-o",
                            MenuOrder = 2,
                            Name = "List Clients",
                            ParentEntityID = 1,
                            RequiredRole = 1
                        },
                        new
                        {
                            ID = 3,
                            Action = "clientsAdd",
                            CssClass = "fa-circle-o",
                            MenuOrder = 3,
                            Name = "Add Client",
                            ParentEntityID = 1,
                            RequiredRole = 1
                        },
                        new
                        {
                            ID = 6,
                            Action = "actionUsers",
                            CssClass = "fa-users",
                            MenuOrder = 6,
                            Name = "Users",
                            RequiredRole = 1
                        },
                        new
                        {
                            ID = 7,
                            Action = "usersList",
                            CssClass = "fa-circle-o",
                            MenuOrder = 7,
                            Name = "List Users",
                            ParentEntityID = 6,
                            RequiredRole = 1
                        },
                        new
                        {
                            ID = 8,
                            Action = "usersAdd",
                            CssClass = "fa-circle-o",
                            MenuOrder = 8,
                            Name = "Add User",
                            ParentEntityID = 6,
                            RequiredRole = 1
                        },
                        new
                        {
                            ID = 11,
                            Action = "actionConfiguration",
                            CssClass = "fa-wrench",
                            MenuOrder = 11,
                            Name = "Configurations",
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 12,
                            Action = "configurationsList",
                            CssClass = "fa-circle-o",
                            MenuOrder = 12,
                            Name = "Edit Configurations",
                            ParentEntityID = 11,
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 13,
                            Action = "configurationsAdd",
                            CssClass = "fa-circle-o",
                            MenuOrder = 13,
                            Name = "Add Configuration",
                            ParentEntityID = 11,
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 14,
                            Action = "resetCache",
                            CssClass = "fa-circle-o",
                            MenuOrder = 14,
                            Name = "Reset Cache",
                            ParentEntityID = 11,
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 15,
                            Action = "actionMenuEditor",
                            CssClass = "fa-list",
                            MenuOrder = 15,
                            Name = "Menu Editor",
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 16,
                            Action = "menuEditorList",
                            CssClass = "fa-circle-o",
                            MenuOrder = 16,
                            Name = "List Menu Items",
                            ParentEntityID = 15,
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 17,
                            Action = "menuEditorAdd",
                            CssClass = "fa-circle-o",
                            MenuOrder = 17,
                            Name = "Add Menu Item",
                            ParentEntityID = 15,
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 18,
                            Action = "actionMessages",
                            CssClass = "fa-envelope",
                            MenuOrder = 18,
                            Name = "Messages",
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 19,
                            Action = "messagesList",
                            CssClass = "fa-circle-o",
                            MenuOrder = 19,
                            Name = "List Messages",
                            ParentEntityID = 18,
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 20,
                            Action = "messagesAdd",
                            CssClass = "fa-circle-o",
                            MenuOrder = 20,
                            Name = "Add Message",
                            ParentEntityID = 18,
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 21,
                            Action = "actionPushNotification",
                            CssClass = "fa-comment-alt",
                            MenuOrder = 21,
                            Name = "Push Notifications",
                            RequiredRole = 2
                        },
                        new
                        {
                            ID = 22,
                            Action = "pushNotificationList",
                            CssClass = "fa-circle-o",
                            MenuOrder = 22,
                            Name = "List Messages",
                            ParentEntityID = 21,
                            RequiredRole = 2
                        },
                        new
                        {
                            ID = 23,
                            Action = "pushNotificationSend",
                            CssClass = "fa-circle-o",
                            MenuOrder = 23,
                            Name = "Send",
                            ParentEntityID = 21,
                            RequiredRole = 2
                        },
                        new
                        {
                            ID = 24,
                            Action = "actionResource",
                            CssClass = "fa-address-book",
                            MenuOrder = 24,
                            Name = "Resources",
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 25,
                            Action = "resourcesList",
                            CssClass = "fa-circle-o",
                            MenuOrder = 25,
                            Name = "Edit Resources",
                            ParentEntityID = 24,
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 26,
                            Action = "resourceAdd",
                            CssClass = "fa-circle-o",
                            MenuOrder = 26,
                            Name = "Add Resource",
                            ParentEntityID = 24,
                            RequiredRole = 0
                        },
                        new
                        {
                            ID = 27,
                            Action = "actionImages",
                            CssClass = "fa-image",
                            MenuOrder = 27,
                            Name = "Images",
                            RequiredRole = 1
                        },
                        new
                        {
                            ID = 28,
                            Action = "imagesEdit",
                            CssClass = "fa-circle-o",
                            MenuOrder = 28,
                            Name = "Edit Images",
                            ParentEntityID = 27,
                            RequiredRole = 1
                        },
                        new
                        {
                            ID = 29,
                            Action = "imageAdd",
                            CssClass = "fa-circle-o",
                            MenuOrder = 29,
                            Name = "Add Image",
                            ParentEntityID = 27,
                            RequiredRole = 1
                        });
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.IOUserEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("TokenDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("UserToken")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("ID");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Password = "$IOPSSWD$V1$10000$WitkW0dOGZqh3h9w+T/hpmGauOnpg3/MecP2JLC4cIa3Pm1B+8Fh+IPC9NNt+CrZ0ekPRjxXvpU6XPmty3tydFcOe9Qo4h/OblYPGRnl7g01LA4O1wIBIEGl+8J5k1Ep",
                            TokenDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            UserName = "root",
                            UserRole = 0,
                            UserToken = ""
                        });
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationDeliveredMessagesEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("PushNotificationID")
                        .HasColumnType("int");

                    b.Property<int?>("PushNotificationMessageID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PushNotificationID");

                    b.HasIndex("PushNotificationMessageID");

                    b.ToTable("PushNotificationDeliveredMessages");
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AppBuildNumber")
                        .HasColumnType("int");

                    b.Property<string>("AppBundleId")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("AppVersion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<int>("BadgeCount")
                        .HasColumnType("int");

                    b.Property<int?>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("DeviceId")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("DeviceName")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("DeviceToken")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("DeviceType")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("LastUpdateTime")
                        .HasColumnType("datetime");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.HasIndex("DeviceId", "DeviceType", "LastUpdateTime");

                    b.ToTable("PushNotifications");
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationMessageEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("DeviceType")
                        .HasColumnType("int");

                    b.Property<int>("IsCompleted")
                        .HasColumnType("int");

                    b.Property<string>("NotificationCategory")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("NotificationData")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTimeOffset>("NotificationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("NotificationMessage")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NotificationTitle")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<int?>("PushNotificationDeviceIDID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.HasIndex("PushNotificationDeviceIDID");

                    b.HasIndex("NotificationDate", "DeviceType", "IsCompleted");

                    b.ToTable("PushNotificationMessages");
                });

            modelBuilder.Entity("IOSwiftUI.DataAccess.Entities.ImagesEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext");

                    b.Property<int?>("ImagesID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreateDate");

                    b.HasIndex("ImagesID");

                    b.ToTable("MemberImages");
                });

            modelBuilder.Entity("IOSwiftUI.DataAccess.Entities.MemberEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("LocationLatitude")
                        .HasColumnType("double");

                    b.Property<double>("LocationLongitude")
                        .HasColumnType("double");

                    b.Property<string>("LocationName")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfilePictureFileName")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("RegisterDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTimeOffset>("TokenDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("UserStatus")
                        .HasColumnType("int");

                    b.Property<string>("UserToken")
                        .HasMaxLength(48)
                        .HasColumnType("varchar(48)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.HasIndex("Name", "Surname");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationDeliveredMessagesEntity", b =>
                {
                    b.HasOne("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationEntity", "PushNotification")
                        .WithMany("DeliveredMessages")
                        .HasForeignKey("PushNotificationID");

                    b.HasOne("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationMessageEntity", "PushNotificationMessage")
                        .WithMany()
                        .HasForeignKey("PushNotificationMessageID");

                    b.Navigation("PushNotification");

                    b.Navigation("PushNotificationMessage");
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationEntity", b =>
                {
                    b.HasOne("IOBootstrap.NET.MW.DataAccess.Entities.IOClientsEntity", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationMessageEntity", b =>
                {
                    b.HasOne("IOBootstrap.NET.MW.DataAccess.Entities.IOClientsEntity", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID");

                    b.HasOne("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationEntity", "PushNotificationDeviceID")
                        .WithMany()
                        .HasForeignKey("PushNotificationDeviceIDID");

                    b.Navigation("Client");

                    b.Navigation("PushNotificationDeviceID");
                });

            modelBuilder.Entity("IOSwiftUI.DataAccess.Entities.ImagesEntity", b =>
                {
                    b.HasOne("IOSwiftUI.DataAccess.Entities.MemberEntity", "Member")
                        .WithMany("Images")
                        .HasForeignKey("ImagesID");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("IOBootstrap.NET.MW.DataAccess.Entities.PushNotificationEntity", b =>
                {
                    b.Navigation("DeliveredMessages");
                });

            modelBuilder.Entity("IOSwiftUI.DataAccess.Entities.MemberEntity", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
