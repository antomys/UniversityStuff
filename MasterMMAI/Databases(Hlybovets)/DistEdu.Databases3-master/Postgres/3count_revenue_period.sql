-- Порахувати вартість проданого товару за період

SELECT pr.product,
       Sum(bp.amount * pr.price) as total_revenue
FROM shop.public.product pr
INNER JOIN bill_product bp
    ON pr.product_id = bp.product_id
INNER JOIN bill bl
    ON bp.bill_id = bl.bill_id
           AND bl.bill_date BETWEEN '2020-03-01' AND '2020-07-14'
GROUP BY pr.product
ORDER BY total_revenue DESC