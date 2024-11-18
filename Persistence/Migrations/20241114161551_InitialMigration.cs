using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "game_results",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                results = table.Column<string>(type: "text", nullable: false),
                player = table.Column<int>(type: "integer", nullable: false),
                computer = table.Column<int>(type: "integer", nullable: false),
                created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_game_results", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "game_results");
    }
}
