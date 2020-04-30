create database Crem_Helado
go
use Crem_helado
go
create table Empleado
(
Nombre_usu nvarchar (30) primary key,
Nombre_empleado nvarchar (50) not null, 
Contraseña nvarchar (50) not null,
ID_Usuario bigint not null,
Telefono_usuario bigint not null,
Correo_usuario nvarchar(50) not null,
Estado_Usuario nvarchar (50) not null, 
Cargo_Usuario nvarchar (20) not null
)

create table Producto
(
Codigo_producto bigint identity (1,1) primary key,
Nombre_producto nvarchar (50) not null,
Existencia_producto bigint not null,
Cantidad_caja int not null,
Costo_caja bigint not null,
Precio_mayor bigint not null,
Precio_vendedor bigint not null,
Precio_publico bigint not null,
Codigo as ('Pro_'+Convert (nvarchar , Codigo_producto))
)

create table Proveedor 
(
Nit_proveedor bigint primary key,
Nombre_proveedor nvarchar (50) not null,
Direccion nvarchar (50) not null,
Teléfono bigint not null,
Correo_proveedor nvarchar (50) 
)

create table Cliente
(
Numero_identificacion bigint primary key,
Nombre_cliente nvarchar (50) not null,
Correo_cliente nvarchar (50),
Telefono_cliente bigint not null,
Dirección_cliente nvarchar (50) not null
)

create table Venta 
(
Codigo_venta bigint identity (1,1) primary key,
Nombre_usu nvarchar (30) constraint FK_Nombre_usu foreign key (Nombre_usu) references Empleado (Nombre_usu) on update cascade on delete cascade,
Numero_identificacion bigint constraint FK_NI foreign key (Numero_identificacion) references Cliente (Numero_identificacion) on update cascade on delete cascade,
Fecha_venta nvarchar (30) not null,
Tipo_venta nvarchar (30) not null,
Valor_total bigint not null,
Codigo as ('CodV_'+Convert(nvarchar, Codigo_venta))
)

create table Pedido 
(
Codigo_pedido bigint identity (1,1) primary key,
Nit_proveedor bigint constraint FK_nit foreign key (Nit_proveedor) references Proveedor (Nit_proveedor) on update cascade on delete cascade,
Nombre_usu nvarchar (30) constraint FK_usuario foreign key (Nombre_usu) references Empleado (Nombre_usu) on update cascade on delete cascade,
Fecha_pedido nvarchar (30) not null,
Fecha_entrega nvarchar(30) not null,
Valor_total bigint not null,
Entregado nvarchar (5) not null,
Codigo as ('CodP_'+Convert(nvarchar, Codigo_pedido) )
)

create table Maquinaria 
(
Codigo_maquinaria bigint identity (1,1) primary key,
Nombre_maquinaria nvarchar (50) not null,
Nombre_usu nvarchar (30) constraint FK_usuarioo foreign key (Nombre_usu) references Empleado (Nombre_usu) on update cascade on delete cascade,
Fecha_cambio nvarchar (30) not null,
Tipo_maquina nvarchar (30) not null,
Estado_maquina nvarchar (30) not null,
Codigo as ('Maq_'+Convert (nvarchar, Codigo_maquinaria))
)
 
create table Dotacion 
(
Nombre_dotacion nvarchar (30) primary key,
Cantidad_dotacion int not null,
Cantidad_libre int not null
)

create table Transaccion
(
Codigo_trans bigint identity (1,1) primary key,
Codigo_pedido bigint  references Pedido (Codigo_pedido) null,
Codigo_venta bigint  references Venta (Codigo_venta) null,
Descripcion nvarchar (30) not null,
Fecha_trans nvarchar (50) not null,
Ingresos_trans bigint,
Gastos_trans bigint,
Capital_total bigint not null,
Codigo as ('Tran_'+Convert (nvarchar , Codigo_trans))
)
-------------------------------------------------------------------------- Terceras Tablas ------------------------------------------------------------------------------------

create table Detalle_venta
(
Codigo_venta bigint foreign key (Codigo_venta) references Venta(Codigo_venta),
Codigo_producto bigint foreign key (codigo_producto) references Producto (codigo_producto),
Cantidad_entrega int not null,
Cantidad_recibida int,
Cantidad_recarga int
) 

create table Detalle_pedido
(
Codigo_pedido bigint foreign key (Codigo_pedido) references Pedido(Codigo_pedido),
Codigo_producto bigint foreign key (codigo_producto) references Producto (codigo_producto),
Cantidad_producto int not null
) 

create table Detalle_dotacion
(
Numero_identifiacion bigint foreign key (Numero_identifiacion) references Cliente (Numero_identificacion),
Nombre_dotacion nvarchar (30) foreign key (Nombre_dotacion) references Dotacion (Nombre_dotacion),
ID_fiador bigint not null,
Nombre_fiador nvarchar (30) not null
)
------------------------------------------------------------------------ Stored Procedures ----------------------------------------------------------------------------------
------------------------------------------------------------------------------- Producto ---------------------------------------------------------------------------------------
go
create procedure Insertar_Producto
@Nombre_producto nvarchar (50),
@Existencia_producto bigint,
@Cantidad_caja int,
@Costo_caja bigint,
@Precio_mayor bigint,
@Precio_vendedor bigint,
@Precio_publico bigint
as
insert into producto values(@Nombre_producto,@Existencia_producto,@Cantidad_caja,@Costo_caja,@Precio_mayor,@Precio_vendedor,@Precio_publico)
go
create proc Actualizar_Producto
@C_producto bigint,
@Nombre_producto nvarchar (50),
@Existencia_producto bigint,
@Cantidad_caja int,
@Costo_caja bigint,
@Precio_mayor bigint,
@Precio_vendedor bigint,
@Precio_publico bigint
as 
begin
update Producto set Nombre_Producto = @Nombre_producto, Existencia_producto = @Existencia_producto, Cantidad_caja = @Cantidad_Caja,
Costo_caja = @Costo_caja, Precio_mayor = @Precio_mayor, Precio_vendedor = @Precio_vendedor, Precio_publico = @Precio_publico where Codigo_producto = @C_producto 
Select*from producto
end 
------------------------------------------------------------------------------- Empleado ----------------------------------------------------------------------------------------
go
create procedure loggin
@nombre nvarchar (30),
@contraseña nvarchar(30),
@logg int output,
@mensaje nvarchar(50)output
as
Select @logg=count(h.Nombre_usu) from Empleado h
where Nombre_usu = @nombre and Contraseña = @contraseña
if (@logg > 0) 
select @mensaje = 'Bienvenido '+h.cargo_usuario+' '+h.Nombre_Empleado from Empleado h
where Nombre_usu = @nombre and Contraseña = @contraseña 
go
create procedure Registrar_Usu
@Nombre_usu nvarchar (30),
@Nombre_Empleado nvarchar (50),
@Contraseña_usu nvarchar (50),
@ID_usu bigint,
@Telefono_usu bigint,
@Correo_usu nvarchar (50),
@Estado_usu nvarchar (50),
@Cargo_usu nvarchar (20),
@mensaje nvarchar (50) output,
@Contador int output
as
Select @Contador = count (Nombre_usu) from Empleado where Nombre_usu = @Nombre_usu
if (@Contador > 0)
select @mensaje = 'El usuario ya ha sido registrado'
else
insert into Empleado values (@Nombre_usu, @Nombre_Empleado, @Contraseña_usu, @ID_usu, @Telefono_usu, @Correo_usu, @Estado_usu, @Cargo_usu)
select * from Empleado
go
create proc Actualizar_usu
@Nombre_usu nvarchar (30),
@Nombre_Empleado nvarchar (50),
@Contraseña_usu nvarchar (50),
@ID_usu bigint,
@Telefono_usu bigint,
@Correo_usu nvarchar (50),
@Estado_usu nvarchar (50),
@Cargo_usu nvarchar (20)
as
begin
update Empleado set  Nombre_empleado = @Nombre_Empleado, Contraseña = @Contraseña_usu, ID_usuario = @ID_usu, Telefono_usuario = @Telefono_usu, Correo_usuario = @Correo_usu, Estado_Usuario = @Estado_usu, Cargo_Usuario = @Cargo_usu where Nombre_usu = @Nombre_usu
Select * from Empleado
End
go
create proc Actualizar_usu2
@Nombre_usu nvarchar (30),
@Nombre_Empleado nvarchar (50),
@Contraseña_usu nvarchar (50),
@ID_usu bigint,
@Telefono_usu bigint,
@Correo_usu nvarchar (50),
@Estado_usu nvarchar (50),
@Cargo_usu nvarchar (20)
as
update Empleado set  Nombre_empleado = @Nombre_Empleado, ID_usuario = @ID_usu, Telefono_usuario = @Telefono_usu, Correo_usuario = @Correo_usu, Estado_Usuario = @Estado_usu, Cargo_Usuario = @Cargo_usu where Nombre_usu = @Nombre_usu
Select * from Empleado
------------------------------------------------------------------------------- Cliente ----------------------------------------------------------------------------------------
go
create procedure Registrar_Cli
@Numero_identificacion bigint,
@Nombre_cliente nvarchar (50),
@Correo_cliente nvarchar (50),
@Telefono_cliente bigint,
@Direccion_cliente nvarchar (50),
@mensaje nvarchar (50) output,
@Contador int output
as
Select @Contador = count (Numero_identificacion) from Cliente where Numero_identificacion = @Numero_identificacion
if (@Contador > 0)
select @mensaje = 'El cliente ya ha sido registrado'
else
insert into Cliente values (@Numero_identificacion,@Nombre_cliente, @Correo_cliente, @Telefono_cliente, @Direccion_cliente)
select * from Cliente
go
create proc Actualizar_cli
@Numero_identificacion bigint,
@Nombre_cliente nvarchar (50),
@Correo_cliente nvarchar (50),
@Telefono_cliente bigint,
@Direccion_cliente nvarchar (50)
as
begin
update Cliente set Nombre_cliente = @Nombre_cliente, Correo_cliente = @Correo_cliente, Telefono_cliente = @Telefono_cliente, Dirección_cliente = @Direccion_cliente where Numero_identificacion = @Numero_identificacion
End
---------------------------------------------------------------------------------- Venta ------------------------------------------------------------------------------------------
go
create proc Registrar_venta
@Nombre_u nvarchar (30),
@Numero_id bigint,
@Fecha_ve nvarchar (30),
@Valor_to bigint,
@Tipo_ve nvarchar (30)
as 
insert into Venta values(@Nombre_u,@Numero_id,@fecha_ve,@Tipo_ve,@Valor_to)
go
create proc Actualizar_venta
@codigo_ve bigint,
@Nombre_u nvarchar (30),
@Numero_id bigint,
@Fecha_ve nvarchar (30),
@Valor_to bigint
as
update Venta set Numero_identificacion = @numero_id, Fecha_venta= @fecha_ve, 
 Valor_total=@valor_to where Codigo_venta= @codigo_ve
go
Create proc registrar_3venta
@Codigo_ve bigint,
@cod_pro bigint,
@C_entrega int,
@C_recibida int,
@c_recarga int
as
insert into Detalle_venta values(@codigo_ve,@cod_pro,@C_entrega,@C_recibida,@C_recarga)
go
create proc Actualizar_3venta
@Codigo_ve bigint,
@cod_pro bigint,
@C_entrega int,
@C_recibida int
as
update Detalle_venta set Cantidad_entrega=@C_entrega, Cantidad_recibida=@C_recibida
where Codigo_venta = @codigo_ve and Codigo_producto= @cod_pro
------------------------------------------------------------------------------- Proveedor ----------------------------------------------------------------------------------------
go
create procedure Registrar_Prove
@Nit_proveedor bigint,
@Nombre_proveedor nvarchar (50),
@Direccion nvarchar (50),
@Telefono bigint,
@Correo_proveedor nvarchar (50), 
@mensaje nvarchar (50) output,
@Contador int output
as
Select @Contador = count (Nit_proveedor) from Proveedor where Nit_proveedor = @Nit_proveedor
if (@Contador > 0)
select @mensaje = 'El proveedor ha sido registrado anteriormente'
else
insert into Proveedor values (@Nit_proveedor, @Nombre_proveedor, @Direccion, @Telefono, @Correo_proveedor)
select * from Proveedor
go
create proc Actualizar_Prove
@Nit_proveedor bigint,
@Nombre_proveedor nvarchar (50),
@Direccion nvarchar (50),
@Telefono bigint,
@Correo_proveedor nvarchar (50)
as
begin
update Proveedor set  Nombre_proveedor = @Nombre_proveedor, Direccion = @Direccion, Teléfono = @Telefono, Correo_proveedor = @Correo_proveedor where Nit_proveedor = @Nit_proveedor
Select * from Proveedor
End
go
----------------------------------------------------------------------------- Dotación ---------------------------------------------------------------------------------------
create proc Registrar_dotacion 
@Nombre_dotacion nvarchar (50),
@Cantidad_dotacion int,
@Cantidad_libre int
as 
insert into Dotacion values ( @Nombre_dotacion, @Cantidad_dotacion,@Cantidad_libre) 
go
create proc Actualizar_dotacion 
@Nombre_dotacion nvarchar (50),
@Cantidad_dotacion bigint 
as 
update Dotacion set Cantidad_dotacion = @Cantidad_dotacion where Nombre_dotacion = @Nombre_dotacion
go
------------------------------------------------------------------------------- Pedido ----------------------------------------------------------------------------------------

create proc Registrar_Pedido
@Nit_proveedor bigint,
@Nombre_usu nvarchar (30),
@Fecha_pedido nvarchar (30),
@Fecha_entrega nvarchar(30),
@Valor_total bigint, 
@Entregado nvarchar(5)
as 
insert into Pedido values(@Nit_proveedor,@Nombre_usu,@Fecha_pedido,@Fecha_entrega,@Valor_total,@Entregado)
go
create proc Actualizar_Pedido
@Codigo_pe bigint,
@Nit_proveedor bigint,
@Nombre_usu nvarchar (30),
@Fecha_pedido nvarchar (30),
@Fecha_entrega nvarchar(30),
@Valor_total bigint, 
@Entregado nvarchar(5)
as
update Pedido set Nit_proveedor = @Nit_proveedor, Nombre_usu= @Nombre_usu, Fecha_pedido= @Fecha_pedido,
Fecha_entrega = @Fecha_entrega,Valor_total=@valor_total, Entregado=@Entregado where Codigo_pedido= @codigo_pe
go
Create proc registrar_3pedido
@Codigo_pedido bigint,
@Codigo_producto bigint,
@Cantidad_producto int
as
insert into Detalle_pedido values(@codigo_pedido,@Codigo_producto,@Cantidad_producto)
go
create proc Actualizar_3pedido
@Codigo_pedido bigint,
@Codigo_producto bigint,
@Cantidad_producto int
as
update Detalle_pedido set Cantidad_producto =@Cantidad_producto 
where Codigo_pedido = @codigo_pedido and Codigo_producto= @Codigo_producto
-------------------------------------------------------------------------------- Maquinaria -----------------------------------------------------------------------------------------
go
create proc Registrar_maquinaria
@Nombre nvarchar(50),
@Nombre_usu nvarchar(30),
@Fecha nvarchar (30),
@Tipo nvarchar (30),
@Estado nvarchar (30)
as
insert into Maquinaria values (@Nombre, @Nombre_usu, @Fecha, @Tipo, @Estado)
go
create proc Actualizar_maquinaria
@Codigo bigint,
@Nombre nvarchar(50),
@Nombre_usu nvarchar(30),
@Fecha nvarchar (30),
@Tipo nvarchar (30),
@Estado nvarchar (30)
as
update Maquinaria set Nombre_maquinaria = @Nombre, Nombre_usu = @Nombre_usu, Fecha_cambio = @Fecha, Tipo_maquina = @Tipo, Estado_maquina = @Estado where Codigo_maquinaria = @Codigo
go
----------------------------------------------------------------------------- Transacción ---------------------------------------------------------------------------------------
create procedure Registrar_trans
@Codigo_pedido bigint,
@Codigo_venta bigint,
@Descripcion nvarchar (30),
@Fecha_trans nvarchar (50),
@Ingresos_trans bigint,
@Gastos_trans bigint,
@Capital_total bigint
as
insert into Transaccion values(@Codigo_pedido,@Codigo_venta,@Descripcion,@Fecha_trans,@Ingresos_trans,@Gastos_trans,@Capital_total)
go
create procedure Actualizar_TransV
@Codigo bigint,
@Ingresos bigint,
@Total bigint
as
update transaccion set ingresos_trans = @ingresos, Capital_total = @Total where Codigo_trans = @Codigo
go
create procedure Actualizar_TransP
@Codigo bigint,
@gastos bigint,
@Total bigint
as
update transaccion set gastos_trans = @gastos, Capital_total = @Total where Codigo_trans = @Codigo
go
create view Consulta_transaccion
as
select Codigo, Codigo_pedido as "Codigo pedido", Codigo_venta as "Codigo Venta", Descripcion as Descripción, Fecha_trans as Fecha, Ingresos_trans as Ingresos, Gastos_trans as Gastos, Capital_total as "Capital actual" from transaccion where Descripcion = 'Empresarial'
---------------------------------------------------------------------------------- Vistas -------------------------------------------------------------------------------------------
go
create view Consulta_transaccion2
as 
select Codigo, Descripcion, Ingresos_trans as Total, Capital_total from transaccion where Descripcion like '% Individual' 
go
create view Consulta_maquinaria
as
select Codigo, Nombre_maquinaria as Nombre, Fecha_cambio as Fecha, Tipo_Maquina as Tipo, Estado_maquina as Estado from maquinaria
go
create view Consulta_venta
as
select c.Nombre_cliente as Nombre, v.Valor_total as Total, e.Nombre_empleado as Empleado, v.fecha_venta as Fecha from Venta as v inner join Cliente as c on v.Numero_identificacion = c.Numero_identificacion inner join Empleado as e on v.Nombre_usu = e.Nombre_usu
go
create view consulta_empleado
as
Select nombre_usu as Usuario, nombre_empleado as Nombres, ID_usuario as ID, Telefono_usuario as Teléfono, Correo_usuario as "E-mail", Estado_usuario as Estado, cargo_usuario as Cargo from Empleado
go
create procedure multi
@Fecha nvarchar (100)
as
select cliente.Nombre_cliente as Nombre,  venta.Fecha_venta as Fecha, venta.codigo as Codigo from venta inner join Cliente on Venta.Numero_identificacion = cliente.numero_identificacion where venta.fecha_venta = @fecha
go
create view Consulta_pedido
as
select pv.Nombre_proveedor as Nombre, p.Valor_total as Total, e.Nombre_empleado as Empleado, p.fecha_pedido as "Fecha pedido", p.Fecha_entrega as "Fecha entrega" from Pedido as p inner join Proveedor as pv on p.Nit_proveedor = pv.Nit_proveedor inner join Empleado as e on p.Nombre_usu = e.Nombre_usu
go
create view Consulta_Dotacion
as 
Select Nombre_dotacion as Nombre, Cantidad_dotacion as Existencia, Cantidad_libre as "Cantidad libre" from Dotacion
go
create proc cargar_3pedido
@codi int,
@fecha nvarchar(50)
as
select producto.nombre_producto as Nombre, detalle_pedido.cantidad_producto as "Cantidad (cajas)" from detalle_pedido inner join producto on detalle_pedido.Codigo_producto = producto.codigo_producto inner join pedido on pedido.codigo_pedido = detalle_pedido.codigo_pedido where pedido.codigo_pedido = @codi and pedido.fecha_pedido = @fecha
go
create view consulta_producto
as
select Codigo, Nombre_producto as Nombre, Existencia_producto as Existencia, Cantidad_caja as "Cantidad por caja", Costo_caja as "Costo por caja", Precio_mayor as "Precio por mayor", Precio_vendedor as "Precio al vendedor", Precio_publico as "Precio al Público" from producto
go
create proc consultar_llegada
@fecha nvarchar(30)
as
select * from pedido where fecha_entrega = @fecha
go
create view Consulta_cliente
as
select Numero_identificacion as ID, Nombre_cliente as Nombres, Correo_cliente as "e-mail", telefono_cliente as Teléfono, Dirección_cliente as Dirección from cliente 
go
Create proc Consulta_3venta
@Codigo_ven nvarchar (50),
@Fecha_ven nvarchar (50)
as
select Nombre_producto as "Nombre producto", Cantidad_Entrega as "Entrega", Cantidad_Recibida as "Recibida", Cantidad_recarga as "Recarga" from detalle_venta  inner join producto on producto.Codigo_producto = detalle_venta.Codigo_producto inner join venta on  venta.Codigo_venta = detalle_venta.Codigo_venta where venta.Codigo_venta = @Codigo_ven and venta.Fecha_venta = @Fecha_ven
go
Create proc Consulta_3pedido
@Codigo_ped nvarchar (50),
@Fecha_entrega nvarchar (50)
as
select Nombre_producto as "Nombre producto", Cantidad_producto as "Cantidad(cajas)" from detalle_pedido  inner join producto on producto.Codigo_producto = detalle_pedido.Codigo_producto inner join pedido on  pedido.Codigo_pedido = detalle_pedido.Codigo_pedido where pedido.Codigo_pedido = @Codigo_ped and pedido.Fecha_entrega = @Fecha_entrega
------------------------------------------------------------------------------- Busqueda ----------------------------------------------------------------------------------------
go
create proc busca_producto
@Nombre nvarchar (100)
as
select Codigo, Nombre_producto as Nombre, Existencia_producto as Existencia, Cantidad_caja as "Cantidad por caja", Costo_caja as "Costo por caja", Precio_mayor as "Precio por mayor", Precio_vendedor as "Precio al vendedor", Precio_publico as "Precio al Público" 
from producto where Nombre_producto like @Nombre+'%'
go
create proc busca_empleado
@Nombre nvarchar(100)
as
Select nombre_usu as Usuario, nombre_empleado as Nombres, ID_usuario as ID, Telefono_usuario as Teléfono, Correo_usuario as "E-mail", Estado_usuario as Estado, cargo_usuario as Cargo 
from Empleado where nombre_empleado like '%'+@nombre+'%'
go 
create proc busca_maquinaria
@Nombre nvarchar(100)
as
select Codigo, Nombre_maquinaria as Nombre, Fecha_cambio as Fecha, Tipo_Maquina as Tipo, Estado_maquina as Estado
from maquinaria where Nombre_maquinaria like @nombre+'%'
go
create proc busca_proveedor
@Nombre nvarchar(100)
as
select Nit_proveedor as Nit, Nombre_proveedor as Nombre, Direccion, Teléfono, Correo_proveedor as Correo 
from Proveedor where Nombre_proveedor like @Nombre+'%'
go
create proc busca_dotacion
@Nombre nvarchar (100)
as
Select Nombre_dotacion as Nombre, Cantidad_dotacion as Existencia, Cantidad_libre as "Cantidad libre" 
from Dotacion where Nombre_dotacion like @Nombre +'%'
go
create proc busca_cliente 
@Nombre nvarchar (100)
as 
select Numero_identificacion as ID, Nombre_cliente as Nombre, Correo_cliente as "e-mail", telefono_cliente as Teléfono, Dirección_cliente as Dirección 
from cliente where Nombre_cliente like ''+@Nombre +'%'


------------------------------------------------------------------------------- Consultas ----------------------------------------------------------------------------------------

select * from pedido
select * from Detalle_pedido
select * from producto
select * from Empleado
select * from detalle_venta
select * from venta
select * from consultar_clientes
select * from transaccion
delete pedido
delete Proveedor
delete detalle_venta
delete venta
delete transaccion
delete detalle_pedido
update producto set existencia_producto = 20 where codigo_producto > 0
update empleado set Nombre_usu = '""', contraseña = '""'
select Pedido.Codigo as Código, Pedido.Fecha_pedido as Fecha, Empleado.Nombre_Empleado as Nombre, Pedido.Valor_total as Total from pedido inner join Empleado on Pedido.Nombre_usu = Empleado.Nombre_usu
update pedido set fecha_entrega = '20/05/2016' where codigo_pedido >0
update producto set existencia_producto =70 where codigo_producto >0
*----------------------------------------------------------------------------------------Inicio---------------------------------------------------------------------------------------------------------*
insert into empleado values('admin','Juandro','admin',1234567890,987987987,'juandro@cremhelado.com','Activo','Administrador')
insert into cliente values (65423,'Aleblo','aleblo@gmail.com',5789641,'cll68#ss42')
insert into Venta values ('raven',65423,'30/05/2016','Individual',1000)
insert into Transaccion values (null,null,'Empresarial','30/05/2016',100000,null,1000000)

Select  Cliente.Nombre_cliente, Venta.Fecha_venta, Venta.Valor_total from Cliente inner join Venta on Cliente.Numero_identificacion = Venta.Numero_identificacion  
select * from Consulta_transaccion



----------------------------------------------------------------- Microempresa BETA ------------------------------------------------------------------------------------------------------------------------

insert into empleado values('Lucho','Luis Guillermo Castiblanco Romero','4572',3021500,5719504,'me.mito057@hotmail.com','Activo','Administrador')
insert into Transaccion values (null,null,'Empresarial','12/11/2016',null,null,2000000)

select *from 
