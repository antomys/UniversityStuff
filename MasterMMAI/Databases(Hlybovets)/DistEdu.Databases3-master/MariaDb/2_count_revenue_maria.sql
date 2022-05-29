# Порахувати вартість проданого товару

SELECT PR.product,
       Sum(BP.amount * PR.price) as total_revenue
FROM product PR
INNER JOIN bill_product BP
    ON PR.product_id = BP.product_id
GROUP BY PR.product
ORDER BY total_revenue DESC