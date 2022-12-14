// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServiceLayer;

#nullable disable

namespace ServiceLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DomainLayer.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("employee_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("EmployeeCode")
                        .HasColumnType("text")
                        .HasColumnName("employee_code");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("boolean")
                        .HasColumnName("is_disabled");

                    b.Property<int>("PeopleId")
                        .HasColumnType("integer")
                        .HasColumnName("people_id");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer")
                        .HasColumnName("position_id");

                    b.Property<int>("Salary")
                        .HasColumnType("integer")
                        .HasColumnName("salary");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.HasKey("EmployeeId")
                        .HasName("pk_employees");

                    b.HasIndex("PeopleId")
                        .HasDatabaseName("ix_employees_people_id");

                    b.HasIndex("PositionId")
                        .HasDatabaseName("ix_employees_position_id");

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("DomainLayer.Models.EmployeeHistory", b =>
                {
                    b.Property<int>("EmployeeHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("employee_history_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmployeeHistoryId"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer")
                        .HasColumnName("employee_id");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer")
                        .HasColumnName("position_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.HasKey("EmployeeHistoryId")
                        .HasName("pk_employee_histories");

                    b.HasIndex("EmployeeId")
                        .HasDatabaseName("ix_employee_histories_employee_id");

                    b.HasIndex("PositionId")
                        .HasDatabaseName("ix_employee_histories_position_id");

                    b.ToTable("employee_histories", (string)null);
                });

            modelBuilder.Entity("DomainLayer.Models.People", b =>
                {
                    b.Property<int>("PeopleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("people_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PeopleId"));

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.HasKey("PeopleId")
                        .HasName("pk_people");

                    b.ToTable("people", (string)null);
                });

            modelBuilder.Entity("DomainLayer.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("position_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PositionId"));

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("PositionId")
                        .HasName("pk_positions");

                    b.ToTable("positions", (string)null);
                });

            modelBuilder.Entity("DomainLayer.Models.Employee", b =>
                {
                    b.HasOne("DomainLayer.Models.People", null)
                        .WithMany("Employees")
                        .HasForeignKey("PeopleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_employees_people_people_id");

                    b.HasOne("DomainLayer.Models.Position", null)
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_employees_positions_position_id");
                });

            modelBuilder.Entity("DomainLayer.Models.EmployeeHistory", b =>
                {
                    b.HasOne("DomainLayer.Models.Employee", null)
                        .WithMany("EmployeeHistories")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_employee_histories_employees_employee_id");

                    b.HasOne("DomainLayer.Models.Position", null)
                        .WithMany("EmployeeHistories")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_employee_histories_positions_position_id");
                });

            modelBuilder.Entity("DomainLayer.Models.Employee", b =>
                {
                    b.Navigation("EmployeeHistories");
                });

            modelBuilder.Entity("DomainLayer.Models.People", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("DomainLayer.Models.Position", b =>
                {
                    b.Navigation("EmployeeHistories");

                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
