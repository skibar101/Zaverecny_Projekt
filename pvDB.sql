use skibar;

create table player(
    id int primary key identity(1,1),
    firstName varchar(20),
    lastName varchar(20),
    email varchar(20) unique,
    passw varchar(20),
    money int default 500,
    ban bit
);

create table bet(
    id int primary key identity(1,1),
    date_of_bet date,
    amount int, 
    win bit,
    result int, 
    player_id int foreign key references player(id)
);

go
create trigger UpdateMoney on bet after insert
as
begin
    declare @player_id int;
    declare @money int;
    declare @win bit;

    select @player_id = player_id, @money = amount, @win = win from inserted;

    if @win = 1
    begin
        update player
        set money = money + @money
        where id = @player_id;
    end
    else
    begin
        update player
        set money = money - @money
        where id = @player_id;
    end

    if (select money from player where id = @player_id) <= 0
    begin
        update player
        set ban = 1
        where id = @player_id;
    end
end
go