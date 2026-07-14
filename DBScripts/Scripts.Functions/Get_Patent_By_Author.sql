-- FUNCTION: public.Get_Patent_By_Author(uuid)

-- DROP FUNCTION IF EXISTS public."Get_Patent_By_Author"(uuid);

CREATE OR REPLACE FUNCTION public."Get_Patent_By_Author"(
	"p_AuthorId" uuid)
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
FROM public."PatentView"
WHERE "AuthorId" = "p_AuthorId";
$BODY$;

ALTER FUNCTION public."Get_Patent_By_Author"(uuid)
    OWNER TO postgres;

