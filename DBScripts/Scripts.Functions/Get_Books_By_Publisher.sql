-- FUNCTION: public.Get_Books_By_Publisher(uuid)

-- DROP FUNCTION IF EXISTS public."Get_Books_By_Publisher"(uuid);

CREATE OR REPLACE FUNCTION public."Get_Books_By_Publisher"(
	"PublisherId" uuid)
    RETURNS TABLE("Title" character varying, "Isbn" character varying, "AuthorName" character varying, "LastName" character varying, "PublisherName" character varying, "CountryId" character varying, "AuthorCountry" character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
SELECT 
    "Title",
    "Isbn",
    "AuthorName",
    "LastName",
    "PublisherName",
    "CountryId",
    "AuthorCountry"
FROM public."BookView"
WHERE "PublisherId" = "Get_Books_By_Publisher"."PublisherId";
$BODY$;

ALTER FUNCTION public."Get_Books_By_Publisher"(uuid)
    OWNER TO postgres;

