﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using logisticsApi.Infrastructure;

#nullable disable

namespace logisticsApi.Migrations
{
    [DbContext(typeof(LogisticsDbContext))]
    [Migration("20230603024212_CreacionTablaUsuario")]
    partial class CreacionTablaUsuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("logisticsApi.Models.Bodegas", b =>
                {
                    b.Property<int>("BodegaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BodegaID"));

                    b.Property<string>("Ciudad")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Direccion")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Pais")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefono")
                        .HasColumnType("varchar(20)");

                    b.HasKey("BodegaID");

                    b.ToTable("bodegas");
                });

            modelBuilder.Entity("logisticsApi.Models.Clientes", b =>
                {
                    b.Property<int>("ClienteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteID"));

                    b.Property<string>("Ciudad")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Direccion")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Pais")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefono")
                        .HasColumnType("varchar(20)");

                    b.HasKey("ClienteID");

                    b.ToTable("clientes");
                });

            modelBuilder.Entity("logisticsApi.Models.LogisticaMaritima", b =>
                {
                    b.Property<int>("LogisticaMaritimaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogisticaMaritimaID"));

                    b.Property<int>("CantidadProducto")
                        .HasColumnType("int");

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumeroFlota")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("NumeroGuia")
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("PrecioEnvio")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("PuertoID")
                        .HasColumnType("int");

                    b.Property<int>("TipoProductoID")
                        .HasColumnType("int");

                    b.HasKey("LogisticaMaritimaID");

                    b.HasIndex("ClienteID");

                    b.HasIndex("PuertoID");

                    b.HasIndex("TipoProductoID");

                    b.ToTable("logisticaMaritima");
                });

            modelBuilder.Entity("logisticsApi.Models.LogisticaTerrestre", b =>
                {
                    b.Property<int>("LogisticaTerrestreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogisticaTerrestreID"));

                    b.Property<int>("BodegaID")
                        .HasColumnType("int");

                    b.Property<int>("CantidadProducto")
                        .HasColumnType("int");

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumeroFlota")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("NumeroGuia")
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("PrecioEnvio")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("TipoProductoID")
                        .HasColumnType("int");

                    b.HasKey("LogisticaTerrestreID");

                    b.HasIndex("BodegaID");

                    b.HasIndex("ClienteID");

                    b.HasIndex("TipoProductoID");

                    b.ToTable("logisticaTerrestre");
                });

            modelBuilder.Entity("logisticsApi.Models.Puertos", b =>
                {
                    b.Property<int>("PuertoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PuertoID"));

                    b.Property<string>("Ciudad")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Direccion")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Pais")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefono")
                        .HasColumnType("varchar(20)");

                    b.HasKey("PuertoID");

                    b.ToTable("puertos");
                });

            modelBuilder.Entity("logisticsApi.Models.TiposProductos", b =>
                {
                    b.Property<int>("TipoProductoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoProductoID"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("TipoProductoID");

                    b.ToTable("tiposProductos");
                });

            modelBuilder.Entity("logisticsApi.Models.Usuarios", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioID"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioID");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("logisticsApi.Models.LogisticaMaritima", b =>
                {
                    b.HasOne("logisticsApi.Models.Clientes", "clientes")
                        .WithMany()
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("logisticsApi.Models.Puertos", "puertos")
                        .WithMany()
                        .HasForeignKey("PuertoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("logisticsApi.Models.TiposProductos", "tiposProductos")
                        .WithMany()
                        .HasForeignKey("TipoProductoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("clientes");

                    b.Navigation("puertos");

                    b.Navigation("tiposProductos");
                });

            modelBuilder.Entity("logisticsApi.Models.LogisticaTerrestre", b =>
                {
                    b.HasOne("logisticsApi.Models.Bodegas", "bodegas")
                        .WithMany()
                        .HasForeignKey("BodegaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("logisticsApi.Models.Clientes", "clientes")
                        .WithMany()
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("logisticsApi.Models.TiposProductos", "tiposProductos")
                        .WithMany()
                        .HasForeignKey("TipoProductoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bodegas");

                    b.Navigation("clientes");

                    b.Navigation("tiposProductos");
                });
#pragma warning restore 612, 618
        }
    }
}
