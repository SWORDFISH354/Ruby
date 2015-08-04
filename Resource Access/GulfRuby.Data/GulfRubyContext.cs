﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using GulRuby.Business.Entities;

namespace GulfRuby.Data
{

    public class GulfRubyContext : DbContext
    {

        public GulfRubyContext()
            : base("name=GulfRuby")
        {
            Database.SetInitializer<GulfRubyContext>(null);
        }

        public DbSet<Airline> AirlineSet { get; set; }
        public DbSet<Employee> EmployeeSet { get; set; }
        public DbSet<CompanyInformation> CompanyInformationSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Airline>().HasKey(a => a.ID).Ignore(e => e.EntityId).Ignore(e=>e.IsDirty);
            modelBuilder.Entity<Employee>().HasKey(a => a.Id).Ignore(e => e.EntityId).Ignore(e => e.IsDirty);
            modelBuilder.Entity<CompanyInformation>().HasKey(a => a.ID).Ignore(e => e.EntityId).Ignore(e => e.IsDirty);


            modelBuilder.Entity<Ticket>().HasKey(a => a.ID).Ignore(e => e.EntityId).Ignore(e => e.IsDirty);

            modelBuilder.Entity<Ticket>()
                        .HasMany<PassengerInfo>(s => s.Passengers)
                        .WithRequired(s => s.Ticket)
                        .HasForeignKey(s => s.TicketId);

            modelBuilder.Entity<Ticket>()
                        .HasMany<Itinerary>(s => s.Itinerary)
                        .WithRequired(s => s.Ticket)
                        .HasForeignKey(s => s.TicketId);

        }
    }
}
