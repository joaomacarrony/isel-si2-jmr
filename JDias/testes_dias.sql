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
	- Aceita os pedidos que tenham o preço mais baixo e que satisfaçam a quantidade pretendida
*/

-- Ler o preço médio dos produtos

--  

use si2
select * from Vendas where Vendas.codigo_produto = 1
select * from PedidosProdutos
select * from RespostaPedido

--insert into PedidosProdutos values (1,5, '2019-12-31') -- Pedir produto 1, quantidade 5
insert into RespostaPedido (ppid,preco,quantidade) values (1,2,5) --Resposta Vende 10 produtos a 2.2€

exec VerificaOfertas 1


update RespostaPedido set resposta = NULL

