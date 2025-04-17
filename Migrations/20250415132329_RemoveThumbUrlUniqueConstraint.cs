using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gacha.Migrations
{
    /// <inheritdoc />
    public partial class RemoveThumbUrlUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cards_ThumbUrl",
                table: "Cards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cards_ThumbUrl",
                table: "Cards",
                column: "ThumbUrl",
                unique: true);
        }
    }
}
