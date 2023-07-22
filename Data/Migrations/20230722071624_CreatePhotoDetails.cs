using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CreatePhotoDetails : Migration
    {
        private const string PHOTO_DETAILS_TABLE = "PhotoDetails";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: PHOTO_DETAILS_TABLE,
                columns: table => new
                {
                    Id = table
                        .Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoId = table.Column<int>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Id", x => x.Id);
                    table.UniqueConstraint("UK_PhotoId", x => x.PhotoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(PHOTO_DETAILS_TABLE);
        }
    }
}
