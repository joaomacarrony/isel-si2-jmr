USE SI2;

/* Testes */

exec ForceDeleteProduto 4

select * from Stock
select * from Produto

exec InsertStock 4,4,4,4,4,2;

exec InsertProduto 5,'Alimentar','ola',5,5,5,1

------------------------------------------------
/*

3ª Fase do Processo de encomendas

Ler as propostas e decidir se as propostas foram aceites ou não:
	- São recusadas caso o valor seja inferior a 30% da média de valor dos preço dos produtos
*/

-- Ler o preço médio dos produtos

--  

create procedure VerificaOfertas
	@pid int

as
	DEclare @preco float;
	DEclare @preco_proposto float;
	DEclare @resposta bit;
	Declare @preco_proposto_calculado float
	Declare @rid int
	Declare @ppid int
	Declare @quantidade int
	Declare pointer cursor for 
		select RespostaPedido.rid, RespostaPedido.preco, RespostaPedido.resposta
			from RespostaPedido
			join PedidosProdutos
			on PedidosProdutos.ppid = RespostaPedido.ppid
			where PedidosProdutos.codigo_produto = @pid;
	begin
		set @preco = select AVG(preco) from Stock where codigo_produto = @pid;
		open pointer;
		while 1 = 1
		-- Recusar todas as propostas que cujo preço seja inferior a 30% da média de valores do produto
		begin
			fetch next from pointer into  @rid, @preco_proposto, @resposta;

			if @@fetch_status <> 0
			begin
				break
			end

			set @preco_proposto_calculado = @preco * 0.7;
			if @preco_proposto < @preco_proposto_calculado
			begin
				update RespostaPedido set resposta = 'false' where rid = @rid;
			end
		end

		close pointer
		deallocate pointer;

		-- Aceita a proposta que tenha o preço mais baixo para a quantidade pretendida

		Declare pointer2 cursor for
			select RespostaPedido.rid, RespostaPedido.preco, RespostaPedido.quantidade, PedidosProdutos.ppid
				from RespostaPedido
				join PedidosProdutos
				on PedidosProdutos.ppid = RespostaPedido.ppid
				where PedidosProdutos.codigo_produto = @pid and RespostaPedido.resposta = 'false'
				order by RespostaPedido.preco asc;

		Declare @quantidade_pretendida int;
		set @quantidade_pretendida = Select quantidade from PedidosProdutos where codigo_produto = @pid;
		
		while 1 = 1
		begin
			fetch next from pointer2 into  @rid, @preco_proposto, @quantidade, @ppid
			if @@fetch_status <> 0
			begin
				break
			end

			if @quantidade_pretendida >= @quantidade
			begin
				update RespostaPedido set resposta = 'true' where rid = @rid;
				delete from PedidosProdutos where codigo_produto = @ppid;
			end
		end
		close pointer2;
		deallocate pointer2;
	end
go

