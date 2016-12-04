namespace TicketingSolution.DataAccess.Direct.Impl.DataStore
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    internal partial class TicketingDataStore : DbContext
    {
        public TicketingDataStore(string connectionString)
            : base(connectionString)
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
