using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogEngineApi.Migrations
{
    public partial class CreatePostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    PostID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    content = table.Column<string>(maxLength: 256, nullable: false),
                    author = table.Column<string>(maxLength: 50, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    approval = table.Column<DateTime>(type: "timestamp", nullable: true),
                    publish = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("postID", x => x.PostID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post");
        }
    }
}
