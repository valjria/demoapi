﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using demoapi.Data;

#nullable disable

namespace demoapi.Migrations
{
    [DbContext(typeof(EducationDbContext))]
    partial class EducationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            CourseName = "Aviation Safety and Operations",
                            Description = "An introductory course on aviation safety and cockpit operations."
                        },
                        new
                        {
                            CourseId = 2,
                            CourseName = "Aerodynamics",
                            Description = "Basics of aerodynamics and flight principles."
                        });
                });

            modelBuilder.Entity("Grade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradeId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("GradeId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Grades");

                    b.HasData(
                        new
                        {
                            GradeId = 1,
                            CourseId = 1,
                            StudentId = 1,
                            Value = 85
                        },
                        new
                        {
                            GradeId = 2,
                            CourseId = 1,
                            StudentId = 2,
                            Value = 90
                        },
                        new
                        {
                            GradeId = 3,
                            CourseId = 2,
                            StudentId = 1,
                            Value = 88
                        },
                        new
                        {
                            GradeId = 4,
                            CourseId = 2,
                            StudentId = 2,
                            Value = 92
                        });
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            Name = "John Doe",
                            Role = "Kokpit"
                        },
                        new
                        {
                            StudentId = 2,
                            Name = "Jane Smith",
                            Role = "Kabin"
                        });
                });

            modelBuilder.Entity("Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TopicId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TopicId");

                    b.HasIndex("CourseId");

                    b.ToTable("Topics");

                    b.HasData(
                        new
                        {
                            TopicId = 1,
                            CourseId = 1,
                            TopicName = "Aviation Safety Basics"
                        },
                        new
                        {
                            TopicId = 2,
                            CourseId = 1,
                            TopicName = "Cockpit Procedures"
                        },
                        new
                        {
                            TopicId = 3,
                            CourseId = 1,
                            TopicName = "Emergency Protocols"
                        },
                        new
                        {
                            TopicId = 4,
                            CourseId = 2,
                            TopicName = "Lift and Drag"
                        },
                        new
                        {
                            TopicId = 5,
                            CourseId = 2,
                            TopicName = "Flight Maneuvers"
                        });
                });

            modelBuilder.Entity("Grade", b =>
                {
                    b.HasOne("Course", "Course")
                        .WithMany("Grades")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Topic", b =>
                {
                    b.HasOne("Course", "Course")
                        .WithMany("Topics")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Course", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("Topics");
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.Navigation("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}