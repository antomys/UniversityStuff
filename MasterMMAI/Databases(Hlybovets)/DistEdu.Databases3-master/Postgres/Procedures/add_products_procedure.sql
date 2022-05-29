CREATE OR REPLACE PROCEDURE add_products()
LANGUAGE plpgsql AS $$
DECLARE rand_name varchar;
DECLARE str_id int;
DECLARE prd_id int;
DECLARE bl_id int;
begin
    if not exists (select 1 from shop.public.product where product.product = 'Bread') then
        Insert Into shop.public.product(product_id, product, price) values (1, 'Bread', floor(random() * 50 + 1)::int);
    end if;

    if not exists (select 1 from shop.public.product where product.product = 'Milk') then
         Insert Into shop.public.product(product_id, product, price) values (2, 'Milk', floor(random() * 50 + 1)::int);
    end if;

    if not exists (select 1 from shop.public.product where product.product = 'Butter') then
         Insert Into shop.public.product(product_id, product, price) values (3, 'Butter', floor(random() * 50 + 1)::int);
    end if;

    for i in 1..100 loop
        rand_name := random_string(10);
        Insert Into shop.public.store(store_name) values (rand_name) RETURNING store_id into str_id;

        Insert Into shop.public.store_product(store_id, product_id) values (str_id, 1);
        Insert Into shop.public.store_product(store_id, product_id) values (str_id, 2);
        Insert Into shop.public.store_product(store_id, product_id) values (str_id, 3);

        for j in 1..1000 loop
            rand_name := random_string(10);
            if not exists (select 1 from shop.public.product where product.product = rand_name) then
                 Insert Into shop.public.product(product, price) values (rand_name, floor(random() * 50 + 1)::int) returning product_id into prd_id;
            else
                SELECT prd_id = product_id FROM shop.public.product WHERE product.product = rand_name;
            end if;

            Insert Into shop.public.store_product(store_id, product_id) values (str_id, prd_id);
            Insert Into shop.public.bill(bill, bill_date) values (random_string(10), now() - random() * INTERVAL '2 years') returning bill_id into bl_id;

            if i < 10 then
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, floor(random() * 3 + 1)::int, str_id,  random() * 50 + 1);
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, prd_id, str_id,  random() * 50 + 1);
            elseif i > 10 and i < 30 then
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, 1, str_id,  random() * 50 + 1);
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, 2, str_id,  random() * 50 + 1);
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, prd_id, str_id,  random() * 50 + 1);
            elseif i > 30 and i > 60 then
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, 1, str_id,  random() * 50 + 1);
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, 2, str_id,  random() * 50 + 1);
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, 3, str_id,  random() * 50 + 1);
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, prd_id, str_id,  random() * 50 + 1);
            else
                Insert Into shop.public.bill_product(bill_id, product_id, store_id, amount) values (bl_id, prd_id, str_id,  random() * 50 + 1);
            end if;
        end loop;
    end loop;
end;
    $$


