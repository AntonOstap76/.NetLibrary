-- View: public.BookView

-- DROP VIEW public."BookView";

CREATE OR REPLACE VIEW public."BookView"
 AS
 SELECT b."Id" AS "BookId",
    b."Title",
    b."Isbn",
    a."Id" AS "AuthorId",
    a."Name" AS "AuthorName",
    a."LastName",
    p."Name" AS "PublisherName",
    p."CountryId",
    c."Name" AS "AuthorCountry"
   FROM "Book" b
     JOIN "BookAuthors" ba ON b."Id" = ba."BookId"
     JOIN "Author" a ON ba."AuthorId" = a."Id"
     JOIN "PublisherBook" pb ON b."Id" = pb."BookId"
     JOIN "Publisher" p ON pb."PublisherId" = p."Id"
     JOIN "Country" c ON a."CountryId"::text = c."Code"::text;

ALTER TABLE public."BookView"
    OWNER TO postgres;

