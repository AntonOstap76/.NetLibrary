-- FUNCTION: public.Get_Book_By_Id(uuid)

-- DROP FUNCTION IF EXISTS public."Get_Book_By_Id"(uuid);

CREATE OR REPLACE FUNCTION public."Get_Book_By_Id"(
	"Book_Id" uuid)
    RETURNS TABLE("BookId" uuid, "Title" character varying, "Isbn" character varying, "AuthorId" uuid, "AuthorName" character varying, "LastName" character varying, "PublisherName" character varying, "CountryId" character varying, "AuthorCountry" character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
SELECT 
    "BookId",
    "Title",
    "Isbn",
    "AuthorId",
    "AuthorName",
    "LastName",
    "PublisherName",
    "CountryId",
    "AuthorCountry"
FROM public."BookView"
WHERE "BookId" = "Book_Id";
$BODY$;

ALTER FUNCTION public."Get_Book_By_Id"(uuid)
    OWNER TO postgres;

