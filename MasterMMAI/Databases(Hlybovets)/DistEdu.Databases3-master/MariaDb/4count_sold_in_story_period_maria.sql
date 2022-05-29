# Порахувати скільки було придбано товару А в мазазині В за період С

SELECT PR.product,
       SR.store_name,
       Sum(BP.amount) as sold_amount
FROM shop.product PR
Inner join store_product sp
    on PR.product_id = sp.product_id
INNER JOIN shop.store SR
    ON SR.store_name = 'fivqfrqStY' and sp.store_id = SR.store_id
INNER JOIN shop.bill_product BP
    ON SR.store_id = BP.store_id
INNER JOIN shop.bill BL
    ON BP.bill_id = BL.bill_id
           AND BL.bill_date
               BETWEEN '1996-07-01' AND '2024-05-20'
WHERE PR.product = 'Milk'
GROUP BY PR.product, SR.store_name