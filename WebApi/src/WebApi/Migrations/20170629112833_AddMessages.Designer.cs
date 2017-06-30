using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.Models;

namespace WebApi.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20170629112833_AddMessages")]
    partial class AddMessages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

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

            modelBuilder.Entity("WebApi.Models.Message", b =>
                {
                    b.HasOne("WebApi.Models.Message", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");
                });
        }
    }
}
