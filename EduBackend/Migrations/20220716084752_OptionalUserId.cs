using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EduBackend.Migrations
{
    public partial class OptionalUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AccountVerificationCodes_Id",
                table: "Users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AccountVerificationCodes_UserId",
                table: "AccountVerificationCodes");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "AccountVerificationCodes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_AccountVerificationCodes_UserId",
                table: "AccountVerificationCodes",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountVerificationCodes_Users_UserId",
                table: "AccountVerificationCodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountVerificationCodes_Users_UserId",
                table: "AccountVerificationCodes");

            migrationBuilder.DropIndex(
                name: "IX_AccountVerificationCodes_UserId",
                table: "AccountVerificationCodes");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "AccountVerificationCodes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AccountVerificationCodes_UserId",
                table: "AccountVerificationCodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AccountVerificationCodes_Id",
                table: "Users",
                column: "Id",
                principalTable: "AccountVerificationCodes",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
