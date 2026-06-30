-- FUNCTION: public.Get_MagazineIssues()

-- DROP FUNCTION IF EXISTS public."Get_MagazineIssues"();

CREATE OR REPLACE FUNCTION public."Get_MagazineIssues"(
	)
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
FROM public."MagazineIssueView";
$BODY$;

ALTER FUNCTION public."Get_MagazineIssues"()
    OWNER TO postgres;

