-- Table: public.BookAuthors

-- DROP TABLE IF EXISTS public."BookAuthors";

CREATE TABLE IF NOT EXISTS public."BookAuthors"
(
    "BookId" uuid NOT NULL,
    "AuthorId" uuid NOT NULL,
    CONSTRAINT "AuthorId_key" FOREIGN KEY ("AuthorId")
        REFERENCES public."Author" ("Id") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID,
    CONSTRAINT "BookId_key" FOREIGN KEY ("BookId")
        REFERENCES public."Book" ("Id") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."BookAuthors"
    OWNER to postgres;