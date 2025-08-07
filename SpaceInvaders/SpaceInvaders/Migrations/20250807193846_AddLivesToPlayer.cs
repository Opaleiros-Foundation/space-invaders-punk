using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceInvaders.Migrations
{
    /// <inheritdoc />
    public partial class AddLivesToPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lives",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lives",
                table: "Players");
        }
    }
}
