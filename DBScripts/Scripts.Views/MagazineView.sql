-- View: public.MagazineView

-- DROP VIEW public."MagazineView";

CREATE OR REPLACE VIEW public."MagazineView"
 AS
 SELECT m."Id",
    m."Issn",
    m."Title",
    m."PublishDate",
    m."EndOfPublish",
    p."Name" AS "PublisherName"
   FROM "Magazine" m
     JOIN "Publisher" p ON m."PublisherId" = p."Id";

ALTER TABLE public."MagazineView"
    OWNER TO postgres;

