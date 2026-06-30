-- View: public.MagazineIssueView

-- DROP VIEW public."MagazineIssueView";

CREATE OR REPLACE VIEW public."MagazineIssueView"
 AS
 SELECT mi."Id",
    mi."Number",
    mi."PublishDate",
    mi."Content",
    mi."MagazineId",
    m."Title" AS "MagazineTitle",
    m."Issn",
    p."Name" AS "PublisherName"
   FROM "MagazineIssue" mi
     JOIN "Magazine" m ON mi."MagazineId" = m."Id"
     JOIN "Publisher" p ON m."PublisherId" = p."Id";

ALTER TABLE public."MagazineIssueView"
    OWNER TO postgres;

