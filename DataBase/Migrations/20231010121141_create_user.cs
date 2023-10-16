using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class create_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IUser");
        }
    }
}
