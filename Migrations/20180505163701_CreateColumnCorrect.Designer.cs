﻿// <auto-generated />
using associatetool.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace associatetool.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180505163701_CreateColumnCorrect")]
    partial class CreateColumnCorrect
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("associatetool.Model.Tag_Address", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("codigo");

                    b.Property<string>("ferramenta_id");

                    b.Property<string>("nome_ferramenta");

                    b.Property<string>("trigger");

                    b.Property<string>("vida_util");

                    b.Property<string>("vida_util_acumulado");

                    b.Property<string>("vida_util_max");

                    b.Property<string>("vida_util_status");

                    b.Property<string>("vida_util_unidade");

                    b.HasKey("id");

                    b.ToTable("Tag_Address");
                });

            modelBuilder.Entity("associatetool.Model.ToolTag", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("codigo");

                    b.Property<bool>("trigger");

                    b.Property<int>("vida_util");

                    b.Property<int>("vida_util_unidade");

                    b.HasKey("id");

                    b.ToTable("ToolTag");
                });
#pragma warning restore 612, 618
        }
    }
}
