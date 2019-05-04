use SI2

BEGIN TRY
    BEGIN TRANSACTION 

        insert into Franqueado values (210210210,'Franqueado1','Av. do Brasil Nº 6')
        insert into Franqueado values (330330330,'Franqueado2','Rua do Jardim Botânico Nº 69 1º Dto')

		insert into Fornecedor values (100100100,'Fornecedor1')
		insert into Fornecedor values (200200200,'Fornecedor2')
			   
		insert into Armazem values ('Armazém A')

		insert into Produto values (1,'Alimentar','Arroz Integral',50,20,200,1)
		insert into Produto values (2,'Alimentar','Cereais',10,0,100,1)
		insert into Produto values (3,'Casa','Pano para pó',120,100,500,1)
		insert into Produto values (4,'Higiene Pessoal','Cotonetes',73,20,300,1)

		insert into FornecedoresProdutos values (1,1);
		insert into FornecedoresProdutos values (1,2);
		insert into FornecedoresProdutos values (2,1);

		insert into PedidosFranqueados values (1,1,10)
		insert into PedidosFranqueados values (1,2,2)
		insert into PedidosFranqueados values (2,3,10)
		insert into PedidosFranqueados values (2,2,3)
	   
	   insert into PedidosProdutos values (1,1,10,'31-12-2019')
	   insert into PedidosProdutos values (1,2,2, '31-12-2019')
	   insert into PedidosProdutos values (2,2,3, '31-12-2019')

	   insert into RespostaPedido values (1,5,10)
	   insert into RespostaPedido values (2,10.6,2)
	   insert into RespostaPedido values (3,4.2,3)
	   
	   insert into Stock values (1,2.20,10,5,100,1)
	   insert into Stock values (2,3.00,5,5,20,1)
	   insert into Stock values (3,1.50,20,10,50,1)
	   insert into Stock values (3,1.75,11,10,50,2)
	   insert into Stock values (4,1.00,2,0,10,2)

	   insert into EntregasFranqueados values (1,1,10)
	   insert into EntregasFranqueados values (1,2,34)
	   insert into EntregasFranqueados values (2,2,6)

	   insert into Consumidor (cid,nome) values (1,'Consumidor 1')
	   insert into Consumidor (cid,nome) values (2,'Consumidor 2')
	   insert into Consumidor (cid,nome) values (3,'Consumidor 3')

	   insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (1,1,1,1,2.2,10)
	   insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (1,1,1,1,2.2,9)
	   insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (1,1,1,1,2.2,8)
	   insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (1,2,2,2,3,7)
	   insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (2,2,2,3,1.5,6)
	   insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (2,3,3,3,1.5,5)
	   insert into Vendas (fid,cid,tid,codigo_produto,preco_venda,quantidade) values (2,3,3,4,1,5)

	   insert into HistoricoVendas values (1,1,20,30)
	   insert into HistoricoVendas values (2,3,10,20)

	   commit transaction

	END TRY

	BEGIN CATCH
		print error_message();
		ROLLBACK

	END CATCH
