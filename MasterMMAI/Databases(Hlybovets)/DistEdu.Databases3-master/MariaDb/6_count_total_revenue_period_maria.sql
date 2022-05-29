# Порахувати сумарну виручку магазинів за період С

SELECT
    str.store_name,
    GROUP_CONCAT(DISTINCT pr.product separator ';') as products,
    Sum(bp.amount) as sold_amount,
    Sum(bp.amount * pr.price) as total_revenue
FROM shop.product as pr
INNER JOIN shop.bill_product bp
    ON pr.product_id = bp.product_id
INNER JOIN shop.bill b
    ON bp.bill_id = b.bill_id
        AND b.bill_date BETWEEN '1996-07-01' AND '2021-05-14'
INNER JOIN shop.store str
    ON bp.store_id = str.store_id
GROUP BY str.store_name
