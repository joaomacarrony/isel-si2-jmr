use SI2

create table Franqueado(
	fid int identity(1,1) primary key,
	nif int unique,
	nome varchar(40) unique,
	morada varchar(100)
);

create table Fornecedor(
	foid int identity(1,1) primary key,
	nif int,
	nome varchar(40) not null
);


create table Produto(
	codigo int primary key,
	tipo varchar(20) check(tipo = 'Alimentar' or tipo = 'Higiene Pessoal' or tipo = 'Casa'),
	descricao varchar(100),
	quantidade int,
	quantidade_minima int,
	quantidade_maxima int
);

create table FornecedoresProdutos(
	codigo_produto int foreign key references Produto(codigo),
	foid int foreign key references Fornecedor(foid),
	constraint PK_Key primary key (codigo_produto,foid)
);

create table PedidosFranqueados(
	pfid int identity (1,1) primary key,
	fid int foreign key references Franqueado(fid),
	codigo_produto int foreign key references Produto(codigo),
	quantidade int
);

create table PedidosProdutos(
	ppid int identity(1,1) primary key,
	codigo_produto int foreign key references Produto(codigo),
	quantidade int,
	data date
);

create table RespostaPedido(
	rid int identity(1,1),
	ppid int foreign key references PedidosProdutos(ppid),
	preco float,
	quantidade int,
	resposta bit,
	primary key (ppid, rid)
);

create table Stock(
	codigo_produto int foreign key references Produto(codigo),
	preco float,
	quantidade int,
	quantidade_minima int,
	quantidade_maxima int,
	fid int foreign key references Franqueado(fid),
	primary key(codigo_produto,fid)
);

create table EntregasFranqueados(
	efid int identity(1,1) primary key,
	fid int foreign key references Franqueado(fid),
	codigo_produto int foreign key references Produto(codigo),
	quantidade int,
	data date
);

create table Consumidor(
	tid int identity(1,1) unique, 
	cid int unique,
	nome varchar(100),
	data_transacao date,
	primary key(cid,tid)
);

create table Vendas(
	vid int identity(1,1),
	fid int foreign key references Franqueado(fid),
	cid int foreign key references Consumidor(cid),
	tid int foreign key references Consumidor(tid),
	codigo_produto int foreign key references Produto(codigo),
	data_venda date default getdate(),
	preco_venda float,
	quantidade int,
	primary key(vid,cid,tid,fid)
);

create table HistoricoVendas(
	fid int foreign key references Franqueado(fid),
	codigo_produto int foreign key references Produto(codigo),
	valor_ano_corrente float,
	valor_3_anos float,
	primary key (fid,codigo_produto)
);





