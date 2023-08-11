using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tetris_api.Migrations
{
    /// <inheritdoc />
    public partial class UserModelGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("user_id", "users");
            migrationBuilder.AddColumn<Guid>("user_id", "users", "uuid",
                                             defaultValue: Guid.NewGuid());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
