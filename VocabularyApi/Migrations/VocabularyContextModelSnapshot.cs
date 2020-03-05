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
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VocabularyApi.Models.UserVocabulary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Translation");

                    b.Property<string>("UserId");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("UserVocabularies");
                });

            modelBuilder.Entity("VocabularyApi.Models.VocabularyWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("AudioRecord");

                    b.Property<int>("Language");

                    b.Property<int?>("ThumbnailId");

                    b.Property<string>("Trasncription");

                    b.Property<string>("Word");

                    b.Property<int?>("WordImageId");

                    b.HasKey("Id");

                    b.HasIndex("ThumbnailId");

                    b.HasIndex("WordImageId");

                    b.ToTable("VocabularyWords");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image");

                    b.Property<bool>("IsThumbnail");

                    b.HasKey("Id");

                    b.ToTable("WordImage");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("WordSets");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordSetItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Translation");

                    b.Property<string>("Word");

                    b.Property<int>("WordSetId");

                    b.HasKey("Id");

                    b.HasIndex("WordSetId");

                    b.ToTable("WordSetItem");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Language");

                    b.Property<string>("Translation");

                    b.Property<int>("WordId");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("WordTranslations");
                });

            modelBuilder.Entity("VocabularyApi.Models.VocabularyWord", b =>
                {
                    b.HasOne("VocabularyApi.Models.WordImage", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailId");

                    b.HasOne("VocabularyApi.Models.WordImage", "Image")
                        .WithMany()
                        .HasForeignKey("WordImageId");
                });

            modelBuilder.Entity("VocabularyApi.Models.WordSetItem", b =>
                {
                    b.HasOne("VocabularyApi.Models.WordSet", "WordSet")
                        .WithMany("WordSetItems")
                        .HasForeignKey("WordSetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VocabularyApi.Models.WordTranslation", b =>
                {
                    b.HasOne("VocabularyApi.Models.VocabularyWord", "Word")
                        .WithMany("WordTranslations")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
