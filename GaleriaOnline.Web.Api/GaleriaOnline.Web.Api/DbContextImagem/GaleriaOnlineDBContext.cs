﻿using System;
using System.Collections.Generic;
using GaleriaOnline.Web.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GaleriaOnline.Web.Api.DbContextImagem;

public partial class GaleriaOnlineDBContext : DbContext
{
    public GaleriaOnlineDBContext()
    {
    }

    public GaleriaOnlineDBContext(DbContextOptions<GaleriaOnlineDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Imagem> Imagens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NOTE39-S28\\SQLEXPRESS;DataBase=GaleriaOnlineDB;user ID= sa; Pwd= Senai@134;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Imagem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Imagens__3214EC07CA36900A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
