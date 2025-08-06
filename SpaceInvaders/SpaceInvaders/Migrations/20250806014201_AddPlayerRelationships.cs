using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceInvaders.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Weapons_WeaponId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Projectiles_Players_PlayerId",
                table: "Projectiles");

            migrationBuilder.DropIndex(
                name: "IX_Players_WeaponId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Weapons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Projectiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_PlayerId",
                table: "Weapons",
                column: "PlayerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projectiles_Players_PlayerId",
                table: "Projectiles",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Players_PlayerId",
                table: "Weapons",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projectiles_Players_PlayerId",
                table: "Projectiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Players_PlayerId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_PlayerId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Weapons");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Projectiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "WeaponId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_WeaponId",
                table: "Players",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Weapons_WeaponId",
                table: "Players",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projectiles_Players_PlayerId",
                table: "Projectiles",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
