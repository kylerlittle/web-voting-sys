using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace web_voting_sys.Migrations
{
    public partial class PollContext3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollChoice_PollQuestion_PollQuestionID",
                table: "PollChoice");

            migrationBuilder.DropForeignKey(
                name: "FK_PollQuestion_Poll_PollID",
                table: "PollQuestion");

            migrationBuilder.AlterColumn<int>(
                name: "PollID",
                table: "PollQuestion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PollQuestionID",
                table: "PollChoice",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PollChoice_PollQuestion_PollQuestionID",
                table: "PollChoice",
                column: "PollQuestionID",
                principalTable: "PollQuestion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PollQuestion_Poll_PollID",
                table: "PollQuestion",
                column: "PollID",
                principalTable: "Poll",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollChoice_PollQuestion_PollQuestionID",
                table: "PollChoice");

            migrationBuilder.DropForeignKey(
                name: "FK_PollQuestion_Poll_PollID",
                table: "PollQuestion");

            migrationBuilder.AlterColumn<int>(
                name: "PollID",
                table: "PollQuestion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PollQuestionID",
                table: "PollChoice",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PollChoice_PollQuestion_PollQuestionID",
                table: "PollChoice",
                column: "PollQuestionID",
                principalTable: "PollQuestion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PollQuestion_Poll_PollID",
                table: "PollQuestion",
                column: "PollID",
                principalTable: "Poll",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
