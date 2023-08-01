﻿// <auto-generated />
using System;
using BigBang.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BigBang.Migrations
{
    [DbContext(typeof(TravelDbContext))]
    partial class TravelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BigBang.Models.Bookings", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<int>("Adult")
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<int>("Child")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int?>("TourPackagePackageId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("TourPackagePackageId");

                    b.ToTable("bookings");
                });

            modelBuilder.Entity("BigBang.Models.Feedback", b =>
                {
                    b.Property<int>("FeedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FeedId");

                    b.HasIndex("UserId");

                    b.ToTable("feedback");
                });

            modelBuilder.Entity("BigBang.Models.Hotels", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelId"));

                    b.Property<string>("HotelImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int?>("tourpackagePackageId")
                        .HasColumnType("int");

                    b.HasKey("HotelId");

                    b.HasIndex("tourpackagePackageId");

                    b.ToTable("hotels");
                });

            modelBuilder.Entity("BigBang.Models.Imagetable", b =>
                {
                    b.Property<int>("Imgid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Imgid"));

                    b.Property<string>("ImgName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Imgid");

                    b.ToTable("imagetable");
                });

            modelBuilder.Entity("BigBang.Models.NearbySpots", b =>
                {
                    b.Property<int>("SpotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpotId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<string>("Spotsimg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("tourpackagePackageId")
                        .HasColumnType("int");

                    b.HasKey("SpotId");

                    b.HasIndex("tourpackagePackageId");

                    b.ToTable("nearbyspots");
                });

            modelBuilder.Entity("BigBang.Models.Restaurents", b =>
                {
                    b.Property<int>("RestaurentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestaurentId"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<string>("RestaurentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("tourpackagePackageId")
                        .HasColumnType("int");

                    b.HasKey("RestaurentId");

                    b.HasIndex("tourpackagePackageId");

                    b.ToTable("restaurents");
                });

            modelBuilder.Entity("BigBang.Models.TourPackage", b =>
                {
                    b.Property<int>("PackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageId"));

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackageImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceForAdult")
                        .HasColumnType("int");

                    b.Property<int>("PriceForChild")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PackageId");

                    b.HasIndex("UserId");

                    b.ToTable("tourpackage");
                });

            modelBuilder.Entity("BigBang.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Agency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("BigBang.Models.Bookings", b =>
                {
                    b.HasOne("BigBang.Models.TourPackage", "TourPackage")
                        .WithMany("bookings")
                        .HasForeignKey("TourPackagePackageId");

                    b.Navigation("TourPackage");
                });

            modelBuilder.Entity("BigBang.Models.Feedback", b =>
                {
                    b.HasOne("BigBang.Models.User", "user")
                        .WithMany("feedback")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("BigBang.Models.Hotels", b =>
                {
                    b.HasOne("BigBang.Models.TourPackage", "tourpackage")
                        .WithMany("hotels")
                        .HasForeignKey("tourpackagePackageId");

                    b.Navigation("tourpackage");
                });

            modelBuilder.Entity("BigBang.Models.NearbySpots", b =>
                {
                    b.HasOne("BigBang.Models.TourPackage", "tourpackage")
                        .WithMany("nearbyspots")
                        .HasForeignKey("tourpackagePackageId");

                    b.Navigation("tourpackage");
                });

            modelBuilder.Entity("BigBang.Models.Restaurents", b =>
                {
                    b.HasOne("BigBang.Models.TourPackage", "tourpackage")
                        .WithMany("restaurents")
                        .HasForeignKey("tourpackagePackageId");

                    b.Navigation("tourpackage");
                });

            modelBuilder.Entity("BigBang.Models.TourPackage", b =>
                {
                    b.HasOne("BigBang.Models.User", "user")
                        .WithMany("tourpackage")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("BigBang.Models.TourPackage", b =>
                {
                    b.Navigation("bookings");

                    b.Navigation("hotels");

                    b.Navigation("nearbyspots");

                    b.Navigation("restaurents");
                });

            modelBuilder.Entity("BigBang.Models.User", b =>
                {
                    b.Navigation("feedback");

                    b.Navigation("tourpackage");
                });
#pragma warning restore 612, 618
        }
    }
}
