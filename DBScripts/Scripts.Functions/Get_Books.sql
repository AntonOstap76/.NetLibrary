-- FUNCTION: public.Get_Books()

-- DROP FUNCTION IF EXISTS public."Get_Books"();

CREATE OR REPLACE FUNCTION public."Get_Books"(
	)
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
FROM public."BookView";
$BODY$;

ALTER FUNCTION public."Get_Books"()
    OWNER TO postgres;

