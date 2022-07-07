create database Hotel
use Hotel

create table Users(
id int identity not null,
Username nvarchar(70),
pass nvarchar(500),
)
insert into Users values('Admin','1234')

create table Room(
Id_room int not null identity primary key,
num_chambre int,
type_chambre int foreign key  references catégorie(Id_cat),
prix_chambre money,
Disponibilite bit 
)

select r.num_chambre,c.cat,r.prix_chambre,r.Disponibilite
from Room r, catégorie c where r.type_chambre=c.Id_cat

select r.Id_room, r.num_chambre 
from Room r, catégorie c
where c.Id_cat=r.type_chambre and c.cat like 'Double'





alter table Room
alter column prix_chambre decimal(16,0)

drop table Room
create trigger trRoom01 on Room instead of insert
as 
begin declare @NewNum int
set @NewNum =(select num_chambre from inserted)
declare @nbr int
set @nbr=(select count(num_chambre) from Room where num_chambre=@NewNum)
if(@nbr=0)
begin
print'Existe aucune chambre avec ce numéro. Ajout réussi!'
insert into Room select num_chambre ,type_chambre ,prix_chambre,Disponibilite from inserted
end 
else
begin
print 'Existe déjâ une chambre avec ce numéro. Ajout échoué !'
rollback 
end
end

insert into Room values(101,'Single',250)

create table catégorie(
Id_cat int not null primary key identity,
cat nvarchar(70)
)





insert into catégorie values
('Single'),
('Double'),
('Suite')

create table Customer(
Id int primary key identity not null,
cin nvarchar(50) not null,
nom nvarchar(100),
prenom nvarchar(100),
numtel nvarchar(20),
type_chambre int foreign key references catégorie(Id_cat),
num_chambre int foreign key references Room(Id_room),
check_in date,
check_out date,
nbr_jour int,
prix decimal(16,0),
)

select prix_chambre from Room where num_chambre like '101'



drop table Customer
--alter table Customer
--add constraint TEL check (numtel like '[0][6 7][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
update Room set type_chambre= 1 , prix_chambre= 250 , Disponibilite= 'True' where num_chambre = 101

select prix_chambre from Room where num_chambre like '101'
delete Customer where cin = 'j553429';

select * from Customer where type_chambre like '2'

select c.cin , c.nom , c.prenom , r.num_chambre , c.check_in from Customer c , catégorie cat,Room r where c.type_chambre=cat.Id_cat and r.type_chambre=cat.Id_cat and c.num_chambre= r.Id_room and c.check_in