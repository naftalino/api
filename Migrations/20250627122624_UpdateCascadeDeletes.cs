using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeDeletes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Genres_GenreId",
                table: "Series");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Subgenres_SubGenreId",
                table: "Series");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Genres_GenreId",
                table: "Series",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Subgenres_SubGenreId",
                table: "Series",
                column: "SubGenreId",
                principalTable: "Subgenres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Genres_GenreId",
                table: "Series");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Subgenres_SubGenreId",
                table: "Series");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Genres_GenreId",
                table: "Series",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Subgenres_SubGenreId",
                table: "Series",
                column: "SubGenreId",
                principalTable: "Subgenres",
                principalColumn: "Id");
        }
    }
}
