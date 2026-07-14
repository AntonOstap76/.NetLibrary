-- View: public.AuthorView

-- DROP VIEW public."AuthorView";

CREATE OR REPLACE VIEW public."AuthorView"
 AS
 SELECT a."Id" AS "AuthorId",
    a."Name" AS "AuthorName",
    a."LastName",
    a."BirthYear",
    a."CountryId",
    c."Name" AS "AuthorCountry"
   FROM "Author" a
     JOIN "Country" c ON a."CountryId"::text = c."Code"::text;

ALTER TABLE public."AuthorView"
    OWNER TO postgres;

