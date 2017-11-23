IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'Customers'
                 AND COLUMN_NAME = 'FoundationDate')
BEGIN
	ALTER TABLE [dbo].[Customers]
		ADD [FoundationDate] DATE NULL;
END
IF NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[Regions]'))
BEGIN
	EXEC sp_rename 'dbo.Region', 'Regions'
END