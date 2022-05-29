# Порахувати кількість проданового товару

SELECT PR.product,
       SUM(BP.amount) as total_amount
FROM product PR
INNER JOIN bill_product BP on PR.product_id = BP.product_id
GROUP BY product
ORDER BY total_amount DESC