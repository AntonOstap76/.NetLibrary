-- Table: public.Book

-- DROP TABLE IF EXISTS public."Book";

CREATE TABLE IF NOT EXISTS public."Book"
(
    "Id" uuid NOT NULL,
    "Isbn" character varying(13) COLLATE pg_catalog."default" NOT NULL,
    "Title" character varying(255) COLLATE pg_catalog."default" NOT NULL,
    "Content" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Book_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Book"
    OWNER to postgres;