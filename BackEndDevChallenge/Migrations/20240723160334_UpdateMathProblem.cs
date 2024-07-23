using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndDevChallenge.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMathProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Result",
                table: "MathProblems",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "MathProblems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ErrorType",
                table: "MathProblems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "MathProblems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "MathProblems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "MathProblems");

            migrationBuilder.DropColumn(
                name: "ErrorType",
                table: "MathProblems");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "MathProblems");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "MathProblems");

            migrationBuilder.AlterColumn<int>(
                name: "Result",
                table: "MathProblems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
