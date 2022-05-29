﻿// <auto-generated />
using System;
using DistEduDatabases2.Relational.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DistEduDatabases2.Relational.Migrations
{
    [DbContext(typeof(ServerContext))]
    partial class ServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("CVLanguage", b =>
                {
                    b.Property<Guid>("CvsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LanguagesId")
                        .HasColumnType("TEXT");

                    b.HasKey("CvsId", "LanguagesId");

                    b.HasIndex("LanguagesId");

                    b.ToTable("CVLanguage");
                });

            modelBuilder.Entity("CVPassion", b =>
                {
                    b.Property<Guid>("CvsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PassionsId")
                        .HasColumnType("TEXT");

                    b.HasKey("CvsId", "PassionsId");

                    b.HasIndex("PassionsId");

                    b.ToTable("CVPassion");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.CV", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PersonalInformationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInformationId");

                    b.HasIndex("UserId");

                    b.ToTable("CV");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.Education", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CVId")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("FinishTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CVId");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.Experience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CVId")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("FinishDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MajorProjects")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CVId");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.Passion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Passion");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.PersonalInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Birthday")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CivilStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Religion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PersonalInformation");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CVId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CVId");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CVLanguage", b =>
                {
                    b.HasOne("DistEduDatabases2.Common.Entities.CV", null)
                        .WithMany()
                        .HasForeignKey("CvsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DistEduDatabases2.Common.Entities.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CVPassion", b =>
                {
                    b.HasOne("DistEduDatabases2.Common.Entities.CV", null)
                        .WithMany()
                        .HasForeignKey("CvsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DistEduDatabases2.Common.Entities.Passion", null)
                        .WithMany()
                        .HasForeignKey("PassionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.CV", b =>
                {
                    b.HasOne("DistEduDatabases2.Common.Entities.PersonalInformation", "PersonalInformation")
                        .WithMany()
                        .HasForeignKey("PersonalInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DistEduDatabases2.Common.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalInformation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.Education", b =>
                {
                    b.HasOne("DistEduDatabases2.Common.Entities.CV", null)
                        .WithMany("Educations")
                        .HasForeignKey("CVId");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.Experience", b =>
                {
                    b.HasOne("DistEduDatabases2.Common.Entities.CV", null)
                        .WithMany("Experiences")
                        .HasForeignKey("CVId");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.PersonalInformation", b =>
                {
                    b.HasOne("DistEduDatabases2.Common.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.Skill", b =>
                {
                    b.HasOne("DistEduDatabases2.Common.Entities.CV", null)
                        .WithMany("Skills")
                        .HasForeignKey("CVId");
                });

            modelBuilder.Entity("DistEduDatabases2.Common.Entities.CV", b =>
                {
                    b.Navigation("Educations");

                    b.Navigation("Experiences");

                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}