using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wpapers.Data.Migrations
{
    /// <inheritdoc />
    public partial class WallpaperLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Wallpapers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WallpaperLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HasLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallpaperLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WallpaperLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WallpaperLikes_Wallpapers_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Wallpapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WallpaperLikes_PhotoId",
                table: "WallpaperLikes",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_WallpaperLikes_UserId",
                table: "WallpaperLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WallpaperLikes");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Wallpapers");
        }
    }
}
