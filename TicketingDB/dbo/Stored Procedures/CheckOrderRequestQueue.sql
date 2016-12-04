CREATE PROCEDURE dbo.CheckOrderRequestQueue

AS
BEGIN
	SET NOCOUNT ON;

    
	DECLARE @RecvReqDlgHandle UNIQUEIDENTIFIER;
	DECLARE @RecvReqMsgName sysname;
	DECLARE @XmlContent XML;

	  WHILE (1=1)
	  BEGIN

		BEGIN TRANSACTION;

		WAITFOR
		( RECEIVE TOP(1)
			@RecvReqDlgHandle = conversation_handle,
			@XmlContent = message_body,
			@RecvReqMsgName = message_type_name
		  FROM TargetQueue2DB
		), TIMEOUT 5000;

		IF (@@ROWCOUNT = 0)
		BEGIN
		  ROLLBACK TRANSACTION;
		  BREAK;
		END

		IF @RecvReqMsgName = N'//MyCompany/TicketService/RequestMessage'
		BEGIN

			INSERT INTO Orders
				(RequestId, Name, Quantity, EventDate)
			select 
				x.Req.value('@RequestId', 'uniqueidentifier'),
				x.Req.value('@Name', 'varchar(100)'),
				x.Req.value('@Quantity', 'smallint'),
				x.Req.value('@EventDate', 'datetime')
			from @XmlContent.nodes('/OrderRequest') as x(Req)


			END CONVERSATION @RecvReqDlgHandle;
	    END
		ELSE IF @RecvReqMsgName = N'http://schemas.microsoft.com/SQL/ServiceBroker/EndDialog'
		BEGIN
			END CONVERSATION @RecvReqDlgHandle;
		END
		ELSE IF @RecvReqMsgName = N'http://schemas.microsoft.com/SQL/ServiceBroker/Error'
		BEGIN
			END CONVERSATION @RecvReqDlgHandle;
		END
      
		COMMIT TRANSACTION;

	  END
END
