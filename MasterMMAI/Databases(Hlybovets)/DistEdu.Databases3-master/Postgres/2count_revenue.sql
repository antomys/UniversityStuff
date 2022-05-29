-- Порахувати вартість проданого товару

SELECT shop.public.product.product,
       Sum(shop.public.bill_product.amount * shop.public.product.price) as total_revenue
FROM shop.public.product
INNER JOIN bill_product
    ON shop.public.product.product_id = bill_product.product_id
GROUP BY shop.public.product.product
ORDER BY total_revenue DESC