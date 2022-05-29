create or replace function random_key() returns int
    language plpgsql
as
$$
    declare
        result integer := 1;
begin
    result := (select product_id from shop.public.product order by random() limit 1);

    return result;
    end;
$$;