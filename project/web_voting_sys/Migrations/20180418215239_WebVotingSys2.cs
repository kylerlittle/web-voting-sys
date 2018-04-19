using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace web_voting_sys.Migrations
{
    public partial class WebVotingSys2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poll",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NumberOfQuestions = table.Column<int>(nullable: false),
                    PollCreator = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poll", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    PollID = table.Column<int>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Poll_PollID",
                        column: x => x.PollID,
                        principalTable: "Poll",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PollQuestion",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PollID = table.Column<int>(nullable: true),
                    Question = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollQuestion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PollQuestion_Poll_PollID",
                        column: x => x.PollID,
                        principalTable: "Poll",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PollChoice",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Choice = table.Column<string>(nullable: true),
                    PollQuestionID = table.Column<int>(nullable: true),
                    VoteTally = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollChoice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PollChoice_PollQuestion_PollQuestionID",
                        column: x => x.PollQuestionID,
                        principalTable: "PollQuestion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_PollID",
                table: "ApplicationUser",
                column: "PollID");

            migrationBuilder.CreateIndex(
                name: "IX_PollChoice_PollQuestionID",
                table: "PollChoice",
                column: "PollQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_PollQuestion_PollID",
                table: "PollQuestion",
                column: "PollID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "PollChoice");

            migrationBuilder.DropTable(
                name: "PollQuestion");

            migrationBuilder.DropTable(
                name: "Poll");
        }
    }
}
