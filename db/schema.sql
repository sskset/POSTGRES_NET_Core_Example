CREATE SCHEMA customer AUTHORIZATION test_user;

-- Table: public."Customers"

-- DROP TABLE public."Customers";

CREATE TABLE customer.Customers
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    firstname character varying(50) COLLATE pg_catalog."default",
    lastname character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT "Customers_pkey" PRIMARY KEY (id)
);

ALTER TABLE customer.Customers OWNER to test_user;