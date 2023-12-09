﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Infrastructure.Persistence;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(F1DbContext))]
    [Migration("20230801202308_addEventModel")]
    partial class addEventModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Repositories.ConsumedEventForDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecieveDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.HasIndex("TypeId");

                    b.ToTable("ConsumedEvent");
                });

            modelBuilder.Entity("Domain.DomainEntities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Driver");
                });

            modelBuilder.Entity("Domain.DomainEntities.DriverRaceParticipation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<byte?>("Position")
                        .HasColumnType("tinyint");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<byte?>("ScoreForRace")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("ScoreInSeasonAfterRace")
                        .HasColumnType("tinyint");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("RaceId");

                    b.HasIndex("TeamId");

                    b.ToTable("DriverRaceParticipation");
                });

            modelBuilder.Entity("Domain.DomainEntities.DriverSeasonParticipation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<byte?>("Position")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("Score")
                        .HasColumnType("tinyint");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("SeasonId");

                    b.ToTable("DriverSeasonParticipation");
                });

            modelBuilder.Entity("Domain.DomainEntities.DriverTeamContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("TeamId");

                    b.ToTable("DriverTeamContract");
                });

            modelBuilder.Entity("Domain.DomainEntities.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("NumberInSeason")
                        .HasColumnType("tinyint");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Race");
                });

            modelBuilder.Entity("Domain.DomainEntities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<short>("Year")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("Domain.DomainEntities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Domain.DomainEntities.TeamRaceParticipation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<byte?>("ScoreForRace")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("ScoreInSeasonAfterRace")
                        .HasColumnType("tinyint");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamRaceParticipation");
                });

            modelBuilder.Entity("Domain.DomainEntities.TeamSeasonParticipation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<byte?>("Position")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("Score")
                        .HasColumnType("tinyint");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("SeasonId");

                    b.ToTable("TeamSeasonParticipation");
                });

            modelBuilder.Entity("Domain.DomainEntities.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Track");
                });

            modelBuilder.Entity("Domain.DomainEvents.ConsumedEventState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConsumedEventState");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "Recieved",
                            Name = "Событие получено"
                        },
                        new
                        {
                            Id = 2,
                            Code = "Processed",
                            Name = "Событие обработано"
                        },
                        new
                        {
                            Id = 3,
                            Code = "ToRepeatProcess",
                            Name = "Событие должно быть обработано повторно"
                        });
                });

            modelBuilder.Entity("Domain.DomainEvents.ConsumedEventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConsumedEventType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "SeasonCalendarPublished",
                            Name = "Опубликован календарь сезона"
                        },
                        new
                        {
                            Id = 2,
                            Code = "SeasonParticipantsPublished",
                            Name = "Опубликован состав команд-участников сезона"
                        },
                        new
                        {
                            Id = 3,
                            Code = "DriverContractSigned",
                            Name = "Заключен контракт с гонщиком"
                        },
                        new
                        {
                            Id = 4,
                            Code = "RaceFinished",
                            Name = "Гонка завершилась"
                        });
                });

            modelBuilder.Entity("Domain.DomainEvents.PublishedEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("PublishedEvent");
                });

            modelBuilder.Entity("Domain.DomainEvents.PublishedEventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PublishedEventType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "AfterRaceDriverStandings",
                            Name = "Позиции в чемпионате мира по итогам гонки"
                        },
                        new
                        {
                            Id = 2,
                            Code = "AfterRaceCunstructorStandings",
                            Name = "Позиции в кубке конструкторов по итогам гонки"
                        },
                        new
                        {
                            Id = 3,
                            Code = "DriverChampionDetermined",
                            Name = "Определен чемпион мира"
                        },
                        new
                        {
                            Id = 4,
                            Code = "ConstructorChampionDetermined",
                            Name = "Определен обладатель кубка конструкторов"
                        });
                });

            modelBuilder.Entity("Infrastructure.Repositories.ConsumedEventForDb", b =>
                {
                    b.HasOne("Domain.DomainEvents.ConsumedEventState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.DomainEvents.ConsumedEventType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.DomainEntities.DriverRaceParticipation", b =>
                {
                    b.HasOne("Domain.DomainEntities.Driver", "Driver")
                        .WithMany("DriverRaceParticipations")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.DomainEntities.Race", "Race")
                        .WithMany("DriverRaceParticipations")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.DomainEntities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Race");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Domain.DomainEntities.DriverSeasonParticipation", b =>
                {
                    b.HasOne("Domain.DomainEntities.Driver", "Driver")
                        .WithMany("DriverSeasonParticipations")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.DomainEntities.Season", "Season")
                        .WithMany("DriverSeasonParticipations")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("Domain.DomainEntities.DriverTeamContract", b =>
                {
                    b.HasOne("Domain.DomainEntities.Driver", "Driver")
                        .WithMany("DriverTeamContracts")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.DomainEntities.Team", "Team")
                        .WithMany("DriverTeamContracts")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Domain.DomainEntities.Race", b =>
                {
                    b.HasOne("Domain.DomainEntities.Track", "Track")
                        .WithMany("Races")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.DomainEntities.Season", "Season")
                        .WithMany("Races")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Season");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Domain.DomainEntities.TeamRaceParticipation", b =>
                {
                    b.HasOne("Domain.DomainEntities.Race", "Race")
                        .WithMany("TeamRaceParticipations")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.DomainEntities.Team", "Team")
                        .WithMany("TeamRaceParticipations")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Race");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Domain.DomainEntities.TeamSeasonParticipation", b =>
                {
                    b.HasOne("Domain.DomainEntities.Team", "Team")
                        .WithMany("TeamSeasonParticipations")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.DomainEntities.Season", null)
                        .WithMany("TeamSeasonParticipations")
                        .HasForeignKey("SeasonId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Domain.DomainEvents.PublishedEvent", b =>
                {
                    b.HasOne("Domain.DomainEvents.PublishedEventType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.DomainEntities.Driver", b =>
                {
                    b.Navigation("DriverRaceParticipations");

                    b.Navigation("DriverSeasonParticipations");

                    b.Navigation("DriverTeamContracts");
                });

            modelBuilder.Entity("Domain.DomainEntities.Race", b =>
                {
                    b.Navigation("DriverRaceParticipations");

                    b.Navigation("TeamRaceParticipations");
                });

            modelBuilder.Entity("Domain.DomainEntities.Season", b =>
                {
                    b.Navigation("DriverSeasonParticipations");

                    b.Navigation("Races");

                    b.Navigation("TeamSeasonParticipations");
                });

            modelBuilder.Entity("Domain.DomainEntities.Team", b =>
                {
                    b.Navigation("DriverTeamContracts");

                    b.Navigation("TeamRaceParticipations");

                    b.Navigation("TeamSeasonParticipations");
                });

            modelBuilder.Entity("Domain.DomainEntities.Track", b =>
                {
                    b.Navigation("Races");
                });
#pragma warning restore 612, 618
        }
    }
}