use si2

IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'HistoricoVendas')
 DROP TRIGGER HistoricoVendas

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'Vendas')
 DROP TRIGGER Vendas

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'EntregasFranqueados')
 DROP TRIGGER EntregasFranqueados

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'Stock')
 DROP TRIGGER Stock

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'PedidosProdutos')
 DROP TRIGGER PedidosProdutos

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'PedidosFranqueados')
 DROP TRIGGER PedidosFranqueados

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'FornecedoresProdutos')
 DROP TRIGGER FornecedoresProdutos

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'Produto')
 DROP TRIGGER Produto

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'Armazem')
 DROP TRIGGER Armazem

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'Fornecedor')
 DROP TRIGGER Fornecedor

 IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'Franqueado')
 DROP TRIGGER Franqueado

 -----------------------------------------------------------------------------------------------
 use SI2
 DROP TABLE HistoricoVendas
 DROP TABLE Vendas
 DROP TABLE Consumidor
 DROP TABLE EntregasFranqueados
 DROP TABLE Stock
 DROP TABLE RespostaPedido
 DROP TABLE PedidosProdutos
 DROP TABLE PedidosFranqueados
 DROP TABLE FornecedoresProdutos
 DROP TABLE Produto
 DROP TABLE Armazem
 DROP TABLE Fornecedor
 DROP TABLE Franqueado


 