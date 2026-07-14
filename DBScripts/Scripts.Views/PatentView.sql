-- View: public.PatentView

-- DROP VIEW public."PatentView";

CREATE OR REPLACE VIEW public."PatentView"
 AS
 SELECT p."Id",
    p."Code",
    p."Title",
    p."PatentDate",
    a."Id" AS "AuthorId",
    a."Name" AS "AuthorName",
    a."LastName",
    c."Name" AS "AuthorCountry"
   FROM "Patent" p
     JOIN "PatentAuthors" pa ON p."Id" = pa."PatentId"
     JOIN "Author" a ON pa."AuthorId" = a."Id"
     JOIN "Country" c ON a."CountryId"::text = c."Code"::text;

ALTER TABLE public."PatentView"
    OWNER TO postgres;

