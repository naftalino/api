using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pd.Migrations
{
    /// <inheritdoc />
    public partial class AddGenreSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Series");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Series",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubGenreId",
                table: "Series",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subgenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subgenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subgenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Series_GenreId",
                table: "Series",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_SubGenreId",
                table: "Series",
                column: "SubGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Subgenres_GenreId",
                table: "Subgenres",
                column: "GenreId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Genres_GenreId",
                table: "Series");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Subgenres_SubGenreId",
                table: "Series");

            migrationBuilder.DropTable(
                name: "Subgenres");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Series_GenreId",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_SubGenreId",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "SubGenreId",
                table: "Series");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Series",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
