﻿// <auto-generated />
using System;
using CG.Recruitment.Sweepstake.DataStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CG.Recruitment.Sweepstake.DataStore.Migrations
{
    [DbContext(typeof(SweepstakeContext))]
    [Migration("20180913154228_CompetitionAddedAtCompetitor")]
    partial class CompetitionAddedAtCompetitor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CG.Recruitment.Sweepstake.DataStore.Competition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<decimal>("EntryFee");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Competition","Sweepstake");
                });

            modelBuilder.Entity("CG.Recruitment.Sweepstake.DataStore.Competitor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CompetitionId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.ToTable("Competitor","Sweepstake");
                });

            modelBuilder.Entity("CG.Recruitment.Sweepstake.DataStore.Gambler", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Gambler","Sweepstake");
                });

            modelBuilder.Entity("CG.Recruitment.Sweepstake.DataStore.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<Guid>("FromGamblerId");

                    b.Property<string>("Subject");

                    b.Property<Guid>("ToGamblerId");

                    b.HasKey("Id");

                    b.HasIndex("FromGamblerId")
                        .IsUnique();

                    b.HasIndex("ToGamblerId")
                        .IsUnique();

                    b.ToTable("Message","Sweepstake");
                });

            modelBuilder.Entity("CG.Recruitment.Sweepstake.DataStore.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BoughtAt");

                    b.Property<Guid>("CompetitionId");

                    b.Property<Guid>("CompetitorId");

                    b.Property<Guid>("GamblerId");

                    b.Property<bool>("IsPaymentReceived");

                    b.Property<DateTime?>("PaymentReceivedAt");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("CompetitorId");

                    b.HasIndex("GamblerId");

                    b.ToTable("Ticket","Sweepstake");
                });

            modelBuilder.Entity("CG.Recruitment.Sweepstake.DataStore.Competitor", b =>
                {
                    b.HasOne("CG.Recruitment.Sweepstake.DataStore.Competition", "Competition")
                        .WithMany("Competitors")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CG.Recruitment.Sweepstake.DataStore.Message", b =>
                {
                    b.HasOne("CG.Recruitment.Sweepstake.DataStore.Gambler", "FromGambler")
                        .WithOne()
                        .HasForeignKey("CG.Recruitment.Sweepstake.DataStore.Message", "FromGamblerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CG.Recruitment.Sweepstake.DataStore.Gambler", "ToGambler")
                        .WithOne()
                        .HasForeignKey("CG.Recruitment.Sweepstake.DataStore.Message", "ToGamblerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CG.Recruitment.Sweepstake.DataStore.Ticket", b =>
                {
                    b.HasOne("CG.Recruitment.Sweepstake.DataStore.Competition", "Competition")
                        .WithMany("Tickets")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CG.Recruitment.Sweepstake.DataStore.Competitor", "Competitor")
                        .WithMany("Tickets")
                        .HasForeignKey("CompetitorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CG.Recruitment.Sweepstake.DataStore.Gambler", "Gambler")
                        .WithMany("Tickets")
                        .HasForeignKey("GamblerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
