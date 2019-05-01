use si2

CREATE PROCEDURE InsertStock
	@produto int,          
	@preco float,
	@quantidade int,
	@quantidade_minima int,
	@quantidade_maxima int,
	@fid int               
AS
BEGIN
	insert into Stock values (@produto,@preco,@quantidade,@quantidade_minima,@quantidade_maxima,@fid);
END
GO