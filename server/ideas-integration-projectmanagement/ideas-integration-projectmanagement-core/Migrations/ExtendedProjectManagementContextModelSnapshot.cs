﻿// <auto-generated />
using CoE.Ideas.ProjectManagement.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace CoE.Ideas.ProjectManagement.Core.Migrations
{
    [DbContext(typeof(ExtendedProjectManagementContext))]
    partial class ExtendedProjectManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubIssueEventInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<long?>("AssigneeId");

                    b.Property<long?>("AssignerId");

                    b.Property<long?>("IssueId");

                    b.Property<long?>("RepositoryId");

                    b.Property<long?>("SenderId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("AssignerId");

                    b.HasIndex("IssueId");

                    b.HasIndex("RepositoryId");

                    b.HasIndex("SenderId");

                    b.ToTable("GitHub_Issue_Events");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubLabelInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternateKey");

                    b.Property<string>("Color");

                    b.Property<long?>("GitHubIssueInternalId");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("GitHubIssueInternalId");

                    b.ToTable("GitHub_Labels");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubRepositoryInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternateKey");

                    b.Property<string>("ArchiveUrl");

                    b.Property<string>("AssigneesUrl");

                    b.Property<string>("BlobsUrl");

                    b.Property<string>("BranchesUrl");

                    b.Property<string>("CloneUrl");

                    b.Property<string>("CollaboratorsUrl");

                    b.Property<string>("CommentsUrl");

                    b.Property<string>("CommitsUrl");

                    b.Property<string>("CompareUrl");

                    b.Property<string>("ContentsUrl");

                    b.Property<string>("ContributorsUrl");

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("DefaultBranch");

                    b.Property<string>("Description");

                    b.Property<string>("DownloadsUrl");

                    b.Property<string>("EventsUrl");

                    b.Property<int>("Forks");

                    b.Property<int>("ForksCount");

                    b.Property<string>("ForksUrl");

                    b.Property<string>("FullName");

                    b.Property<string>("GitCommitsUrl");

                    b.Property<string>("GitRefsUrl");

                    b.Property<string>("GitTagsUrl");

                    b.Property<string>("GitUrl");

                    b.Property<bool>("HasDownloads");

                    b.Property<bool>("HasIssues");

                    b.Property<bool>("HasPages");

                    b.Property<bool>("HasWiki");

                    b.Property<string>("HomePage");

                    b.Property<string>("HooksUrl");

                    b.Property<string>("HtmlUrl");

                    b.Property<bool>("IsFork");

                    b.Property<bool>("IsPrivate");

                    b.Property<string>("IssueCommentUrl");

                    b.Property<string>("IssueEventsUrl");

                    b.Property<string>("IssuesUrl");

                    b.Property<string>("KeysUrl");

                    b.Property<string>("LabelsUrl");

                    b.Property<string>("Language");

                    b.Property<string>("LanguagesUrl");

                    b.Property<string>("MergesUrl");

                    b.Property<string>("MilstonesUrl");

                    b.Property<string>("MirrorUrl");

                    b.Property<string>("Name");

                    b.Property<string>("NotificationsUrl");

                    b.Property<int>("OpenIssues");

                    b.Property<int>("OpenIssuesCount");

                    b.Property<long?>("OwnerId");

                    b.Property<string>("PullsUrl");

                    b.Property<DateTimeOffset>("PushedAt");

                    b.Property<string>("ReleasesUrl");

                    b.Property<long>("Size");

                    b.Property<string>("SshUrl");

                    b.Property<int>("StargazersCount");

                    b.Property<string>("StargazersUrl");

                    b.Property<string>("StatusesUrl");

                    b.Property<string>("SubscribersUrl");

                    b.Property<string>("SubscriptionUrl");

                    b.Property<string>("SvcUrl");

                    b.Property<string>("TagsUrl");

                    b.Property<string>("TeamsUrl");

                    b.Property<string>("TreesUrl");

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.Property<string>("Url");

                    b.Property<int>("Watchers");

                    b.Property<int>("WatchersCount");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("GitHub_Repositories");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubUserInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternateKey");

                    b.Property<string>("AvatarUrl");

                    b.Property<string>("EventsUrl");

                    b.Property<string>("FollowersUrl");

                    b.Property<string>("FollowingUrl");

                    b.Property<string>("GistsUrl");

                    b.Property<long?>("GitHubIssueInternalId");

                    b.Property<string>("GravatarId");

                    b.Property<string>("HtmlUrl");

                    b.Property<bool>("IsSiteAdmin");

                    b.Property<string>("Login");

                    b.Property<string>("OrganizationsUrl");

                    b.Property<string>("ReceivedEventsUrl");

                    b.Property<string>("ReposUrl");

                    b.Property<string>("StarredUrl");

                    b.Property<string>("SubscriptionsUrl");

                    b.Property<string>("Type");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("GitHubIssueInternalId");

                    b.ToTable("GitHub_Users");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.IssueInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternateKey");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("IssueStatus");

                    b.Property<int>("ProjectManagementSystem");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Issues");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IssueInternal");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.IssueStatusChangeInternal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("ChangeDate");

                    b.Property<long?>("IssueId");

                    b.Property<int>("NewStatus");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("Issue_Status_Changes");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubIssueInternal", b =>
                {
                    b.HasBaseType("CoE.Ideas.ProjectManagement.Core.Internal.IssueInternal");

                    b.Property<long?>("AssigneeId");

                    b.Property<string>("Body");

                    b.Property<DateTimeOffset?>("ClosedAt");

                    b.Property<string>("Comments");

                    b.Property<string>("CommentsUrl");

                    b.Property<string>("EventsUrl");

                    b.Property<string>("HtmlUrl");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("LabelsUrl");

                    b.Property<string>("Milestone");

                    b.Property<int>("Number");

                    b.Property<string>("State");

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.Property<long?>("UserId");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("UserId");

                    b.ToTable("GitHub_Issues");

                    b.HasDiscriminator().HasValue("GitHubIssueInternal");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubIssueEventInternal", b =>
                {
                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubUserInternal", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubUserInternal", "Assigner")
                        .WithMany()
                        .HasForeignKey("AssignerId");

                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubIssueInternal", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId");

                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubRepositoryInternal", "Repository")
                        .WithMany()
                        .HasForeignKey("RepositoryId");

                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubUserInternal", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubLabelInternal", b =>
                {
                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubIssueInternal")
                        .WithMany("Labels")
                        .HasForeignKey("GitHubIssueInternalId");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubRepositoryInternal", b =>
                {
                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubUserInternal", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubUserInternal", b =>
                {
                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubIssueInternal")
                        .WithMany("Asseignees")
                        .HasForeignKey("GitHubIssueInternalId");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.IssueStatusChangeInternal", b =>
                {
                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.IssueInternal", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId");
                });

            modelBuilder.Entity("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubIssueInternal", b =>
                {
                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubUserInternal", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("CoE.Ideas.ProjectManagement.Core.Internal.GitHub.GitHubUserInternal", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
