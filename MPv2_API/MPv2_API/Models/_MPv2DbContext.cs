using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MPv2_API.Models;

public partial class _MPv2DbContext : DbContext
{
    public _MPv2DbContext()
    {
    }

    public _MPv2DbContext(DbContextOptions<_MPv2DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Practitioner> Practitioners { get; set; }

    public virtual DbSet<PractitionerType> PractitionerTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-K8MR6C6\\MSSQLSERVER01;Database=MPv2;User=RobertSSA1;Password=black-Panther12!;Encrypt=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__26A111AD4289F9DA");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("addressId");
            entity.Property(e => e.HouseUnitLotNo)
                .HasMaxLength(5)
                .HasColumnName("houseUnitLotNo");
            entity.Property(e => e.Postcode)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("postcode");
            entity.Property(e => e.State)
                .HasMaxLength(3)
                .HasColumnName("state");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
            entity.Property(e => e.Suburb)
                .HasMaxLength(50)
                .HasColumnName("suburb");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppId).HasName("PK__Appointm__C00006D57751C41F");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.AppDate).HasColumnName("appDate");
            entity.Property(e => e.AppTime).HasColumnName("appTime");
            entity.Property(e => e.PatientId).HasColumnName("patientId");
            entity.Property(e => e.PractitionerId).HasColumnName("practitionerId");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__patie__48CFD27E");

            entity.HasOne(d => d.Practitioner).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PractitionerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__pract__47DBAE45");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("PK__Day__B0FA5F20B90D3A38");

            entity.ToTable("Day");

            entity.Property(e => e.DayId)
                .ValueGeneratedNever()
                .HasColumnName("dayId");
            entity.Property(e => e.DayName)
                .HasMaxLength(9)
                .HasColumnName("dayName");

            entity.HasMany(d => d.Practitioners).WithMany(p => p.Days)
                .UsingEntity<Dictionary<string, object>>(
                    "Availability",
                    r => r.HasOne<Practitioner>().WithMany()
                        .HasForeignKey("PractitionerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Availabil__pract__440B1D61"),
                    l => l.HasOne<Day>().WithMany()
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Availabil__dayId__44FF419A"),
                    j =>
                    {
                        j.HasKey("DayId", "PractitionerId");
                        j.ToTable("Availability");
                        j.IndexerProperty<int>("DayId").HasColumnName("dayId");
                        j.IndexerProperty<int>("PractitionerId").HasColumnName("practitionerId");
                    });
        });

        modelBuilder.Entity<Practitioner>(entity =>
        {
            entity.HasKey(e => e.PractitionerId).HasName("PK__Practiti__316DB5DC22192B83");

            entity.ToTable("Practitioner");

            entity.Property(e => e.PractitionerId).HasColumnName("practitionerId");
            entity.Property(e => e.MedicalRegistrationNo)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("medicalRegistrationNo");
            entity.Property(e => e.PracTypeId).HasColumnName("pracTypeId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.PracType).WithMany(p => p.Practitioners)
                .HasForeignKey(d => d.PracTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Practitio__pracT__3F466844");

            entity.HasOne(d => d.User).WithMany(p => p.Practitioners)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Practitio__userI__3E52440B");
        });

        modelBuilder.Entity<PractitionerType>(entity =>
        {
            entity.HasKey(e => e.PracTypeId).HasName("PK__Practiti__E78B7D23A73308DC");

            entity.ToTable("PractitionerType");

            entity.Property(e => e.PracTypeId).HasColumnName("pracTypeId");
            entity.Property(e => e.PracTypeName)
                .HasMaxLength(50)
                .HasColumnName("pracTypeName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFFB2C857CE");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.AddressId).HasColumnName("addressId");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.FName)
                .HasMaxLength(50)
                .HasColumnName("fName");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.HomePhoneNo)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("homePhoneNo");
            entity.Property(e => e.LName)
                .HasMaxLength(50)
                .HasColumnName("lName");
            entity.Property(e => e.MedicareNo)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("medicareNo");
            entity.Property(e => e.MiddleInitial)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("middleInitial");
            entity.Property(e => e.MobilePhoneNo)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("mobilePhoneNo");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasColumnName("title");

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__addressId__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
