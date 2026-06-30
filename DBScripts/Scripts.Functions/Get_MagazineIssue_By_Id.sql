-- FUNCTION: public.Get_MagazineIssue_By_Id(uuid)

-- DROP FUNCTION IF EXISTS public."Get_MagazineIssue_By_Id"(uuid);

CREATE OR REPLACE FUNCTION public."Get_MagazineIssue_By_Id"(
	"IssueId" uuid)
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
WHERE "Id" = "IssueId";
$BODY$;

ALTER FUNCTION public."Get_MagazineIssue_By_Id"(uuid)
    OWNER TO postgres;

