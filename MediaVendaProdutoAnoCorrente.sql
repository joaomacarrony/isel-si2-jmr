-- Alínea l) - Média de Vendas de um dado produto do ano corrente

create procedure MediaVendaProdutoAnoCorrente @codigo_produto int
as
select codigo_produto, AVG(preco_venda)
	from Vendas
	where codigo_produto = @codigo_produto and year(data_venda) = year(getdate())
	group by codigo_produto;
go