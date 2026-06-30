-- Table: public.PublisherBook

-- DROP TABLE IF EXISTS public."PublisherBook";

CREATE TABLE IF NOT EXISTS public."PublisherBook"
(
    "BookId" uuid NOT NULL,
    "PublisherId" uuid NOT NULL,
    CONSTRAINT "BookId_key" FOREIGN KEY ("BookId")
        REFERENCES public."Book" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "PublisherId_key" FOREIGN KEY ("PublisherId")
        REFERENCES public."Publisher" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."PublisherBook"
    OWNER to postgres;