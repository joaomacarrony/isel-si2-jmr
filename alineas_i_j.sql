-- Alínea  i) Pedido de produtos a um dado fornecedor

Use SI2

create procedure PedidoProdutoFornecedor
	@codigo_produto int,
	@quantidade int,
	@data date
as
	insert into PedidosProdutos values(@codigo_produto,@quantidade,@data);
go

-- Alínea j) Propostas de preços e quantidades para um dado produto

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