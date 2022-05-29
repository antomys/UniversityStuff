-- Порахувати скільки було придбано товару А в мазазині В за період С

SELECT shop.public.product.product,
       shop.public.store.store_name,
       Sum(shop.public.bill_product.amount) as sold_amount
FROM shop.public.product
INNER JOIN store
    ON store_name = '95Zrzmzcm0'
INNER JOIN bill_product
    ON shop.public.store.store_id = bill_product.store_id
INNER JOIN bill
    ON bill_product.bill_id = bill.bill_id
           AND shop.public.bill.bill_date
               BETWEEN '1996-07-01' AND '2024-05-20'
WHERE shop.public.product.product = 'Milk'
GROUP BY shop.public.product.product, shop.public.store.store_name