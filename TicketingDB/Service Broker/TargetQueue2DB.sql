CREATE QUEUE [dbo].[TargetQueue2DB]
    WITH ACTIVATION (STATUS = ON, PROCEDURE_NAME = [dbo].[CheckOrderRequestQueue], MAX_QUEUE_READERS = 10, EXECUTE AS N'dbo');

