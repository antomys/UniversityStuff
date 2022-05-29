-- Порахувати сумарну виручку магазинів за період С

SELECT
    str.store_name,
    STRING_AGG(DISTINCT pr.product,';') as products,
    Sum(bp.amount) as sold_amount,
    Sum(bp.amount * pr.price) as total_revenue
FROM shop.public.product as pr
INNER JOIN bill_product bp
    ON pr.product_id = bp.product_id
INNER JOIN bill b
    ON bp.bill_id = b.bill_id
        AND b.bill_date BETWEEN '1996-07-01' AND '2024-05-14'
INNER JOIN store str
    ON bp.store_id = str.store_id
GROUP BY str.store_name
