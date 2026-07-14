-- FUNCTION: public.Get_Authors_By_Patent_Id(uuid)

-- DROP FUNCTION IF EXISTS public."Get_Authors_By_Patent_Id"(uuid);

CREATE OR REPLACE FUNCTION public."Get_Authors_By_Patent_Id"(
	"Patent_Id" uuid)
    RETURNS TABLE("AuthorId" uuid, "AuthorName" character varying, "LastName" character varying, "BirthYear" smallint, "CountryId" character varying, "AuthorCountry" character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
SELECT
    av."AuthorId",
    av."AuthorName",
    av."LastName",
    av."BirthYear",
    av."CountryId",
    av."AuthorCountry"
FROM public."AuthorView" av
  JOIN "PatentAuthors" pa ON pa."AuthorId" = av."AuthorId"
WHERE pa."PatentId" = "Patent_Id";
$BODY$;

ALTER FUNCTION public."Get_Authors_By_Patent_Id"(uuid)
    OWNER TO postgres;

