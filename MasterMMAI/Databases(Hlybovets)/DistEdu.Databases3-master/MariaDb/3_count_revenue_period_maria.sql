# Порахувати вартість проданого товару за період

SELECT PR.product,
       Sum(BP.amount * PR.price) as total_revenue
FROM shop.product PR
INNER JOIN shop.bill_product BP
    ON PR.product_id = BP.product_id
INNER JOIN shop.bill BL
    ON BP.bill_id = BL.bill_id
           AND BL.bill_date
               BETWEEN '2022-03-01' AND '2022-07-31'
GROUP BY PR.product
ORDER BY total_revenue DESC