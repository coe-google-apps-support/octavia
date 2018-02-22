﻿// <auto-generated />
using CoE.Ideas.Core.Internal;
using CoE.Ideas.Core.Internal.Initiatives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CoE.Ideas.Core.Migrations
{
    [DbContext(typeof(IdeaContext))]
    [Migration("20180119160524_MoreSecondaryFields")]
    partial class MoreSecondaryFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("CoE.Ideas.Core.Internal.BranchInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.DepartmentInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BranchId");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.IdeaInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BusinessBenefites");

                    b.Property<string>("BusinessSponsorEmail");

                    b.Property<string>("BusinessSponsorName");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<long?>("DepartmentId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime?>("ExpectedTargetDate");

                    b.Property<bool>("HasBudget");

                    b.Property<bool>("HasBusinessSponsor");

                    b.Property<string>("OneCityAlignment");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url");

                    b.Property<int>("WordPressKey");

                    b.Property<string>("WorkItemId");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.StakeholderInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<long?>("IdeaInternalId");

                    b.Property<int>("Type");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdeaInternalId");

                    b.ToTable("Stakeholders");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.TagInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<long?>("IdeaInternalId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdeaInternalId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.DepartmentInternal", b =>
                {
                    b.HasOne("CoE.Ideas.Core.Internal.BranchInternal", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.IdeaInternal", b =>
                {
                    b.HasOne("CoE.Ideas.Core.Internal.DepartmentInternal", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.StakeholderInternal", b =>
                {
                    b.HasOne("CoE.Ideas.Core.Internal.IdeaInternal")
                        .WithMany("Stakeholders")
                        .HasForeignKey("IdeaInternalId");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.TagInternal", b =>
                {
                    b.HasOne("CoE.Ideas.Core.Internal.IdeaInternal")
                        .WithMany("Tags")
                        .HasForeignKey("IdeaInternalId");
                });
#pragma warning restore 612, 618
        }
    }
}