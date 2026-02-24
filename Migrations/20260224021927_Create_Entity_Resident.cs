using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portly.Migrations
{
    /// <inheritdoc />
    public partial class Create_Entity_Resident : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "resident_id",
                table: "visitors",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "residents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    document = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    apartment = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    block = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_residents", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_visitors_resident_id",
                table: "visitors",
                column: "resident_id");

            migrationBuilder.CreateIndex(
                name: "ix_residents_document",
                table: "residents",
                column: "document",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_residents_email",
                table: "residents",
                column: "email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_visitors_residents_resident_id",
                table: "visitors",
                column: "resident_id",
                principalTable: "residents",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_visitors_residents_resident_id",
                table: "visitors");

            migrationBuilder.DropTable(
                name: "residents");

            migrationBuilder.DropIndex(
                name: "ix_visitors_resident_id",
                table: "visitors");

            migrationBuilder.DropColumn(
                name: "resident_id",
                table: "visitors");
        }
    }
}
