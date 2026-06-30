-- Table: public.Patent

-- DROP TABLE IF EXISTS public."Patent";

CREATE TABLE IF NOT EXISTS public."Patent"
(
    "Id" uuid NOT NULL,
    "Code" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    "Title" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Content" text COLLATE pg_catalog."default" NOT NULL,
    "PatentDate" date NOT NULL,
    CONSTRAINT "Patent_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Patent"
    OWNER to postgres;