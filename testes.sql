USE SI2;

/* Testes */

exec ForceDeleteProduto 4

select * from Stock
select * from Produto

exec InsertStock 4,4,4,4,4,2;

exec InsertProduto 5,'Alimentar','ola',5,5,5,1

----------------- Teste Trigger Vendas ------------------------

use SI2
drop table Consumidor 
drop table vendas

select * from Vendas
select * from Stock
select * from PedidosFranqueados

insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (1,1,1,1,2.2,5)

insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (1,1,1,2,5,1)




