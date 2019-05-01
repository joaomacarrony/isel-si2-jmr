use si2

CREATE PROCEDURE InsertProduto
	@codigo int,
	@tipo varchar(20),
	@descricao varchar(100),
	@quantidade int,
	@quantidade_minima int,
	@quantidade_maxima int,
	@armazem int
AS
BEGIN
	insert into Produto values (@codigo,@tipo,@descricao,@quantidade,@quantidade_minima,@quantidade_maxima,@armazem);
END
GO

CREATE PROCEDURE UpdateProduto
	@pid int,
	@codigo int,
	@tipo varchar(20),
	@descricao varchar(100),
	@quantidade int,
	@quantidade_minima int,
	@quantidade_maxima int,
	@armazem int
AS
BEGIN
	update Produto set  codigo = @codigo,
						tipo = @tipo,
						descricao = @descricao,
						quantidade = @quantidade,
						quantidade_minima = @quantidade_minima,
						quantidade_maxima = @quantidade_maxima,
						armazem = @armazem
	where pid = @pid;

END
GO


CREATE PROCEDURE DeleteProduto @pid int
AS
BEGIN
	begin tran
		Declare @num int
		Set @num = (select Count(*) from Stock where (Stock.produto = @pid));

		if @num = 0
		begin try
			delete from Produto	where pid = @pid;
			commit
		end try
		begin catch
			rollback
		end catch
END
GO

CREATE PROCEDURE ForceDeleteProduto @pid int
AS
BEGIN
	Declare @num int, @fid int
	set @num = (select Count(*) from Stock where (Stock.produto = @pid));
	if @num != 0
	begin
		select fid into Tabela_Fornecedores from Stock where Stock.produto = @pid;

		while (select Count(*) from Tabela_Fornecedores) > 0
		begin
			select top 1 @fid = fid from Tabela_Fornecedores
			begin try
				begin transaction
				delete from Stock where produto = @pid and fid = @fid;
				delete from Tabela_Fornecedores where fid = @fid
				commit transaction
			end try
			begin catch
				rollback transaction
				break
			end catch
		end
		drop table Tabela_Fornecedores
	end
	begin try
		begin transaction
		delete from Produto where pid = @pid;
		print 'Estibe aqui'
		commit transaction;
	end try
	begin catch
		rollback transaction
	end catch
END
GO

exec ForceDeleteProduto 4

select * from Stock
select * from Produto


exec InsertStock 4,4,4,4,4,2;

exec InsertProduto 5,'Alimentar','ola',5,5,5,1

/* Modificar o procedure, eliminou do Stock ms não eliminou da tabela Produto*/