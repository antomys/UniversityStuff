-- Порахувати кількість проданового товару

SELECT shop.public.product.product,
       SUM(shop.public.bill_product.amount) as total_amount
FROM shop.public.product
INNER JOIN bill_product
    ON shop.public.product.product_id = bill_product.product_id
GROUP BY shop.public.product.product
ORDER BY total_amount DESC