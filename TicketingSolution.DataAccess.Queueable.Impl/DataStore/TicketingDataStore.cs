namespace TicketingSolution.DataAccess.Queueable.Impl.DataStore
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    internal partial class TicketingDataStore : DbContext
    {
        public TicketingDataStore()
            : base("name=TicketingDataStore")
        {
        }

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
