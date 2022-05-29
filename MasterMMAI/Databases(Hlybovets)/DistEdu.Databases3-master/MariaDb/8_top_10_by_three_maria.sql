# Вивести топ 10 купівель товарів по три за період С (наприклад масло, хліб - 1000 разів)

select p.products, COUNT(p.products) as total_bought, SUM(p.total_amount) as total_revenue
from (
    select
        GROUP_CONCAT(pr.product separator ',') as products,
        SUM(rep.amount) as total_amount
    from (SELECT p.*,
                       count(*) OVER (PARTITION BY bill_id) as count
                FROM (select bp.*
                      from bill_product bp
                               inner join bill b on b.bill_id = bp.bill_id
                          AND b.bill_date
                                                        BETWEEN '2020-04-01' AND '2021-05-20') p
                )rep
    inner join product pr on pr.product_id = rep.product_id
    where rep.count = 3
    group by rep.bill_id
     )p
group by p.products
order by total_bought desc
limit 10