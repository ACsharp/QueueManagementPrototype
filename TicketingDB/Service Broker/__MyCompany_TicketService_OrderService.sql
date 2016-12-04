CREATE SERVICE [//MyCompany/TicketService/OrderService]
    AUTHORIZATION [dbo]
    ON QUEUE [dbo].[TargetQueue2DB]
    ([//MyCompany/TicketService/OrderContract]);

