using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServiceLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    people_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_people", x => x.people_id);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    position_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_positions", x => x.position_id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    people_id = table.Column<int>(type: "integer", nullable: false),
                    position_id = table.Column<int>(type: "integer", nullable: false),
                    salary = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    employee_code = table.Column<string>(type: "text", nullable: true),
                    is_disabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.employee_id);
                    table.ForeignKey(
                        name: "fk_employees_people_people_id",
                        column: x => x.people_id,
                        principalTable: "people",
                        principalColumn: "people_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employees_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "position_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_histories",
                columns: table => new
                {
                    employee_history_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    position_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_histories", x => x.employee_history_id);
                    table.ForeignKey(
                        name: "fk_employee_histories_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employee_histories_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "position_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employee_histories_employee_id",
                table: "employee_histories",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_employee_histories_position_id",
                table: "employee_histories",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_people_id",
                table: "employees",
                column: "people_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_position_id",
                table: "employees",
                column: "position_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee_histories");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "people");

            migrationBuilder.DropTable(
                name: "positions");
        }
    }
}
