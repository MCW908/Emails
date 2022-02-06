using Microsoft.EntityFrameworkCore.Migrations;

namespace Emails.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sender = table.Column<string>(nullable: false),
                    Recipient = table.Column<string>(nullable: false),
                    CC = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailId);
                });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "EmailId", "Body", "CC", "Recipient", "Sender", "Subject" },
                values: new object[] { 1, "Sample Text Sample Text", null, "JoeBloggs@gmail.com", "TestMail123@gmail.com", "Seed Email" });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "EmailId", "Body", "CC", "Recipient", "Sender", "Subject" },
                values: new object[] { 2, "Seed Data Seed Data Seed Data", null, "AABB@gmail.com", "JoeBloggs@gmail.com", "Testing" });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "EmailId", "Body", "CC", "Recipient", "Sender", "Subject" },
                values: new object[] { 3, "CC Email Test CC Email Test", "JoeBloggs@gmail.com", "TestMail123@gmail.com", "AABB@gmail.com", "CC Email" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");
        }
    }
}
