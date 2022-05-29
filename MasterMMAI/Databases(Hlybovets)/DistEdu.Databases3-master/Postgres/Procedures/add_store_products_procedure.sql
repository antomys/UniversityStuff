CREATE OR REPLACE PROCEDURE add_default_products()
LANGUAGE plpgsql AS $$
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
end;
    $$


