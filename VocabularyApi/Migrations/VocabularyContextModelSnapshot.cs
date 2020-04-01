﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VocabularyApi.Infrastructure.DataAccess;

namespace VocabularyApi.Migrations
{
    [DbContext(typeof(VocabularyContext))]
    partial class VocabularyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VocabularyApi.Models.TrainingStatistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsReverseTraining")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastRightAnswerDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastWrongAnswerDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RightAnswerCount")
                        .HasColumnType("int");

                    b.Property<int>("TrainingType")
                        .HasColumnType("int");

                    b.Property<int>("UserVocabularyWordId")
                        .HasColumnType("int");

                    b.Property<int>("WrongAnswerCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserVocabularyWordId");

                    b.ToTable("TrainingStatistic");
                });

            modelBuilder.Entity("VocabularyApi.Models.UserVocabulary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("WordSetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordSetId");

                    b.ToTable("UserVocabularies");
                });

            modelBuilder.Entity("VocabularyApi.Models.UserVocabularyWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Translation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserVocabularyId")
                        .HasColumnType("int");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserVocabularyId");

                    b.ToTable("UserVocabularyWords");
                });

            modelBuilder.Entity("VocabularyApi.Models.VocabularyWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("AudioRecord")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<int?>("ThumbnailId")
                        .HasColumnType("int");

                    b.Property<string>("Trasncription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("ThumbnailId");

                    b.ToTable("VocabularyWords");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsThumbnail")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("WordImage");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WordSets");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordSetItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Translation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WordSetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordSetId");

                    b.ToTable("WordSetItem");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Translation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("WordTranslations");
                });

            modelBuilder.Entity("VocabularyApi.Models.TrainingStatistic", b =>
                {
                    b.HasOne("VocabularyApi.Models.UserVocabularyWord", "UserVocabularyWord")
                        .WithMany("TrainingStatistics")
                        .HasForeignKey("UserVocabularyWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VocabularyApi.Models.UserVocabulary", b =>
                {
                    b.HasOne("VocabularyApi.Models.WordSet", "WordSet")
                        .WithMany()
                        .HasForeignKey("WordSetId");
                });

            modelBuilder.Entity("VocabularyApi.Models.UserVocabularyWord", b =>
                {
                    b.HasOne("VocabularyApi.Models.UserVocabulary", "UserVocabulary")
                        .WithMany("Words")
                        .HasForeignKey("UserVocabularyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VocabularyApi.Models.VocabularyWord", b =>
                {
                    b.HasOne("VocabularyApi.Models.WordImage", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("VocabularyApi.Models.WordImage", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailId");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordSetItem", b =>
                {
                    b.HasOne("VocabularyApi.Models.WordSet", "WordSet")
                        .WithMany("WordSetItems")
                        .HasForeignKey("WordSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VocabularyApi.Models.WordTranslation", b =>
                {
                    b.HasOne("VocabularyApi.Models.VocabularyWord", "Word")
                        .WithMany("WordTranslations")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
