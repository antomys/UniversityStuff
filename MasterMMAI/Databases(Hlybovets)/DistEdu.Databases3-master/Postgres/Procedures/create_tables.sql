CREATE TABLE IF NOT EXISTS Product (
  product_id    serial PRIMARY KEY,  -- implicit primary key constraint
  product       text NOT NULL,
  price         numeric NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS Bill (
    bill_id     serial PRIMARY KEY,
    bill        text NOT NULL,
    bill_date   date NOT NULL DEFAULT CURRENT_DATE
);

CREATE TABLE IF NOT EXISTS Store (
    store_id serial PRIMARY KEY,
    store_name text NOT NULL
);

CREATE TABLE IF NOT EXISTS Bill_Product (
    bill_id     int REFERENCES Bill (bill_id) ON UPDATE CASCADE ON DELETE CASCADE,
    product_id  int REFERENCES Product (product_id) ON UPDATE CASCADE,
    store_id    int REFERENCES Store (store_id) ON UPDATE CASCADE ON DELETE CASCADE,
    amount      numeric NOT NULL DEFAULT 1,
    CONSTRAINT bill_product_pKey PRIMARY KEY (bill_id, product_id, store_id)  -- explicit pk
);

CREATE TABLE IF NOT EXISTS Store_Product (
    sp_id       serial,
    store_id    int REFERENCES Store (store_id) ON UPDATE CASCADE ON DELETE CASCADE,
    product_id  int REFERENCES Product (product_id) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT store_product_pKey PRIMARY KEY (sp_id, store_id, product_id) -- explicit pk
);