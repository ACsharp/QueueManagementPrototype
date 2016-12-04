CREATE PROCEDURE dbo.EnqueueOrderRequest
(
	@RequestId uniqueidentifier,
	@Name varchar(100),
	@Quantity smallint,
	@EventDate datetime
)
AS
BEGIN
	SET NOCOUNT ON;
	declare @message XML
	DECLARE @InitDlgHandle UNIQUEIDENTIFIER

	set @message = (select * from
		(select @RequestId RequestId,
			    @Name Name,
				@Quantity Quantity,
				@EventDate EventDate) OrderRequest
	for xml auto)

	BEGIN DIALOG @InitDlgHandle
     FROM SERVICE [//MyCompany/TicketService/OrderInitiatorService]
     TO SERVICE N'//MyCompany/TicketService/OrderService'
     ON CONTRACT [//MyCompany/TicketService/OrderContract]
     WITH
         ENCRYPTION = OFF;

	SEND ON CONVERSATION @InitDlgHandle
    MESSAGE TYPE [//MyCompany/TicketService/RequestMessage]
      (@message);
END