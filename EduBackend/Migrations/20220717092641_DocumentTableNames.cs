using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduBackend.Migrations
{
    public partial class DocumentTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentParagraph_Document_DocumentId",
                table: "DocumentParagraph");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentParagraph",
                table: "DocumentParagraph");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "DocumentParagraph",
                newName: "DocumentParagraphs");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentParagraph_DocumentId",
                table: "DocumentParagraphs",
                newName: "IX_DocumentParagraphs_DocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentParagraphs",
                table: "DocumentParagraphs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentParagraphs_Documents_DocumentId",
                table: "DocumentParagraphs",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentParagraphs_Documents_DocumentId",
                table: "DocumentParagraphs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentParagraphs",
                table: "DocumentParagraphs");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Document");

            migrationBuilder.RenameTable(
                name: "DocumentParagraphs",
                newName: "DocumentParagraph");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentParagraphs_DocumentId",
                table: "DocumentParagraph",
                newName: "IX_DocumentParagraph_DocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentParagraph",
                table: "DocumentParagraph",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentParagraph_Document_DocumentId",
                table: "DocumentParagraph",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
