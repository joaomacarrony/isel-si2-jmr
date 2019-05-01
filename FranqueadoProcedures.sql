use si2

CREATE PROCEDURE InsertFranqueado 
	@nif int, 
	@nome varchar(40),
	@morada varchar(100)
AS
BEGIN
	insert into Franqueado values (@nif,@nome,@morada);
END
GO

CREATE PROCEDURE UpdateFranqueado 
	@fid int,
	@nif int, 
	@nome varchar(40),
	@morada varchar(100)
AS
BEGIN
	update Franqueado set nif = @nif,
						 nome = @nome,
						 morada = @morada
	where fid = @fid;
END
GO


CREATE PROCEDURE RemoveFranqueado 
	@fid int
AS
BEGIN
	delete from Franqueado where fid = @fid;
END
GO

