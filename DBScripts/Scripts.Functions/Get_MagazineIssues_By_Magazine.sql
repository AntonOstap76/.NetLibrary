-- FUNCTION: public.Get_MagazineIssues_By_Magazine(uuid)

-- DROP FUNCTION IF EXISTS public."Get_MagazineIssues_By_Magazine"(uuid);

CREATE OR REPLACE FUNCTION public."Get_MagazineIssues_By_Magazine"(
	"MagazineId" uuid)
    RETURNS TABLE("Number" smallint, "PublishDate" date, "Content" text, "MagazineTitle" character varying, "Issn" character varying, "PublisherName" character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
SELECT 
    "Number",
    "PublishDate",
    "Content",
    "MagazineTitle",
    "Issn",
    "PublisherName"
FROM public."MagazineIssueView"
WHERE "MagazineId" = "MagazineId";
$BODY$;

ALTER FUNCTION public."Get_MagazineIssues_By_Magazine"(uuid)
    OWNER TO postgres;

