using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
	public partial class AddMessages : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Messages",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					CreateDate = table.Column<DateTimeOffset>(nullable: false),
					QuestionId = table.Column<Guid>(nullable: true),
					Text = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Messages", x => x.Id);
					table.ForeignKey(
						name: "FK_Messages_Messages_QuestionId",
						column: x => x.QuestionId,
						principalTable: "Messages",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Messages_QuestionId",
				table: "Messages",
				column: "QuestionId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Messages");

            migrationBuilder.DropTable(
               name: "Comments");
        }
	}
}