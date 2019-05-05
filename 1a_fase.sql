-- RESOLUÇÃO DAS ALÍNEAS --

/************************* Alínea a) ******************************/

-- Ficheiro create_tables.sql

/************************* Alínea b) ******************************/

-- Ficheiro drops.sql

/************************* Alínea c) ******************************/

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

/************************* Alínea d) ******************************/

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

/************************* Alínea e) ******************************/


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

/************************* Alínea f) ******************************/

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


/************************* Alínea g) ******************************/

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

/************************* Alínea h) ******************************/

-- Implementado no seguinte Trigger

create TRIGGER updatestock on Vendas
after insert
AS DECLARE @fid int,
	   @codigo_produto int,
	   @quantidadeapedir int ,
	   @quantidade_min int ,
	   @quantidade_max int,
	   @quantidade_atual int,
	   @quantidade_vendida int,
	   @quantidade int 
	
--@fid=SELECT fid  FROM inserted ;
--@pid=SELECT  pid FROM inserted ;
--print '@fid ='+fid+'pid ='+pid
--begin try
DECLARE @MyCursor as CURSOR;

SET @MyCursor = CURSOR FOR SELECT fid,codigo_produto,quantidade FROM inserted;
OPEN @MyCursor;
FETCH NEXT FROM @MyCursor INTO @fid, @codigo_produto,@quantidade;

WHILE @@FETCH_STATUS = 0
BEGIN
	PRINT cast(@fid as VARCHAR (50)) + ' '+ cast(@codigo_produto as VARCHAR (50));
	select @quantidade_min=quantidade_minima from Stock where codigo_produto=@codigo_produto and fid=@fid 
	select @quantidade_max=quantidade_maxima from Stock where codigo_produto=@codigo_produto and fid=@fid 
	select @quantidade_atual=quantidade from Stock where codigo_produto=@codigo_produto and fid=@fid 
	print cast(@quantidade_atual as VARCHAR (50)) + ' '+ cast(@quantidade_min as VARCHAR (50))+ ' '+ cast(@quantidade_max as VARCHAR (50))+ ' '+ cast(@quantidade as VARCHAR (50));
if(@quantidade_atual>=@quantidade)
begin
	set @quantidade=@quantidade_atual-@quantidade
	UPDATE Stock SET quantidade=@quantidade WHERE codigo_produto=@codigo_produto and fid=@fid;

	if(@quantidade<@quantidade_min)
	begin
	insert into PedidosFranqueados values(@fid,@codigo_produto,@quantidade_max-@quantidade)
	end
	--set  @quantidadeapedir=@quantidade_max-@quantidade_atual
end 
else begin print 'quantidade a ser vendida é superior À quantidade em stock so estao '+cast(@quantidade_atual as VARCHAR (50)) +' disponiveis'
			   rollback
			   end
	--PRINT cast(@quantidade_min as VARCHAR (50)) + ' '+ cast(@quantidade_max as VARCHAR (50))+' '+ cast(@quantidade as VARCHAR (50))+' '+ cast(@quantidade_atual as VARCHAR (50))+' '+ cast(@quantidadeapedir as VARCHAR (50));
	FETCH NEXT FROM @MyCursor INTO @fid, @codigo_produto,@quantidade;
END

CLOSE @MyCursor;
DEALLOCATE @MyCursor;

/************************* Alínea i) ******************************/

create procedure PedidoProdutoFornecedor
	@codigo_produto int,
	@quantidade int,
	@data date
as
	insert into PedidosProdutos values(@codigo_produto,@quantidade,@data);
go

/************************* Alínea j) ******************************/

create procedure RespostaPedidoFornecedor
	@codigo_produto int

as
	select codigo_produto as Produto,
		   preco as Preço,
		   RespostaPedido.quantidade as QuantidadeResposta,
		   PedidosProdutos.quantidade as QuantidadePretendida,
		   resposta as Resposta
		from PedidosProdutos
		join RespostaPedido
		on (PedidosProdutos.ppid = RespostaPedido.ppid)
		where PedidosProdutos.codigo_produto = @codigo_produto;
go

/************************* Alínea k) ******************************/

/*
recebendo as propostas dos
fornecedores e numa terceira fase escolhendo as propostas entregues com os seguintes
critérios:
	1- Se existirem propostas cujo preço unitário se afaste mais do que 30% da média dos
	preços unitários dos vários fornecedores, elas devem ser descartadas;
	2- Vão-se satisfazendo as encomendas começando pela proposta com preço mais baixo;
	se o fornecedor não fornecer toda a quantidade pretendida, repete-se o processo com
	a segunda melhor oferta e assim sucessivamente.
*/

create procedure VerificaOfertas
	@pid int
as
	DEclare @preco_medio float;
	DEclare @preco_proposto float;
	DEclare @resposta bit;
	Declare @preco_minimo_descontado float
	Declare @rid int
	Declare @ppid int
	Declare @quantidade_oferecida int
	begin
		begin transaction
			begin try
				set @preco_medio = (select AVG(preco) from Stock where codigo_produto = @pid);
				set @preco_minimo_descontado = @preco_medio * 1.3;
				Declare pointer2 cursor local for
					select RespostaPedido.rid, RespostaPedido.preco, RespostaPedido.quantidade as QuantidadeResposta,
						   PedidosProdutos.quantidade as QuantidadePretendida, PedidosProdutos.ppid, resposta
						from RespostaPedido
						join PedidosProdutos
						on PedidosProdutos.ppid = RespostaPedido.ppid
						where PedidosProdutos.codigo_produto = @pid
						order by RespostaPedido.preco asc;

				Declare @quantidade_pretendida int;
				open pointer2;
				while 1 = 1
				begin
					fetch next from pointer2 into  @rid, @preco_proposto, @quantidade_oferecida,@quantidade_pretendida, @ppid, @resposta
					if @@fetch_status <> 0
					begin
						break
					end
					if @preco_proposto < @preco_minimo_descontado
					begin
						if @quantidade_oferecida >= @quantidade_pretendida
						begin
							update RespostaPedido set resposta = 'true' where rid = @rid; -- Aceita a proposta
							--delete from PedidosProdutos where codigo_produto = @ppid;
							break
						end
						else
						begin
							update RespostaPedido set resposta = 'false' where rid = @rid; -- Recusa a proposta
						end
					end
					else
					begin
						update RespostaPedido set resposta = 'false' where rid = @rid; -- Recusa a proposta
					end
				end
				close pointer2;
				deallocate pointer2;
				commit transaction;
			end try
			begin catch
				print error_message(); 
				rollback transaction;
			end catch
	end
go

/************************* Alínea l) ******************************/

create procedure MediaVendaProdutoAnoCorrente @codigo_produto int
as
select codigo_produto, AVG(preco_venda)
	from Vendas
	where codigo_produto = @codigo_produto and year(data_venda) = year(getdate())
	group by codigo_produto;
go





