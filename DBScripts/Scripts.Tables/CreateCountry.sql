-- Table: public.Country

-- DROP TABLE IF EXISTS public."Country";

CREATE TABLE IF NOT EXISTS public."Country"
(
    "Code" character varying(5) COLLATE pg_catalog."default" NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Language" character varying(25) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Country_pkey" PRIMARY KEY ("Code")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Country"
    OWNER to postgres;