USE SI2;

-- Alínea k) Valida as ofertas dos fornecedores para um dado produto

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