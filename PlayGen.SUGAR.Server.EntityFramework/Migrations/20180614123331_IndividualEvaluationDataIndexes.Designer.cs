﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using PlayGen.SUGAR.Common;
using PlayGen.SUGAR.Common.Authorization;
using PlayGen.SUGAR.Server.EntityFramework;
using System;

namespace PlayGen.SUGAR.Server.EntityFramework.Migrations
{
    [DbContext(typeof(SUGARContext))]
    [Migration("20180614123331_IndividualEvaluationDataIndexes")]
    partial class IndividualEvaluationDataIndexes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountSourceId");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AccountSourceId");

                    b.HasIndex("UserId");

                    b.HasIndex("Name", "AccountSourceId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.AccountSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiSecret");

                    b.Property<bool>("AutoRegister");

                    b.Property<string>("Description");

                    b.Property<bool>("RequiresPassword");

                    b.Property<string>("Token");

                    b.Property<string>("UsernamePattern");

                    b.HasKey("Id");

                    b.ToTable("AccountSources");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(1023);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Actors");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Actor");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.ActorClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<int>("ClaimId");

                    b.Property<int>("EntityId");

                    b.HasKey("Id");

                    b.HasIndex("ClaimId");

                    b.HasIndex("ActorId", "EntityId", "ClaimId")
                        .IsUnique();

                    b.ToTable("ActorClaims");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.ActorData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<int>("EvaluationDataType");

                    b.Property<int>("GameId");

                    b.Property<string>("Key");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("Key", "GameId", "ActorId", "EvaluationDataType")
                        .IsUnique();

                    b.ToTable("ActorData");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.ActorRelationship", b =>
                {
                    b.Property<int>("RequestorId");

                    b.Property<int>("AcceptorId");

                    b.HasKey("RequestorId", "AcceptorId");

                    b.HasIndex("AcceptorId");

                    b.ToTable("Relationships");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.ActorRelationshipRequest", b =>
                {
                    b.Property<int>("RequestorId");

                    b.Property<int>("AcceptorId");

                    b.HasKey("RequestorId", "AcceptorId");

                    b.HasIndex("AcceptorId");

                    b.ToTable("RelationshipRequests");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.ActorRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<int>("EntityId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("ActorId", "EntityId", "RoleId")
                        .IsUnique();

                    b.ToTable("ActorRoles");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClaimScope");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorType");

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("GameId");

                    b.Property<string>("Name");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.HasIndex("Token", "GameId", "ActorType")
                        .IsUnique();

                    b.ToTable("Evaluations");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Evaluation");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.EvaluationCriteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComparisonType");

                    b.Property<int>("CriteriaQueryType");

                    b.Property<int>("EvaluationDataCategory");

                    b.Property<string>("EvaluationDataKey");

                    b.Property<int>("EvaluationDataType");

                    b.Property<int>("EvaluationId");

                    b.Property<int>("Scope");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationId");

                    b.ToTable("EvaluationCriterias");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.EvaluationData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<int>("Category");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<int>("EvaluationDataType");

                    b.Property<int>("GameId");

                    b.Property<string>("Key");

                    b.Property<int?>("MatchId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("Category");

                    b.HasIndex("Key");

                    b.HasIndex("MatchId");

                    b.ToTable("EvaluationData");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Leaderboard", b =>
                {
                    b.Property<string>("Token");

                    b.Property<int>("GameId");

                    b.Property<int>("ActorType");

                    b.Property<int>("CriteriaScope");

                    b.Property<int>("EvaluationDataCategory");

                    b.Property<string>("EvaluationDataKey");

                    b.Property<int>("EvaluationDataType");

                    b.Property<int>("LeaderboardType");

                    b.Property<string>("Name");

                    b.HasKey("Token", "GameId");

                    b.ToTable("Leaderboards");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatorId");

                    b.Property<DateTime?>("Ended");

                    b.Property<int>("GameId");

                    b.Property<DateTime?>("Started");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("GameId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Reward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EvaluationDataCategory");

                    b.Property<string>("EvaluationDataKey");

                    b.Property<int>("EvaluationDataType");

                    b.Property<int>("EvaluationId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationId");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClaimScope");

                    b.Property<bool>("Default");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.RoleClaim", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("ClaimId");

                    b.HasKey("RoleId", "ClaimId");

                    b.HasIndex("ClaimId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.SentEvaluationNotification", b =>
                {
                    b.Property<int>("GameId");

                    b.Property<int>("ActorId");

                    b.Property<int>("EvaluationId");

                    b.Property<float>("Progress");

                    b.HasKey("GameId", "ActorId", "EvaluationId");

                    b.ToTable("SentEvaluationNotifications");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Group", b =>
                {
                    b.HasBaseType("PlayGen.SUGAR.Server.Model.Actor");


                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Groups");

                    b.HasDiscriminator().HasValue("Group");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.User", b =>
                {
                    b.HasBaseType("PlayGen.SUGAR.Server.Model.Actor");


                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Achievement", b =>
                {
                    b.HasBaseType("PlayGen.SUGAR.Server.Model.Evaluation");


                    b.ToTable("Achievements");

                    b.HasDiscriminator().HasValue("Achievement");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Skill", b =>
                {
                    b.HasBaseType("PlayGen.SUGAR.Server.Model.Evaluation");


                    b.ToTable("Skills");

                    b.HasDiscriminator().HasValue("Skill");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Account", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.AccountSource", "AccountSource")
                        .WithMany()
                        .HasForeignKey("AccountSourceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlayGen.SUGAR.Server.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.ActorClaim", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlayGen.SUGAR.Server.Model.Claim", "Claim")
                        .WithMany()
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.ActorRelationship", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.Actor", "Acceptor")
                        .WithMany("Acceptors")
                        .HasForeignKey("AcceptorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlayGen.SUGAR.Server.Model.Actor", "Requestor")
                        .WithMany("Requestors")
                        .HasForeignKey("RequestorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.ActorRelationshipRequest", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.Actor", "Acceptor")
                        .WithMany("RequestRequestors")
                        .HasForeignKey("AcceptorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlayGen.SUGAR.Server.Model.Actor", "Requestor")
                        .WithMany("RequestAcceptors")
                        .HasForeignKey("RequestorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.ActorRole", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlayGen.SUGAR.Server.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.EvaluationCriteria", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.Evaluation", "Evaluation")
                        .WithMany("EvaluationCriterias")
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.EvaluationData", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.Match", "Match")
                        .WithMany("Data")
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Match", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlayGen.SUGAR.Server.Model.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.Reward", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.Evaluation", "Evaluation")
                        .WithMany("Rewards")
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayGen.SUGAR.Server.Model.RoleClaim", b =>
                {
                    b.HasOne("PlayGen.SUGAR.Server.Model.Claim", "Claim")
                        .WithMany("RoleClaims")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PlayGen.SUGAR.Server.Model.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
