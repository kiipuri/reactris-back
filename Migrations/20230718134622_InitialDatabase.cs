using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace tetris_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table =>
                    new
                    {
                        id = table.Column<int>(type: "integer", nullable: false)
                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                               NpgsqlValueGenerationStrategy
                                                   .IdentityAlwaysColumn),
                        user_id = table.Column<int>(type: "integer", nullable: false),
                        username = table.Column<string>(type: "text", nullable: true),
                        hashed_password =
                              table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table => { table.PrimaryKey("pk_users", x => x.id); });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "users");
        }
    }
}
