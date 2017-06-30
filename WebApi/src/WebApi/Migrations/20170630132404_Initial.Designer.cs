using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.Models;

namespace WebApi.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20170630132404_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("WebApi.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorName")
                        .IsRequired();

                    b.Property<DateTimeOffset>("ChangeDate");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid?>("MessageId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WebApi.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<Guid?>("QuestionId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("WebApi.Models.Comment", b =>
                {
                    b.HasOne("WebApi.Models.Message", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId");
                });

            modelBuilder.Entity("WebApi.Models.Message", b =>
                {
                    b.HasOne("WebApi.Models.Message", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");
                });
        }
    }
}
