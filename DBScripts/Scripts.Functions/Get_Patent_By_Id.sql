-- FUNCTION: public.Get_Patent_By_Id(uuid)

-- DROP FUNCTION IF EXISTS public."Get_Patent_By_Id"(uuid);

CREATE OR REPLACE FUNCTION public."Get_Patent_By_Id"(
	"PatentId" uuid)
    RETURNS TABLE("Id" uuid, "Code" character varying, "Title" character varying, "PatentDate" date, "AuthorName" character varying, "LastName" character varying, "AuthorCountry" character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
SELECT 
    "Id",
    "Code",
    "Title",
    "PatentDate",
    "AuthorName",
    "LastName",
    "AuthorCountry"
FROM public."PatentView"
WHERE "Id" = "PatentId";
$BODY$;

ALTER FUNCTION public."Get_Patent_By_Id"(uuid)
    OWNER TO postgres;

