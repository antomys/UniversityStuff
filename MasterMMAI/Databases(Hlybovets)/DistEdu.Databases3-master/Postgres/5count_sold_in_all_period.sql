-- Порахувати скільки було придбано товару А в усіх магазинах за період С

SELECT
       STRING_AGG(DISTINCT store_name,';') as stores,
       Sum(bp.amount) as sold_amount,
       pr.price as product_price,
       Sum(bp.amount * pr.price) as total_revenue
FROM shop.public.product as pr
INNER JOIN bill_product bp
    ON bp.product_id = pr.product_id
INNER JOIN store str
    ON bp.store_id = str.store_id
INNER JOIN bill bd
    ON bd.bill_date BETWEEN '1996-07-01' AND '2021-05-19'
           AND bp.bill_id = bd.bill_id
WHERE pr.product = 'Milk'
GROUP BY product, price