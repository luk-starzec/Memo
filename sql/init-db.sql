
create schema memo

go

create table memo.Memos
(
	Url nvarchar(100) not null,
	Title nvarchar(100) not null,
	Created datetime2(0) not null,
	Expires date null,
	IV nvarchar(200) not null,
	Data nvarchar(4000) not null,
	constraint PK_Memos primary key (Url)
)
