CREATE TABLE [dbo].[CreditCards](
	[CreditCardID] [int] IDENTITY(1,1) NOT NULL,
	[ExpireDate] [DateTime] NOT NULL,
	[CardHolder] [nvarchar](50) NULL,
	[CustomerID] [nchar](5) NOT NULL,
	CONSTRAINT [PK_CreditCards] PRIMARY KEY ( [CreditCardID] ASC),
	CONSTRAINT [FK_CreditCards_Customers] FOREIGN KEY ( [CustomerID] ) REFERENCES [dbo].[Customers] (CustomerID)
)