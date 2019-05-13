﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedLogin.Infrastructure.Contexts;

namespace SharedLogin.Infrastructure.Contexts.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    partial class SqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SharedLogin.Domain.AccessHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName");

                    b.Property<DateTime?>("EndLoginDateTime");

                    b.Property<DateTime>("LoginDateTime");

                    b.Property<string>("SharedAccountId");

                    b.Property<int?>("SharedAccountId1");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("SharedAccountId1");

                    b.ToTable("AccessHistories");
                });

            modelBuilder.Entity("SharedLogin.Domain.SharedAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("SharedAccounts");
                });

            modelBuilder.Entity("SharedLogin.Domain.AccessHistory", b =>
                {
                    b.HasOne("SharedLogin.Domain.SharedAccount", "SharedAccount")
                        .WithMany("AccessHistories")
                        .HasForeignKey("SharedAccountId1");
                });
#pragma warning restore 612, 618
        }
    }
}
