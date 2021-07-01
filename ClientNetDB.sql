USE master
GO

IF EXISTS(select *	from sys.databases where name = 'NetDB')
	DROP DATABASE NetDB;
CREATE DATABASE NetDB
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NetDB', FILENAME = N'D:\IDEs\msserver\MSSQL14.MSSQLSERVER\MSSQL\DATA\NetDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NetDB_log', FILENAME = N'D:\IDEs\msserver\MSSQL14.MSSQLSERVER\MSSQL\DATA\NetDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

use NetDB
GO

CREATE TABLE Client(
	ClientId int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(100) NOT NULL,
	Gender varchar(7) NOT NULL CHECK (Gender = 'Женский' OR Gender = 'Мужской'),
	Age int NULL,
)

use NetDB
GO

CREATE TABLE Net(
	NetId int PRIMARY KEY REFERENCES Client(ClientId),
	IpAddress varchar(19) NOT NULL CHECK (IpAddress like '%.%.%.%/%'),
	Info varchar(500) NULL,
)

insert into Client (Name, Gender, Age) values ('Иванов Иван Иванович', 'Мужской', 35)
insert into Client (Name, Gender, Age) values ('Яковлев Карл Богуславович', 'Мужской', 26)
insert into Client (Name, Gender, Age) values ('Фролов Ярослав Демьянович', 'Мужской', 44)
insert into Client (Name, Gender, Age) values ('Мамонтова Зарина Олеговна', 'Женский', 50)
insert into Client (Name, Gender, Age) values ('Горбачёва Феодосия Митрофановна', 'Женский', 29)

