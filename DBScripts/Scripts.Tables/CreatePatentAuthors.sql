-- Table: public.PatentAuthors

-- DROP TABLE IF EXISTS public."PatentAuthors";

CREATE TABLE IF NOT EXISTS public."PatentAuthors"
(
    "PatentId" uuid NOT NULL,
    "AuthorId" uuid NOT NULL,
    CONSTRAINT "AuthorId_key" FOREIGN KEY ("AuthorId")
        REFERENCES public."Author" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "PatentId_key" FOREIGN KEY ("PatentId")
        REFERENCES public."Patent" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."PatentAuthors"
    OWNER to postgres;