using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pd.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoTabela1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavoriteCardId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoriteCardId",
                table: "Users",
                column: "FavoriteCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cards_FavoriteCardId",
                table: "Users",
                column: "FavoriteCardId",
                principalTable: "Cards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cards_FavoriteCardId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FavoriteCardId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FavoriteCardId",
                table: "Users");
        }
    }
}
