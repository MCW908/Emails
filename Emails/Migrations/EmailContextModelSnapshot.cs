﻿// <auto-generated />
using Emails.Models.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Emails.Migrations
{
    [DbContext(typeof(EmailContext))]
    partial class EmailContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Emails.Models.Email", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recipient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmailId");

                    b.ToTable("Emails");

                    b.HasData(
                        new
                        {
                            EmailId = 1,
                            Body = "Sample Text Sample Text",
                            Recipient = "JoeBloggs@gmail.com",
                            Sender = "TestMail123@gmail.com",
                            Subject = "Seed Email"
                        },
                        new
                        {
                            EmailId = 2,
                            Body = "Seed Data Seed Data Seed Data",
                            Recipient = "AABB@gmail.com",
                            Sender = "JoeBloggs@gmail.com",
                            Subject = "Testing"
                        },
                        new
                        {
                            EmailId = 3,
                            Body = "CC Email Test CC Email Test",
                            CC = "JoeBloggs@gmail.com",
                            Recipient = "TestMail123@gmail.com",
                            Sender = "AABB@gmail.com",
                            Subject = "CC Email"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}