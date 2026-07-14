-- FUNCTION: public.Get_Magazines()

-- DROP FUNCTION IF EXISTS public."Get_Magazines"();

CREATE OR REPLACE FUNCTION public."Get_Magazines"(
	)
    RETURNS TABLE("Issn" character varying, "Title" character varying, "PublishDate" date, "EndOfPublish" date, "PublisherName" character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
SELECT 
    "Issn",
    "Title",
    "PublishDate",
    "EndOfPublish",
    "PublisherName"
FROM public."MagazineView";
$BODY$;

ALTER FUNCTION public."Get_Magazines"()
    OWNER TO postgres;

