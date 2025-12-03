use DBCarrito
go

create table CATEGORIA(
	idCategoria int primary key identity,
	description varchar(100),
	activo bit default 1,
	fechaRegistro datetime default getdate()
)
go

create table MARCA(
	idMarca int primary key identity,
	description varchar(100),
	activo bit default 1,
	fechaRegistro datetime default getdate()
)
go

create table PRODUCTO(
	idProducto int primary key identity,
	nombre varchar(500),
	description varchar(500),
	idMarca int references MARCA(idMarca),
	idCategoria int references CATEGORIA(idCategoria),
	precio decimal(10,2) default 0,
	stock int,
	rutaImagen varchar(100),
	nombreImagen varchar(100),
	activo bit default 1,
	fechaRegistro datetime default getdate()
)
go

create table CLIENTE(
	idCliente int primary key identity,
	nombres varchar(100),
	apellidos varchar(100),
	correo varchar(100),
	clave varchar(150),
	reestablecer bit default 0,
	fechaRegistro datetime default getdate()
)
go

create table CARRITO(
	idCarrito int primary key identity,
	idCliente int references CLIENTE(idCliente),
	idProducto int references PRODUCTO(idProducto),
	cantidad int
)
go

create table VENTA(
	idVenta int primary key identity,
	idCliente int references CLIENTE(idCliente),
	totalProducto int,
	montoTotal decimal(10,2),
	contacto varchar(50),
	idDistrito varchar(10),
	telefono varchar(50),
	direccion varchar(500),
	idTransaccion varchar(50),
	fechaVenta datetime default getdate()
)
go

create table DETALLE_VENTA(
	idDetalleVenta int primary key identity,
	idVenta int references VENTA(idVenta),
	idProducto int references PRODUCTO(idProducto),
	cantidad int,
	total decimal(10,2)
)
go

create table USUARIO(
	idUsuario int primary key identity,
	nombres varchar(100),
	apellidos varchar(100),
	correo varchar(100),
	clave varchar(150),
	reestablecer bit default 1,
	activo bit default 1,
	fechaRegistro datetime default getdate()
)
go

create table DEPARTAMENTO(
	idDepartamento varchar(2) not null,
	description varchar(50) not null
)
go

create table PROVINCIA(
	idProvincia varchar(4) not null,
	description varchar(50),
	idDepartamento varchar(2) not null,
)
go

create table DISTRITO(
	idDistrito varchar(10) not null,
	description varchar(50) not null,
	idProvincia varchar(4) not null,
	idDepartamento varchar(2) not null
)
go