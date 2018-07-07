﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Syzoj.Api.Data;

namespace Syzoj.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("Syzoj.Api.Models.DiscussionEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorEmail");

                    b.Property<string>("Content");

                    b.Property<bool>("ShowInBoard")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("AuthorEmail");

                    b.ToTable("Discussions");
                });

            modelBuilder.Entity("Syzoj.Api.Models.ReplyEntry", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorEmail");

                    b.Property<string>("Content");

                    b.Property<int?>("DiscussionEntryId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorEmail");

                    b.HasIndex("DiscussionEntryId");

                    b.ToTable("ReplyEntry");
                });

            modelBuilder.Entity("Syzoj.Api.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HashedPassword");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordSalt");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Syzoj.Api.Models.DiscussionEntry", b =>
                {
                    b.HasOne("Syzoj.Api.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorEmail");
                });

            modelBuilder.Entity("Syzoj.Api.Models.ReplyEntry", b =>
                {
                    b.HasOne("Syzoj.Api.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorEmail");

                    b.HasOne("Syzoj.Api.Models.DiscussionEntry")
                        .WithMany("Reply")
                        .HasForeignKey("DiscussionEntryId");
                });
#pragma warning restore 612, 618
        }
    }
}
