USE MASTER
GO
DROP DATABASE LIBRERIA_DB
GO

CREATE DATABASE LIBRERIA_DB	COLLATE Latin1_general_CI_AI
GO

USE LIBRERIA_DB
GO
CREATE TABLE CATEGORIAS (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(100) NOT NULL,
	Activo BIT NOT NULL DEFAULT(1)
	)
GO
CREATE TABLE PROVEEDORES (
	Id INT PRIMARY KEY IDENTITY (1,1),
	Nombre VARCHAR(50) NOT NULL,
	CUIT VARCHAR(15) NOT NULL,
	Email VARCHAR(50),
	Telefono VARCHAR(30) NOT NULL,
	Direccion VARCHAR(50),
	Activo BIT NOT NULL DEFAULT(1)
	)
GO
CREATE TABLE MARCAS(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(100) NULL,
	IdProveedor INT NOT NULL FOREIGN KEY REFERENCES PROVEEDORES(Id),
	ImagenUrl VARCHAR(1000) NULL,
	Activo BIT NOT NULL DEFAULT(1)
	)
GO

CREATE TABLE ARTICULOS (
    Id BIGINT PRIMARY KEY IDENTITY (1,1),
    Codigo VARCHAR(6) NOT NULL,	
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(100) NOT NULL,
    IdMarca INT NULL FOREIGN KEY REFERENCES MARCAS(Id),
    IdCategoria INT NULL FOREIGN KEY REFERENCES CATEGORIAS(Id),
    Ganancia_Porcentaje DECIMAL(4,2) NOT NULL CHECK (Ganancia_Porcentaje >= 0),
	Imagen VARCHAR(500) NULL,
    Stock_Minimo INT NOT NULL CHECK (Stock_Minimo >= 0),
	Activo BIT NOT NULL DEFAULT(1)
  )
GO

CREATE TABLE COMPRAS (
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	FechaCompra DATE NOT NULL,
	IdProveedor INT NOT NULL FOREIGN KEY REFERENCES PROVEEDORES(Id),
	TotalCompra MONEY NOT NULL
)
GO

CREATE TABLE DETALLE_COMPRAS(
	Id BIGINT PRIMARY KEY IDENTITY (1,1),
	IdCompra BIGINT NOT NULL FOREIGN KEY REFERENCES COMPRAS(Id),
	IdArticulo BIGINT NOT NULL FOREIGN KEY REFERENCES ARTICULOS(Id),
	Precio MONEY NOT NULL,
	Cantidad INT NOT NULL,
)
GO

CREATE TABLE CLIENTES (
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(30) NOT NULL,
	Apellido VARCHAR(30) NOT NULL,
	DNI VARCHAR(10) NOT NULL,
	Telefono VARCHAR (15) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	Direccion VARCHAR(50) NOT NULL,
	Activo BIT NOT NULL DEFAULT(1)
)
GO

CREATE TABLE VENTAS (
	Id BIGINT PRIMARY KEY IDENTITY(1,1),
	FechaVenta DATE NOT NULL,
	IdCliente BIGINT NOT NULL FOREIGN KEY REFERENCES CLIENTES(Id),
	TotalVenta MONEY NOT NULL,
)
GO

CREATE TABLE DETALLE_VENTAS(
	Id BIGINT PRIMARY KEY IDENTITY (1,1),
	IdVenta BIGINT NOT NULL FOREIGN KEY REFERENCES VENTAS(Id),
	IdArticulo BIGINT NOT NULL FOREIGN KEY REFERENCES ARTICULOS(Id),
	Cantidad INT NOT NULL
)
GO

CREATE TABLE DATOS_ARTICULOS(
	Id BIGINT PRIMARY KEY IDENTITY (1,1),
	IdArticulo BIGINT NOT NULL FOREIGN KEY REFERENCES ARTICULOS(Id),
	Fecha DATE NOT NULL,
	Stock INT NOT NULL,
	Precio MONEY NOT NULL
)
GO

CREATE TABLE USUARIOS(
	Id INT PRIMARY KEY IDENTITY (1,1),
	Nombre VARCHAR(50) NULL,
	Apellido VARCHAR(50) NULL,
	Email NVARCHAR(50) NULL,
	Usuario VARCHAR(30) NOT NULL,
	Contraseña NVARCHAR(50) NOT NULL,
	Imagen VARCHAR(30) NULL,
	Tipo BIT NOT NULL DEFAULT(0)
)
GO
