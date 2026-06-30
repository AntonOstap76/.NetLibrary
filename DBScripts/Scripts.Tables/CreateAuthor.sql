-- Table: public.Author

-- DROP TABLE IF EXISTS public."Author";

CREATE TABLE IF NOT EXISTS public."Author"
(
    "Id" uuid NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "LastName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "BirthYear" smallint NOT NULL,
    "CountryId" character varying(5) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Author_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "AuthorCountryId" FOREIGN KEY ("CountryId")
        REFERENCES public."Country" ("Code") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Author"
    OWNER to postgres;