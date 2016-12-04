namespace TicketingSolution.DataAccess.Queueable.Impl.DataStore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public Guid RequestId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public short Quantity { get; set; }

        public DateTime EventDate { get; set; }
    }
}
