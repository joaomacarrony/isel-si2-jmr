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
	begin transaction
		Declare @num int
		begin try
		set @num = (select Count(*) from Stock where (Stock.codigo_produto = @codigo_produto));
--			if @num != 0
--			begin
				delete from Stock where codigo_produto = @codigo_produto;
--			end
			delete from FornecedoresProdutos where codigo_produto = @codigo_produto;
			delete from PedidosFranqueados where codigo_produto = @codigo_produto;
			delete from PedidosProdutos where codigo_produto = @codigo_produto;
			delete from EntregasFranqueados where codigo_produto = @codigo_produto;
			delete from Vendas where codigo_produto = @codigo_produto;
			delete from HistoricoVendas where codigo_produto = @codigo_produto;
			delete from Produto where codigo = @codigo_produto;
			commit;
		end try
		begin catch
			print error_message();
			print 'Rollback' 
			rollback;
		end catch
END
GO

exec ForceDeleteProduto 4

select * from Stock
select * from Produto

exec InsertStock 4,4,4,4,4,2;

exec InsertProduto 5,'Alimentar','ola',5,5,5,1

/* Modificar o procedure, eliminou do Stock ms não eliminou da tabela Produto*/