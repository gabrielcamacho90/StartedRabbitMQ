INSERT INTO DEMO
		([Name], [Active], [Register], [Validate])
VALUES
		(@Name, @Active, @Register, @Validate);

SELECT CAST(SCOPE_IDENTITY() as int);