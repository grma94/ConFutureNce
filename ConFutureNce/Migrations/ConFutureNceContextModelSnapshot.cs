﻿// <auto-generated />
using ConFutureNce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace ConFutureNce.Migrations
{
    [DbContext(typeof(ConFutureNceContext))]
    partial class ConFutureNceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConFutureNce.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ConFutureNce.Models.Conference", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AssignDeadline");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("PaperDeadline");

                    b.Property<DateTime>("ReviewDeadline");

                    b.Property<DateTime>("SelectionDeadline");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Name");

                    b.ToTable("Conference");
                });

            modelBuilder.Entity("ConFutureNce.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BillingAddress");

                    b.Property<string>("Name");

                    b.Property<string>("TaxNumber");

                    b.HasKey("InvoiceID");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("ConFutureNce.Models.Language", b =>
                {
                    b.Property<int>("LanguageID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LanguageName");

                    b.HasKey("LanguageID");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("ConFutureNce.Models.Paper", b =>
                {
                    b.Property<int>("PaperID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abstract");

                    b.Property<int?>("AuthorUserTypeID");

                    b.Property<string>("Authors");

                    b.Property<int?>("LanguageID");

                    b.Property<string>("OrgName");

                    b.Property<int?>("PaymentID");

                    b.Property<int?>("ReviewID");

                    b.Property<int?>("ReviewerUserTypeID");

                    b.Property<DateTime>("SubmissionDate");

                    b.Property<string>("TitleENG");

                    b.Property<string>("TitleORG");

                    b.HasKey("PaperID");

                    b.HasIndex("AuthorUserTypeID");

                    b.HasIndex("LanguageID");

                    b.HasIndex("PaymentID")
                        .IsUnique()
                        .HasFilter("[PaymentID] IS NOT NULL");

                    b.HasIndex("ReviewID")
                        .IsUnique()
                        .HasFilter("[ReviewID] IS NOT NULL");

                    b.HasIndex("ReviewerUserTypeID");

                    b.ToTable("Paper");
                });

            modelBuilder.Entity("ConFutureNce.Models.PaperKeyword", b =>
                {
                    b.Property<string>("KeyWord")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PaperID");

                    b.HasKey("KeyWord");

                    b.HasIndex("PaperID");

                    b.ToTable("PaperKeyword");
                });

            modelBuilder.Entity("ConFutureNce.Models.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("InvoiceID");

                    b.Property<bool>("IsDone");

                    b.HasKey("PaymentID");

                    b.HasIndex("InvoiceID")
                        .IsUnique()
                        .HasFilter("[InvoiceID] IS NOT NULL");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("ConFutureNce.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Achievements");

                    b.Property<DateTime>("Date");

                    b.Property<string>("GeneralComments");

                    b.Property<string>("Grade");

                    b.Property<string>("NotMentioned");

                    b.Property<string>("Problems");

                    b.Property<string>("Solution");

                    b.Property<string>("WhyProblems");

                    b.HasKey("ReviewID");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("ConFutureNce.Models.UserType", b =>
                {
                    b.Property<int>("UserTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("UserTypeID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserType");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ConFutureNce.Models.Author", b =>
                {
                    b.HasBaseType("ConFutureNce.Models.UserType");

                    b.Property<string>("OrgName");

                    b.Property<string>("ScTitle");

                    b.ToTable("Author");

                    b.HasDiscriminator().HasValue("Author");
                });

            modelBuilder.Entity("ConFutureNce.Models.Reviewer", b =>
                {
                    b.HasBaseType("ConFutureNce.Models.UserType");

                    b.Property<int?>("Language1LanguageID");

                    b.Property<int?>("Language2LanguageID");

                    b.Property<int?>("Language3LanguageID");

                    b.Property<string>("OrgName")
                        .HasColumnName("Reviewer_OrgName");

                    b.Property<string>("ScTitle")
                        .HasColumnName("Reviewer_ScTitle");

                    b.HasIndex("Language1LanguageID");

                    b.HasIndex("Language2LanguageID");

                    b.HasIndex("Language3LanguageID");

                    b.ToTable("Reviewer");

                    b.HasDiscriminator().HasValue("Reviewer");
                });

            modelBuilder.Entity("ConFutureNce.Models.ApplicationUser", b =>
                {
                    b.HasOne("ConFutureNce.Models.Conference", "Conference")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("Name");
                });

            modelBuilder.Entity("ConFutureNce.Models.Paper", b =>
                {
                    b.HasOne("ConFutureNce.Models.Author", "Author")
                        .WithMany("Papers")
                        .HasForeignKey("AuthorUserTypeID");

                    b.HasOne("ConFutureNce.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageID");

                    b.HasOne("ConFutureNce.Models.Payment", "Payment")
                        .WithOne("Paper")
                        .HasForeignKey("ConFutureNce.Models.Paper", "PaymentID");

                    b.HasOne("ConFutureNce.Models.Review", "Review")
                        .WithOne("Paper")
                        .HasForeignKey("ConFutureNce.Models.Paper", "ReviewID");

                    b.HasOne("ConFutureNce.Models.Reviewer", "Reviewer")
                        .WithMany("Papers")
                        .HasForeignKey("ReviewerUserTypeID");
                });

            modelBuilder.Entity("ConFutureNce.Models.PaperKeyword", b =>
                {
                    b.HasOne("ConFutureNce.Models.Paper", "Paper")
                        .WithMany("PaperKeywords")
                        .HasForeignKey("PaperID");
                });

            modelBuilder.Entity("ConFutureNce.Models.Payment", b =>
                {
                    b.HasOne("ConFutureNce.Models.Invoice", "Invoice")
                        .WithOne("Payment")
                        .HasForeignKey("ConFutureNce.Models.Payment", "InvoiceID");
                });

            modelBuilder.Entity("ConFutureNce.Models.UserType", b =>
                {
                    b.HasOne("ConFutureNce.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Users")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ConFutureNce.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ConFutureNce.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ConFutureNce.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ConFutureNce.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ConFutureNce.Models.Reviewer", b =>
                {
                    b.HasOne("ConFutureNce.Models.Language", "Language1")
                        .WithMany()
                        .HasForeignKey("Language1LanguageID");

                    b.HasOne("ConFutureNce.Models.Language", "Language2")
                        .WithMany()
                        .HasForeignKey("Language2LanguageID");

                    b.HasOne("ConFutureNce.Models.Language", "Language3")
                        .WithMany()
                        .HasForeignKey("Language3LanguageID");
                });
#pragma warning restore 612, 618
        }
    }
}
