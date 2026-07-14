-- Table: public.Publisher

-- DROP TABLE IF EXISTS public."Publisher";

CREATE TABLE IF NOT EXISTS public."Publisher"
(
    "Id" uuid NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "CountryId" character varying(5) COLLATE pg_catalog."default" NOT NULL,
    "FoundedYear" smallint NOT NULL,
    CONSTRAINT "Publisher_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "CountryId_key" FOREIGN KEY ("CountryId")
        REFERENCES public."Country" ("Code") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Publisher"
    OWNER to postgres;