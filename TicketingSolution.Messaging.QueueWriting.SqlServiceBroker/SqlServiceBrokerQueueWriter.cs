using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.BusinessEntities;

namespace TicketingSolution.Messaging.QueueWriting.SqlServiceBroker
{
    public class SqlServiceBrokerQueueWriter : Interfaces.IQueueWriter
    {
        private readonly SqlServiceBrokerWriterConfiguration configuration;

        public SqlServiceBrokerQueueWriter(SqlServiceBrokerWriterConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task CreateOrderRequestAsync(OrderRequest orderRequest)
        {
            using (var con = new SqlConnection(configuration.InitiatorDbConnectionString))
            {
                using (var cmd = new SqlCommand("EnqueueOrderRequest", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RequestId", orderRequest.RequestId);
                    cmd.Parameters.AddWithValue("@Name", orderRequest.Name);
                    cmd.Parameters.AddWithValue("@Quantity", orderRequest.Quantity);
                    cmd.Parameters.AddWithValue("@EventDate", orderRequest.EventDate);
                    cmd.Connection = con;
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }

            }
        }
    }
}
