--AL�NEA g)

/* Iniciar uma sess�o para um novo consumidor; */

-- Criar uma tabela consumidor, com um c�digo �nico para cada transa��o nova de cada cliente tid 

create procedure InsertConsumidor 
	@cid int,
	@nome varchar(100)

as
	begin
		insert into Consumidor (@cid,@nome) values (30,'Consumidor Teste');
	end
go


/* A opera��o equivalente � passagem de um produto pelo leitor de c�digo de barras; */

-- Inserir uma entrada na tabela Vendas o produto a ser comprado de uma dada transa��o tid

create procedure InsertVenda 
	@fid int,
	@cid int,
	@tid int,
	@codigo_produto int,
	@preco_venda float
as
	begin
		-- Verifica se a quantidade em Stock � superior a 0
		-- Se sim efectua a venda, caso n�o efectue lan�a uma mensagem de erro
		insert into Vendas (fid,cid,tid,codigo_produto,preco_venda) values (@fid,@cid,@tid,@codigo_produto,@preco_venda);
		-- Decresce o valor do stock para um dado produto de um franqueado (Trigger em Vendas)
	end
go


/* Finalizar a conta do cliente, com c�lculo do valor a pagar e emiss�o de recibo com lista de compras. */

-- Calcular o valor total da compra do cliente e imprimir os produtos e o seu respectivo valor

-- NAO DEIXAR VENDER CASO NAO HAJA STOCK E REDUZ A QUANTIDADE NO STOCK

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