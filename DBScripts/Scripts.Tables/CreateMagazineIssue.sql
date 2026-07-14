-- Table: public.MagazineIssue

-- DROP TABLE IF EXISTS public."MagazineIssue";

CREATE TABLE IF NOT EXISTS public."MagazineIssue"
(
    "Id" uuid NOT NULL,
    "Number" smallint NOT NULL,
    "MagazineId" uuid NOT NULL,
    "PublishDate" date NOT NULL,
    "Content" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "MagazineIssue_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "MagazineId_key" FOREIGN KEY ("MagazineId")
        REFERENCES public."Magazine" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."MagazineIssue"
    OWNER to postgres;