using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Brorep.Persistence.Migrations
{
    public partial class SubmissionFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Reps_Submissions_SubmissionId", "Reps");

            migrationBuilder.DropIndex("IX_Reps_SubmissionId", "Reps");

            migrationBuilder.DropColumn("SubmissionId", "Reps");

            migrationBuilder.AddColumn<Guid>(
                name: "SubmissionId",
                table: "Reps",
                nullable: false);

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropColumn("SubmissionId", "Submissions");

            migrationBuilder.AddColumn<Guid>(
                name: "SubmissionId",
                table: "Submissions",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reps_Submissions_SubmissionId",
                table: "Reps",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "SubmissionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.CreateIndex(
                name: "IX_Reps_SubmissionId",
                table: "Reps",
                column: "SubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Reps_Submissions_SubmissionId", "Reps");

            migrationBuilder.DropIndex("IX_Reps_SubmissionId", "Reps");

            migrationBuilder.DropColumn("SubmissionId", "Reps");

            migrationBuilder.AddColumn<Guid>(
                name: "SubmissionId",
                table: "Reps",
                nullable: false);

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropColumn("SubmissionId", "Submissions");

            migrationBuilder.AddColumn<int>(
                name: "SubmissionId",
                table: "Submissions",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reps_Submissions_SubmissionId",
                table: "Reps",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "SubmissionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.CreateIndex(
                name: "IX_Reps_SubmissionId",
                table: "Reps",
                column: "SubmissionId");
        }
    }
}
