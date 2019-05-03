USE SI2;

/* Testes */

exec ForceDeleteProduto 4

select * from Stock
select * from Produto

exec InsertStock 4,4,4,4,4,2;

exec InsertProduto 5,'Alimentar','ola',5,5,5,1

------------------------------------------------

/* Iniciar uma sessão para um novo consumidor; */

-- Criar uma tabela consumidor, com um código único para cada transação nova de cada cliente tid 

create procedure InsertConsumidor 
	@cid int,
	@nome varchar(100)

as
	begin
		insert into Consumidor (@cid,@nome) values (30,'Consumidor Teste');
	end
go


/* A operação equivalente à passagem de um produto pelo leitor de código de barras; */

-- Inserir uma entrada na tabela Vendas o produto a ser comprado de uma dada transação tid

create procedure InsertVenda 
	@fid int,
	@cid int,
	@tid int,
	@codigo_produto int,
	@preco_venda float
as
	begin
		insert into Vendas (fid,cid,tid,codigo_produto,preco_venda) values (@fid,@cid,@tid,@codigo_produto,@preco_venda);
	end
go


/* Finalizar a conta do cliente, com cálculo do valor a pagar e emissão de recibo com lista de compras. */

-- Calcular o valor total da compra do cliente e imprimir os produtos e o seu respectivo valor

create procedure CloseVenda 
	@cid int,
	@tid int
as
	begin
		select cid, codigo_produto, data_venda, Sum(preco_venda) as Total
			from Vendas
			where (tid = @tid and cid = @cid)
			group by cid, codigo_produto, data_venda;
	end
go



