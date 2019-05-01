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

create table Armazem(
	aid int identity(1,1) primary key,
	nome varchar(40)
);

create table Produto(
	pid int identity(1,1) primary key,
	codigo int unique,
	tipo varchar(20) check(tipo = 'Alimentar' or tipo = 'Higiene Pessoal' or tipo = 'Casa'),
	descricao varchar(100),
	quantidade int,
	quantidade_minima int,
	quantidade_maxima int,
	armazem int foreign key references Armazem(aid)
);

create table FornecedoresProdutos(
	pid int foreign key references Produto(pid),
	foid int foreign key references Fornecedor(foid),
	constraint PK_Key primary key (pid,foid)
);

create table PedidosFranqueados(
	pfid int identity (1,1) primary key,
	fid int foreign key references Franqueado(fid),
	pid int foreign key references Produto(pid),
	quantidade int
);

create table PedidosProdutos(
	ppid int identity(1,1) primary key,
	pid int foreign key references Produto(pid),
	foid int foreign key references Fornecedor(foid),
	quantidade int
);

create table Stock(
	stid int identity(1,1) primary key,
	produto int foreign key references Produto(pid),
	preco float,
	quantidade int,
	quantidade_minima int,
	quantidade_maxima int,
	fid int foreign key references Franqueado(fid)
);

create table EntregasFranqueados(
	efid int identity(1,1) primary key,
	fid int foreign key references Franqueado(fid),
	pid int foreign key references Produto(pid),
	quantidade int
);

create table Vendas(
	vid int identity(1,1) primary key,
	fid int foreign key references Franqueado(fid),
	pid int foreign key references Produto(pid),
	data_venda date default getdate(),
	preco_venda float
);

create table HistoricoVendas(
	hvid int identity(1,1) primary key,
	fid int foreign key references Franqueado(fid),
	pid int foreign key references Produto(pid),
	valor_ano_corrente float,
	valor_3_anos float
);


