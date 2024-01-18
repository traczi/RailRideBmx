using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class modifyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_ProductId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProductId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId1",
                table: "Comments",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_ProductId1",
                table: "Comments",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
