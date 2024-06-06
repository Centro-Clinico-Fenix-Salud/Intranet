﻿// <auto-generated />
using System;
using Intranet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Intranet.Migrations
{
    [DbContext(typeof(IntranetContext))]
    partial class IntranetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Intranet.Modelos.Admin.C1_Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("c1_Categorias");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.Categoria_SubCategoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubCategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("SubCategoriaId");

                    b.ToTable("categoria_SubCategorias");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.P1_Permiso", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("p1_Permiso");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.Permisos_SubCategoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermisoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubCategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PermisoId");

                    b.HasIndex("SubCategoriaId");

                    b.ToTable("permisos_SubCategorias");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.R1_Rol", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("r1_Rol");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.Rol_Permiso", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("P1_PermisoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("R1_RolId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("P1_PermisoId");

                    b.ToTable("rol_Permiso");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.S1_SubCategoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("s1_SubCategorias");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.U1_Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("R1_RolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("R1_RolId");

                    b.ToTable("u1_Usuario");
                });

            modelBuilder.Entity("Intranet.Modelos.Agenda.AgendaTelefonica", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Concurrencia")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Extension")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UbicacionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UnidadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Usuario")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioModificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("numeroTelefonico")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("agendaTelefonicas");
                });

            modelBuilder.Entity("Intranet.Modelos.Tablas.Ubicacion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ubicaciones");
                });

            modelBuilder.Entity("Intranet.Modelos.Tablas.Unidad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("unidades");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.Categoria_SubCategoria", b =>
                {
                    b.HasOne("Intranet.Modelos.Admin.C1_Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intranet.Modelos.Admin.S1_SubCategoria", "SubCategoria")
                        .WithMany()
                        .HasForeignKey("SubCategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("SubCategoria");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.Permisos_SubCategoria", b =>
                {
                    b.HasOne("Intranet.Modelos.Admin.P1_Permiso", "Permiso")
                        .WithMany()
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intranet.Modelos.Admin.S1_SubCategoria", "SubCategoria")
                        .WithMany()
                        .HasForeignKey("SubCategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permiso");

                    b.Navigation("SubCategoria");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.Rol_Permiso", b =>
                {
                    b.HasOne("Intranet.Modelos.Admin.P1_Permiso", null)
                        .WithMany("Rol_Permisos")
                        .HasForeignKey("P1_PermisoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.U1_Usuario", b =>
                {
                    b.HasOne("Intranet.Modelos.Admin.R1_Rol", null)
                        .WithMany("Usuarios")
                        .HasForeignKey("R1_RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.P1_Permiso", b =>
                {
                    b.Navigation("Rol_Permisos");
                });

            modelBuilder.Entity("Intranet.Modelos.Admin.R1_Rol", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
