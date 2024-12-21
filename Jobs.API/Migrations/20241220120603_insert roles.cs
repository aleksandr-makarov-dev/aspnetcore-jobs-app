using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jobs.API.Migrations
{
    /// <inheritdoc />
    public partial class insertroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a6535dcc-c116-41ac-b54f-ad0eff1c1cab"), null, "Admin", "ADMIN" },
                    { new Guid("dc65c5a2-fc10-4efc-8cbd-9879405390d8"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6535dcc-c116-41ac-b54f-ad0eff1c1cab"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dc65c5a2-fc10-4efc-8cbd-9879405390d8"));
        }
    }
}
