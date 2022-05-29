# Порахувати скільки було придбано товару А в усіх магазинах за період С

SELECT
       GROUP_CONCAT(DISTINCT store_name separator ';') as stores,
       Sum(bp.amount) as sold_amount,
       pr.price as product_price,
       Sum(bp.amount * pr.price) as total_revenue
FROM shop.product as pr
INNER JOIN shop.bill_product bp
    ON bp.product_id = pr.product_id
INNER JOIN shop.store str
    ON bp.store_id = str.store_id
INNER JOIN shop.bill bd
    ON bd.bill_date BETWEEN '1996-07-01' AND '2022-05-19'
           AND bp.bill_id = bd.bill_id
WHERE pr.product = 'Milk'
GROUP BY pr.product, pr.price