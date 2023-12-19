select * from Actores

select * from Actuaciones ac
left join Actores a
on ac.id_actor = a.id_actor
where ac.id_actor = 1010

select count (*) from Actuaciones