﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using phone_shop_server.Database;

#nullable disable

namespace phone_shop_server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230729153859_v4_add_payment_receipt")]
    partial class v4_add_payment_receipt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DetailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeletId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("HomeletId");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsBlock")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("PhoneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.HasIndex("UserId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.District", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProvinceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("District");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Homelet", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DistrictId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Homelet");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentOnlineReceipt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Order");

                    b.HasCheckConstraint("CK_Order_PaymentMethod", "PaymentStatus = 'UNPAID' OR PaymentStatus='PAID' OR PaymentStatus='CONFIRMING'");

                    b.HasCheckConstraint("CK_Order_PaymentStatus", "PaymentMethod = 'COD' OR PaymentMethod='ONLINE'");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PhoneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PhoneId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.OrderStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("OrderId", "StatusId")
                        .IsUnique();

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BehindCamera")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FrontCamera")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ImportPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherBenefit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("RAM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ROM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ScreenResolution")
                        .HasColumnType("float");

                    b.Property<double>("ScreenSize")
                        .HasColumnType("float");

                    b.Property<string>("ScreenTouch")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SoldPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Phone");

                    b.HasCheckConstraint("CK_Phone_Price_ImportSold", "ImportPrice <= SoldPrice");

                    b.HasCheckConstraint("CK_Phone_Quantity", "Quantity >= 0");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.PhoneImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PhoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.ToTable("PhoneImage");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Promote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Promote");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.PromoteDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Percent")
                        .HasColumnType("float");

                    b.Property<Guid>("PhoneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PromoteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.HasIndex("PromoteId", "PhoneId")
                        .IsUnique();

                    b.ToTable("PromoteDetail");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Province", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Province");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StatusType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeletId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HomeletId");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.TicketDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PhoneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("WarehouseTicketId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.HasIndex("WarehouseTicketId");

                    b.ToTable("TicketDetail");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.WarehouseTicket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("UserId");

                    b.ToTable("WarehouseTicket");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("phone_shop_server.Database.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Address", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Homelet", "Homelet")
                        .WithMany("Address")
                        .HasForeignKey("HomeletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Homelet_Address");

                    b.HasOne("phone_shop_server.Database.Entity.AppUser", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Address");

                    b.Navigation("Homelet");

                    b.Navigation("User");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Cart", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Phone", "Phone")
                        .WithMany("Carts")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Phone_Carts");

                    b.HasOne("phone_shop_server.Database.Entity.AppUser", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Cart");

                    b.Navigation("Phone");

                    b.Navigation("User");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.District", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Province", "Province")
                        .WithMany("District")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Province_District");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Homelet", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.District", "District")
                        .WithMany("Homelet")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_District_Homelet");

                    b.Navigation("District");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Order", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Address", "Address")
                        .WithMany("Orders")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Address_Order");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.OrderDetail", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Order", "Order")
                        .WithMany("OrderDetail")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Order_OrderDetail");

                    b.HasOne("phone_shop_server.Database.Entity.Phone", "Phone")
                        .WithMany("OrderDetails")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Phone_OrderDetails");

                    b.Navigation("Order");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.OrderStatus", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Order", "Order")
                        .WithMany("OrderStatus")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Order_OrderStatus");

                    b.HasOne("phone_shop_server.Database.Entity.Status", "Status")
                        .WithMany("OrderStatus")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Status_OrderStatus");

                    b.Navigation("Order");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Phone", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Brand", "Brand")
                        .WithMany("Phones")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Brand_Phone");

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.PhoneImage", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Phone", "Phone")
                        .WithMany("PhoneImages")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Phone_PhoneImages");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.PromoteDetail", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Phone", "Phone")
                        .WithMany("PromoteDetails")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Phone_PromoteDetail");

                    b.HasOne("phone_shop_server.Database.Entity.Promote", "Promote")
                        .WithMany("PromoteDetail")
                        .HasForeignKey("PromoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Promote_PromoteDetail");

                    b.Navigation("Phone");

                    b.Navigation("Promote");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Supplier", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Homelet", "Homelet")
                        .WithMany("Supplier")
                        .HasForeignKey("HomeletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Homelet_Supplier");

                    b.Navigation("Homelet");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.TicketDetail", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Phone", "Phone")
                        .WithMany("TicketDetails")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Phone_TicketDetails");

                    b.HasOne("phone_shop_server.Database.Entity.WarehouseTicket", "WarehouseTicket")
                        .WithMany("TicketDetails")
                        .HasForeignKey("WarehouseTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_WarehouseTicket_TicketDetail");

                    b.Navigation("Phone");

                    b.Navigation("WarehouseTicket");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.WarehouseTicket", b =>
                {
                    b.HasOne("phone_shop_server.Database.Entity.Supplier", "Supplier")
                        .WithMany("WarehouseTickets")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Supplier_WarehouseTicket");

                    b.HasOne("phone_shop_server.Database.Entity.AppUser", "User")
                        .WithMany("WarehouseTicket")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_WarehouseTicket");

                    b.Navigation("Supplier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Address", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.AppUser", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Carts");

                    b.Navigation("WarehouseTicket");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Brand", b =>
                {
                    b.Navigation("Phones");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.District", b =>
                {
                    b.Navigation("Homelet");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Homelet", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Order", b =>
                {
                    b.Navigation("OrderDetail");

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Phone", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("OrderDetails");

                    b.Navigation("PhoneImages");

                    b.Navigation("PromoteDetails");

                    b.Navigation("TicketDetails");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Promote", b =>
                {
                    b.Navigation("PromoteDetail");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Province", b =>
                {
                    b.Navigation("District");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Status", b =>
                {
                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.Supplier", b =>
                {
                    b.Navigation("WarehouseTickets");
                });

            modelBuilder.Entity("phone_shop_server.Database.Entity.WarehouseTicket", b =>
                {
                    b.Navigation("TicketDetails");
                });
#pragma warning restore 612, 618
        }
    }
}