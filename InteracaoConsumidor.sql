--ALÍNEA g)

/* Iniciar uma sessão para um novo consumidor; */

-- Criar uma tabela consumidor, com um código único para cada transação nova de cada cliente tid 

create procedure InsertConsumidor 
	@cid int,
	@nome varchar(100)

as
	begin
		insert into Consumidor (cid,nome) values (@cid,@nome);
	end
go


/* A operação equivalente à passagem de um produto pelo leitor de código de barras; */

-- Inserir uma entrada na tabela Vendas o produto a ser comprado de uma dada transação tid

create procedure InsertVenda 
	@fid int,
	@cid int,
	@tid int,
	@codigo_produto int,
	@preco_venda float,
	@quantidade int
as
	begin
		-- Verifica se a quantidade em Stock é superior a 0 - CHECKED
		-- Se sim efectua a venda, caso não efectue lança uma mensagem de erro - CHECKED
		insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (@fid,@cid,@tid,@codigo_produto,@preco_venda,@quantidade);
		-- Decresce o valor do stock para um dado produto de um franqueado (Trigger em Vendas) - CHECKED
	end
go


/* Finalizar a conta do cliente, com cálculo do valor a pagar e emissão de recibo com lista de compras. */

-- Calcular o valor total da compra do cliente e imprimir os produtos e o seu respectivo valor

create procedure CloseVenda 
	@cid int,
	@tid int
as
	Declare @total float; 
	Declare @total_parcial float; 
	Declare @preco_venda float; 
	Declare @quantidade int;
	Declare @descricao varchar(100);
	Declare @count int;
	Declare pointer cursor for 
		select Vendas.preco_venda, Vendas.quantidade, Produto.descricao
			from Vendas
			join Produto
			on (Vendas.codigo_produto = Produto.codigo)
			where cid = @cid and tid = @tid;

	begin
		open pointer;
		set @total = 0;
		set @count = 0;
		while 1 = 1
		begin
			fetch next from pointer into @preco_venda, @quantidade, @descricao

			if @@fetch_status <> 0
			begin
				break
			end
			set @total_parcial = @preco_venda * @quantidade;
			set @total = @total + @total_parcial;
			print @descricao + ' : ' + cast(@total_parcial as varchar) + '€'; 
			set @count = @count +1;
		end
		close pointer
		deallocate pointer;
		print 'Total: ' + cast(@total as varchar);
	end
go

use si2

drop procedure CloseVenda;

exec CloseVenda 2, 2

select * from Vendas
select * from Produto