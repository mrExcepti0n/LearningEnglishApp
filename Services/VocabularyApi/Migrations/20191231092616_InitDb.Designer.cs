﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VocabularyApi.Infrastructure.DataAccess;

namespace VocabularyApi.Migrations
{
    [DbContext(typeof(VocabularyContext))]
    [Migration("20191231092616_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("UserId");

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

                    b.Property<byte[]>("Image");

                    b.Property<int>("Language");

                    b.Property<string>("Trasncription");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("VocabularyWords");
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

            modelBuilder.Entity("VocabularyApi.Models.WordTranslation", b =>
                {
                    b.HasOne("VocabularyApi.Models.VocabularyWord", "Word")
                        .WithMany()
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
