CREATE TABLE [dbo].[Orders] (
    [OrderId]   INT              IDENTITY (1, 1) NOT NULL,
    [RequestId] UNIQUEIDENTIFIER NOT NULL,
    [Name]      VARCHAR (100)    NOT NULL,
    [Quantity]  SMALLINT         NOT NULL,
    [EventDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Orders]
    ON [dbo].[Orders]([RequestId] ASC);

