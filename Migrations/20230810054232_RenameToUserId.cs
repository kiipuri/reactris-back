using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tetris_api.Migrations
{
    /// <inheritdoc />
    public partial class RenameToUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_scores_users_this_user_id",
                table: "scores");

            migrationBuilder.RenameColumn(
                name: "this_user_id",
                table: "scores",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_scores_this_user_id",
                table: "scores",
                newName: "ix_scores_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_scores_users_user_id",
                table: "scores",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_scores_users_user_id",
                table: "scores");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "scores",
                newName: "this_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_scores_user_id",
                table: "scores",
                newName: "ix_scores_this_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_scores_users_this_user_id",
                table: "scores",
                column: "this_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
