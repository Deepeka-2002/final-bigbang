using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBang.Migrations
{
    /// <inheritdoc />
    public partial class tourism : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "imagegallery",
                columns: table => new
                {
                    Image = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagegallery", x => x.Image);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Agency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "feedback",
                columns: table => new
                {
                    FeedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedback", x => x.FeedId);
                    table.ForeignKey(
                        name: "FK_feedback_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tourpackage",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceForAdult = table.Column<int>(type: "int", nullable: false),
                    PriceForChild = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourpackage", x => x.PackageId);
                    table.ForeignKey(
                        name: "FK_tourpackage_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adult = table.Column<int>(type: "int", nullable: false),
                    Child = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    TourPackagePackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_bookings_tourpackage_TourPackagePackageId",
                        column: x => x.TourPackagePackageId,
                        principalTable: "tourpackage",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateTable(
                name: "hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    tourpackagePackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotels", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_hotels_tourpackage_tourpackagePackageId",
                        column: x => x.tourpackagePackageId,
                        principalTable: "tourpackage",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateTable(
                name: "nearbyspots",
                columns: table => new
                {
                    SpotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Spotsimg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    tourpackagePackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nearbyspots", x => x.SpotId);
                    table.ForeignKey(
                        name: "FK_nearbyspots_tourpackage_tourpackagePackageId",
                        column: x => x.tourpackagePackageId,
                        principalTable: "tourpackage",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateTable(
                name: "restaurents",
                columns: table => new
                {
                    RestaurentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    tourpackagePackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurents", x => x.RestaurentId);
                    table.ForeignKey(
                        name: "FK_restaurents_tourpackage_tourpackagePackageId",
                        column: x => x.tourpackagePackageId,
                        principalTable: "tourpackage",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_TourPackagePackageId",
                table: "bookings",
                column: "TourPackagePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_UserId",
                table: "feedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_hotels_tourpackagePackageId",
                table: "hotels",
                column: "tourpackagePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_nearbyspots_tourpackagePackageId",
                table: "nearbyspots",
                column: "tourpackagePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_restaurents_tourpackagePackageId",
                table: "restaurents",
                column: "tourpackagePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_tourpackage_UserId",
                table: "tourpackage",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "feedback");

            migrationBuilder.DropTable(
                name: "hotels");

            migrationBuilder.DropTable(
                name: "imagegallery");

            migrationBuilder.DropTable(
                name: "nearbyspots");

            migrationBuilder.DropTable(
                name: "restaurents");

            migrationBuilder.DropTable(
                name: "tourpackage");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
