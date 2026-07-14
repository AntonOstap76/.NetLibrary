-- FUNCTION: public.Get_Patent()

-- DROP FUNCTION IF EXISTS public."Get_Patent"();

CREATE OR REPLACE FUNCTION public."Get_Patent"(
	)
    RETURNS TABLE("Title" character varying, "Code" character varying, "PatentDate" date, "AuthorName" character varying, "LastName" character varying, "AuthorCountry" character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
SELECT 
    "Title",
    "Code",
    "PatentDate",
    "AuthorName",
    "LastName",
    "AuthorCountry"
FROM public."PatentView";
$BODY$;

ALTER FUNCTION public."Get_Patent"()
    OWNER TO postgres;

