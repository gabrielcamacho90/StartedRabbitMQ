INSERT INTO DEMO
		([Name], [Active], [Register], [Validate])
VALUES
		(@Name, @Active, @Register, @Validate) returning CodDemo into :CodDemo