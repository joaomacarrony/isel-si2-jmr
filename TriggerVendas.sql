use SI2
drop trigger updatestock
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