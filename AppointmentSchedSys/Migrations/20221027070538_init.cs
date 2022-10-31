using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentSchedSys.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    Key = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Slot = table.Column<int>(nullable: false),
                    Rescheduled_Date = table.Column<DateTime>(nullable: true),
                    Rescheduled_Slot = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Notification = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Slot_1 = table.Column<int>(nullable: true),
                    Slot_2 = table.Column<int>(nullable: true),
                    Slot_3 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_Slot_1",
                        column: x => x.Slot_1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_Slot_2",
                        column: x => x.Slot_2,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_Slot_3",
                        column: x => x.Slot_3,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DOB", "Email", "Key", "Name", "Password", "PasswordHash", "Phone", "Role", "Username" },
                values: new object[] { 1, new DateTime(2000, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@company.com", new byte[] { 124, 67, 3, 238, 165, 87, 51, 206, 97, 61, 61, 237, 236, 193, 174, 5, 128, 191, 153, 55, 254, 152, 237, 74, 129, 48, 4, 41, 136, 183, 185, 145, 219, 55, 105, 74, 131, 159, 159, 186, 3, 150, 44, 171, 10, 34, 132, 172, 73, 240, 166, 110, 217, 173, 157, 38, 134, 170, 166, 35, 207, 105, 200, 230, 148, 161, 62, 100, 48, 108, 204, 161, 21, 139, 112, 107, 183, 229, 196, 133, 56, 96, 125, 140, 101, 126, 231, 190, 126, 23, 22, 110, 21, 30, 148, 48, 28, 97, 49, 58, 224, 195, 239, 148, 82, 95, 174, 112, 54, 254, 94, 176, 128, 196, 143, 195, 37, 201, 89, 236, 47, 160, 140, 160, 195, 22, 243, 140 }, "Admin", "admin@pass", new byte[] { 0, 92, 167, 98, 111, 91, 176, 109, 228, 3, 237, 249, 243, 129, 3, 8, 79, 220, 166, 72, 246, 179, 215, 246, 108, 29, 70, 24, 110, 22, 195, 68, 11, 142, 228, 90, 132, 136, 177, 1, 193, 194, 229, 21, 194, 173, 40, 209, 19, 197, 80, 34, 137, 45, 108, 115, 60, 167, 2, 192, 161, 163, 51, 208 }, "9543457896", "Admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentEntries_UserId",
                table: "AppointmentEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Slot_1",
                table: "Bookings",
                column: "Slot_1");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Slot_2",
                table: "Bookings",
                column: "Slot_2");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Slot_3",
                table: "Bookings",
                column: "Slot_3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentEntries");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
