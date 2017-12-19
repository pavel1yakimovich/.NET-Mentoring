create table T1
(
	Id int Not null,
	Name nvarchar(max) null
);
go;

create role [ReaderRole];
go;

GRANT SELECT ON OBJECT::T1 TO ReaderRole;
go;