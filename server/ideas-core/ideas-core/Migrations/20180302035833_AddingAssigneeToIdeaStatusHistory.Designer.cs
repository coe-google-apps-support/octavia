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
    [Migration("20180302035833_AddingAssigneeToIdeaStatusHistory")]
    partial class AddingAssigneeToIdeaStatusHistory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.BranchInternal", b =>
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

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.DepartmentInternal", b =>
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

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.IdeaInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AssigneeId");

                    b.Property<string>("BusinessBenefites");

                    b.Property<string>("BusinessCaseUrl");

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

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url");

                    b.Property<int>("WordPressKey");

                    b.Property<string>("WorkItemId");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.IdeaStatusHistoryInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AssigneeId");

                    b.Property<long?>("InitiativeId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("StatusEntryDateUtc");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("InitiativeId");

                    b.ToTable("IdeaStatusHistories");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.PersonInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.StakeholderInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("IdeaInternalId");

                    b.Property<long>("PersonId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("IdeaInternalId");

                    b.HasIndex("PersonId");

                    b.ToTable("Stakeholders");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.StringTemplateInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<string>("Key");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("StringTemplates");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.TagInternal", b =>
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

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.DepartmentInternal", b =>
                {
                    b.HasOne("CoE.Ideas.Core.Internal.Initiatives.BranchInternal", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.IdeaInternal", b =>
                {
                    b.HasOne("CoE.Ideas.Core.Internal.Initiatives.PersonInternal", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("CoE.Ideas.Core.Internal.Initiatives.DepartmentInternal", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.IdeaStatusHistoryInternal", b =>
                {
                    b.HasOne("CoE.Ideas.Core.Internal.Initiatives.PersonInternal", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("CoE.Ideas.Core.Internal.Initiatives.IdeaInternal", "Initiative")
                        .WithMany()
                        .HasForeignKey("InitiativeId");
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.StakeholderInternal", b =>
                {
                    b.HasOne("CoE.Ideas.Core.Internal.Initiatives.IdeaInternal")
                        .WithMany("Stakeholders")
                        .HasForeignKey("IdeaInternalId");

                    b.HasOne("CoE.Ideas.Core.Internal.Initiatives.PersonInternal", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoE.Ideas.Core.Internal.Initiatives.TagInternal", b =>
                {
                    b.HasOne("CoE.Ideas.Core.Internal.Initiatives.IdeaInternal")
                        .WithMany("Tags")
                        .HasForeignKey("IdeaInternalId");
                });
#pragma warning restore 612, 618
        }
    }
}
