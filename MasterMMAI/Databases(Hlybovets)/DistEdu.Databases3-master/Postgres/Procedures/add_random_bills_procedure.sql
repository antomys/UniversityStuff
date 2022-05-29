CREATE OR REPLACE PROCEDURE add_products()
LANGUAGE plpgsql AS $$
DECLARE rand_name varchar;
DECLARE str_id int;
DECLARE prd_id int;
DECLARE bl_id int;
DECLARE rand_id int;
begin
    for i in 1..40000 loop
        if i < 10000 then
            Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount)
            values
                ((select bill.bill_id from shop.public.bill order by random() limit 1),
                 (select product_id from shop.public.product order by random() limit 1),
                 (select store_id from shop.public.st order by random() limit 1),
                 (random() * 50 + 1));
            Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, prd_id - 2, str_id,  floor(random() * 50 + 1)::int);
            elseif i > 10000 and j < 20000 then
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id,prd_id - 3, str_id,  floor(random() * 50 + 1)::int);
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, prd_id - 2, str_id,  floor(random() * 50 + 1)::int);
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id,prd_id - 1, str_id,  floor(random() * 50 + 1)::int);
            end if;
    end loop;
    end;
    $$
