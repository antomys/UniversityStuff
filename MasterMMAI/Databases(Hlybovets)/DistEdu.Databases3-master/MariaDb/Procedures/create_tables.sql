CREATE TABLE IF NOT EXISTS product (
    product_id    INTEGER COMMENT 'autoincrement=1',
    product       VARCHAR(45) NOT NULL,
    price         FLOAT NOT NULL DEFAULT 0
) engine=columnstore default character set=utf8;

CREATE TABLE IF NOT EXISTS bill (
    bill_id     INTEGER COMMENT 'autoincrement=1',
    bill        VARCHAR(45) NOT NULL,
    bill_date   DATE NOT NULL
) engine=columnstore default character set=utf8;

CREATE TABLE IF NOT EXISTS store (
    store_id    INTEGER COMMENT 'autoincrement=1',
    store_name  VARCHAR(45) NOT NULL
) engine=columnstore default character set=utf8;

CREATE TABLE IF NOT EXISTS bill_product (
    bp_id       INTEGER COMMENT 'autoincrement=1',
    bill_id     INTEGER,
    product_id  INTEGER,
    store_id    INTEGER,
    amount      FLOAT NOT NULL DEFAULT 1
) engine=columnstore default character set=utf8;

CREATE TABLE IF NOT EXISTS store_product (
    sp_id       INTEGER COMMENT 'autoincrement=1',
    store_id    INTEGER,
    product_id  INTEGER
) engine=columnstore default character set=utf8;