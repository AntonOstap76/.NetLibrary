-- Table: public.Magazine

-- DROP TABLE IF EXISTS public."Magazine";

CREATE TABLE IF NOT EXISTS public."Magazine"
(
    "Id" uuid NOT NULL,
    "Issn" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    "Title" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "PublisherId" uuid NOT NULL,
    "PublishDate" date NOT NULL,
    "EndOfPublish" date NOT NULL,
    CONSTRAINT "Magazine_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "PublisherId_key" FOREIGN KEY ("PublisherId")
        REFERENCES public."Publisher" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Magazine"
    OWNER to postgres;