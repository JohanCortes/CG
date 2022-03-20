create database cgreen
go
use cgreen
go
create table vendedor(
cc bigint primary key identity(1,1),
nombre varchar(15) not null,
apellido varchar(15) not null,
email varchar(50),
telefono bigint,
p_acumulado money not null)
go
create table punto(
id smallint primary key identity(1,1),
nombre varchar(20) not null,
minima money not null,
direccion varchar(30) not null)
go
create table venta(
id int primary key identity(1,1),
cc_vendedor bigint foreign key references vendedor(cc),
id_punto smallint foreign key references punto(id),
unidades smallint not null,
unidades_xl smallint not null,
v_sin_utilidad money not null,
v_con_utilidad money not null,
fecha smalldatetime not null)
go
create table producto(
id int primary key identity(1,1),
descripcion varchar(50) not null,
v_interno money not null,
v_venta money not null)
go
create table venta_producto(
id_venta int foreign key references venta(id),
id_producto int foreign key references producto(id),
cantidad smallint not null,
constraint venta_producto_pk primary key(id_venta, id_producto))
go
create table inventario(
id_producto int foreign key references producto(id),
id_punto smallint foreign key references punto(id),
stock smallint not null,
constraint inventario_pk primary key(id_producto, id_punto))
go
/*create table asignacion(
id_punto smallint foreign key references punto(id),
cc_vendedor bigint foreign key references vendedor(cc),
fecha date not null,
constraint asignacion_pk primary key(id_punto, cc_vendedor, fecha),
cargo char(3) not null)
go*/
create table tipo_control(
id tinyint primary key,
descripcion varchar(20) not null)
go
create table control_inv(
id_producto int foreign key references producto(id),
id_punto smallint foreign key references punto(id),
fecha smalldatetime,
id_tipo tinyint foreign key references tipo_control(id),
unidades smallint not null,
constraint control_pk primary key(id_producto, id_punto, fecha))
go
create table tipo_trans(
id tinyint primary key,
descripcion varchar(20) not null)
go
create table transaccion(
cc_vendedor bigint foreign key references vendedor(cc),
fecha datetime2,
valor money not null,
id_tipo tinyint foreign key references tipo_trans(id)
constraint transaccion_pk primary key(cc_vendedor, fecha))
go

--Triggers

create trigger update_inventario
on venta_producto
after insert
as
begin
update inventario set stock=stock-(select cantidad from inserted) where id_producto=(select id_producto from inserted) and
id_punto=(select v.id_punto from venta v join inserted i on v.id=i.id_venta)
insert into control_inv(id_producto,id_punto,fecha,id_tipo,unidades) 
values((select id_producto from inserted),
(select v.id_punto from venta v join inserted i on v.id=i.id_venta),
(select fecha from venta v join inserted i on i.id_venta=v.id),1,
(select cantidad from inserted)*-1)
end
go

create trigger generar_pago
on venta
after insert
as
insert into transaccion(cc_vendedor,fecha,id_tipo,valor)values((select cc_vendedor from inserted),sysdatetime(),1,
(select max(a.v) from (select 'v'=(v_con_utilidad-v_sin_utilidad)/2 from inserted union select 'v'=minima from inserted i join punto p on i.id_punto=p.id) a))
go

create trigger nuevo_producto
on producto
after insert
as
insert into inventario(id_producto,id_punto,stock) select i.id, p.id, 0 from inserted i, punto p
go

create trigger nuevo_punto
on punto
after insert
as
insert into inventario(id_producto,id_punto,stock) select p.id, i.id, 0 from inserted i, producto p
go

create trigger update_money
on transaccion
after insert
as
update vendedor set p_acumulado=p_acumulado+(select valor from inserted) where cc=(select cc_vendedor from inserted)
go

--Procedures Vendedor

/*create proc ins_vendedor
@cc bigint,
@nom varchar(15),
@ape varchar(15),
@email varchar(50),
@tel bigint
as
insert into vendedor(cc,nombre,apellido,email,telefono,p_acumulado)values(@cc,@nom,@ape,@email,@tel,0)
go*/
create proc up_vendedor
@cc bigint,
@email varchar(50),
@tel bigint,
@p_acumulado money
as
update vendedor set email=@email, telefono=@tel, p_acumulado=@p_acumulado where cc=@cc
go
create proc sel_vendedor
as
select * from vendedor
go

--Procedures Punto

create proc ins_punto
@nom varchar(20),
@dir varchar(30),
@min money
as
insert into punto(nombre,direccion,minima)values(@nom,@dir,@min)
go

create proc up_punto
@id smallint,
@nom varchar(20),
@dir varchar(30),
@min money
as
update punto set nombre=@nom, direccion=@dir, minima=@min where id=@id
go

create proc sel_punto
as
select * from punto
go

--Procedures Venta

create proc ins_venta
@cc_v bigint,
@id_p smallint,
@uni smallint,
@uni_xl smallint,
@v_sin_uti money,
@v_con_uti money
as
insert into venta(cc_vendedor,id_punto,unidades,unidades_xl,v_sin_utilidad,v_con_utilidad,fecha)
values(@cc_v,@id_p,@uni,@uni_xl,@v_sin_uti,@v_con_uti,sysdatetime())
go

create proc up_venta
@id int,
@v_con_uti money
as
update venta set v_con_utilidad=@v_con_uti where id=@id
go

create proc sel_venta
as
select * from venta
go

--Procedures Producto

create proc ins_producto
@desc varchar(50),
@v_in money,
@v_ven money
as
insert into producto(descripcion,v_interno,v_venta)values(@desc,@v_in,@v_ven)
go

create proc up_producto
@id int,
@desc varchar(50),
@v_in money,
@v_ven money
as
update producto set descripcion=@desc, v_interno=@v_in, v_venta=@v_ven where id=@id
go

create proc sel_producto
as
select * from producto
go

create proc ins_ven_pro
@id_v int,
@id_p int,
@can smallint
as
insert into venta_producto(id_venta,id_producto,cantidad)values(@id_v,@id_p,@can)
go

create proc sel_ven_pro
@id_v int
as
select p.descripcion, vp.cantidad from venta_producto vp join producto p on vp.id_producto=p.id where vp.id_venta=@id_v
go

--Procedures Inventario

create proc sel_inventario
@id_p smallint
as
select pr.descripcion, i.stock from inventario i join producto pr on i.id_producto=pr.id where i.id_punto=@id_p
go

--Procedures Control_Inv

create proc ins_control
@id_producto int,
@id_punto smallint,
@id_tipo tinyint,
@unidades smallint
as
insert into control_inv(id_producto,id_punto,fecha,id_tipo,unidades)values(@id_producto,@id_punto,sysdatetime(),@id_tipo,@unidades)
go

create proc sel_control_inv
@fecha smalldatetime,
@id_punto smallint
as
select a.id_producto, 'stock'=stock-isnull(cantidad,0) 
from (select id_producto, stock from inventario where id_punto=@id_punto) a left join 
(select id_producto, 'cantidad'=sum(unidades) from control_inv where id_punto=@id_punto and fecha>=convert(datetime,@fecha)+1 group by id_producto)
b on a.id_producto=b.id_producto
--select id_producto, sum(unidades) from control_inv where id_punto=1 and fecha>=convert(datetime,'2022-02-16')+1 group by id_producto
go

--Procedures Transaccion

create proc ins_transaccion
@cc bigint,
@valor money,
@id_tipo tinyint
as
insert into transaccion(cc_vendedor,fecha,valor,id_tipo)values(@cc,sysdatetime(),@valor,@id_tipo)
go

create proc sel_transaccion
@cc bigint
as
select * from transaccion where cc_vendedor=@cc order by fecha
go

create proc utilidad_punto
@fecha1 date,
@fecha2 date
as
select 'punto'=nombre, 'utilidad'=v_con_utilidad-v_sin_utilidad, 'fecha'=fecha
from venta v join punto p on v.id_punto=p.id where fecha>=@fecha1 and fecha<convert(datetime,@fecha2)+1
go
exec utilidad_punto @fecha1 = '2022-02-07', @fecha2 = '2022-02-10'
--exec sel_control_inv @fecha='2022-02-26', @id_punto=2
--exec sel_ven_pro @id_v=2
exec utilidad_punto @fecha1 = '2022-02-07', @fecha2 = '2022-02-10'
--Test

/*insert into punto(nombre,direccion,minima)values('Toberin','Kr 22 #164-02',30000)
go
insert into punto(nombre,direccion,minima)values('Mambo','Kr 8 #167-29',25000)
go
select * from punto
go
insert into producto(descripcion,v_interno,v_venta)values('c bl cr M',6000,8000)
go
insert into producto(descripcion,v_interno,v_venta)values('c bl cr XL',7000,9000)
go
select * from producto
go
insert into tipo_control(id,descripcion)values(1,'vendido')
go
insert into tipo_control(id,descripcion)values(2,'pedido')
go
select * from tipo_control
go
insert into tipo_trans(id,descripcion)values(1,'pago diario')
go
insert into tipo_trans(id,descripcion)values(2,'descontado')
go
select * from tipo_trans
go
update inventario set stock=30 where id_producto=1 and id_punto=1
go
update inventario set stock=46 where id_producto=1 and id_punto=2
go
select pr.descripcion, i.stock, pu.nombre from producto pr join inventario i on pr.id=i.id_producto join punto pu on i.id_punto=pu.id
go
select * from inventario
go
insert into vendedor(cc,nombre,apellido,email,telefono,p_acumulado)values(489418912,'juan','diaz','baehribv',3216536516,0)
go
insert into vendedor(cc,nombre,apellido,email,telefono,p_acumulado)values(994284381,'jose','vargas','awbqrugf',3027919473,0)
go
select * from vendedor
go
insert into venta(cc_vendedor,id_punto,unidades,unidades_xl,v_sin_utilidad,v_con_utilidad,fecha)values(489418912,1,2,0,12000,16000,sysdatetime())
go
insert into venta(cc_vendedor,id_punto,unidades,unidades_xl,v_sin_utilidad,v_con_utilidad,fecha)values(994284381,2,3,0,18000,24000,sysdatetime())
go
insert into venta(cc_vendedor,id_punto,unidades,unidades_xl,v_sin_utilidad,v_con_utilidad,fecha)values(489418912,1,1,0,45000,55000,sysdatetime())
go
insert into venta(cc_vendedor,id_punto,unidades,unidades_xl,v_sin_utilidad,v_con_utilidad,fecha)values(994284381,2,0,2,35000,43000,sysdatetime())
go
select * from venta order by fecha
go
insert into venta_producto(id_venta,id_producto,cantidad)values(1,1,2)
go
insert into venta_producto(id_venta,id_producto,cantidad)values(2,1,3)
go
insert into venta_producto(id_venta,id_producto,cantidad)values(3,1,4)
go
insert into venta_producto(id_venta,id_producto,cantidad)values(3,2,3)
go
insert into venta_producto(id_venta,id_producto,cantidad)values(4,2,5)
go
select * from venta_producto
go
--insert into transaccion(cc_vendedor,fecha,id_tipo,valor)values(489418912,sysdatetime(),2,-7000)
go
select * from transaccion
go
declare @x int
set @x = 0
while (@x<29)
	begin
		insert into venta(cc_vendedor,id_punto,unidades,unidades_xl,v_sin_utilidad,v_con_utilidad,fecha)values(994284381,2,1,0,floor(rand()*32000)+8000,floor(rand()*40000)+35000,convert(smalldatetime,'2022-02-'+CONVERT(varchar,@x)+' '+CONVERT(varchar,floor(rand()*23))+':'+CONVERT(varchar,floor(rand()*59))))
		set @x += 1
	end

delete from venta where id>4
go
*/

declare @x int
set @x = 1
while @x <= 8
	begin
		insert into punto(nombre,direccion,minima)values('Punto '+convert(varchar,@x),'Kr '+
		convert(varchar,ceiling(rand()*145))+' #'+convert(varchar,ceiling(rand()*145))+'-'+
		convert(varchar,ceiling(rand()*99)),ceiling(rand()*10)*1000+20000)
		set @x += 1
	end
go

declare @x int
set @x = 1
while @x <= 25
	begin
		declare @v money
		set @v = ceiling(rand()*100)*100
		insert into producto(descripcion,v_interno,v_venta)values('Producto '+convert(varchar,@x),@v,@v+ceiling(rand()*5)*1000)
		set @x += 1
	end
go

insert into tipo_control(id,descripcion)values(1,'vendido')
go
insert into tipo_control(id,descripcion)values(2,'pedido')
go
insert into tipo_trans(id,descripcion)values(1,'pago diario')
go
insert into tipo_trans(id,descripcion)values(2,'descontado')
go

declare @t table(id_producto int, id_punto int)
insert into @t select id_producto, id_punto from inventario
declare @c int = (select count(*) from @t)
while @c > 0
	begin
		declare @id_producto int = (select top(1) id_producto from @t order by id_producto, id_punto)
		declare @id_punto int = (select top(1) id_punto from @t order by id_producto, id_punto)
		update inventario set stock=ceiling(rand()*40)+40 where id_producto=@id_producto and id_punto=@id_punto
		delete from @t where id_producto=@id_producto and id_punto=@id_punto
		set @c = (select count(*) from @t)
	end
go

declare @x int
set @x = 1
while @x <= 12
	begin
		insert into vendedor(nombre,apellido,email,telefono,p_acumulado)values(
		'Nombre '+convert(varchar,@x),'Apellido '+convert(varchar,@x),
		'Email '+convert(varchar,@x),ceiling(rand()*10000000000),0)
		set @x += 1
	end
go

declare @id_venta int, @c_venta int, @c_vp int
set @id_venta = 1
set @c_vp = 1
declare @vp table(id_venta int,id_producto int,cantidad smallint,primary key(id_venta, id_producto))

while @id_venta <= 128
begin
	while @c_vp <= ceiling(rand()*8)
	begin
		begin try
			insert into @vp(id_venta,id_producto,cantidad)values(@id_venta,(select top 1 id from producto order by newid()),ceiling(rand()*5))
		end try
		begin catch
		end catch
		set @c_vp += 1
	end
	set @id_venta += 1
	set @c_vp = 1
end
select * from @vp;
select id_venta, 'v_sin_uti'=sum(v.cantidad*p.v_interno), 'v_con_uti'=sum(v.cantidad*p.v_venta) 
from @vp v join producto p on v.id_producto=p.id group by id_venta
declare @con int
set @con = 1
while @con <= 128
	begin
		insert into venta(cc_vendedor,id_punto,unidades,unidades_xl,v_sin_utilidad,v_con_utilidad,fecha)values
		((@con%8)+1,
		(@con%8)+1,0,0,
		(select sum(v.cantidad*p.v_interno)
		from @vp v join producto p on v.id_producto=p.id where id_venta=@con),
		(select sum(v.cantidad*p.v_venta)
		from @vp v join producto p on v.id_producto=p.id where id_venta=@con),
		convert(smalldatetime,'2022-02-'+convert(varchar,floor(@con/8)+1)+' '+convert(varchar,floor(rand()*23))+':'+convert(varchar,floor(rand()*59))))
		set @con += 1
	end

declare @c int = (select count(*) from @vp)
while @c > 0
	begin
		declare @idp int = (select top(1) id_producto from @vp order by id_venta, id_producto)
		declare @idv int = (select top(1) id_venta from @vp order by id_venta, id_producto)
		declare @can smallint = (select top(1) cantidad from @vp order by id_venta, id_producto)
		insert into venta_producto(id_venta,id_producto,cantidad)values(@idv,@idp,@can)
		delete from @vp where id_producto=@idp and id_venta=@idv
		set @c = (select count(*) from @vp)
	end
go
exec sel_control_inv @fecha='2022-01-16', @id_punto=5
go
select id_producto, unidades, fecha from control_inv where fecha>='2022-02-17' and id_punto=1
go
select id_producto, 'stock actual'=stock from inventario where id_punto=5
go
select id_producto, sum(cantidad), min(fecha) from venta_producto vp join venta v on vp.id_venta=v.id 
where v.id_punto=5 and fecha>=convert(datetime,'2022-01-02')+1 group by id_producto
go

