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

    public virtual DbSet<AppointmentPractitionerPatient> AppointmentPractitionerPatients { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Practitioner> Practitioners { get; set; }

    public virtual DbSet<PractitionerType> PractitionerTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-K8MR6C6\\MSSQLSERVER01;Database=MedicalPracticeV2;User=RobertSSA1;Password=black-Panther12!;Encrypt=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__26A111AD6FD99692");

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
            entity.HasKey(e => e.AppId).HasName("PK__Appointm__C00006D592163A11");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.AppDate).HasColumnName("appDate");
            entity.Property(e => e.AppTime).HasColumnName("appTime");
        });

        modelBuilder.Entity<AppointmentPractitionerPatient>(entity =>
        {
            entity.HasKey(e => new { e.AppId, e.PractitionerId, e.PatientId }).HasName("PK_Appintment_Practitione_Patient");

            entity.ToTable("Appointment_Practitioner_Patient");

            entity.Property(e => e.AppId).HasColumnName("appId");
            entity.Property(e => e.PractitionerId).HasColumnName("practitionerId");
            entity.Property(e => e.PatientId).HasColumnName("patientId");

            entity.HasOne(d => d.App).WithMany(p => p.AppointmentPractitionerPatients)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__appId__07C12930");

            entity.HasOne(d => d.Patient).WithMany(p => p.AppointmentPractitionerPatients)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__patie__09A971A2");

            entity.HasOne(d => d.Practitioner).WithMany(p => p.AppointmentPractitionerPatients)
                .HasForeignKey(d => d.PractitionerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__pract__08B54D69");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("PK__Day__B0FA5F20D349A4F4");

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
                        .HasConstraintName("FK__Availabil__pract__5FB337D6"),
                    l => l.HasOne<Day>().WithMany()
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Availabil__dayId__60A75C0F"),
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
            entity.HasKey(e => e.PractitionerId).HasName("PK__Practiti__316DB5DC731A804D");

            entity.ToTable("Practitioner");

            entity.Property(e => e.PractitionerId).HasColumnName("practitionerId");
            entity.Property(e => e.MedicalRegistrationNo)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("medicalRegistrationNo");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Practitioners)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Practitio__userI__3F466844");

            entity.HasMany(d => d.PracTypes).WithMany(p => p.Practitioners)
                .UsingEntity<Dictionary<string, object>>(
                    "PractitionerPracType",
                    r => r.HasOne<PractitionerType>().WithMany()
                        .HasForeignKey("PracTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Practitio__pracT__5CD6CB2B"),
                    l => l.HasOne<Practitioner>().WithMany()
                        .HasForeignKey("PractitionerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Practitio__pract__5BE2A6F2"),
                    j =>
                    {
                        j.HasKey("PractitionerId", "PracTypeId").HasName("PK_Prac_PracType");
                        j.ToTable("Practitioner_PracType");
                        j.IndexerProperty<int>("PractitionerId").HasColumnName("practitionerId");
                        j.IndexerProperty<int>("PracTypeId").HasColumnName("pracTypeId");
                    });
        });

        modelBuilder.Entity<PractitionerType>(entity =>
        {
            entity.HasKey(e => e.PracTypeId).HasName("PK__Practiti__E78B7D23CF95323F");

            entity.ToTable("PractitionerType");

            entity.Property(e => e.PracTypeId).HasColumnName("pracTypeId");
            entity.Property(e => e.PracTypeName)
                .HasMaxLength(50)
                .HasColumnName("pracTypeName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFF431FB1F5");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
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

            entity.HasMany(d => d.Addresses).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserAddress",
                    r => r.HasOne<Address>().WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__User_Addr__addre__6C190EBB"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__User_Addr__userI__6B24EA82"),
                    j =>
                    {
                        j.HasKey("UserId", "AddressId");
                        j.ToTable("User_Address");
                        j.IndexerProperty<int>("UserId").HasColumnName("userId");
                        j.IndexerProperty<int>("AddressId").HasColumnName("addressId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
