﻿// <auto-generated />
using System;
using Clinique2000_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Clinique2000_DataAccess.Migrations
{
    [DbContext(typeof(CliniqueDbContext))]
    [Migration("20240111233931_migrationFix")]
    partial class migrationFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Clinique2000_Core.Models.Clinique", b =>
                {
                    b.Property<int>("CliniqueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CliniqueID"), 1L, 1);

                    b.Property<int>("TempsMoyenConsultation")
                        .HasColumnType("int");

                    b.HasKey("CliniqueID");

                    b.ToTable("Cliniques");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.Consultation", b =>
                {
                    b.Property<int>("ConsultationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConsultationID"), 1L, 1);

                    b.Property<DateTime>("HeureDateDebutPrevue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("HeureDateDebutReele")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HeureDateFinPrevue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("HeureDateFinReele")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PatientID")
                        .HasColumnType("int");

                    b.Property<int>("PlageHoraireID")
                        .HasColumnType("int");

                    b.Property<int>("StatutConsultation")
                        .HasColumnType("int");

                    b.HasKey("ConsultationID");

                    b.HasIndex("PatientID")
                        .IsUnique()
                        .HasFilter("[PatientID] IS NOT NULL");

                    b.HasIndex("PlageHoraireID");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.ListeAttente", b =>
                {
                    b.Property<int>("ListeAttenteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ListeAttenteID"), 1L, 1);

                    b.Property<int>("CliniqueID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEffectivite")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DureeConsultationMinutes")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("HeureFermeture")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HeureOuverture")
                        .HasColumnType("time");

                    b.Property<bool>("IsOuverte")
                        .HasColumnType("bit");

                    b.Property<int>("NbMedecinsDispo")
                        .HasColumnType("int");

                    b.HasKey("ListeAttenteID");

                    b.HasIndex("CliniqueID");

                    b.ToTable("ListeAttentes");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.Personne", b =>
                {
                    b.Property<int>("PersonneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonneId"), 1L, 1);

                    b.Property<string>("Courriel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("PersonneId");

                    b.ToTable("Personne", (string)null);
                });

            modelBuilder.Entity("Clinique2000_Core.Models.PlageHoraire", b =>
                {
                    b.Property<int>("PlageHoraireID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlageHoraireID"), 1L, 1);

                    b.Property<DateTime>("HeureDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HeureFin")
                        .HasColumnType("datetime2");

                    b.Property<int>("ListeAttenteID")
                        .HasColumnType("int");

                    b.HasKey("PlageHoraireID");

                    b.HasIndex("ListeAttenteID");

                    b.ToTable("PlagesHoraires");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.Patient", b =>
                {
                    b.HasBaseType("Clinique2000_Core.Models.Personne");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<int>("ConsultationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDeNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<string>("MotDePasseConfirmation")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<string>("NAM")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.ToTable("Patient", (string)null);
                });

            modelBuilder.Entity("Clinique2000_Core.Models.PatientACharge", b =>
                {
                    b.HasBaseType("Clinique2000_Core.Models.Personne");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDeNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("NAM")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("PatientAChargeId")
                        .HasColumnType("int");

                    b.Property<int?>("PatientPersonneId")
                        .HasColumnType("int");

                    b.HasIndex("PatientPersonneId");

                    b.ToTable("PatientACharge", (string)null);
                });

            modelBuilder.Entity("Clinique2000_Core.Models.Consultation", b =>
                {
                    b.HasOne("Clinique2000_Core.Models.Patient", "Patient")
                        .WithOne("consultation")
                        .HasForeignKey("Clinique2000_Core.Models.Consultation", "PatientID");

                    b.HasOne("Clinique2000_Core.Models.PlageHoraire", "PlageHorarie")
                        .WithMany("Consultations")
                        .HasForeignKey("PlageHoraireID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("PlageHorarie");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.ListeAttente", b =>
                {
                    b.HasOne("Clinique2000_Core.Models.Clinique", "Clinique")
                        .WithMany("ListeAttente")
                        .HasForeignKey("CliniqueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinique");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.PlageHoraire", b =>
                {
                    b.HasOne("Clinique2000_Core.Models.ListeAttente", "ListeAttente")
                        .WithMany("PlagesHoraires")
                        .HasForeignKey("ListeAttenteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ListeAttente");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.Patient", b =>
                {
                    b.HasOne("Clinique2000_Core.Models.Personne", null)
                        .WithOne()
                        .HasForeignKey("Clinique2000_Core.Models.Patient", "PersonneId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clinique2000_Core.Models.PatientACharge", b =>
                {
                    b.HasOne("Clinique2000_Core.Models.Patient", null)
                        .WithMany("PatientsACharge")
                        .HasForeignKey("PatientPersonneId");

                    b.HasOne("Clinique2000_Core.Models.Personne", null)
                        .WithOne()
                        .HasForeignKey("Clinique2000_Core.Models.PatientACharge", "PersonneId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clinique2000_Core.Models.Clinique", b =>
                {
                    b.Navigation("ListeAttente");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.ListeAttente", b =>
                {
                    b.Navigation("PlagesHoraires");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.PlageHoraire", b =>
                {
                    b.Navigation("Consultations");
                });

            modelBuilder.Entity("Clinique2000_Core.Models.Patient", b =>
                {
                    b.Navigation("PatientsACharge");

                    b.Navigation("consultation");
                });
#pragma warning restore 612, 618
        }
    }
}
